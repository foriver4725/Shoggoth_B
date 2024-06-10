using Ex;
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
        [NonSerialized] public HashSet<Vector2Int> EnemyStokingPositions = new();
        [NonSerialized] public GameObject Player;
        [NonSerialized] public GameObject Enemy;

        void Start()
        {
            // 「path」タグが付いているゲームオブジェクトの座標を全て、整数座標に変換して格納する。
            GameObject[] paths = GameObject.FindGameObjectsWithTag("path");
            foreach (GameObject path in paths)
            {
                PathPositions.Add(path.transform.position.ToVec2I());
            }

            // 「type_stokingposition」タグが付いているゲームオブジェクトの座標を全て、整数座標に変換して格納する。
            GameObject[] stokingPoses = GameObject.FindGameObjectsWithTag("type_stokingpoint");
            foreach (GameObject stokingPos in stokingPoses)
            {
                EnemyStokingPositions.Add(stokingPos.transform.position.ToVec2I());
            }

            Player = GameObject.FindGameObjectWithTag("player");
            Enemy = GameObject.FindGameObjectWithTag("shoggoth");
        }

        void Update()
        {

        }
    }
}
