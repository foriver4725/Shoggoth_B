using UnityEngine;
using TMPro;
using IA;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel;
    public TextMeshProUGUI[] menuOptions;
    private int currentIndex = 0;

    void Start()
    {
        menuPanel.SetActive(false);
        UpdateMenuOptions();
    }

    void Update()
    {
        if (IA.InputGetter.Instance.MainGame_IsPause)
        {
            menuPanel.SetActive(!menuPanel.activeSelf);
        }

        if (menuPanel.activeSelf)
        {
            HandleMenuNavigation();
        }
    }

    void HandleMenuNavigation()
    {
        if (IA.InputGetter.Instance.MainGame_IsUp)
        {
            currentIndex = (currentIndex > 0) ? currentIndex - 1 : menuOptions.Length - 1;
            UpdateMenuOptions();
        }
        else if (IA.InputGetter.Instance.MainGame_IsDown)
        {
            currentIndex = (currentIndex < menuOptions.Length - 1) ? currentIndex + 1 : 0;
            UpdateMenuOptions();
        }
        else if (IA.InputGetter.Instance.System_IsSubmit)
        {
            ExecuteMenuOption();
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

    void ExecuteMenuOption()
    {
        switch (currentIndex)
        {
            case 0:
                ResumeGame();
                break;
            case 1:
                OpenItems();
                break;
            case 2:
                OpenStatus();
                break;
            case 3:
                ReturnToTitle();
                break;
        }
    }

    void ResumeGame()
    {
        menuPanel.SetActive(false);
    }

    void OpenItems()
    {
        Debug.Log("アイテムを開く");
    }

    void OpenStatus()
    {
        Debug.Log("ステータスを開く");
    }

    void ReturnToTitle()
    {
        Debug.Log("タイトルに戻る");
        // ここで実際のシーン遷移処理を追加する
    }
}
