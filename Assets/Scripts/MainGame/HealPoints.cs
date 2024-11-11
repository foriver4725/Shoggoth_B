using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MainGame
{
    [Serializable]
    public sealed class HealPoints
    {
        [SerializeField, Required, SceneObjectsOnly]
        private List<Transform> healPoints;

        public bool IsInteractableAgainstAny(PlayerMove playerMove, bool isRemoveFromList = true)
        {
            if (healPoints is null) return false;
            if (playerMove == null) return false;

            foreach (Transform e in healPoints)
            {
                if (e == null) continue;
                if (playerMove.IsInteractableAgainst(e.position))
                {
                    if (isRemoveFromList) healPoints.Remove(e);
                    return true;
                }
            }
            return false;
        }
    }
}
