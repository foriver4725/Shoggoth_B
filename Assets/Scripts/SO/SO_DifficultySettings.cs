using System;
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
        private static SO_DifficultySettings _entity = null;
        public static SO_DifficultySettings Entity
        {
            get
            {
                // ���A�N�Z�X���Ƀ��[�h����
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_DifficultySettings>(PATH);

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


        [SerializeField, Header("��Փx�ݒ�\nE, N, H, N")]
        private Difficulty[] difficulty;

        public int VisibilityRange => difficulty[Difficulty.Type.ToInt()].VisibilityRange;
        public float StaminaRecover => difficulty[Difficulty.Type.ToInt()].StaminaRecover;
        public bool IsItemRandom => difficulty[Difficulty.Type.ToInt()].IsItemRandom;
    }

    [Serializable]
    public class Difficulty
    {
        [Header("���E�͈�")] public int VisibilityRange;
        [Header("�X�^�~�i�񕜑��x")] public float StaminaRecover;
        [Header("�A�C�e���z�u�����_����")] public bool IsItemRandom;

        public static DifficultyType Type = DifficultyType.Normal;
    }

    public enum DifficultyType
    {
        Easy,
        Normal,
        Hard,
        Nightmare
    }

    public static class DifficultyEx
    {
        public static int ToInt(this DifficultyType type) => type switch
        {
            DifficultyType.Easy => 0,
            DifficultyType.Normal => 1,
            DifficultyType.Hard => 2,
            DifficultyType.Nightmare => 3,
            _ => 1,
        };
    }
}