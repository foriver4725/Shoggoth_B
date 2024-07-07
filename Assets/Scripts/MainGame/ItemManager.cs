using MainGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemManager : MonoBehaviour
{
    

    [SerializeField]
    Image ImageItem;
   // private Sprite sprite;
    static public int currentIndex = 0;  // インクリメントする変数
    
    List<string> items = ItemDatabase.Items;
    static public string itemName = " ";
    static public bool[] UseItem= { false };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ここのキーを今日の授業に変えて
        if (Input.GetKeyDown(KeyCode.Z)  )
        {
            DecrementIndex();  // インクリメントしてアイテムを変更
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            IncrementIndex();  // インクリメントしてアイテムを変更
        }
        //該当のキーを入力するとアイテムを消費し効果発動し別プログラムにUseItemの情報を渡す
        //
        if (Input.GetKeyDown(KeyCode.C))
        {
            UseItem[currentIndex]=true;
        }
    }


    void IncrementIndex()
    {
      

        if (items.Count == 0) return;  // アイテムがない場合は何もしない

        currentIndex++;  // インクリメント
        if (currentIndex >= items.Count)
        {
            currentIndex = 0;  // リストの最後に達した場合は最初に戻る
        }

        //string itemName = items[currentIndex];
         itemName = items[currentIndex];

        // Resourcesフォルダからスプライトをロード
        Sprite loadedSprite = Resources.Load<Sprite>(itemName);

        // スプライトがロードできたら、ImageItemに設定
        if (loadedSprite != null)
        {
            ImageItem.sprite = loadedSprite;
        }
        else
        {
            Debug.LogWarning($"Item image not found for: {itemName}");
        }
    }

    void DecrementIndex()
    {
       

        if (items.Count == 0) return;  // アイテムがない場合は何もしない

        currentIndex--;  // デクリメント
        if (currentIndex <= items.Count)
        {
            currentIndex = items.Count;  // リストの最初に達した場合は最後に戻る
        }

        //string itemName = items[currentIndex];
         itemName = items[currentIndex];
        // Resourcesフォルダからスプライトをロード
        //Addressable Assetsパッケージを追加し、アセットをアドレス可能にすれば他のフォルダでも可能
        Sprite loadedSprite = Resources.Load<Sprite>(itemName);

        // スプライトがロードできたら、ImageItemに設定
        if (loadedSprite != null)
        {
            ImageItem.sprite = loadedSprite;
        }
        else
        {
            Debug.LogWarning($"Item image not found for: {itemName}");
        }
    }



}
