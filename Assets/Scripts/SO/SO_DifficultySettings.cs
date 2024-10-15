using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/DifficultySettings", fileName = "SO_DifficultySettings")]
    public class SO_DifficultySettings : ScriptableObject
    {
        #region QOL���㏈��
        // CakeParamsSO���ۑ����Ă���ꏊ�̃p�X
        public const string PATH = "SO_DifficultySettings";

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


        [Header("��Փx�ݒ�")] public List<Difficulty> Difficulty;
        
    }

    [Serializable]
    public class Difficulty
    {
        [Header("���E�͈�")] public int VisibilityRange;
        [Header("�X�^�~�i�񕜑��x")] public float StaminaRecover;
        [Header("�A�C�e���z�u�����_����")] public bool IsItemRandom;
    }
}