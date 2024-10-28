using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/GameState", fileName = "SO_GameState")]
    public class SO_GameState : ScriptableObject
    {
        #region QOL向上処理
        // CakeParamsSOが保存してある場所のパス
        public const string PATH = "SO_GameState";

        // CakeParamsSOの実体
        private static SO_GameState _entity = null;
        public static SO_GameState Entity
        {
            get
            {
                // 初アクセス時にロードする
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_GameState>(PATH);

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

        [Header("解像度(例：1920*1080)")] public Vector2Int Resolution;
        [Header("フルスクリーンにする")] public bool IsFullScreen;
        [Header("Vsyncをオンにする")] public bool IsVsyncOn;
        [Header("(Vsyncがオフの時のみ)ターゲットフレームレート")] public int TargetFrameRate;
    }
}