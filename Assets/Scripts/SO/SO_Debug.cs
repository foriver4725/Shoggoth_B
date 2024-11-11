using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/Debug", fileName = "SO_Debug")]
    public class SO_Debug : ScriptableObject
    {
        #region QOL向上処理
        // CakeParamsSOが保存してある場所のパス
        public const string PATH = "SO_Debug";

        // CakeParamsSOの実体
        private static SO_Debug _entity = null;
        public static SO_Debug Entity
        {
            get
            {
                // 初アクセス時にロードする
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_Debug>(PATH);

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

#if UNITY_EDITOR
        [Header("無敵時間を10000秒にする")] public bool IsInvincible;
        [Header("移動速度を5倍にする")] public bool IsExtraSpeed;
        [Header("スタミナの減少速度を0.01倍にする")] public bool IsInfiniteStamina;
        [Header("有効にした状態でゲームを実行すると、\nセーブデータを全て削除する")] public bool IsDeleteAllSaveData;
        [Header("有効にした状態でゲームを実行すると、\n実績を全て達成してセーブする\n(↑の方が優先)")]
        public bool IsAchieveAllSaveData;
#endif
    }
}
