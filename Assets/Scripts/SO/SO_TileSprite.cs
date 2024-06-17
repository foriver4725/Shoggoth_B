using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/TileSprite", fileName = "SO_TileSprite")]
    public class SO_TileSprite : ScriptableObject
    {
        #region QOL向上処理
        // CakeParamsSOが保存してある場所のパス
        public const string PATH = "SO_TileSprite";

        // CakeParamsSOの実体
        private static SO_TileSprite _entity = null;
        public static SO_TileSprite Entity
        {
            get
            {
                // 初アクセス時にロードする
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_TileSprite>(PATH);

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

        [Header("床")] public List<TagSprite> Floors;
        [Header("壁")] public List<TagSprite> Walls;
    }

    [Serializable]
    public class TagSprite
    {
        public string TagName;
        public Sprite Sprite;
    }
}