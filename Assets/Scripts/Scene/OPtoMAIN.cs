using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System;
using Ex;
using TMPro;
using System.Threading;
using IA;

namespace Scene
{
    public class OPtoMAIN : MonoBehaviour
    {
        [SerializeField] private GameObject _startDescription;
        [SerializeField] private GameObject _startDifficulty;
        [SerializeField] private TextMeshProUGUI _startText;
        [SerializeField] private TextMeshProUGUI _nextText;

        [SerializeField] private TextMeshProUGUI _easyText;
        [SerializeField] private TextMeshProUGUI _normalText;
        [SerializeField] private TextMeshProUGUI _hardText;
        [SerializeField] private TextMeshProUGUI _nightmareText;
        
        [SerializeField] private AudioSource _click1st;
        [SerializeField] private AudioSource _click2nd;
        [SerializeField] private AudioSource _titleBGM;

        private CancellationToken _ct;

        void Start()
        {
            _ct = this.GetCancellationTokenOnDestroy();
            _titleBGM.Raise(SO_Sound.Entity.TitleBGM, SType.BGM);
            SceneChange(_ct).Forget();
        }

        private void Update()
        {
            if (InputGetter.Instance.System_IsCancel && !_startDescription.activeSelf)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }

        private async UniTask SceneChange(CancellationToken ct)
        {
            _startDescription.SetActive(false);
            _startText.color = Color.black;
            _nextText.color = Color.black;
            
            _startDifficulty.SetActive(false);
            _easyText.color = Color.black;
            _normalText.color = Color.black;
            _hardText.color = Color.black;
            _nightmareText.color = Color.black;

            await UniTask.Delay(TimeSpan.FromSeconds(SO_General.Entity.ClickDur), cancellationToken: ct);
            await UniTask.WaitUntil(() => IA.InputGetter.Instance.System_IsSubmit, cancellationToken: ct);
            _startText.color = Color.yellow;
            _click1st.Raise(SO_Sound.Entity.ClickSE, SType.SE);
            await UniTask.Delay(TimeSpan.FromSeconds(SO_General.Entity.AfterClickDur), cancellationToken: ct);
            
            _startDifficulty.SetActive(true);

            await UniTask.Delay(TimeSpan.FromSeconds(SO_General.Entity.ClickDur), cancellationToken: ct);
            await UniTask.WaitUntil(() => IA.InputGetter.Instance.System_IsSubmit, cancellationToken: ct);
            _easyText.color = Color.yellow;
            _normalText.color = Color.yellow;
            _hardText.color = Color.yellow;
            _nightmareText.color = Color.yellow;
            _click2nd.Raise(SO_Sound.Entity.ClickSE, SType.SE);
            await UniTask.Delay(TimeSpan.FromSeconds(SO_General.Entity.AfterClickDur), cancellationToken: ct);



            _startDescription.SetActive(true);

            await UniTask.Delay(TimeSpan.FromSeconds(SO_General.Entity.ClickDur), cancellationToken: ct);
            await UniTask.WaitUntil(() => IA.InputGetter.Instance.System_IsSubmit, cancellationToken: ct);
            _nextText.color = Color.yellow;
            _click2nd.Raise(SO_Sound.Entity.ClickSE, SType.SE);
            await UniTask.Delay(TimeSpan.FromSeconds(SO_General.Entity.AfterClickDur), cancellationToken: ct);
            await SceneManager.LoadSceneAsync(SO_SceneName.Entity.MainGame);
        }
    }
}