using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/ItemSprite", fileName = "SO_ItemSprite")]
    public class SO_ItemSprite : ScriptableObject
    {
        #region QOL���㏈��
        // CakeParamsSO���ۑ����Ă���ꏊ�̃p�X
        public const string PATH = "SO_ItemSprite";

        // CakeParamsSO�̎���
        private static SO_ItemSprite _entity = null;
        public static SO_ItemSprite Entity
        {
            get
            {
                // ���A�N�Z�X���Ƀ��[�h����
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_ItemSprite>(PATH);

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

        [Header("�A�C�e���X�v���C�g")] public List<NameSprite> ItemSprites;
    }

    [Serializable]
    public class NameSprite
    {
        public string Name;
        public Sprite Sprite;
    }
}