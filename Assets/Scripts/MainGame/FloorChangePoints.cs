using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace MainGame
{
    [Serializable]
    public sealed class FloorChangePoints
    {
        [SerializeField, Tooltip("エレベーターのインタラクトポイント"), FormerlySerializedAs("floorChangePoints")]
        private ElevatorPoints[] elevatorPoints;

        [SerializeField, Tooltip("階段のインタラクトポイント")]
        private StairPoints[] stairPoints;

        /// <summary>
        /// インタラクト可能ならtoPosに移動先を代入する
        /// </summary>
        public bool InteractCheck(PlayerMove playerMove, out Vector3 toPos, out bool isElevator)
        {
            foreach (var e in elevatorPoints)
            {
                if (playerMove.IsInteractableAgainst(e.FromPos.position))
                {
                    toPos = e.ToPos.position;
                    isElevator = true;
                    return true;
                }
            }

            foreach (var e in stairPoints)
            {
                if (playerMove.IsInteractableAgainst(e.FromPos.position))
                {
                    toPos = e.ToPos.position;
                    isElevator = false;
                    return true;
                }
            }

            toPos = default;
            isElevator = default;
            return false;
        }
    }

    [Serializable]
    public sealed class ElevatorPoints
    {
        [SerializeField, Tooltip("インタラクト地点")]
        private Transform fromPos;
        public Transform FromPos => fromPos;

        [SerializeField, Tooltip("移動先")]
        private Transform toPos;
        public Transform ToPos => toPos;
    }

    [Serializable]
    public sealed class StairPoints
    {
        [SerializeField, Tooltip("インタラクト地点")]
        private Transform fromPos;
        public Transform FromPos => fromPos;

        [SerializeField, Tooltip("移動先")]
        private Transform toPos;
        public Transform ToPos => toPos;
    }
}
