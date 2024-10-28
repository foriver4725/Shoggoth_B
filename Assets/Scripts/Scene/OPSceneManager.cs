﻿using SO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using IA;
using UnityEngine.UI;
using MainGame;
using Ex;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Video;

namespace Scene
{
    public class OPSceneManager : MonoBehaviour
    {
        [SerializeField] private GameObject difficultySelectUI;
        [SerializeField] private GameObject openingVideo;

        [Serializable]
        private sealed class DifficultyPanel
        {
            [SerializeField] private Image easy;
            public Color EasyColor { set { easy.color = value; } }

            [SerializeField] private Image normal;
            public Color NormalColor { set { normal.color = value; } }

            [SerializeField] private Image hard;
            public Color HardColor { set { hard.color = value; } }

            [SerializeField] private Image nightmare;
            public Color NightmareColor { set { nightmare.color = value; } }
        }
        [SerializeField] private DifficultyPanel difficultyPanel;

        [Serializable]
        private sealed class AudioSources
        {
            [SerializeField] private AudioSource bgmAS;
            public void PlayBGM() => bgmAS.Raise(SO_Sound.Entity.TitleBGM, SType.BGM);
            public void StopBGM() => bgmAS.Stop();

            [SerializeField] private AudioSource clickAS;
            public void PlayClickSE() => clickAS.Raise(SO_Sound.Entity.ClickSE, SType.SE);
        }
        [SerializeField] private AudioSources audioSources;

        private enum State
        {
            TitleImage,
            DifficultySelect,
            SceneChanging
        }
        private State state = State.TitleImage;

        private LoopedInt difficultyIndex = new(4);

        private bool isClickEnabled = true;
        // これを通じてフラグを書き換える
        private void OnClick() => OnClickImpl(destroyCancellationToken).Forget();
        private async UniTaskVoid OnClickImpl(CancellationToken ct)
        {
            isClickEnabled = false;
            audioSources.PlayClickSE();
            await UniTask.Delay(TimeSpan.FromSeconds(SO_General.Entity.ClickDur), cancellationToken: ct);
            isClickEnabled = true;
        }

        private async UniTaskVoid AfterClick(Action action, CancellationToken ct)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(SO_General.Entity.AfterClickDur), cancellationToken: ct);
            action();
        }

        private void OnEnable()
        {
            audioSources.PlayBGM();
        }

        private void Update()
        {
            switch (state)
            {
                case State.TitleImage:
                    UpdateOnTitleImage();
                    break;
                case State.DifficultySelect:
                    UpdateOnDifficultySelect();
                    break;
                default:
                    break;
            }
        }

        private void UpdateOnTitleImage()
        {
            if (InputGetter.Instance.System_IsCredit)
            {
                OnClick();
                state = State.SceneChanging;
                AfterClick(() => SceneManager.LoadScene(SO_SceneName.Entity.Credit), destroyCancellationToken).Forget();
            }
            else if (InputGetter.Instance.System_IsCancel)
            {
                OnClick();
                state = State.SceneChanging;
                AfterClick(() =>
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                }, destroyCancellationToken).Forget();
            }
            else if (InputGetter.Instance.System_IsSubmit)
            {
                OnClick();
                state = State.SceneChanging;
                AfterClick(() =>
                {
                    state = State.DifficultySelect;
                    difficultySelectUI.SetActive(true);
                }, destroyCancellationToken).Forget();
            }
        }

        private void UpdateOnDifficultySelect()
        {
            if (InputGetter.Instance.MainGame_IsUp) difficultyIndex.Value--;
            else if (InputGetter.Instance.MainGame_IsDown) difficultyIndex.Value++;

            if (InputGetter.Instance.System_IsSubmit)
            {
                OnClick();
                state = State.SceneChanging;
                AfterClick(() =>
                {
                    Difficulty.Type = difficultyIndex.Value switch
                    {
                        0 => DifficultyType.Easy,
                        1 => DifficultyType.Normal,
                        2 => DifficultyType.Hard,
                        3 => DifficultyType.Nightmare,
                        _ => DifficultyType.Easy
                    };
                    ShowOpeningVideo();
                }, destroyCancellationToken).Forget();
            }

            difficultyPanel.EasyColor = difficultyIndex.Value == 0 ? Color.yellow : Color.white;
            difficultyPanel.NormalColor = difficultyIndex.Value == 1 ? Color.yellow : Color.white;
            difficultyPanel.HardColor = difficultyIndex.Value == 2 ? Color.yellow : Color.white;
            difficultyPanel.NightmareColor = difficultyIndex.Value == 3 ? Color.yellow : Color.white;
        }

        private void ShowOpeningVideo()
        {
            VideoPlayer vp = openingVideo.GetComponent<VideoPlayer>();
            vp.loopPointReached += _ => LoadMainScene(destroyCancellationToken).Forget();
            audioSources.StopBGM();
            openingVideo.SetActive(true);
        }

        private async UniTask LoadMainScene(CancellationToken ct)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: ct);
            SceneManager.LoadScene(SO_SceneName.Entity.MainGame);
        }
    }
}
