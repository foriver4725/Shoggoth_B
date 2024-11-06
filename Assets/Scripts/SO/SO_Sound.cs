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
        [Header("歩く足音")] public AudioClip FootstepBGM;
        [Header("走る足音")] public AudioClip DashFootstepBGM;
        [Space(25)]
        [Header("SE")]
        [Header("被ダメージ")] public AudioClip DamageTookSE;
        [Header("鍵のかかったドアに対してインタラクトする")] public AudioClip LockedDoorSE;
        [Header("ポーションを使う")] public AudioClip UsePotionSE;
        [Header("クリックする")] public AudioClip ClickSE;
        [Header("1Fのエレベーター")] public AudioClip Elevator1FSE;
        [Header("B1Fのエレベーター")] public AudioClip ElevatorB1FSE;
        [Header("B2Fのエレベーター")] public AudioClip ElevatorB2FSE;
        [Header("ブレーカーが落ちる")] public AudioClip BreakerOffSE;
        [Header("ブレーカーが復旧する")] public AudioClip BreakerOnSE;
        [Header("鉄格子が閉まる")] public AudioClip IronFenceCloseSE;
    }
}