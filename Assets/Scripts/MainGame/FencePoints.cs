using System;
using Ex;
using UnityEngine;

namespace MainGame
{
    [Serializable]
    public sealed class FencePoints
    {
        [SerializeField, Tooltip("フェンスの配置場所")]
        private SpriteRenderer[] fencePoints;

        [SerializeField, Tooltip("フェンスのスプライト")]
        private Sprite sprite;

        private bool isActive = false;

        public bool IsPath(Vector2Int pos)
        {
            foreach (var e in fencePoints)
            {
                if (e.transform.position.ToVec2I() != pos) continue;
                return !isActive;
            }

            return true;
        }

        public void Arrange() { foreach (var e in fencePoints) e.sprite = sprite; isActive = true; }
        public void Dearrange() { foreach (var e in fencePoints) e.sprite = null; isActive = false; }
    }
}
