using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/GameState", fileName = "SO_GameState")]
    public class SO_GameState : ScriptableObject
    {
        #region QOL���㏈��
        // CakeParamsSO���ۑ����Ă���ꏊ�̃p�X
        public const string PATH = "SO_GameState";

        // CakeParamsSO�̎���
        private static SO_GameState _entity = null;
        public static SO_GameState Entity
        {
            get
            {
                // ���A�N�Z�X���Ƀ��[�h����
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_GameState>(PATH);

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

        [Header("�𑜓x(��F1920*1080)")] public Vector2Int Resolution;
        [Header("�t���X�N���[���ɂ���")] public bool IsFullScreen;
        [Header("Vsync���I���ɂ���")] public bool IsVsyncOn;
        [Header("(Vsync���I�t�̎��̂�)�^�[�Q�b�g�t���[�����[�g")] public int TargetFrameRate;
    }
}