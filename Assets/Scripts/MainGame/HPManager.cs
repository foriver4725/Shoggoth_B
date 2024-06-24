using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public Image[] hearts;
    private int currentHP;

    void Start()
    {
        currentHP = hearts.Length;
        UpdateHearts();
    }

    public void DecreaseHP()
    {
        if (currentHP > 0)
        {
            currentHP--;
            UpdateHearts();
        }

        if (currentHP == 0)
        {
            // ゲームオーバー処理をここに記述
            Debug.Log("Game Over");
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < currentHP;
        }
    }
}
