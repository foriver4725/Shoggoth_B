using Cysharp.Threading.Tasks;
using Ex;
using SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class CLEARtoOP : MonoBehaviour
    {
        [SerializeField] private GameObject _endDescription;
        [SerializeField] private TextMeshProUGUI _nextText;
        [SerializeField] private TextMeshProUGUI _backText;
        [SerializeField] private AudioSource _click1st;
        [SerializeField] private AudioSource _click2nd;

        private CancellationToken _ct;

        void Start()
        {
            _ct = this.GetCancellationTokenOnDestroy();
            SceneChange(_ct).Forget();
        }

        private async UniTask SceneChange(CancellationToken ct)
        {
            _endDescription.SetActive(true);
            _nextText.color = Color.black;
            _backText.color = Color.black;

            await UniTask.Delay(TimeSpan.FromSeconds(SO_General.Entity.ClickDur), cancellationToken: ct);
            await UniTask.WaitUntil(() => IA.InputGetter.Instance.System_IsSubmit, cancellationToken: ct);
            _nextText.color = Color.yellow;
            _click1st.Raise(SO_Sound.Entity.ClickSE, SType.SE);
            await UniTask.Delay(TimeSpan.FromSeconds(SO_General.Entity.AfterClickDur), cancellationToken: ct);
            _endDescription.SetActive(false);

            await UniTask.Delay(TimeSpan.FromSeconds(SO_General.Entity.ClickDur), cancellationToken: ct);
            await UniTask.WaitUntil(() => IA.InputGetter.Instance.System_IsSubmit, cancellationToken: ct);
            _backText.color = Color.yellow;
            _click2nd.Raise(SO_Sound.Entity.ClickSE, SType.SE);
            await UniTask.Delay(TimeSpan.FromSeconds(SO_General.Entity.AfterClickDur), cancellationToken: ct);
            await SceneManager.LoadSceneAsync(SO_SceneName.Entity.Title);
        }
    }
}