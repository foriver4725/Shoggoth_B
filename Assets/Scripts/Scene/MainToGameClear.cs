using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainGame
{
    public class MainToGameClear : MonoBehaviour
    {
        [SerializeField] Image whiteOutImage;

        Coroutine whiteOutCor;

        public void Clear()
        {
            if (whiteOutCor != null) return;
            whiteOutCor = StartCoroutine(FadeOut());
        }

        IEnumerator FadeOut()
        {
            float DUR = SO_General.Entity.FadeWhiteDur;
            float t = 0;

            while (t < DUR)
            {
                t += Time.deltaTime;

                float a = t / DUR;
                Color color = whiteOutImage.color;
                color.a = a;
                whiteOutImage.color = color;

                yield return null;
            }

            Color _color = whiteOutImage.color;
            _color.a = 1;
            whiteOutImage.color = _color;

            SceneManager.LoadSceneAsync(SO_SceneName.Entity.GameClear);
        }
    }
}
