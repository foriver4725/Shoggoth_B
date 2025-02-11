﻿using Cysharp.Threading.Tasks;
using Ex;
using General;
using SO;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class GameOvertoOP : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _backText;
        [SerializeField] private AudioSource _click1st;

        private CancellationToken _ct;

        void Start()
        {
            SaveDataHolder.Instance.SaveData.OverNum++;

            _ct = this.GetCancellationTokenOnDestroy();
            SceneChange(_ct).Forget();
        }

        private async UniTask SceneChange(CancellationToken ct)
        {
            _backText.color = Color.black;

            await UniTask.Delay(TimeSpan.FromSeconds(SO_General.Entity.ClickDur), cancellationToken: ct);
            await UniTask.WaitUntil(() => IA.InputGetter.Instance.SystenmSubmit.Bool, cancellationToken: ct);
            _backText.color = Color.yellow;
            _click1st.Raise(SO_Sound.Entity.ClickSE, SType.SE);
            await UniTask.Delay(TimeSpan.FromSeconds(SO_General.Entity.AfterClickDur), cancellationToken: ct);
            await SceneManager.LoadSceneAsync(SO_SceneName.Entity.Title);
        }
    }
}