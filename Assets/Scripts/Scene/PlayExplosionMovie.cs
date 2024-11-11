using Cysharp.Threading.Tasks;
using General;
using SO;
using System.Threading;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using IA;

namespace Scene
{
    public sealed class PlayExplosionMovie : MonoBehaviour
    {
        [SerializeField] private VideoPlayer videoPlayer;

        private void Start()
        {
            bool isFirstExplosion = SaveDataHolder.Instance.SaveData.HasToiletExploded is false;
            Save();

            videoPlayer.loopPointReached += OnPlayEnded;
            SkipMovie(isFirstExplosion, destroyCancellationToken).Forget();
            videoPlayer.Play();
        }

        private async UniTaskVoid SkipMovie(bool isFirstExplosion, CancellationToken ct)
        {
            if (isFirstExplosion) return;
            await UniTask.WaitUntil(() => InputGetter.Instance.SystemCancel.Bool, cancellationToken: ct);
            SceneManager.LoadScene(SO_SceneName.Entity.GameOver);
        }

        private void OnPlayEnded(VideoPlayer _) => SceneChange(destroyCancellationToken).Forget();

        private async UniTaskVoid SceneChange(CancellationToken ct)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: ct);
            SceneManager.LoadScene(SO_SceneName.Entity.GameOver);
        }

        private void Save() => SaveDataHolder.Instance.SaveData.HasToiletExploded = true;
    }
}
