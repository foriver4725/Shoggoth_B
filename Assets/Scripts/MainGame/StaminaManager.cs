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
    //最大HPと現在のHP。
    public static int maxStamina = 300;
    float StaminaTime = 0.0f;
    static public bool StaminaDetection=true;
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

        if (InputGetter.Instance.MainGame_IsDash && StaminaDetection == true) 
        {
            //スピードポーションを使用したとき
            //三十秒間スタミナ消費無効
            //三十秒経過したら使ったアイテムをデータベースから削除する
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
            //スピードポーションを使用していないとき
            else if (FrontStamina.fillAmount>0)
            {
                StaminaTime += Time.deltaTime;
                FrontStamina.fillAmount -= StaminaTime / maxStamina;
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
