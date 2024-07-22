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

        void Update()
        {
            if (GameManager.Instance.IsClear)
            {
                if (whiteOutCor == null) whiteOutCor = StartCoroutine(FadeOut());
            }
        }

        IEnumerator FadeOut()
        {
            yield return new WaitForSeconds(SO_General.Entity.FadeWhiteStartDur);

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
