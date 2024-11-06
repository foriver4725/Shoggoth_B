using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MainGame
{
    [Serializable]
    public sealed class ExtraShogghth
    {
        [SerializeField, Required, SceneObjectsOnly, Header("エクストラショゴスの配置場所一覧(座標を読み取るだけ)")]
        private Transform[] arrangePositions;

        [SerializeField, Required, SceneObjectsOnly, Header("出てくる壁のSprite Renderer一覧")]
        private SpriteRenderer[] wallSpriteRenderers;

        [SerializeField, Required, AssetsOnly, LabelText("壊れた壁のスプライト")]
        private Sprite brokenWallSprite;

        [SerializeField, Required, AssetsOnly, LabelText("プレハブ"), Tooltip("この中からランダム")]
        private GameObject[] prefabs;

        [SerializeField, Required, SceneObjectsOnly, LabelText("生成後の親")]
        private Transform parent;

        private bool isDone = false;

        // 1回きり
        public void Raise()
        {
            if (isDone) return;
            isDone = true;

            CreateEnemies();
            BreakWalls();
        }

        private void CreateEnemies()
        {
            if (arrangePositions is null) return;
            if (prefabs is null) return;
            if (parent == null) return;
            if (prefabs.Length <= 0) return;

            foreach (Transform tf in arrangePositions)
            {
                if (tf == null) continue;

                Vector3 pos = new(tf.position.x, tf.position.y, 0);
                GameObject prefab = prefabs[UnityEngine.Random.Range(0, prefabs.Length)];
                if (prefab == null) continue;
                GameObject go = GameObject.Instantiate(prefab, pos, Quaternion.identity, parent);
                GameManager.Instance.ExtraShoggoth.Add(go);
            }
        }

        private void BreakWalls()
        {
            if (wallSpriteRenderers is null) return;
            if (brokenWallSprite == null) return;

            foreach (SpriteRenderer sr in wallSpriteRenderers)
            {
                if (sr == null) continue;

                // 壊れた壁にする
                sr.sprite = brokenWallSprite;

                // 剥製を消す
                try
                {
                    SpriteRenderer srChild = sr.transform.GetChild(3).GetComponent<SpriteRenderer>();
                    srChild.enabled = false;
                }
                catch (Exception) { continue; }
            }
        }
    }
}