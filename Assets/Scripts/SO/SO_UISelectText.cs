using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/UISelectText", fileName = "SO_UISelectText")]
    public class SO_UISelectText : ScriptableObject
    {
        #region QOL向上処理
        // CakeParamsSOが保存してある場所のパス
        public const string PATH = "SO_UISelectText";

        // CakeParamsSOの実体
        private static SO_UISelectText _entity = null;
        public static SO_UISelectText Entity
        {
            get
            {
                // 初アクセス時にロードする
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_UISelectText>(PATH);

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

        [Header("アイテム自販機")] public List<string> VendingItem;

        [Header("妨害自販機")] public List<string> VendingArms;
        [Header("調合")] public List<string> Mixture;
        [Header("セーブ")] public List<string> Save;

    }


    //[Serializable]
    //public class TagSprite
    //{
    //    public string TagName;
    //    public Sprite Sprite;
    //}


}



