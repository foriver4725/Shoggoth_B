using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/UIConsoleText", fileName = "SO_UIConsoleText")]
    public class SO_UIConsoleText : ScriptableObject
    {
        #region QOL向上処理
        // CakeParamsSOが保存してある場所のパス
        public const string PATH = "SO_UIConsoleText";

        // CakeParamsSOの実体
        private static SO_UIConsoleText _entity = null;
        public static SO_UIConsoleText Entity
        {
            get
            {
                // 初アクセス時にロードする
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_UIConsoleText>(PATH);

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

        [Header("書斎に向かうことをを示唆するログ"), TextArea(1, 1000)] public string ShowDirectionLog;
        [Header("脱出方法を示唆するログ"), TextArea(1, 1000)] public string EscapeTeachLog;
        [Header("アイテムを全て入手した後、\n脱出する場所を示唆するログ"), TextArea(1, 1000)] public string ItemCompletedLog;
    }
}
