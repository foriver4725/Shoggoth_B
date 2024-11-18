using System;
using Ex;
using Sirenix.OdinInspector;
using SO;
using UnityEngine;
using UnityEngine.UI;

namespace MainGame
{
    [Serializable]
    public sealed class AquaPoints
    {
        [SerializeField, Required, SceneObjectsOnly]
        private Transform[] points;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("↑と一致させること！")]
        private SpriteRenderer[] spriteRenderers;

        [SerializeField, Required, SceneObjectsOnly]
        private Image fishImage;

        [SerializeField, Required, AssetsOnly]
        private Sprite brokenAquaImage;

        private bool hasInteracted = false;

        public bool CheckInteract(PlayerMove playerMove)
        {
            if (hasInteracted is true) return false;
            if (points is null) return false;
            if (spriteRenderers is null) return false;
            if (fishImage == null) return false;
            if (brokenAquaImage == null) return false;

            foreach (Transform p in points)
            {
                if (p == null) continue;

                if (playerMove.IsInteractableAgainst(p.position))
                {
                    hasInteracted = true;

                    GameManager.Instance.AquaGlassBreakSE.Raise(SO_Sound.Entity.GlassBreakSE, SType.SE);

                    fishImage.enabled = true;

                    foreach (SpriteRenderer sr in spriteRenderers)
                    {
                        if (sr == null) continue;
                        sr.sprite = brokenAquaImage;
                    }

                    return true;
                }
            }

            return false;
        }
    }
}