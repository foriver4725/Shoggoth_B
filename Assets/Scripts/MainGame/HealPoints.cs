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

        [SerializeField, Required, SceneObjectsOnly, Tooltip("↑と一致させること！")]
        private List<SpriteRenderer> kirakiras;

        public bool IsInteractableAgainstAny(PlayerMove playerMove, bool isRemoveFromList = true)
        {
            if (healPoints is null) return false;
            if (kirakiras is null) return false;
            if (playerMove == null) return false;

            foreach (Transform e in healPoints)
            {
                if (e == null) continue;
                if (playerMove.IsInteractableAgainst(e.position))
                {
                    int i = healPoints.IndexOf(e);
                    SpriteRenderer k = kirakiras[i];
                    if (k != null) k.transform.position = new(-100, -100, k.transform.position.z);

                    if (isRemoveFromList)
                    {
                        healPoints.Remove(e);
                        kirakiras.Remove(k);
                    }

                    return true;
                }
            }
            return false;
        }
    }
}
