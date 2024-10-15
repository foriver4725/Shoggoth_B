using System;
using System.Collections;
using System.Collections.Generic;
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


        [Header("難易度設定")] public List<Difficulty> Difficulty;
        
    }

    [Serializable]
    public class Difficulty
    {
        [Header("視界範囲")] public int VisibilityRange;
        [Header("スタミナ回復速度")] public float StaminaRecover;
        [Header("アイテム配置ランダム化")] public bool IsItemRandom;
    }
}