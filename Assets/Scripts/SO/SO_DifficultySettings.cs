using System;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/DifficultySettings", fileName = "SO_DifficultySettings")]
    public class SO_DifficultySettings : ScriptableObject
    {
        #region QOL向上処理
        // CakeParamsSOが保存してある場所のパス
        public const string PATH = "SO_DifficultySettings";

        // CakeParamsSOの実体
        private static SO_DifficultySettings _entity = null;
        public static SO_DifficultySettings Entity
        {
            get
            {
                // 初アクセス時にロードする
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_DifficultySettings>(PATH);

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

        [SerializeField, Header("難易度設定\nE, N, H, N")]
        private Difficulty[] difficulty;

        public float VisibilityRange => difficulty[Difficulty.Type.ToInt()].VisibilityRange;
        public float StaminaRecover => difficulty[Difficulty.Type.ToInt()].StaminaRecover;
        public bool IsItemRandom => difficulty[Difficulty.Type.ToInt()].IsItemRandom;
        public bool IsExtraShoggothArrangeDifficult => difficulty[Difficulty.Type.ToInt()].IsExtraShoggothArrangeDifficult;
    }

    [Serializable]
    public class Difficulty
    {
        [Header("視界範囲")] public float VisibilityRange;
        [Header("スタミナ回復速度")] public float StaminaRecover;
        [Header("アイテム配置ランダム化")] public bool IsItemRandom;
        [Header("エクストラショゴスの配置難化")] public bool IsExtraShoggothArrangeDifficult;

        public static DifficultyType Type = DifficultyType.Normal;
    }

    public enum DifficultyType
    {
        Easy,
        Normal,
        Hard,
        Nightmare
    }

    public static class DifficultyEx
    {
        public static int ToInt(this DifficultyType type) => type switch
        {
            DifficultyType.Easy => 0,
            DifficultyType.Normal => 1,
            DifficultyType.Hard => 2,
            DifficultyType.Nightmare => 3,
            _ => 1,
        };
    }
}