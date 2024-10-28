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

#if false
        // アイテム画像のUI
        [SerializeField] private Image _itemImage;

        // 現在手に持っているアイテムのインデックス番号(0~アイテム数-1 をループする)
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

        public static Sprite Sprite; // 今表示するべきスプライト

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
            // カーソルを回すとアイテムが切り替わる
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

            // スプライトを変更
            _itemImage.sprite = Sprite;
        }

#endif
    }
}
