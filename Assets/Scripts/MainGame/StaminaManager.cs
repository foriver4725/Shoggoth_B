using IA;
using MainGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StaminaManager : MonoBehaviour
{
   // GameObject FrontStamina;
    [SerializeField]
    Image FrontStamina;
    //�ő�HP�ƌ��݂�HP�B
    public static int maxStamina = 300;
    float StaminaTime = 0.0f;
    static public bool StaminaDetection=true;
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

        if (InputGetter.Instance.MainGame_IsDash && StaminaDetection == true) 
        {
            //�X�s�[�h�|�[�V�������g�p�����Ƃ�
            //�O�\�b�ԃX�^�~�i�����
            //�O�\�b�o�߂�����g�����A�C�e�����f�[�^�x�[�X����폜����
            if(ItemManager.itemName== "speedposion" && ItemManager.UseItem[ItemManager.currentIndex] ==true)
            {
                StaminaTime += Time.deltaTime;
                if (StaminaTime >= 20f)
                {
                    StaminaTime = 0f;
                    ItemManager.UseItem[ItemManager.currentIndex] = false;
                    ItemDatabase.RemoveItem(ItemManager.itemName);
                }
                FrontStamina.fillAmount = 1;
            }
            //�X�s�[�h�|�[�V�������g�p���Ă��Ȃ��Ƃ�
            else if (FrontStamina.fillAmount>0)
            {
                StaminaTime += Time.deltaTime;
                FrontStamina.fillAmount -= StaminaTime / maxStamina;
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
                if (StaminaTime >= 5f)
                {
                    FrontStamina.fillAmount += StaminaTime / (maxStamina * 20);
                    
                }
            }
            else
            {

                StaminaTime += Time.deltaTime;
                if (StaminaTime >= 3f)
                {
                    //StaminaTime = 0f;

                    FrontStamina.fillAmount += StaminaTime / (maxStamina*4);
                }
            }

            
        }



    }
}
