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

        // 現在のスタミナ (0 ~ 1)
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
            // ダッシュしているなら...
            if (InputGetter.Instance.MainGame_IsDash && playerMove.InputDir != Vector2.zero)
            {
                // スタミナ減少
                Stamina -= 1 / SO_Player.Entity.StaminaDecreaseDur * Time.deltaTime;
            }
            // ダッシュしていない時...
            else
            {
                // 常にスタミナは回復する
                Stamina += 1 / SO_Player.Entity.StaminaIncreaseDur * Time.deltaTime;
            }

            // UIを更新
            FrontStamina.fillAmount = Stamina;
        }
    }
}
