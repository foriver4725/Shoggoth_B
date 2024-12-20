﻿using UnityEngine;
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
            if (InputGetter.Instance.MainGamePause.Bool)
            {
                if (pauseAnnounceTextBack.activeSelf) pauseAnnounceTextBack.SetActive(false);
                Time.timeScale = 0f;
                menuPanel.SetActive(true);
            }
        }
        else
        {
            HandleMenuNavigation();
            if (InputGetter.Instance.SystenmSubmit.Bool)
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
        if (InputGetter.Instance.MainGameUp.Bool)
        {
            currentIndex = (currentIndex > 0) ? currentIndex - 1 : menuOptions.Length - 1;
            UpdateMenuOptions();
        }
        else if (InputGetter.Instance.MainGameDown.Bool)
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
