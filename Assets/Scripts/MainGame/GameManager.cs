using Ex;
using IA;
using SO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        private PlayerMove _player;
        private EnemyMove _enemy;

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

            _player = Player.GetComponent<PlayerMove>();
            _enemy = Enemy.GetComponent<EnemyMove>();
        }

        void Update()
        {
            if (InputGetter.Instance.System_IsSubmit)
            {
                InteractCheck();
            }
        }

        void InteractCheck()
        {
            Vector3 pos = _player.transform.position;
            DIR dir = _player.LookingDir;

            if (pos == new Vector3(0,37,-1) && dir == DIR.UP)
            {
                // B1に行く
                _player.transform.position = new(101, 36, -1);
            }
            else if (pos == new Vector3(1, 37, -1) && dir == DIR.UP)
            {
                // B1に行く
                _player.transform.position = new(101, 36, -1);
            }
            else if (pos == new Vector3(100, 37, -1) && dir == DIR.UP)
            {
                // 1に行く
                _player.transform.position = new(1, 36, -1);
            }
            else if (pos == new Vector3(101, 37, -1) && dir == DIR.UP)
            {
                // B2に行く
                _player.transform.position = new(1, 136, -1);
            }
            else if (pos == new Vector3(0, 137, -1) && dir == DIR.UP)
            {
                // B1に行く
                _player.transform.position = new(101, 36, -1);
            }
            else if (pos == new Vector3(1, 137, -1) && dir == DIR.UP)
            {
                // B1に行く
                _player.transform.position = new(101, 36, -1);
            }
        }
    }
}
