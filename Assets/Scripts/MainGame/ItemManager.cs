using MainGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SO;
using IA;

namespace MainGame
{
    public class ItemManager : MonoBehaviour
    {
        // �A�C�e���摜��UI
        [SerializeField] private Image _itemImage;

        // ���ݎ�Ɏ����Ă���A�C�e���̃C���f�b�N�X�ԍ�(0~�A�C�e����-1 �����[�v����)
        private static int _currentIndex = 0;
        public static int CurrentIndex
        {
            get { return _currentIndex; }
            set
            {
                int valueIndex = 0;
                if (value < 0)
                {
                    valueIndex = ItemDatabase.Items.Count - 1;
                }
                else if (value > ItemDatabase.Items.Count - 1)
                {
                    valueIndex = 0;
                }
                else
                {
                    valueIndex = value;
                }
                _currentIndex = valueIndex;
            }
        }

        public static Sprite Sprite; // ���\������ׂ��X�v���C�g

        private void Start()
        {
            foreach (var e in SO_ItemSprite.Entity.ItemSprites)
            {
                ItemDatabase.AddItem(e.Name, e.Sprite);
            }
            Sprite = ItemDatabase.FindItem(CurrentIndex).Sprite;
            _itemImage.sprite = Sprite;
        }

        void Update()
        {
            // �J�[�\�����񂷂ƃA�C�e�����؂�ւ��
            if (InputGetter.Instance.MainGame_ValueScrollItem == -1)
            {
                CurrentIndex--;
                Sprite = ItemDatabase.FindItem(CurrentIndex).Sprite;
            }
            if (InputGetter.Instance.MainGame_ValueScrollItem == 1)
            {
                CurrentIndex++;
                Sprite = ItemDatabase.FindItem(CurrentIndex).Sprite;
            }

            // �X�v���C�g��ύX
            _itemImage.sprite = Sprite;
        }
    }
}
