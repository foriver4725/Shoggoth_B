using SO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using IA;
using UnityEngine.UI;
using MainGame;
using Ex;

namespace Scene
{
    public class OPSceneManager : MonoBehaviour
    {
        [SerializeField] private GameObject difficultySelectUI;

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
                state = State.SceneChanging;
                SceneManager.LoadScene(SO_SceneName.Entity.Credit);
            }
            else if (InputGetter.Instance.System_IsCancel)
            {
                state = State.SceneChanging;
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
            else if (InputGetter.Instance.System_IsSubmit)
            {
                state = State.DifficultySelect;
                difficultySelectUI.SetActive(true);
            }
        }

        private void UpdateOnDifficultySelect()
        {
            if (InputGetter.Instance.MainGame_IsUp) difficultyIndex.Value--;
            else if (InputGetter.Instance.MainGame_IsDown) difficultyIndex.Value++;

            if (InputGetter.Instance.System_IsSubmit)
            {
                state = State.SceneChanging;
                Difficulty.Type = difficultyIndex.Value switch
                {
                    0 => DifficultyType.Easy,
                    1 => DifficultyType.Normal,
                    2 => DifficultyType.Hard,
                    3 => DifficultyType.Nightmare,
                    _ => DifficultyType.Easy
                };
                SceneManager.LoadScene(SO_SceneName.Entity.MainGame);
            }

            difficultyPanel.EasyColor = difficultyIndex.Value == 0 ? Color.yellow : Color.white;
            difficultyPanel.NormalColor = difficultyIndex.Value == 1 ? Color.yellow : Color.white;
            difficultyPanel.HardColor = difficultyIndex.Value == 2 ? Color.yellow : Color.white;
            difficultyPanel.NightmareColor = difficultyIndex.Value == 3 ? Color.yellow : Color.white;
        }
    }
}
