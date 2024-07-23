using IA;
using SO;
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

        void Update()
        {
            // �_�b�V�����Ă���Ȃ�...
            if (InputGetter.Instance.MainGame_IsDash && playerMove.InputDir != Vector2.zero)
            {
                // �X�^�~�i����
                GameManager.Instance.Stamina -= (SO_Debug.Entity.IsInfiniteStamina ? 0.01f : 1) / SO_Player.Entity.StaminaDecreaseDur * Time.deltaTime;
            }
            // �_�b�V�����Ă��Ȃ���...
            else
            {
                // ��ɃX�^�~�i�͉񕜂���
                GameManager.Instance.Stamina += 1 / SO_Player.Entity.StaminaIncreaseDur * Time.deltaTime;
            }

            // UI���X�V
            FrontStamina.fillAmount = GameManager.Instance.Stamina;
        }
    }
}
