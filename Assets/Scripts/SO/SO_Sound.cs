using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/Sound", fileName = "SO_Sound")]
    public class SO_Sound : ScriptableObject
    {
        #region QOL���㏈��
        // CakeParamsSO���ۑ����Ă���ꏊ�̃p�X
        public const string PATH = "SO_Sound";

        // CakeParamsSO�̎���
        private static SO_Sound _entity = null;
        public static SO_Sound Entity
        {
            get
            {
                // ���A�N�Z�X���Ƀ��[�h����
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_Sound>(PATH);

                    //���[�h�o���Ȃ������ꍇ�̓G���[���O��\��
                    if (_entity == null)
                    {
                        Debug.LogError(PATH + " not found");
                    }
                }

                return _entity;
            }
        }
        #endregion

        [Header("Master��AUdioMixerGroup")] public AudioMixerGroup AMGroupMaster;
        [Header("BGM��AUdioMixerGroup")] public AudioMixerGroup AMGroupBGM;
        [Header("SE��AUdioMixerGroup")] public AudioMixerGroup AMGroupSE;
        [Space(25)]
        [Header("BGM")]
        [Header("�^�C�g��")] public AudioClip TitleBGM;
        [Header("�`�F�C�X��")] public AudioClip ChaseBGM;
        [Header("�Q�[�����m�[�}��")] public AudioClip OnGameNormalBGM;
        [Header("��������")] public List<AudioClip> FootstepBGM;
        [Header("���鑫��")] public AudioClip DashFootstepBGM;
        [Space(25)]
        [Header("SE")]
        [Header("��_���[�W")] public List<AudioClip> DamageTookSE;
        [Header("�A�C�e���w��")] public AudioClip ItemPurchaseSE;
        [Header("�Z�[�u����")] public AudioClip SaveCompletedSE;
        [Header("�h�A���J��")] public AudioClip OpenDoorSE;
        [Header("���b�N���������Ă���h�A���J�����Ƃ���")] public AudioClip TryOpenLockedDoorSE;
        [Header("���𓮂���")] public AudioClip MoveObjectSE;
        [Header("�|�[�V�������g��")] public AudioClip UsePotionSE;
        [Header("���𔭌�����")] public AudioClip FindItemSE;

        [Header("�N���b�N����")] public AudioClip ClickSE;
    }
}