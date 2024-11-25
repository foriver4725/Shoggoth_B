using IA;
using MainGame;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu; // ポーズメニューのUIオブジェクト

    public bool isPaused = false;

    void Start()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false); // シーン開始時にポーズメニューを非表示にする
        }
    }

    void Update()
    {
        // クリアまたはゲームオーバーならポーズできない
        if (GameManager.Instance.EventState == EventState.End) return;

        if (InputGetter.Instance.MainGamePause.Bool)
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
            pauseMenu.SetActive(true); // ポーズメニューを表示
        }
        Time.timeScale = 0f; // ゲームを一時停止
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false); // ポーズメニューを非表示
        }
        Time.timeScale = 1f; // ゲームを再開
        isPaused = false;
    }
}
