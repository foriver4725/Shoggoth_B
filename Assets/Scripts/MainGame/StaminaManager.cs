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

        float staminaRecover = 0;

        private void Awake()
        {
            staminaRecover = SO_DifficultySettings.Entity.StaminaRecover;
        }

        void Update()
        {
            // ダッシュしているなら...
            if (InputGetter.Instance.MainGame_IsDash && playerMove.InputDir != Vector2.zero)
            {
                // スタミナ減少
                GameManager.Instance.Stamina -= (SO_Debug.Entity.IsInfiniteStamina ? 0.01f : 1) / SO_Player.Entity.StaminaDecreaseDur * Time.deltaTime;
            }
            // ダッシュしていない時...
            else
            {





                // 常にスタミナは回復する
                GameManager.Instance.Stamina += 1 / staminaRecover * Time.deltaTime;
            }

            // UIを更新
            FrontStamina.fillAmount = GameManager.Instance.Stamina;
        }
    }
}
