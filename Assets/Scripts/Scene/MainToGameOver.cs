using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverChecker : MonoBehaviour
{
    void Update()
    {
        if (HPManager.currentHP == 0)
        {
            SceneManager.LoadSceneAsync("GameOver");
        }
    }
}
