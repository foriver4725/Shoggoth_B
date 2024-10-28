using System;
using UnityEngine;

namespace MainGame
{
    [Serializable]
    public sealed class BreakerPoints
    {
        [SerializeField, Tooltip("ブレーカー1のインタラクトポイント")]
        private Transform breaker1;

        [SerializeField, Tooltip("ブレーカー2のインタラクトポイント")]
        private Transform breaker2;

        public bool IsInteractableAgainstAny(PlayerMove playerMove)
        => playerMove.IsInteractableAgainst(breaker1.position) || playerMove.IsInteractableAgainst(breaker2.position);
    }
}
