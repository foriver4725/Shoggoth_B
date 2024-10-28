using Cysharp.Threading.Tasks;
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

        void Start()
        {
            videoPlayer.loopPointReached += OnPlayEnded;
            videoPlayer.Play();
        }

        private void OnPlayEnded(VideoPlayer _) => SceneChange(destroyCancellationToken).Forget();

        private async UniTaskVoid SceneChange(CancellationToken ct)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: ct);
            SceneManager.LoadScene(SO_SceneName.Entity.Title);
        }
    }
}