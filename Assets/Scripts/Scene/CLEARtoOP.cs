using Cysharp.Threading.Tasks;
using General;
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
            GetIncrementClearNumAction(Difficulty.Type)?.Invoke();

            videoPlayer.loopPointReached += OnPlayEnded;
            videoPlayer.Play();
        }

        private void OnPlayEnded(VideoPlayer _) => SceneChange(destroyCancellationToken).Forget();

        private async UniTaskVoid SceneChange(CancellationToken ct)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: ct);
            SceneManager.LoadScene(SO_SceneName.Entity.Title);
        }

        private Action GetIncrementClearNumAction(DifficultyType type) => type switch
        {
            DifficultyType.Easy => () => SaveDataHolder.Instance.SaveData.ClearNumEasy++,
            DifficultyType.Normal => () => SaveDataHolder.Instance.SaveData.ClearNumNormal++,
            DifficultyType.Hard => () => SaveDataHolder.Instance.SaveData.ClearNumHard++,
            DifficultyType.Nightmare => () => SaveDataHolder.Instance.SaveData.ClearNumNightmare++,
            _ => null
        };
    }
}