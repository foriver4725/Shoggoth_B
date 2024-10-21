using System;
using UnityEngine;

namespace MainGame
{
    [Serializable]
    public sealed class FloorChangePoints
    {
        [SerializeField, Tooltip("階を変えるインタラクトポイント")]
        private FloorChangePoint[] floorChangePoints;

        /// <summary>
        /// インタラクト可能ならtoPosに移動先を代入する
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
        [SerializeField, Tooltip("インタラクト地点")]
        private Transform fromPos;
        public Transform FromPos => fromPos;

        [SerializeField, Tooltip("移動先")]
        private Transform toPos;
        public Transform ToPos => toPos;
    }
}
