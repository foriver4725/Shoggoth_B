using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/ItemSprite", fileName = "SO_ItemSprite")]
    public class SO_ItemSprite : ScriptableObject
    {
        #region QOL向上処理
        // CakeParamsSOが保存してある場所のパス
        public const string PATH = "SO_ItemSprite";

        // CakeParamsSOの実体
        private static SO_ItemSprite _entity = null;
        public static SO_ItemSprite Entity
        {
            get
            {
                // 初アクセス時にロードする
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_ItemSprite>(PATH);

                    //ロード出来なかった場合はエラーログを表示
                    if (_entity == null)
                    {
                        Debug.LogError(PATH + " not found");
                    }
                }

                return _entity;
            }
        }
        #endregion

        [Header("アイテムスプライト")] public List<NameSprite> ItemSprites;
    }

    [Serializable]
    public class NameSprite
    {
        public string Name;
        public Sprite Sprite;
    }
}