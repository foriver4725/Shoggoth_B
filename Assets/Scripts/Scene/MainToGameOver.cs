using SO;
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

        void Update()
        {
            if (GameManager.Instance.CurrentHP == 0)
            {
                if (GameManager.Instance.EventState != EventState.End)
                {
                    GameManager.Instance.EventState = EventState.End;
                    StartCoroutine(FadeOut());
                }
            }
        }

        IEnumerator FadeOut()
        {
            float DUR = SO_General.Entity.FadeWhiteDur;
            float t = 0;

            while (t < DUR)
            {
                t += Time.deltaTime;

                float a = t / DUR;
                Color color = fadeOutImage.color;
                color.a = a;
                fadeOutImage.color = color;

                yield return null;
            }

            Color _color = fadeOutImage.color;
            _color.a = 1;
            fadeOutImage.color = _color;

            SceneManager.LoadSceneAsync(SO_SceneName.Entity.GameOver);
        }
    }
}
