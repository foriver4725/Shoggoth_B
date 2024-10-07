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

        [SerializeField, Header("�v���C���[�̈ړ��X�s�[�h [m/s]")] private float playerSpeed;
        public float PlayerSpeed => SO_Debug.Entity.IsExtraSpeed ? playerSpeed * 5 : playerSpeed;
        [SerializeField, Header("�v���C���[�̈ړ��X�s�[�h�i����j [m/s]")] private float playerDashSpeed;
        public float PlayerDashSpeed => SO_Debug.Entity.IsExtraSpeed ? playerDashSpeed * 5 : playerDashSpeed;
        [Header("�G�̈ړ��X�s�[�h(1F) [m/s]")] public float EnemySpeed1F;
        [Header("�G�̈ړ��X�s�[�h(B1F) [m/s]")] public float EnemySpeedB1F;
        [Header("�G�̈ړ��X�s�[�h(B2F) [m/s]")] public float EnemySpeedB2F;
        [Header("�v���C���[�ƓG�̋������A\n���̐��l��菬�����Ȃ������e����")] public float PlayerDamageRange;
        [Header("�G���v���C���[�𔭌����鋗��")] public float EnemyChaseRange;
        [Header("�G���v���C���[������������")] public float EnemyStopChaseRange;
        [Header("�G�����̋�����艓���ɂ���Ƃ��A�v���C���[���������܂ł̎���")] public float EnemyStopChaseDuration;
        [Header("���G����")] public float InvincibleTime;

        [Header("�X�^�~�i�̉񕜑��x(���b�ōŏ�����ő�ɂȂ邩)")] public float StaminaIncreaseDur;
        [Header("�X�^�~�i�̌������x(���b�ōő傩��ŏ��ɂȂ邩)")] public float StaminaDecreaseDur;

        [Header("BGM�̃v���n�u")] public GameObject bgmOn;
        [Header("SE�̃v���n�u")] public GameObject seOn;
    }
}