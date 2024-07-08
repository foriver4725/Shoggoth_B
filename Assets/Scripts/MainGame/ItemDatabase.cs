using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MainGame
{
    public static class ItemDatabase
    {
        public static List<NameSprite> Items = new();

        public static void AddItem(string itemName, Sprite itemImage)
        {
            Items.Add(new NameSprite(itemName, itemImage));
        }

        public static void RemoveItem(string itemName)
        {
            Items.Remove(FindItem(itemName));
        }

        public static void RemoveItem(int itemIndex)
        {
            Items.Remove(Items[itemIndex]);
        }

        public static bool CheckItem(string itemName)
        {
            foreach (NameSprite e in Items)
            {
                if (e.Name == itemName)
                {
                    return true;
                }
            }

            return false;
        }

        public static NameSprite FindItem(string itemName)
        {
            foreach (NameSprite e in Items)
            {
                if (e.Name == itemName)
                {
                    return e;
                }
            }

            throw new System.Exception("対象の名前のスプライトが見つかりませんでした");
        }

        public static NameSprite FindItem(int itemIndex)
        {
            foreach ((int index, NameSprite e) in Items.Enumerate())
            {
                if (index == itemIndex)
                {
                    return e;
                }
            }

            throw new System.Exception("対象の名前のスプライトが見つかりませんでした");
        }

        // リストのインデックスと要素を同時に列挙するイテレータ
        private static IEnumerable<(int, NameSprite)> Enumerate(this List<NameSprite> nameSprite)
        {
            for (int i = 0; i < nameSprite.Count; i++)
            {
                yield return (i, nameSprite[i]);
            }
        }
    }

    public readonly struct NameSprite
    {
        public readonly string Name;
        public readonly Sprite Sprite;

        public NameSprite(string name, Sprite sprite)
        {
            Name = name;
            Sprite = sprite;
        }
    }
}