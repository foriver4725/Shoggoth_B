using IA;
using MainGame;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu; // �|�[�Y���j���[��UI�I�u�W�F�N�g

    public bool isPaused = false;

    void Start()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false); // �V�[���J�n���Ƀ|�[�Y���j���[���\���ɂ���
        }
    }

    void Update()
    {
        // �N���A�܂��̓Q�[���I�[�o�[�Ȃ�|�[�Y�ł��Ȃ�
        if (GameManager.Instance.IsClear || GameManager.Instance.IsOver) return;

        if (InputGetter.Instance.MainGame_IsPause)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true); // �|�[�Y���j���[��\��
        }
        Time.timeScale = 0f; // �Q�[�����ꎞ��~
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false); // �|�[�Y���j���[���\��
        }
        Time.timeScale = 1f; // �Q�[�����ĊJ
        isPaused = false;
    }
}
