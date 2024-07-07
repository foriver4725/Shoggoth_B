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
    static public int currentIndex = 0;  // �C���N�������g����ϐ�
    
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
        // �����̃L�[�������̎��Ƃɕς���
        if (Input.GetKeyDown(KeyCode.Z)  )
        {
            DecrementIndex();  // �C���N�������g���ăA�C�e����ύX
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            IncrementIndex();  // �C���N�������g���ăA�C�e����ύX
        }
        //�Y���̃L�[����͂���ƃA�C�e����������ʔ������ʃv���O������UseItem�̏���n��
        //
        if (Input.GetKeyDown(KeyCode.C))
        {
            UseItem[currentIndex]=true;
        }
    }


    void IncrementIndex()
    {
      

        if (items.Count == 0) return;  // �A�C�e�����Ȃ��ꍇ�͉������Ȃ�

        currentIndex++;  // �C���N�������g
        if (currentIndex >= items.Count)
        {
            currentIndex = 0;  // ���X�g�̍Ō�ɒB�����ꍇ�͍ŏ��ɖ߂�
        }

        //string itemName = items[currentIndex];
         itemName = items[currentIndex];

        // Resources�t�H���_����X�v���C�g�����[�h
        Sprite loadedSprite = Resources.Load<Sprite>(itemName);

        // �X�v���C�g�����[�h�ł�����AImageItem�ɐݒ�
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
       

        if (items.Count == 0) return;  // �A�C�e�����Ȃ��ꍇ�͉������Ȃ�

        currentIndex--;  // �f�N�������g
        if (currentIndex <= items.Count)
        {
            currentIndex = items.Count;  // ���X�g�̍ŏ��ɒB�����ꍇ�͍Ō�ɖ߂�
        }

        //string itemName = items[currentIndex];
         itemName = items[currentIndex];
        // Resources�t�H���_����X�v���C�g�����[�h
        //Addressable Assets�p�b�P�[�W��ǉ����A�A�Z�b�g���A�h���X�\�ɂ���Α��̃t�H���_�ł��\
        Sprite loadedSprite = Resources.Load<Sprite>(itemName);

        // �X�v���C�g�����[�h�ł�����AImageItem�ɐݒ�
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
