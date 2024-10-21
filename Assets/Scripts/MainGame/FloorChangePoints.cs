using System;
using UnityEngine;

namespace MainGame
{
    [Serializable]
    public sealed class FloorChangePoints
    {
        [SerializeField, Tooltip("�K��ς���C���^���N�g�|�C���g")]
        private FloorChangePoint[] floorChangePoints;

        /// <summary>
        /// �C���^���N�g�\�Ȃ�toPos�Ɉړ����������
        /// </summary>
        public bool InteractCheck(PlayerMove playerMove, out Vector3 toPos)
        {
            foreach (FloorChangePoint e in floorChangePoints)
            {
                if (playerMove.IsInteractableAgainst(e.FromPos.position))
                {
                    toPos = e.ToPos.position;
                    return true;
                }
            }
            toPos = default;
            return false;
        }
    }

    [Serializable]
    public sealed class FloorChangePoint
    {
        [SerializeField, Tooltip("�C���^���N�g�n�_")]
        private Transform fromPos;
        public Transform FromPos => fromPos;

        [SerializeField, Tooltip("�ړ���")]
        private Transform toPos;
        public Transform ToPos => toPos;
    }
}
