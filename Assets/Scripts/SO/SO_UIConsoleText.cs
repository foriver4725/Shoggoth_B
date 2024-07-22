using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/UIConsoleText", fileName = "SO_UIConsoleText")]
    public class SO_UIConsoleText : ScriptableObject
    {
        #region QOL���㏈��
        // CakeParamsSO���ۑ����Ă���ꏊ�̃p�X
        public const string PATH = "SO_UIConsoleText";

        // CakeParamsSO�̎���
        private static SO_UIConsoleText _entity = null;
        public static SO_UIConsoleText Entity
        {
            get
            {
                // ���A�N�Z�X���Ƀ��[�h����
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_UIConsoleText>(PATH);

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
        [Header("�V���S�X")] public List<string> ShoggothLog;
        [Header("�A�C�e��")] public List<string> ItemLog;
        [Header("�}�b�v")] public List<string> MapLog;
        [Header("���"), TextArea(1, 1000)] public List<string> IndexLog;

        [Header("���̋@")] public List<string> Vending;

    }


}



