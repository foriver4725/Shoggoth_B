using MainGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public List<Image> hearts; // ハートのImageリスト
    [SerializeField] Image DamageImg;

    void Start()
    {
        GameManager.Instance.CurrentHP = hearts.Count; // 初期HPをハートの数に設定
        DamageImg.color = Color.clear;
    }
    void Update()
    {
        DamageImg.color = Color.Lerp(DamageImg.color, Color.clear, Time.deltaTime);
            }

    public void DecreaseHP()
    {
        if (GameManager.Instance.CurrentHP > 0)
        {
            GameManager.Instance.CurrentHP--;
            hearts[GameManager.Instance.CurrentHP].color = Color.black; // ハートのImageを非表示にする
            DamageImg.color = new Color(0.7f, 0, 0, 0.7f);
        }
    }
}
