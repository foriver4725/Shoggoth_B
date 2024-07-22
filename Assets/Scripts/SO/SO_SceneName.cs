using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/SceneName", fileName = "SO_SceneName")]
    public class SO_SceneName : ScriptableObject
    {
        #region QOL向上処理
        // CakeParamsSOが保存してある場所のパス
        public const string PATH = "SO_SceneName";

        // CakeParamsSOの実体
        private static SO_SceneName _entity = null;
        public static SO_SceneName Entity
        {
            get
            {
                // 初アクセス時にロードする
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_SceneName>(PATH);

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

        [Header("タイトルシーン名")] public string Title;
        [Header("クレジットシーン名")] public string Credit;
        [Header("メインゲームのシーン名")] public string MainGame;
        [Header("ゲームクリアのシーン名")] public string GameClear;
        [Header("ゲームオーバーのシーン名")] public string GameOver;
    }
}