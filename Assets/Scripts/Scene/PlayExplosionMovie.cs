using Cysharp.Threading.Tasks;
using General;
using SO;
using System.Threading;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using IA;
using UnityEngine.UI;
using Ex;

namespace Scene
{
    public sealed class PlayExplosionMovie : MonoBehaviour
    {
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private Image blackImage;

        private void Start()
        {
            if (videoPlayer != null) videoPlayer.targetTexture.Release();

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
            FadeOut(1, ct).Forget();
            await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: ct);

            async UniTaskVoid FadeOut(float duration, CancellationToken ct)
            {
                SetA(blackImage, 0);
                float t = 0;

                while (true)
                {
                    await UniTask.NextFrame(ct);
                    t += Time.deltaTime;

                    float a = t.Remap(0, duration, 0, 1);
                    SetA(blackImage, a);

                    if (t >= duration)
                    {
                        SetA(blackImage, 1);
                        SceneManager.LoadScene(SO_SceneName.Entity.GameOver);
                    }
                }
            }

            static void SetA(Image image, float a)
            {
                if (image == null) return;
                Color color = image.color;
                color.a = a;
                image.color = color;
            }
        }

        private void Save() => SaveDataHolder.Instance.SaveData.HasToiletExploded = true;
    }
}
