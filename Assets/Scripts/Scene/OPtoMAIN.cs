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
using UnityEngine.UI;

namespace Scene
{
    public class OPtoMAIN : MonoBehaviour
    {
        [SerializeField] private GameObject _startDescription;
        [SerializeField] private GameObject _startDifficulty;
        [SerializeField] private TextMeshProUGUI _startText;
        [SerializeField] private TextMeshProUGUI _nextText;

        //[SerializeField] private TextMeshProUGUI _easyText;
        //[SerializeField] private TextMeshProUGUI _normalText;
        //[SerializeField] private TextMeshProUGUI _hardText;
        //[SerializeField] private TextMeshProUGUI _nightmareText;

        public Button[] buttons;

        //[SerializeField] private Button _easyButton;
        //[SerializeField] private Button _normalButton;
        //[SerializeField] private Button _hardButton;
        //[SerializeField] private Button _nightmareButton;

        [SerializeField] private AudioSource _click1st;
        [SerializeField] private AudioSource _click2nd;
        [SerializeField] private AudioSource _titleBGM;

        private CancellationToken _ct;

        void Start()
        {
            _ct = this.GetCancellationTokenOnDestroy();
            _titleBGM.Raise(SO_Sound.Entity.TitleBGM, SType.BGM);
            SceneChange(_ct).Forget();

            // すべてのボタンにクリックイベントを登録
            foreach (Button button in buttons)
            {
                // ラムダ式を使ってクリックされたボタンを引数として渡す
                button.onClick.AddListener(() => OnButtonClick(button));
            }
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


        // ボタンがクリックされたときに呼ばれるメソッド
        void OnButtonClick(Button clickedButton)
        {
            // クリックされたボタンの名前を取得
            string buttonName = clickedButton.name;
            if (buttonName == "Easy")
            {
                DifficultyType Type = DifficultyType.Easy;
            }
            else if (buttonName == "Normal")
            {
                DifficultyType Type = DifficultyType.Normal;
            }
            else if (buttonName == "Hard")
            {
                DifficultyType Type = DifficultyType.Hard;
            }
            else if (buttonName == "Nightmare")
            {
                DifficultyType Type = DifficultyType.Nightmare;
            }
            else
            {
                Debug.Log("NormalNormalNormal");
            }

        }




        private async UniTask SceneChange(CancellationToken ct)
        {
            _startDescription.SetActive(false);
            _startText.color = Color.black;
            _nextText.color = Color.black;
            
            _startDifficulty.SetActive(false);
            //_easyText.color = Color.black;
            //_normalText.color = Color.black;
            //_hardText.color = Color.black;
            //_nightmareText.color = Color.black;

            await UniTask.Delay(TimeSpan.FromSeconds(SO_General.Entity.ClickDur), cancellationToken: ct);
            await UniTask.WaitUntil(() => IA.InputGetter.Instance.System_IsSubmit, cancellationToken: ct);
            _startText.color = Color.yellow;
            _click1st.Raise(SO_Sound.Entity.ClickSE, SType.SE);
            await UniTask.Delay(TimeSpan.FromSeconds(SO_General.Entity.AfterClickDur), cancellationToken: ct);

            _startDifficulty.SetActive(true);

            await UniTask.Delay(TimeSpan.FromSeconds(SO_General.Entity.ClickDur), cancellationToken: ct);
            await UniTask.WaitUntil(() => IA.InputGetter.Instance.System_IsSubmit, cancellationToken: ct);
            //_easyText.color = Color.green;
            //_normalText.color = Color.yellow;
            //_hardText.color = Color.red;
            //_nightmareText.color = Color.magenta;

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