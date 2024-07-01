using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainGame
{
    public class MainToGameOver : MonoBehaviour
    {
        [SerializeField] PlayerMove playerMove;
        [SerializeField] Image fadeOutImage;

        Coroutine fadeOut = null;

        void Update()
        {
            if (HPManager.currentHP == 0)
            {
                playerMove.IsAlive = false;
                if (fadeOut == null) fadeOut = StartCoroutine(FadeOut(3));
            }
        }

        IEnumerator FadeOut(float dur)
        {
            yield return new WaitForSeconds(1);

            float t = 0;

            while (t < dur)
            {
                t += Time.deltaTime;

                float a = t / dur;
                Color color = fadeOutImage.color;
                color.a = a;
                fadeOutImage.color = color;

                yield return null;
            }

            Color _color = fadeOutImage.color;
            _color.a = 1;
            fadeOutImage.color = _color;

            SceneManager.LoadSceneAsync("GameOver");
        }
    }
}
