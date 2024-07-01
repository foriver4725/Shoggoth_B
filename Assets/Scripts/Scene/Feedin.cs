using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TextFadeIn : MonoBehaviour
{
    public TextMeshProUGUI gameOverText; // �t�F�[�h�C��������Text�R���|�[�l���g
    public float fadeDuration = 2.0f; // �t�F�[�h�C���ɂ����鎞��

    private void Start()
    {
        // ������ԂŃe�L�X�g�̓����x��0�ɐݒ�
        Color textColor = gameOverText.color;
        textColor.a = 0;
        gameOverText.color = textColor;

        // �t�F�[�h�C�����J�n
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);

            // �e�L�X�g�̓����x��ύX
            Color textColor = gameOverText.color;
            textColor.a = alpha;
            gameOverText.color = textColor;

            yield return null;
        }
    }
}
