using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public List<Image> hearts; // ハートのImageリスト
     static public int currentHP;

    void Start()
    {
        currentHP = hearts.Count; // 初期HPをハートの数に設定
    }

    public void DecreaseHP()
    {
        if (currentHP > 0)
        {
            currentHP--;
            hearts[currentHP].enabled = false; // ハートのImageを非表示にする
        }
    }
}
