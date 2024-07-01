using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public List<Image> hearts; // �n�[�g��Image���X�g
     static public int currentHP;

    void Start()
    {
        currentHP = hearts.Count; // ����HP���n�[�g�̐��ɐݒ�
    }

    public void DecreaseHP()
    {
        if (currentHP > 0)
        {
            currentHP--;
            hearts[currentHP].enabled = false; // �n�[�g��Image���\���ɂ���
        }
    }
}
