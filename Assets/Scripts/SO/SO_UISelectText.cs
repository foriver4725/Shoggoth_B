using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/UISelectText", fileName = "SO_UISelectText")]
    public class SO_UISelectText : ScriptableObject
    {
        #region QOL���㏈��
        // CakeParamsSO���ۑ����Ă���ꏊ�̃p�X
        public const string PATH = "SO_UISelectText";

        // CakeParamsSO�̎���
        private static SO_UISelectText _entity = null;
        public static SO_UISelectText Entity
        {
            get
            {
                // ���A�N�Z�X���Ƀ��[�h����
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_UISelectText>(PATH);

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

        [Header("�A�C�e�����̋@")] public List<string> VendingItem;

        [Header("�W�Q���̋@")] public List<string> VendingArms;
        [Header("����")] public List<string> Mixture;
        [Header("�Z�[�u")] public List<string> Save;

    }


    //[Serializable]
    //public class TagSprite
    //{
    //    public string TagName;
    //    public Sprite Sprite;
    //}


}



