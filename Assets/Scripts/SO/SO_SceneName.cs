using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/SceneName", fileName = "SO_SceneName")]
    public class SO_SceneName : ScriptableObject
    {
        #region QOL���㏈��
        // CakeParamsSO���ۑ����Ă���ꏊ�̃p�X
        public const string PATH = "SO_SceneName";

        // CakeParamsSO�̎���
        private static SO_SceneName _entity = null;
        public static SO_SceneName Entity
        {
            get
            {
                // ���A�N�Z�X���Ƀ��[�h����
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_SceneName>(PATH);

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

        [Header("�^�C�g���V�[����")] public string Title;
        [Header("�N���W�b�g�V�[����")] public string Credit;
        [Header("���C���Q�[���̃V�[����")] public string MainGame;
        [Header("�Q�[���N���A�̃V�[����")] public string GameClear;
        [Header("�Q�[���I�[�o�[�̃V�[����")] public string GameOver;
    }
}