using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainGame
{
    public class GameManager : MonoBehaviour
    {
        #region
        public static GameManager Instance { get; set; } = null;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion

        [NonSerialized] public HashSet<Vector2Int> PathPositions = new();
        [NonSerialized] public GameObject Player;
        [NonSerialized] public GameObject Enemy;

        void Start()
        {
            // 「path」タグが付いているゲームオブジェクトの座標を全て、整数座標に変換して格納する。
            GameObject[] paths = GameObject.FindGameObjectsWithTag("path");
            foreach (GameObject path in paths)
            {
                Vector3 pos = path.transform.position;
                PathPositions.Add(new Vector2Int((int)pos.x, (int)pos.y));
            }

            Player = GameObject.FindGameObjectWithTag("player");
            Enemy = GameObject.FindGameObjectWithTag("shoggoth");
        }

        void Update()
        {

        }
    }
}
