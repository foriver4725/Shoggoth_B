using Sirenix.OdinInspector;
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

        [SerializeField, Range(1, 6), Header("プレイヤーの初期体力")] public int PlayerInitHp;
        [SerializeField, Header("プレイヤーの移動スピード [m/s]")] private float playerSpeed;
#if UNITY_EDITOR
        public float PlayerSpeed => SO_Debug.Entity.IsExtraSpeed ? playerSpeed * 5 : playerSpeed;
#else
        public float PlayerSpeed => playerSpeed;
#endif
        [SerializeField, Header("プレイヤーの移動スピード（走り） [m/s]")] private float playerDashSpeed;
#if UNITY_EDITOR
        public float PlayerDashSpeed => SO_Debug.Entity.IsExtraSpeed ? playerDashSpeed * 5 : playerDashSpeed;
#else
        public float PlayerDashSpeed => playerDashSpeed;
#endif
        [Header("敵の移動スピード(1F) [m/s]")] public float EnemySpeed1F;
        [Header("敵の移動スピード(B1F) [m/s]")] public float EnemySpeedB1F;
        [Header("敵の移動スピード(B2F) [m/s]")] public float EnemySpeedB2F;
        [Header("プレイヤーと敵の距離が、\nこの数値より小さくなったら被弾する")] public float PlayerDamageRange;
        [Header("敵がプレイヤーを発見する距離")] public float EnemyChaseRange;
        [Header("敵がプレイヤーを見失う距離")] public float EnemyStopChaseRange;
        [Header("敵が↑の距離より遠くにいるとき、プレイヤーを見失うまでの時間")] public float EnemyStopChaseDuration;
        [SerializeField, Header("無敵時間")] private float invincibleTime;
#if UNITY_EDITOR
        public float InvincibleTime => SO_Debug.Entity.IsInvincible ? 10000 : invincibleTime;
#else
        public float InvincibleTime => invincibleTime;
#endif

        [Header("スタミナの回復速度(何秒で最小から最大になるか)")] public float StaminaIncreaseDur;
        [Header("スタミナの減少速度(何秒で最大から最小になるか)")] public float StaminaDecreaseDur;

        [SerializeField, Required, Range(0.01f, 1.0f), Header("ライトのデフォルトintensity")]
        private float lightIntensityDefault;
        public float LightIntensityDefault => lightIntensityDefault;

        [SerializeField, Required, Range(0.01f, 1.0f), Header("ライトのブレーカーダウン時intensity")]
        private float lightIntensityOnBreakerOff;
        public float LightIntensityOnBreakerOff => lightIntensityOnBreakerOff;

        [Header("BGMのプレハブ")] public GameObject bgmOn;
        [Header("SEのプレハブ")] public GameObject seOn;
    }
}