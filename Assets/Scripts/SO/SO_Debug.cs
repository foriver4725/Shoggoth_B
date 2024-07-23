using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/Debug", fileName = "SO_Debug")]
    public class SO_Debug : ScriptableObject
    {
        #region QOL���㏈��
        // CakeParamsSO���ۑ����Ă���ꏊ�̃p�X
        public const string PATH = "SO_Debug";

        // CakeParamsSO�̎���
        private static SO_Debug _entity = null;
        public static SO_Debug Entity
        {
            get
            {
                // ���A�N�Z�X���Ƀ��[�h����
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_Debug>(PATH);

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

        [Header("���G���Ԃ�10000�b�ɂ���")] public bool IsInvincible;
        [Header("�X�^�~�i�̌������x��0.01�{�ɂ���")] public bool IsInfiniteStamina;
    }
}
