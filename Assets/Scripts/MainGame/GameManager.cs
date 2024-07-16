using Ex;
using IA;
using SO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

            Cash();
        }
        #endregion

        [NonSerialized] public HashSet<Vector2Int> PathPositions = new();
        [NonSerialized] public List<HashSet<Vector2Int>> EnemyStokingPositions = new(); // 0が1F、2がB2F
        [NonSerialized] public GameObject Player;
        [NonSerialized] public GameObject Enemy;
        private PlayerMove _player;
        private EnemyMove _enemy;

        // アイテム取得状況(4つ集めると脱出可能)
        // 濃硝酸1つ(0)、濃塩酸3つ(1,2,3)
        [NonSerialized] public bool[] IsGetItems = new bool[4] { false, false, false, false };
        // ↑に対応するImage
        [SerializeField] private Image[] _preItemImages = new Image[4];
        // ↑の親のGameObject
        [SerializeField] private GameObject _preItemImageParent;
        // 王水のImage
        [SerializeField] private Image _ousuiImage;

        void Cash()
        {
            // 「path」タグが付いているゲームオブジェクトの座標を全て、整数座標に変換して格納する。
            GameObject[] paths = GameObject.FindGameObjectsWithTag("path");
            foreach (GameObject path in paths)
            {
                PathPositions.Add(path.transform.position.ToVec2I());
            }

            // 「type_stokingposition」タグが付いているゲームオブジェクトの座標を全て、整数座標に変換して格納する。
            HashSet<Vector2Int> enemyStokingPositions = new();
            foreach (GameObject stokingPos in GameObject.FindGameObjectsWithTag("type_stokingpoint"))
            {
                enemyStokingPositions.Add(stokingPos.transform.position.ToVec2I());
            }
            EnemyStokingPositions.Add(enemyStokingPositions);
            HashSet<Vector2Int> enemyStokingPositions1 = new();
            foreach (GameObject stokingPos in GameObject.FindGameObjectsWithTag("type_stokingpoint_1"))
            {
                enemyStokingPositions1.Add(stokingPos.transform.position.ToVec2I());
            }
            EnemyStokingPositions.Add(enemyStokingPositions1);
            HashSet<Vector2Int> enemyStokingPositions2 = new();
            foreach (GameObject stokingPos in GameObject.FindGameObjectsWithTag("type_stokingpoint_2"))
            {
                enemyStokingPositions2.Add(stokingPos.transform.position.ToVec2I());
            }
            EnemyStokingPositions.Add(enemyStokingPositions2);

            Player = GameObject.FindGameObjectWithTag("player");
            Enemy = GameObject.FindGameObjectWithTag("shoggoth");

            _player = Player.GetComponent<PlayerMove>();
            _enemy = Enemy.GetComponent<EnemyMove>();
        }

        void Start()
        {
            _ousuiImage.enabled = false;
            _preItemImageParent.SetActive(true);
            foreach (Image e in _preItemImages)
            {
                e.color = Color.black;
            }
        }

        void Update()
        {
            if (InputGetter.Instance.System_IsSubmit)
            {
                InteractCheck();
            }

            // アイテムImage達を更新
            UpdateItemImages();
        }

        void InteractCheck()
        {
            Vector3 pos = _player.transform.position;
            DIR dir = _player.LookingDir;

            if (pos == new Vector3(0, 37, -1) && dir == DIR.UP)
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

            else if (pos == new Vector3(15, 0, -1) && dir == DIR.DOWN)
            {
                CheckEscape();
            }
            else if (pos == new Vector3(16, 0, -1) && dir == DIR.DOWN)
            {
                CheckEscape();
            }
            else if (pos == new Vector3(17, 0, -1) && dir == DIR.DOWN)
            {
                CheckEscape();
            }
            else if (pos == new Vector3(18, 0, -1) && dir == DIR.DOWN)
            {
                CheckEscape();
            }
            else if (pos == new Vector3(19, 0, -1) && dir == DIR.DOWN)
            {
                CheckEscape();
            }
            else if (pos == new Vector3(20, 0, -1) && dir == DIR.DOWN)
            {
                CheckEscape();
            }
            else if (pos == new Vector3(21, 0, -1) && dir == DIR.DOWN)
            {
                CheckEscape();
            }
        }

        void CheckEscape()
        {
            // 脱出判定

            // 全てtrueなら...
            if (!All(IsGetItems, true)) return;

            // クリアの処理を行う
        }

        // アイテムImage達を更新
        void UpdateItemImages()
        {
            // 4つのアイテムをコンプしているなら
            if (All(IsGetItems, true))
            {
                _preItemImageParent.SetActive(false);
                _ousuiImage.enabled = true;
            }
            // 必要アイテムがそろっていないなら
            else
            {
                _ousuiImage.enabled = false;
                _preItemImageParent.SetActive(true);
                for (int i = 0; i < IsGetItems.Length; i++)
                {
                    _preItemImages[i].color = IsGetItems[i] ? Color.white : Color.black;
                }
            }
        }

        // listの要素が全てtargetの時のみ、trueを返す。
        private bool All(bool[] list, bool target)
        {
            foreach (bool e in list)
            {
                if (e != target)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
