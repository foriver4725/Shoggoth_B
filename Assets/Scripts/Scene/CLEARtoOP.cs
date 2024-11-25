using Cysharp.Threading.Tasks;
using General;
using IA;
using SO;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace Scene
{
    public sealed class CLEARtoOP : MonoBehaviour
    {
        [SerializeField] private VideoPlayer videoPlayer;

        private void Start()
        {
            if (videoPlayer != null) videoPlayer.targetTexture.Release();

            bool isFirstClear = SaveDataHolder.Instance.SaveData.ClearNum <= 0;
            Save(Difficulty.Type);

            videoPlayer.loopPointReached += OnPlayEnded;
            SkipMovie(isFirstClear, destroyCancellationToken).Forget();
            videoPlayer.Play();
        }

        private async UniTaskVoid SkipMovie(bool isFirstClear, CancellationToken ct)
        {
            if (isFirstClear) return;
            await UniTask.WaitUntil(() => InputGetter.Instance.SystemCancel.Bool, cancellationToken: ct);
            SceneManager.LoadScene(SO_SceneName.Entity.Title);
        }

        private void OnPlayEnded(VideoPlayer _) => SceneChange(destroyCancellationToken).Forget();

        private async UniTaskVoid SceneChange(CancellationToken ct)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: ct);
            SceneManager.LoadScene(SO_SceneName.Entity.Title);
        }

        private void Save(DifficultyType type)
        {
            if (type == DifficultyType.Easy && SaveDataHolder.Instance.SaveData.HasEasyCleared is false)
                SaveDataHolder.Instance.SaveData.HasEasyCleared = true;
            else if (type == DifficultyType.Normal && SaveDataHolder.Instance.SaveData.HasNormalCleared is false)
                SaveDataHolder.Instance.SaveData.HasNormalCleared = true;
            else if (type == DifficultyType.Hard && SaveDataHolder.Instance.SaveData.HasHardCleared is false)
                SaveDataHolder.Instance.SaveData.HasHardCleared = true;
            else if (type == DifficultyType.Nightmare && SaveDataHolder.Instance.SaveData.HasNightmareCleared is false)
                SaveDataHolder.Instance.SaveData.HasNightmareCleared = true;

            SaveDataHolder.Instance.SaveData.ClearNum++;
        }
    }
}