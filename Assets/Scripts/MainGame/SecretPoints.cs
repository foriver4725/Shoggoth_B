using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace MainGame
{
    [Serializable]
    public sealed class SecretPoints
    {
        [SerializeField, Required, SceneObjectsOnly, Tooltip("�Â���")]
        private Transform secretDirkPoint;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("���邢��")]
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
