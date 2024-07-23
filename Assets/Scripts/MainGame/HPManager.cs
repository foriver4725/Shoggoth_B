using MainGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public List<Image> hearts; // �n�[�g��Image���X�g

    void Start()
    {
        GameManager.Instance.CurrentHP = hearts.Count; // ����HP���n�[�g�̐��ɐݒ�
    }

    public void DecreaseHP()
    {
        if (GameManager.Instance.CurrentHP > 0)
        {
            GameManager.Instance.CurrentHP--;
            hearts[GameManager.Instance.CurrentHP].color = Color.black; // �n�[�g��Image���\���ɂ���
        }
    }
}
