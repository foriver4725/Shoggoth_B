using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/Player", fileName = "SO_Player")]
    public class SO_Player : ScriptableObject
    {
        #region QOL向上処理
        // CakeParamsSOが保存してある場所のパス
        public const string PATH = "SO_Player";

        // CakeParamsSOの実体
        private static SO_Player _entity = null;
        public static SO_Player Entity
        {
            get
            {
                // 初アクセス時にロードする
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_Player>(PATH);

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

        [Header("プレイヤーの移動スピード [m/s]")] public float PlayerSpeed;
        [Header("プレイヤーの移動スピード（走り） [m/s]")] public float PlayerDashSpeed;
        [Header("敵の移動スピード [m/s]")] public float EnemySpeed;
        [Header("敵がプレイヤーを発見する距離")] public float EnemyChaseRange;
        [Header("敵がプレイヤーを見失う距離")] public float EnemyStopChaseRange;
        [Header("敵が↑の距離より遠くにいるとき、プレイヤーを見失うまでの時間")] public float EnemyStopChaseDuration;
        [Header("無敵時間")] public float InvincibleTime;

        [Header("最大スタミナ")] public float MaxStamina;
        [Header("プレイヤーがスタミナを消費しきらなかった時の、回復開始時間")] public float OnDuringStaminaIncreaseDur;
        [Header("プレイヤーがスタミナ消費しきった時の、回復開始時間")] public float StaminaIncreaseDur;
        [Header("スタミナ消費無効の継続時間")] public float InfiniteStaminaDur;

        [Header("BGMのプレハブ")] public GameObject bgmOn;
        [Header("SEのプレハブ")] public GameObject seOn;
        [Header("チェイス中のBGM")] public AudioClip ChaseBGM;
        [Header("通常のBGM")] public AudioClip NormalBGM;
        [Header("TitleのBGM")] public AudioClip TitleBGM;
        [Header("ダメージ")] public AudioClip damegeSE;
        [Header("足音(walk)")] public AudioClip footstep_wBGM;
        [Header("足音(run)")] public AudioClip footstep_rBGM;
    }
}