using Ex;
using IA;
using SO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

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
        [NonSerialized] public List<HashSet<Vector2Int>> EnemyStokingPositions = new(); // 0��1F�A2��B2F
        [NonSerialized] public GameObject Player;
        [NonSerialized] public GameObject Enemy;
        private PlayerMove _player;
        private EnemyMove _enemy;

        // �A�C�e���̏ꏊ
        // �Z�Ɏ_1��(0)�A�Z���_3��(1,2,3)
        static readonly private Vector3[] ITEM__POSITIONS
            = new Vector3[4] { new(25, 110, -0.055f), new(133, 32, -0.055f), new(106, 6, -0.055f), new(35, 136, -0.055f) };
        // �A�C�e���擾��(4�W�߂�ƒE�o�\)
        [NonSerialized] public bool[] IsGetItems = new bool[4] { false, false, false, false };
        // �A�C�e���擾�󋵂̃q���g��������Ă��邩(false�Ȃ�A����Ȃ����擾�ł��Ȃ�)
        [NonSerialized] public bool[] IsHintedItems = new bool[4] { false, false, false, false };
        // �A�C�e���̂��炫��
        [SerializeField] private GameObject[] _kirakiras = new GameObject[4];
        // �A�C�e���擾�󋵂ɑΉ�����Image
        [SerializeField] private Image[] _preItemImages = new Image[4];
        // �A�C�e���擾�󋵂ɑΉ�����Image�̐e��GameObject
        [SerializeField] private GameObject _preItemImageParent;
        // ������Image
        [SerializeField] private Image _ousuiImage;

        void Cash()
        {
            // �upath�v�^�O���t���Ă���Q�[���I�u�W�F�N�g�̍��W��S�āA�������W�ɕϊ����Ċi�[����B
            GameObject[] paths = GameObject.FindGameObjectsWithTag("path");
            foreach (GameObject path in paths)
            {
                PathPositions.Add(path.transform.position.ToVec2I());
            }

            // �utype_stokingposition�v�^�O���t���Ă���Q�[���I�u�W�F�N�g�̍��W��S�āA�������W�ɕϊ����Ċi�[����B
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
            foreach (GameObject e in _kirakiras)
            {
                e.transform.position = new(-100, -100, -0.055f);
            }
        }

        void Update()
        {
            if (InputGetter.Instance.System_IsSubmit)
            {
                InteractCheck();
            }

            // �A�C�e��Image�B���X�V
            UpdateItemImages();
        }

        void InteractCheck()
        {
            Vector3 pos = _player.transform.position;
            DIR dir = _player.LookingDir;

            if (pos == new Vector3(0, 37, -1) && dir == DIR.UP)
            {
                // B1�ɍs��
                _player.transform.position = new(101, 36, -1);
            }
            else if (pos == new Vector3(1, 37, -1) && dir == DIR.UP)
            {
                // B1�ɍs��
                _player.transform.position = new(101, 36, -1);
            }
            else if (pos == new Vector3(100, 37, -1) && dir == DIR.UP)
            {
                // 1�ɍs��
                _player.transform.position = new(1, 36, -1);
            }
            else if (pos == new Vector3(101, 37, -1) && dir == DIR.UP)
            {
                // B2�ɍs��
                _player.transform.position = new(1, 136, -1);
            }
            else if (pos == new Vector3(0, 137, -1) && dir == DIR.UP)
            {
                // B1�ɍs��
                _player.transform.position = new(101, 36, -1);
            }
            else if (pos == new Vector3(1, 137, -1) && dir == DIR.UP)
            {
                // B1�ɍs��
                _player.transform.position = new(101, 36, -1);
            }

            else if (pos == new Vector3(ITEM__POSITIONS[0].x, ITEM__POSITIONS[0].y, -1) + Vector3.right && dir == DIR.LEFT)
            {
                // �A�C�e��0�����
                if (IsHintedItems[0]) IsGetItems[0] = true;
            }
            else if (pos == new Vector3(ITEM__POSITIONS[1].x, ITEM__POSITIONS[1].y, -1) + Vector3.left && dir == DIR.RIGHT)
            {
                // �A�C�e��1�����
                if (IsHintedItems[1]) IsGetItems[1] = true;
            }
            else if (pos == new Vector3(ITEM__POSITIONS[2].x, ITEM__POSITIONS[2].y, -1) + Vector3.down && dir == DIR.UP)
            {
                // �A�C�e��2�����
                if (IsHintedItems[2]) IsGetItems[2] = true;
            }
            else if (pos == new Vector3(ITEM__POSITIONS[3].x, ITEM__POSITIONS[3].y, -1) + Vector3.up && dir == DIR.DOWN)
            {
                // �A�C�e��3�����
                if (IsHintedItems[3]) IsGetItems[3] = true;
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
            // �E�o����

            // �S��true�Ȃ�...
            if (!All(IsGetItems, true)) return;

            // �N���A�̏������s��
            Debug.Log("Clear!!!");
        }

        // �A�C�e��Image�B���X�V
        void UpdateItemImages()
        {
            // 4�̃A�C�e�����R���v���Ă���Ȃ�
            if (All(IsGetItems, true))
            {
                _preItemImageParent.SetActive(false);
                _ousuiImage.enabled = true;
                foreach (GameObject e in _kirakiras)
                {
                    e.transform.position = new(-100, -100, -0.055f);
                }
            }
            // �K�v�A�C�e����������Ă��Ȃ��Ȃ�
            else
            {
                _ousuiImage.enabled = false;
                _preItemImageParent.SetActive(true);
                for (int i = 0; i < IsGetItems.Length; i++)
                {
                    _preItemImages[i].color = IsGetItems[i] ? Color.white : new Color32(100, 100, 100, 255);
                }
                if (IsHintedItems[0]) _kirakiras[0].transform.position
                        = IsGetItems[0] ? new(-100, -100, -0.055f) : ITEM__POSITIONS[0];
                if (IsHintedItems[1]) _kirakiras[1].transform.position
                        = IsGetItems[1] ? new(-100, -100, -0.055f) : ITEM__POSITIONS[1];
                if (IsHintedItems[2]) _kirakiras[2].transform.position
                        = IsGetItems[2] ? new(-100, -100, -0.055f) : ITEM__POSITIONS[2];
                if (IsHintedItems[3]) _kirakiras[3].transform.position
                        = IsGetItems[3] ? new(-100, -100, -0.055f) : ITEM__POSITIONS[3];
            }
        }

        // list�̗v�f���S��target�̎��̂݁Atrue��Ԃ��B
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
