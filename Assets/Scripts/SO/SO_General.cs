using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/General", fileName = "SO_General")]
    public class SO_General : ScriptableObject
    {
        #region QOL向上処理
        // CakeParamsSOが保存してある場所のパス
        public const string PATH = "SO_General";

        // CakeParamsSOの実体
        private static SO_General _entity = null;
        public static SO_General Entity
        {
            get
            {
                // 初アクセス時にロードする
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_General>(PATH);

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

        [Header("ボタンの反応間隔[s]")] public float ClickDur;
        [Header("ボタンを押した後の待機秒数[s]")] public float AfterClickDur;
    }
}