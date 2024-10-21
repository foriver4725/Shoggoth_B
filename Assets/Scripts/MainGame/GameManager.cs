using Cysharp.Threading.Tasks;
using Ex;
using IA;
using SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        [SerializeField] private PlayerController playerController;

        [SerializeField] Image textBack;
        [SerializeField] TextMeshProUGUI textMeshProUGUI;

        [SerializeField] GameObject finalHint;
        [SerializeField] Image redImage;

        [SerializeField] TextMeshProUGUI floorText;

        [SerializeField, Header("�A�C�e���̐ݒu���ꏊ\n(z���W�͂��炫��Ɠ����ɂ���)")] private ItemPoints itemPoints;

        [NonSerialized] public HashSet<Vector2Int> PathPositions = new();
        [NonSerialized] public List<HashSet<Vector2Int>> EnemyStokingPositions = new(); // 0��1F�A2��B2F
        [NonSerialized] public GameObject Player;
        [NonSerialized] public GameObject[] Enemys = new GameObject[6];
        [NonSerialized] public PlayerMove PlayerMove;
        [NonSerialized] public EnemyMove[] EnemyMoves = new EnemyMove[6];

        [NonSerialized] public int CurrentHP; // �v���C���[��HP

        // ���݂̃X�^�~�i (0 ~ 1)
        private float _stamina = 1;
        public float Stamina
        {
            get
            {
                return _stamina;
            }
            set
            {
                _stamina = Mathf.Clamp(value, 0, 1);
            }
        }

        // �G���g�����X�̏ꏊ
        static readonly private Vector3[] ENTRANCE_POSITIONS
            = new Vector3[7] { new(15, -1, -0.055f), new(16, -1, -0.055f), new(17, -1, -0.055f), new(18, -1, -0.055f), new(19, -1, -0.055f), new(20, -1, -0.055f), new(21, -1, -0.055f) };
        // �G���g�����X�̂��炫��
        [SerializeField] private GameObject[] _entranceKirakiras = new GameObject[7];

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
            GameObject[] paths = GameObject.FindGameObjectsWithTag("celltype/path");
            foreach (GameObject path in paths)
            {
                PathPositions.Add(path.transform.position.ToVec2I());
            }

            // �utype_stokingposition�v�^�O���t���Ă���Q�[���I�u�W�F�N�g�̍��W��S�āA�������W�ɕϊ����Ċi�[����B
            HashSet<Vector2Int> enemyStokingPositions = new();
            foreach (GameObject stokingPos in GameObject.FindGameObjectsWithTag("stokingpoint/1F"))
            {
                enemyStokingPositions.Add(stokingPos.transform.position.ToVec2I());
            }
            EnemyStokingPositions.Add(enemyStokingPositions);
            HashSet<Vector2Int> enemyStokingPositions1 = new();
            foreach (GameObject stokingPos in GameObject.FindGameObjectsWithTag("stokingpoint/B1F"))
            {
                enemyStokingPositions1.Add(stokingPos.transform.position.ToVec2I());
            }
            EnemyStokingPositions.Add(enemyStokingPositions1);
            HashSet<Vector2Int> enemyStokingPositions2 = new();
            foreach (GameObject stokingPos in GameObject.FindGameObjectsWithTag("stokingpoint/B2F"))
            {
                enemyStokingPositions2.Add(stokingPos.transform.position.ToVec2I());
            }
            EnemyStokingPositions.Add(enemyStokingPositions2);
        }

        void Start()
        {
            ct = destroyCancellationToken;

            Player = GameObject.FindGameObjectWithTag("character/player");
            Enemys = GameObject.FindGameObjectsWithTag("character/shoggoth");

            PlayerMove = Player.GetComponent<PlayerMove>();
            for (int i = 0; i < Enemys.Length; i++)
            {
                EnemyMoves[i] = Enemys[i].GetComponent<EnemyMove>();
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

            finalHint.SetActive(false);

            ShowDirectionLog();
        }

        void Update()
        {
            if (InputGetter.Instance.System_IsSubmit)
            {
                InteractCheck();
            }

            // ���o��Ԃ�BGM���X�V����
            if (EnemyMoves.Map(e => e.IsOnChase).Any(true))
            {
                chaseBGM.Raise(SO_Sound.Entity.ChaseBGM, SType.BGM);
            }
            else if (EnemyMoves.Map(e => e.IsChasing).All(false))
            {
                chaseBGM.Stop();
            }

            // �A�C�e��Image�B���X�V
            UpdateItemImages();

            // �K�̃e�L�X�g���X�V
            floorText.text = (Player.transform.position.x < 75, Player.transform.position.y < 75) switch
            {
                (true, true) => "1F",
                (true, false) => "B2F",
                (false, true) => "B1F",
                _ => "B2F"
            };

            // �A�C�e������������Ԃ�1F�ɂ���Ȃ�A��ʂ�Ԃ�����B�������A�N���A�ƃQ�[���I�[�o�[���͐Ԃ����Ȃ��B
            redImage.enabled =
                !IsClear
                && !IsOver
                && IsGetItems.All(true)
                && Player.transform.position.x < 75 && Player.transform.position.y < 75;
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
            Vector3 pos = PlayerMove.transform.position;
            DIR dir = PlayerMove.LookingDir;

            if (pos == new Vector3(0, 37, -1) && dir == DIR.UP)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // �G�̔��o��Ԃ���������
                foreach (EnemyMove _enemy in EnemyMoves)
                {
                    _enemy.StopChaseTime = 0;
                    _enemy.IsChasing = false;
                    _enemy.SelectNewStokingPoint();
                }

                // B1�ɍs��
                PlayerMove.transform.position = new(101, 36, -1);
                playerController.OnInteractedElevator();
            }
            else if (pos == new Vector3(1, 37, -1) && dir == DIR.UP)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // �G�̔��o��Ԃ���������
                foreach (EnemyMove _enemy in EnemyMoves)
                {
                    _enemy.StopChaseTime = 0;
                    _enemy.IsChasing = false;
                    _enemy.SelectNewStokingPoint();
                }

                // B1�ɍs��
                PlayerMove.transform.position = new(101, 36, -1);
                playerController.OnInteractedElevator();
            }
            else if (pos == new Vector3(100, 37, -1) && dir == DIR.UP)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // �G�̔��o��Ԃ���������
                foreach (EnemyMove _enemy in EnemyMoves)
                {
                    _enemy.StopChaseTime = 0;
                    _enemy.IsChasing = false;
                    _enemy.SelectNewStokingPoint();
                }

                // 1�ɍs��
                PlayerMove.transform.position = new(1, 36, -1);
                playerController.OnInteractedElevator();
            }
            else if (pos == new Vector3(101, 37, -1) && dir == DIR.UP)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // �G�̔��o��Ԃ���������
                foreach (EnemyMove _enemy in EnemyMoves)
                {
                    _enemy.StopChaseTime = 0;
                    _enemy.IsChasing = false;
                    _enemy.SelectNewStokingPoint();
                }

                // B2�ɍs��
                PlayerMove.transform.position = new(1, 136, -1);
                playerController.OnInteractedElevator();
            }
            else if (pos == new Vector3(0, 137, -1) && dir == DIR.UP)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // �G�̔��o��Ԃ���������
                foreach (EnemyMove _enemy in EnemyMoves)
                {
                    _enemy.StopChaseTime = 0;
                    _enemy.IsChasing = false;
                    _enemy.SelectNewStokingPoint();
                }

                // B1�ɍs��
                PlayerMove.transform.position = new(101, 36, -1);
                playerController.OnInteractedElevator();
            }
            else if (pos == new Vector3(1, 137, -1) && dir == DIR.UP)
            {
                // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                InteractCheck_IsInteractable = false;
                // �N�[���^�C���̃J�E���g���J�n����
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // �G�̔��o��Ԃ���������
                foreach (EnemyMove _enemy in EnemyMoves)
                {
                    _enemy.StopChaseTime = 0;
                    _enemy.IsChasing = false;
                    _enemy.SelectNewStokingPoint();
                }

                // B1�ɍs��
                PlayerMove.transform.position = new(101, 36, -1);
                playerController.OnInteractedElevator();
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
            else
            {
                for (int i = 0; i < ENTRANCE_POSITIONS.Length; i++)
                {
                    if (pos == new Vector3(ENTRANCE_POSITIONS[i].x, ENTRANCE_POSITIONS[i].y, -1) + Vector3.up && dir == DIR.DOWN)
                    {
                        // �N�[���^�C����������܂ŃC���^���N�g�o���Ȃ��悤�ɂ�...
                        InteractCheck_IsInteractable = false;
                        // �N�[���^�C���̃J�E���g���J�n����
                        Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                        // ���炫����\��
                        foreach (GameObject e in _entranceKirakiras)
                        {
                            e.transform.position = new(-100, -100, -0.055f);
                        }

                        CheckEscape();

                        break;
                    }
                }
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

                textBack.enabled = false;
                textMeshProUGUI.text = "";
                finalHint.SetActive(true);
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

        // �A�C�e�������������o���܂����Ă��Ȃ��Ȃ�false�A���Ă���Ȃ�true
        bool UpdateItemImages_IsDirection = false;
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

                if (!UpdateItemImages_IsDirection)
                {
                    UpdateItemImages_IsDirection = true;

                    potionSE.Raise(SO_Sound.Entity.UsePotionSE, SType.SE);

                    // ���炫���\��
                    for (int i = 0; i < _entranceKirakiras.Length; i++)
                    {
                        _entranceKirakiras[i].transform.position = ENTRANCE_POSITIONS[i];
                    }

                    // ���O��\��
                    Time.timeScale = 0.0f;
                    textBack.enabled = true;
                    textMeshProUGUI.text = SO_UIConsoleText.Entity.ItemCompletedLog;
                    StartCoroutine(FadeItemCompletedLog());
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

        private IEnumerator FadeItemCompletedLog()
        {
            while (true)
            {
                yield return null;

                if (InputGetter.Instance.System_IsSubmit)
                {
                    textMeshProUGUI.text = "";
                    textBack.enabled = false;
                    Time.timeScale = 1.0f;
                    yield break;
                }
            }
        }

        // ���ւ̒I�𒲂ׂ�
        public void CheckRack()
        {
            // ���ɒ��ׂĂ���Ȃ牽�����Ȃ�
            if (IsCheckedRack) return;
            // �������̃��\�b�h�̏����͍s��Ȃ�
            IsCheckedRack = true;

            StopCoroutine(ShowDirectionLog_Cor);
            ShowDirectionLog_Cor = null;

            // �A�C�e���̃q���g��������Ă����Ԃɂ���
            IsHintedItems = IsHintedItems.Map(e => true).ToArray();

            // ���ւ̒I�̂��炫����\���ɂ���
            foreach (GameObject e in _checkKirakiras)
            {
                e.transform.position = new(-100, -100, -0.055f);
            }

            Time.timeScale = 0.0f;
            textBack.enabled = true;
            textMeshProUGUI.text = SO_UIConsoleText.Entity.EscapeTeachLog;

            StartCoroutine(FadeEscapeTeachLog());
        }

        private IEnumerator FadeEscapeTeachLog()
        {
            while (true)
            {
                yield return null;

                if (InputGetter.Instance.System_IsSubmit)
                {
                    textMeshProUGUI.text = "";
                    textBack.enabled = false;
                    Time.timeScale = 1.0f;
                    yield break;
                }
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
