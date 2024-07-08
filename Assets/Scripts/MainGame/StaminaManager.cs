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
        //���݂�HP�B
        float StaminaTime = 0.0f;
        static public bool StaminaDetection = true;

        private bool _isUsedSpeedPotion = false;

        // Start is called before the first frame update
        void Start()
        {
            //Image��GameObject�Ƃ��Ď擾
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
                //�X�s�[�h�|�[�V�������g�p���Ă��Ȃ��Ƃ�
                else if (FrontStamina.fillAmount > 0)
                {
                    StaminaTime += Time.deltaTime;
                    FrontStamina.fillAmount -= StaminaTime / SO_Player.Entity.MaxStamina;
                }
            }
            //�X�^�~�i���s�����Ƃ�
            if (FrontStamina.fillAmount == 0)
            {
                //�X�^�~�i�����S�ɉ񕜂���܂Ń_�b�V���@�\�𕕈󂷂�
                StaminaDetection = false;

            }
            //�X�^�~�i�����S����������_�b�V���@�\����
            if (FrontStamina.fillAmount == 1)
            {
                StaminaDetection = true;
                StaminaTime = 0f;
            }
            //�X�^�~�i���g���؂�Ȃ������Ƃ�
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
