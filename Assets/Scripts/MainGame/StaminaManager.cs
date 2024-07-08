using IA;
using MainGame;
using SO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

namespace MainGame
{
    public class StaminaManager : MonoBehaviour
    {
        // GameObject FrontStamina;
        [SerializeField]
        Image FrontStamina;
        //現在のHP。
        float StaminaTime = 0.0f;
        static public bool StaminaDetection = true;

        private bool _isUsedSpeedPotion = false;

        // Start is called before the first frame update
        void Start()
        {
            //ImageをGameObjectとして取得
            //image = GameObject.Find("Image");

        }

        // Update is called once per frame
        void Update()
        {
            if (HPManager.currentHP == 0)
            {
                FrontStamina.fillAmount = 0;
            }

            //if (InputGetter.Instance.MainGame_IsUseItem && ItemDatabase.FindItem(ItemManager.CurrentIndex).Name == "SpeedPotion")
            //{
            //    _isUsedSpeedPotion = true;
            //    ItemDatabase.RemoveItem("SpeedPotion");
            //    StartCoroutine(CountTime(() => _isUsedSpeedPotion = false, SO_Player.Entity.InfiniteStaminaDur));
            //}

            if (InputGetter.Instance.MainGame_IsDash && StaminaDetection == true)
            {
                if (_isUsedSpeedPotion)
                {
                    FrontStamina.fillAmount = 1;
                }
                //スピードポーションを使用していないとき
                else if (FrontStamina.fillAmount > 0)
                {
                    StaminaTime += Time.deltaTime;
                    FrontStamina.fillAmount -= StaminaTime / SO_Player.Entity.MaxStamina;
                }
            }
            //スタミナが尽きたとき
            if (FrontStamina.fillAmount == 0)
            {
                //スタミナが完全に回復するまでダッシュ機能を封印する
                StaminaDetection = false;

            }
            //スタミナが完全復活したらダッシュ機能解禁
            if (FrontStamina.fillAmount == 1)
            {
                StaminaDetection = true;
                StaminaTime = 0f;
            }
            //スタミナを使い切らなかったとき
            //if (FrontStamina.fillAmount != 0)
            else
            {
                if (StaminaDetection == false)
                {
                    StaminaTime += Time.deltaTime;
                    if (StaminaTime >= SO_Player.Entity.StaminaIncreaseDur)
                    {
                        FrontStamina.fillAmount += StaminaTime / (SO_Player.Entity.MaxStamina * 20);

                    }
                }
                else
                {

                    StaminaTime += Time.deltaTime;
                    if (StaminaTime >= SO_Player.Entity.OnDuringStaminaIncreaseDur)
                    {
                        //StaminaTime = 0f;

                        FrontStamina.fillAmount += StaminaTime / (SO_Player.Entity.MaxStamina * 4);
                    }
                }
            }
        }

        IEnumerator CountTime(Action action, float time)
        {
            float t = 0;
            while (true)
            {
                t += Time.deltaTime;

                if (t >= time)
                {
                    action();
                }

                yield return null;
            }
        }
    }
}
