using IA;
using SO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainGame
{
    public class StaminaManager : MonoBehaviour
    {
        [SerializeField] PlayerMove playerMove;
        [SerializeField] Image FrontStamina;

        // ���݂̃X�^�~�i (0 ~ 1)
        private static float _stamina = 1;
        public static float Stamina
        {
            get
            {
                return _stamina;
            }
            set
            {
                _stamina = Mathf.Clamp(value, 0, 1);
            }
        }

        void Update()
        {
            // �_�b�V�����Ă���Ȃ�...
            if (InputGetter.Instance.MainGame_IsDash && playerMove.InputDir != Vector2.zero)
            {
                // �X�^�~�i����
                Stamina -= 1 / SO_Player.Entity.StaminaDecreaseDur * Time.deltaTime;
            }
            // �_�b�V�����Ă��Ȃ���...
            else
            {
                // ��ɃX�^�~�i�͉񕜂���
                Stamina += 1 / SO_Player.Entity.StaminaIncreaseDur * Time.deltaTime;
            }

            // UI���X�V
            FrontStamina.fillAmount = Stamina;
        }
    }
}
