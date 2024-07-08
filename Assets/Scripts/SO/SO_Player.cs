using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/Player", fileName = "SO_Player")]
    public class SO_Player : ScriptableObject
    {
        #region QOL���㏈��
        // CakeParamsSO���ۑ����Ă���ꏊ�̃p�X
        public const string PATH = "SO_Player";

        // CakeParamsSO�̎���
        private static SO_Player _entity = null;
        public static SO_Player Entity
        {
            get
            {
                // ���A�N�Z�X���Ƀ��[�h����
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_Player>(PATH);

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

        [Header("�v���C���[�̈ړ��X�s�[�h [m/s]")] public float PlayerSpeed;
        [Header("�v���C���[�̈ړ��X�s�[�h�i����j [m/s]")] public float PlayerDashSpeed;
        [Header("�G�̈ړ��X�s�[�h [m/s]")] public float EnemySpeed;
        [Header("�G���v���C���[�𔭌����鋗��")] public float EnemyChaseRange;
        [Header("�G���v���C���[������������")] public float EnemyStopChaseRange;
        [Header("�G�����̋�����艓���ɂ���Ƃ��A�v���C���[���������܂ł̎���")] public float EnemyStopChaseDuration;
        [Header("���G����")] public float InvincibleTime;

        [Header("�ő�X�^�~�i")] public float MaxStamina;
        [Header("�v���C���[���X�^�~�i���������Ȃ��������́A�񕜊J�n����")] public float OnDuringStaminaIncreaseDur;
        [Header("�v���C���[���X�^�~�i������������́A�񕜊J�n����")] public float StaminaIncreaseDur;
        [Header("�X�^�~�i������̌p������")] public float InfiniteStaminaDur;

        [Header("BGM�̃v���n�u")] public GameObject bgmOn;
        [Header("SE�̃v���n�u")] public GameObject seOn;
        [Header("�`�F�C�X����BGM")] public AudioClip ChaseBGM;
        [Header("�ʏ��BGM")] public AudioClip NormalBGM;
        [Header("Title��BGM")] public AudioClip TitleBGM;
        [Header("�_���[�W")] public AudioClip damegeSE;
        [Header("����(walk)")] public AudioClip footstep_wBGM;
        [Header("����(run)")] public AudioClip footstep_rBGM;
    }
}