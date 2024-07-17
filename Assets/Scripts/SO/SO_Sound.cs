using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/Sound", fileName = "SO_Sound")]
    public class SO_Sound : ScriptableObject
    {
        #region QOL向上処理
        // CakeParamsSOが保存してある場所のパス
        public const string PATH = "SO_Sound";

        // CakeParamsSOの実体
        private static SO_Sound _entity = null;
        public static SO_Sound Entity
        {
            get
            {
                // 初アクセス時にロードする
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_Sound>(PATH);

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

        [Header("MasterのAUdioMixerGroup")] public AudioMixerGroup AMGroupMaster;
        [Header("BGMのAUdioMixerGroup")] public AudioMixerGroup AMGroupBGM;
        [Header("SEのAUdioMixerGroup")] public AudioMixerGroup AMGroupSE;
        [Space(25)]
        [Header("BGM")]
        [Header("タイトル")] public AudioClip TitleBGM;
        [Header("チェイス中")] public AudioClip ChaseBGM;
        [Header("ゲーム内ノーマル")] public AudioClip OnGameNormalBGM;
        [Header("歩く足音")] public List<AudioClip> FootstepBGM;
        [Header("走る足音")] public AudioClip DashFootstepBGM;
        [Space(25)]
        [Header("SE")]
        [Header("被ダメージ")] public List<AudioClip> DamageTookSE;
        [Header("アイテム購入")] public AudioClip ItemPurchaseSE;
        [Header("セーブ完了")] public AudioClip SaveCompletedSE;
        [Header("ドアを開く")] public AudioClip OpenDoorSE;
        [Header("ロックがかかっているドアを開こうとする")] public AudioClip TryOpenLockedDoorSE;
        [Header("物を動かす")] public AudioClip MoveObjectSE;
        [Header("ポーションを使う")] public AudioClip UsePotionSE;
        [Header("物を発見する")] public AudioClip FindItemSE;

        [Header("クリックする")] public AudioClip ClickSE;
    }
}