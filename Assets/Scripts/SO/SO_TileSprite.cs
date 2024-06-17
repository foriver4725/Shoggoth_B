using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/TileSprite", fileName = "SO_TileSprite")]
    public class SO_TileSprite : ScriptableObject
    {
        #region QOL���㏈��
        // CakeParamsSO���ۑ����Ă���ꏊ�̃p�X
        public const string PATH = "SO_TileSprite";

        // CakeParamsSO�̎���
        private static SO_TileSprite _entity = null;
        public static SO_TileSprite Entity
        {
            get
            {
                // ���A�N�Z�X���Ƀ��[�h����
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_TileSprite>(PATH);

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

        [Header("��")] public List<TagSprite> Floors;
        [Header("��")] public List<TagSprite> Walls;
    }

    [Serializable]
    public class TagSprite
    {
        public string TagName;
        public Sprite Sprite;
    }
}