using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace MainGame
{
    /// <summary>
    /// x, yのみで判定
    /// </summary>
    [Serializable]
    public sealed class ToiletPoints
    {
        [SerializeField, Required, SceneObjectsOnly]
        private Transform[] transforms;

        public bool IsInAny(Vector3 playerPosition)
        {
            if (transforms is null) return false;
            foreach (Transform e in transforms)
            {
                if (e.position.x == playerPosition.x && e.position.y == playerPosition.y) return true;
            }
            return false;
        }
    }
}
