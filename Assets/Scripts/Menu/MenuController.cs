using UnityEngine;
using TMPro;
using IA;
using SO;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel;
    public TextMeshProUGUI[] menuOptions;
    [SerializeField] private GameObject pauseAnnounceTextBack;
    private int currentIndex = 0;

    void Start()
    {
        menuPanel.SetActive(false);
        UpdateMenuOptions();
    }

    void Update()
    {
        if (!menuPanel.activeSelf)
        {
            if (InputGetter.Instance.MainGame_IsPause)
            {
                if (pauseAnnounceTextBack.activeSelf) pauseAnnounceTextBack.SetActive(false);
                Time.timeScale = 0f;
                menuPanel.SetActive(true);
            }
        }
        else
        {
            HandleMenuNavigation();
            if (InputGetter.Instance.System_IsSubmit)
            {
                if (currentIndex == 0)
                {
                    ResumeGame();
                }
                else if (currentIndex == menuOptions.Length - 1)
                {
                    ReturnToTitle();
                }
            }
        }
    }

    void HandleMenuNavigation()
    {
        if (InputGetter.Instance.MainGame_IsUp)
        {
            currentIndex = (currentIndex > 0) ? currentIndex - 1 : menuOptions.Length - 1;
            UpdateMenuOptions();
        }
        else if (InputGetter.Instance.MainGame_IsDown)
        {
            currentIndex = (currentIndex < menuOptions.Length - 1) ? currentIndex + 1 : 0;
            UpdateMenuOptions();
        }
    }

    void UpdateMenuOptions()
    {
        for (int i = 0; i < menuOptions.Length; i++)
        {
            if (i == currentIndex)
            {
                menuOptions[i].color = Color.white;
            }
            else
            {
                menuOptions[i].color = Color.gray;
            }
        }
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        menuPanel.SetActive(false);
    }

    void ReturnToTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(SO_SceneName.Entity.Title);
    }
}
