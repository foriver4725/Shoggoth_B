using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TextFadeIn : MonoBehaviour
{
    public TextMeshProUGUI gameOverText; // フェードインさせるTextコンポーネント
    public float fadeDuration = 2.0f; // フェードインにかける時間

    private void Start()
    {
        // 初期状態でテキストの透明度を0に設定
        Color textColor = gameOverText.color;
        textColor.a = 0;
        gameOverText.color = textColor;

        // フェードインを開始
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);

            // テキストの透明度を変更
            Color textColor = gameOverText.color;
            textColor.a = alpha;
            gameOverText.color = textColor;

            yield return null;
        }
    }
}
