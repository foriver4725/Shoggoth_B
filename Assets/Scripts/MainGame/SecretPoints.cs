using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace MainGame
{
    [Serializable]
    public sealed class SecretPoints
    {
        [SerializeField, Required, SceneObjectsOnly, Tooltip("ˆÃ‚¢•û")]
        private Transform secretDirkPoint;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("–¾‚é‚¢•û")]
        private Transform secretLightPoint;

        public bool IsInteractableAgainstSecretDirk(PlayerMove playerMove)
        {
            if (playerMove == null) return false;
            if (secretDirkPoint == null) return false;
            return playerMove.IsInteractableAgainst(secretDirkPoint.position);
        }

        public bool IsInteractableAgainstSecretLight(PlayerMove playerMove)
        {
            if (playerMove == null) return false;
            if (secretLightPoint == null) return false;
            return playerMove.IsInteractableAgainst(secretLightPoint.position);
        }
    }
}
