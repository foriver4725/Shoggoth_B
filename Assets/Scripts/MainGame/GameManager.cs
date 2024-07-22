using Cysharp.Threading.Tasks;
using Ex;
using IA;
using SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
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

        [NonSerialized] public bool IsClear = false;
        [NonSerialized] public bool IsOver = false;

        [SerializeField] Image textBack;
        [SerializeField] TextMeshProUGUI textMeshProUGUI;

        [SerializeField] TextMeshProUGUI floorText;

        [NonSerialized] public HashSet<Vector2Int> PathPositions = new();
        [NonSerialized] public List<HashSet<Vector2Int>> EnemyStokingPositions = new(); // 0��1F�A2��B2F
        [NonSerialized] public GameObject Player;
        [NonSerialized] public GameObject[] Enemys = new GameObject[6];
        private PlayerMove _player;
        private EnemyMove[] _enemys = new EnemyMove[6];

        // ���ւ́A���ׂ���I�̏ꏊ
        static readonly private Vector3[] CHECK_POSITIONS
            = new Vector3[4] { new(18, 104, -0.055f), new(19, 104, -0.055f), new(20, 104, -0.055f), new(21, 104, -0.055f) };
        // ���ւ̒I�𒲂ׂĂ��邩
        [NonSerialized] public bool IsCheckedRack = false;
        // ���ւ̒I�̂��炫��
        [SerializeField] private GameObject[] _checkKirakiras = new GameObject[4];

        // �A�C�e���̏ꏊ
        // �Z�Ɏ_1��(0)�A�Z���_3��(1,2,3)
        static readonly private Vector3[] ITEM__POSITIONS
            = new Vector3[4] { new(25, 110, -0.055f), new(133, 32, -0.055f), new(106, 6, -0.055f), new(35, 136, -0.055f) };
        // �A�C�e���擾��(4�W�߂�ƒE�o�\)
        [NonSerialized] public bool[] IsGetItems = new bool[4] { false, false, false, false };
        // �A�C�e���擾�󋵂̃q���g��������Ă��邩(false�Ȃ�A����Ȃ����擾�ł��Ȃ�)
        [NonSerialized] public bool[] IsHintedItems = new bool[4] { false, false, false, false };
        // �A�C�e���̂��炫��
        [SerializeField] private GameObject[] _itemKirakiras = new GameObject[4];
        // �A�C�e���擾�󋵂ɑΉ�����Image
        [SerializeField] private Image[] _preItemImages = new Image[4];
        // �A�C�e���擾�󋵂ɑΉ�����Image�̐e��GameObject
        [SerializeField] private GameObject _preItemImageParent;
        // ������Image
        [SerializeField] private Image _ousuiImage;

        [SerializeField] private AudioSource _onGameBGM;

        [SerializeField] AudioSource lockedDoorSE;
        [SerializeField] AudioSource potionSE;
        [SerializeField] AudioSource chaseBGM;

        private CancellationToken ct;

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
        }

        void Start()
        {
            ct = this.GetCancellationTokenOnDestroy();

            Player = GameObject.FindGameObjectWithTag("player");
            Enemys = GameObject.FindGameObjectsWithTag("shoggoth");

            _player = Player.GetComponent<PlayerMove>();
            for (int i = 0; i < Enemys.Length; i++)
            {
                _enemys[i] = Enemys[i].GetComponent<EnemyMove>();
            }

            _onGameBGM.Raise(SO_Sound.Entity.OnGameNormalBGM, SType.BGM);

            _ousuiImage.enabled = false;
            _preItemImageParent.SetActive(true);
            foreach (Image e in _preItemImages)
            {
                e.color = Color.black;
            }
            foreach (GameObject e in _itemKirakiras)
            {
                e.transform.position = new(-100, -100, -0.055f);
            }

            textBack.enabled = false;
            textMeshProUGUI.text = "";

            ShowDirectionLog();
        }

        void Update()
        {
            if (InputGetter.Instance.System_IsSubmit)
            {
                InteractCheck();
            }

            // ���o��Ԃ�BGM���X�V����
            if (_enemys.Map(e => e.IsOnChase).Any(true))
            {
                chaseBGM.Raise(SO_Sound.Entity.ChaseBGM, SType.BGM);
            }
            else if (_enemys.Map(e => e.IsChasing).All(false))
            {
                chaseBGM.Stop();
            }

            // �A�C�e��Image�B���X�V
            UpdateItemImages();

            // �K�̃e�L�X�g���X�V
            floorText.text = (Player.transform.position.x < 75, Player.transform.position.y < 75) switch
            {
                (true, true) => "1F",
                (true, false) => "B1F",
                (false, true) => "B2F",
                _ => "B2F"
            };
        }

        bool InteractCheck_IsInteractable = true;
        void InteractCheck()
        {
            // �N���A�܂��̓Q�[���I�[�o�[�Ȃ�C���^���N�g�ł��Ȃ�
            if (IsClear || IsOver) return;

            // �|�[�Y���Ȃ�C���^���N�g�ł��Ȃ�
            if (Time.timeScale == 0) return;

            // �C���^���N�g�s�Ȃ牽�����Ȃ�
            if (!InteractCheck_IsInteractable) return;

            #region �C���^���N�g�̌��m
            Vector3 pos = _player.transform.position;
            DIR dir = _player.LookingDir;

            if (pos == new Vector3(0, 37, -1) && dir == DIR.UP)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // �G�̔��o��Ԃ���������
                foreach (EnemyMove _enemy in _enemys)
                {
                    _enemy.StopChaseTime = 0;
                    _enemy.IsChasing = false;
                    _enemy.SelectNewStokingPoint();
                }

                // B1�ɍs��
                _player.transform.position = new(101, 36, -1);
            }
            else if (pos == new Vector3(1, 37, -1) && dir == DIR.UP)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // �G�̔��o��Ԃ���������
                foreach (EnemyMove _enemy in _enemys)
                {
                    _enemy.StopChaseTime = 0;
                    _enemy.IsChasing = false;
                    _enemy.SelectNewStokingPoint();
                }

                // B1�ɍs��
                _player.transform.position = new(101, 36, -1);
            }
            else if (pos == new Vector3(100, 37, -1) && dir == DIR.UP)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // �G�̔��o��Ԃ���������
                foreach (EnemyMove _enemy in _enemys)
                {
                    _enemy.StopChaseTime = 0;
                    _enemy.IsChasing = false;
                    _enemy.SelectNewStokingPoint();
                }

                // 1�ɍs��
                _player.transform.position = new(1, 36, -1);
            }
            else if (pos == new Vector3(101, 37, -1) && dir == DIR.UP)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // �G�̔��o��Ԃ���������
                foreach (EnemyMove _enemy in _enemys)
                {
                    _enemy.StopChaseTime = 0;
                    _enemy.IsChasing = false;
                    _enemy.SelectNewStokingPoint();
                }

                // B2�ɍs��
                _player.transform.position = new(1, 136, -1);
            }
            else if (pos == new Vector3(0, 137, -1) && dir == DIR.UP)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // �G�̔��o��Ԃ���������
                foreach (EnemyMove _enemy in _enemys)
                {
                    _enemy.StopChaseTime = 0;
                    _enemy.IsChasing = false;
                    _enemy.SelectNewStokingPoint();
                }

                // B1�ɍs��
                _player.transform.position = new(101, 36, -1);
            }
            else if (pos == new Vector3(1, 137, -1) && dir == DIR.UP)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // �G�̔��o��Ԃ���������
                foreach (EnemyMove _enemy in _enemys)
                {
                    _enemy.StopChaseTime = 0;
                    _enemy.IsChasing = false;
                    _enemy.SelectNewStokingPoint();
                }

                // B1�ɍs��
                _player.transform.position = new(101, 36, -1);
            }

            else if (pos == new Vector3(CHECK_POSITIONS[0].x, CHECK_POSITIONS[0].y, -1) + Vector3.left && dir == DIR.RIGHT)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // ���ւ̒I�𒲂ׂ�
                CheckRack();
            }
            else if (pos == new Vector3(CHECK_POSITIONS[0].x, CHECK_POSITIONS[0].y, -1) + Vector3.up && dir == DIR.DOWN)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // ���ւ̒I�𒲂ׂ�
                CheckRack();
            }
            else if (pos == new Vector3(CHECK_POSITIONS[0].x, CHECK_POSITIONS[0].y, -1) + Vector3.down && dir == DIR.UP)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // ���ւ̒I�𒲂ׂ�
                CheckRack();
            }
            else if (pos == new Vector3(CHECK_POSITIONS[1].x, CHECK_POSITIONS[1].y, -1) + Vector3.up && dir == DIR.DOWN)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // ���ւ̒I�𒲂ׂ�
                CheckRack();
            }
            else if (pos == new Vector3(CHECK_POSITIONS[1].x, CHECK_POSITIONS[1].y, -1) + Vector3.down && dir == DIR.UP)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // ���ւ̒I�𒲂ׂ�
                CheckRack();
            }
            else if (pos == new Vector3(CHECK_POSITIONS[2].x, CHECK_POSITIONS[2].y, -1) + Vector3.up && dir == DIR.DOWN)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // ���ւ̒I�𒲂ׂ�
                CheckRack();
            }
            else if (pos == new Vector3(CHECK_POSITIONS[2].x, CHECK_POSITIONS[2].y, -1) + Vector3.down && dir == DIR.UP)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // ���ւ̒I�𒲂ׂ�
                CheckRack();
            }
            else if (pos == new Vector3(CHECK_POSITIONS[3].x, CHECK_POSITIONS[3].y, -1) + Vector3.right && dir == DIR.LEFT)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // ���ւ̒I�𒲂ׂ�
                CheckRack();
            }
            else if (pos == new Vector3(CHECK_POSITIONS[3].x, CHECK_POSITIONS[3].y, -1) + Vector3.up && dir == DIR.DOWN)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // ���ւ̒I�𒲂ׂ�
                CheckRack();
            }
            else if (pos == new Vector3(CHECK_POSITIONS[3].x, CHECK_POSITIONS[3].y, -1) + Vector3.down && dir == DIR.UP)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // ���ւ̒I�𒲂ׂ�
                CheckRack();
            }

            else if (pos == new Vector3(ITEM__POSITIONS[0].x, ITEM__POSITIONS[0].y, -1) + Vector3.right && dir == DIR.LEFT)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // �A�C�e��0�����
                if (IsHintedItems[0])
                {
                    IsGetItems[0] = true;
                }
            }
            else if (pos == new Vector3(ITEM__POSITIONS[1].x, ITEM__POSITIONS[1].y, -1) + Vector3.left && dir == DIR.RIGHT)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // �A�C�e��1�����
                if (IsHintedItems[1])
                {
                    IsGetItems[1] = true;
                }
            }
            else if (pos == new Vector3(ITEM__POSITIONS[2].x, ITEM__POSITIONS[2].y, -1) + Vector3.down && dir == DIR.UP)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // �A�C�e��2�����
                if (IsHintedItems[2])
                {
                    IsGetItems[2] = true;
                }
            }
            else if (pos == new Vector3(ITEM__POSITIONS[3].x, ITEM__POSITIONS[3].y, -1) + Vector3.up && dir == DIR.DOWN)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // �A�C�e��3�����
                if (IsHintedItems[3])
                {
                    IsGetItems[3] = true;
                }
            }

            else if (pos == new Vector3(15, 0, -1) && dir == DIR.DOWN)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                CheckEscape();
            }
            else if (pos == new Vector3(16, 0, -1) && dir == DIR.DOWN)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                CheckEscape();
            }
            else if (pos == new Vector3(17, 0, -1) && dir == DIR.DOWN)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                CheckEscape();
            }
            else if (pos == new Vector3(18, 0, -1) && dir == DIR.DOWN)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                CheckEscape();
            }
            else if (pos == new Vector3(19, 0, -1) && dir == DIR.DOWN)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                CheckEscape();
            }
            else if (pos == new Vector3(20, 0, -1) && dir == DIR.DOWN)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                CheckEscape();
            }
            else if (pos == new Vector3(21, 0, -1) && dir == DIR.DOWN)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                CheckEscape();
            }
            #endregion
        }

        // �h�A���󂵂����ǂ���
        bool CheckEscape_IsDoorBroken = false;
        void CheckEscape()
        {
            // �K�v�A�C�e���������Ă��Ȃ��Ȃ牽�����Ȃ�
            if (IsGetItems.Any(false))
            {
                lockedDoorSE.Raise(SO_Sound.Entity.LockedDoorSE, SType.SE);
                return;
            }

            // �K�v�A�C�e���������Ă���Ȃ�...

            // �ŏ��̃C���^���N�g�ł̓h�A����
            if (!CheckEscape_IsDoorBroken)
            {
                CheckEscape_IsDoorBroken = true;
                potionSE.Raise(SO_Sound.Entity.UsePotionSE, SType.SE);
            }
            // ���̃C���^���N�g�ł͒E�o����
            else
            {
                if (!IsClear && !IsOver)
                {
                    IsClear = true;
                }
            }
        }

        // �A�C�e��Image�B���X�V
        void UpdateItemImages()
        {
            // 4�̃A�C�e�����R���v���Ă���Ȃ�
            if (IsGetItems.All(true))
            {
                _preItemImageParent.SetActive(false);
                _ousuiImage.enabled = true;
                foreach (GameObject e in _itemKirakiras)
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
                if (IsHintedItems[0]) _itemKirakiras[0].transform.position
                        = IsGetItems[0] ? new(-100, -100, -0.055f) : ITEM__POSITIONS[0];
                if (IsHintedItems[1]) _itemKirakiras[1].transform.position
                        = IsGetItems[1] ? new(-100, -100, -0.055f) : ITEM__POSITIONS[1];
                if (IsHintedItems[2]) _itemKirakiras[2].transform.position
                        = IsGetItems[2] ? new(-100, -100, -0.055f) : ITEM__POSITIONS[2];
                if (IsHintedItems[3]) _itemKirakiras[3].transform.position
                        = IsGetItems[3] ? new(-100, -100, -0.055f) : ITEM__POSITIONS[3];
            }
        }



        // ���ւ̒I�𒲂ׂ�
        public void CheckRack()
        {
            // ���ɒ��ׂĂ���Ȃ牽�����Ȃ�
            if (IsCheckedRack) return;

            StopCoroutine(ShowDirectionLog_Cor);
            ShowDirectionLog_Cor = null;

            textBack.enabled = true;
            textMeshProUGUI.text = SO_UIConsoleText.Entity.EscapeTeachLog;

            // �������̃��\�b�h�̏����͍s��Ȃ�
            IsCheckedRack = true;

            FadeLog();
            // �A�C�e���̃q���g��������Ă����Ԃɂ���
            IsHintedItems[0] = true;
            IsHintedItems[1] = true;
            IsHintedItems[2] = true;
            IsHintedItems[3] = true;

            // ���ւ̒I�̂��炫����\���ɂ���
            foreach (GameObject e in _checkKirakiras)
            {
                e.transform.position = new(-100, -100, -0.055f);
            }
        }

        Coroutine ShowDirectionLog_Cor = null;
        void ShowDirectionLog()
        {
            ShowDirectionLog_Cor = StartCoroutine(ShowDirectionLogCor());
        }
        IEnumerator ShowDirectionLogCor()
        {
            yield return new WaitForSeconds(15);
            textBack.enabled = true;
            textMeshProUGUI.text = SO_UIConsoleText.Entity.ShowDirectionLog;
            FadeLog();
        }

        Coroutine FadeLog_Cor = null;
        public void FadeLog()
        {
            if (FadeLog_Cor != null)
            {
                StopCoroutine(FadeLog_Cor);
                FadeLog_Cor = null;
            }

            FadeLog_Cor = StartCoroutine(ResetLog());
        }

        IEnumerator ResetLog()
        {
            yield return new WaitForSeconds(SO_General.Entity.LogFadeDur);
            textBack.enabled = false;
            textMeshProUGUI.text = "";
        }
    }
}
