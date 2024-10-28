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
            ItemGenerate();
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

        [SerializeField] private ItemOutlineTrigger itemOutlineTrigger;
        [SerializeField, Header("アイテムの設置候補場所\n(z座標はきらきらと同じにする)")] private ItemPoints itemPoints;
        [SerializeField] private FloorChangePoints floorChangePoints;

        [NonSerialized] public HashSet<Vector2Int> PathPositions = new();
        [NonSerialized] public List<HashSet<Vector2Int>> EnemyStokingPositions = new(); // 0が1F、2がB2F
        [NonSerialized] public GameObject Player;
        [NonSerialized] public GameObject[] Enemys = new GameObject[6];
        [NonSerialized] public PlayerMove PlayerMove;
        [NonSerialized] public EnemyMove[] EnemyMoves = new EnemyMove[6];

        [NonSerialized] public int CurrentHP; // プレイヤーのHP

        // 現在のスタミナ (0 ~ 1)
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

        // エントランスの場所
        static readonly private Vector3[] ENTRANCE_POSITIONS
            = new Vector3[7] { new(15, -1, -0.055f), new(16, -1, -0.055f), new(17, -1, -0.055f), new(18, -1, -0.055f), new(19, -1, -0.055f), new(20, -1, -0.055f), new(21, -1, -0.055f) };
        // エントランスのきらきら
        [SerializeField] private GameObject[] _entranceKirakiras = new GameObject[7];

        // 書斎の、調べられる棚の場所
        static readonly private Vector3[] CHECK_POSITIONS
            = new Vector3[4] { new(18, 104, -0.055f), new(19, 104, -0.055f), new(20, 104, -0.055f), new(21, 104, -0.055f) };
        // 書斎の棚を調べているか
        [NonSerialized] public bool IsCheckedRack = false;
        // 書斎の棚のきらきら
        [SerializeField] private GameObject[] _checkKirakiras = new GameObject[4];

        // アイテムの場所
        // 濃硝酸1つ(0)、濃塩酸3つ(1,2,3)
        private readonly Vector3[] itemPositions = new Vector3[4];
        // アイテム取得状況(4つ集めると脱出可能)
        [NonSerialized] public bool[] IsGetItems = new bool[4] { false, false, false, false };
        // アイテム取得状況のヒントをもらっているか(falseなら、光らないし取得できない)
        [NonSerialized] public bool[] IsHintedItems = new bool[4] { false, false, false, false };
        // アイテムのきらきら
        [SerializeField] private GameObject[] _itemKirakiras = new GameObject[4];
        // アイテム取得状況に対応するImage
        [SerializeField] private Image[] _preItemImages = new Image[4];
        // アイテム取得状況に対応するImageの親のGameObject
        [SerializeField] private GameObject _preItemImageParent;
        // 王水のImage
        [SerializeField] private Image _ousuiImage;

        [SerializeField] private AudioSource _onGameBGM;

        [SerializeField] AudioSource lockedDoorSE;
        [SerializeField] AudioSource potionSE;
        [SerializeField] AudioSource chaseBGM;

        private CancellationToken ct;

        void Cash()
        {
            // 「path」タグが付いているゲームオブジェクトの座標を全て、整数座標に変換して格納する。
            GameObject[] paths = GameObject.FindGameObjectsWithTag("celltype/path");
            foreach (GameObject path in paths)
            {
                PathPositions.Add(path.transform.position.ToVec2I());
            }

            // 「type_stokingposition」タグが付いているゲームオブジェクトの座標を全て、整数座標に変換して格納する。
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

        void ItemGenerate()
        {
            var pB1F = itemPoints.GetRandomPosition(1, 2);
            var pB2F = itemPoints.GetRandomPosition(2, 2);

            if (pB1F is null || pB2F is null) return;

            itemPositions[0] = pB2F[0];
            itemPositions[1] = pB2F[1];
            itemPositions[2] = pB1F[0];
            itemPositions[3] = pB1F[1];
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
            itemOutlineTrigger.SetActivation(2);

            ShowDirectionLog();
        }

        void Update()
        {
            if (InputGetter.Instance.System_IsSubmit)
            {
                InteractCheck();
            }

            // 発覚状態のBGMを更新する
            if (EnemyMoves.Map(e => e.IsOnChase).Any(true))
            {
                chaseBGM.Raise(SO_Sound.Entity.ChaseBGM, SType.BGM);
            }
            else if (EnemyMoves.Map(e => e.IsChasing).All(false))
            {
                chaseBGM.Stop();
            }

            // アイテムImage達を更新
            UpdateItemImages();

            // 階のテキストを更新
            floorText.text = (Player.transform.position.x < 75, Player.transform.position.y < 75) switch
            {
                (true, true) => "1F",
                (true, false) => "B2F",
                (false, true) => "B1F",
                _ => "B2F"
            };

            // アイテムが揃った状態で1Fにいるなら、画面を赤くする。ただし、クリアとゲームオーバー時は赤くしない。
            redImage.enabled =
                !IsClear
                && !IsOver
                && IsGetItems.All(true)
                && Player.transform.position.x < 75 && Player.transform.position.y < 75;
        }

        bool InteractCheck_IsInteractable = true;
        void InteractCheck()
        {
            if (IsClear || IsOver) return;
            if (Time.timeScale == 0) return;
            if (!InteractCheck_IsInteractable) return;

            #region インタラクトの検知
            Vector3 pos = PlayerMove.transform.position;
            DIR dir = PlayerMove.LookingDir;

            if (floorChangePoints.InteractCheck(PlayerMove, out Vector3 v))
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // 敵の発覚状態を解除する
                foreach (EnemyMove _enemy in EnemyMoves)
                {
                    _enemy.StopChaseTime = 0;
                    _enemy.IsChasing = false;
                    _enemy.SelectNewStokingPoint();
                }

                // その階に行く
                PlayerMove.transform.position = v.SetZ(-1);
                itemOutlineTrigger.SetActivation(v.x >= 100 ? 1 : v.y >= 100 ? 2 : 0);
                playerController.OnInteractedElevator();
            }
            else if (CHECK_POSITIONS.Any(e => PlayerMove.IsInteractableAgainst(e)))
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // 書斎の棚を調べる
                CheckRack();
            }
            else if (CheckItemInteract(out int idx))
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // アイテムを入手
                if (IsHintedItems[idx])
                {
                    IsGetItems[idx] = true;
                }
            }
            else
            {
                foreach (var p in ENTRANCE_POSITIONS)
                {
                    if (PlayerMove.IsInteractableAgainst(p))
                    {
                        // クールタイムが明けるまでインタラクト出来ないようにし...
                        InteractCheck_IsInteractable = false;
                        // クールタイムのカウントを開始する
                        Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                        // きらきらを非表示
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

        bool CheckItemInteract(out int idx)
        {
            for (int i = 0; i < itemPositions.Length; i++)
            {
                if (PlayerMove.IsInteractableAgainst(itemPositions[i]))
                {
                    idx = i;
                    return true;
                }
            }

            idx = -1;
            return false;
        }

        // ドアを壊したかどうか
        bool CheckEscape_IsDoorBroken = false;
        void CheckEscape()
        {
            // 必要アイテムが揃っていないなら何もしない
            if (IsGetItems.Any(false))
            {
                lockedDoorSE.Raise(SO_Sound.Entity.LockedDoorSE, SType.SE);
                return;
            }

            // 必要アイテムが揃っているなら...

            // 最初のインタラクトではドアを壊す
            if (!CheckEscape_IsDoorBroken)
            {
                CheckEscape_IsDoorBroken = true;
                potionSE.Raise(SO_Sound.Entity.UsePotionSE, SType.SE);

                textBack.enabled = false;
                textMeshProUGUI.text = "";
                finalHint.SetActive(true);
            }
            // 次のインタラクトでは脱出する
            else
            {
                if (!IsClear && !IsOver)
                {
                    IsClear = true;
                }
            }
        }

        // アイテムが揃った演出をまだしていないならfalse、しているならtrue
        bool UpdateItemImages_IsDirection = false;
        // アイテムImage達を更新
        void UpdateItemImages()
        {
            // 4つのアイテムをコンプしているなら
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

                    // きらきらを表示
                    for (int i = 0; i < _entranceKirakiras.Length; i++)
                    {
                        _entranceKirakiras[i].transform.position = ENTRANCE_POSITIONS[i];
                    }

                    // ログを表示
                    Time.timeScale = 0.0f;
                    textBack.enabled = true;
                    textMeshProUGUI.text = SO_UIConsoleText.Entity.ItemCompletedLog;
                    StartCoroutine(FadeItemCompletedLog());
                }
            }
            // 必要アイテムがそろっていないなら
            else
            {
                _ousuiImage.enabled = false;
                _preItemImageParent.SetActive(true);
                for (int i = 0; i < IsGetItems.Length; i++)
                {
                    _preItemImages[i].color = IsGetItems[i] ? Color.white : new Color32(100, 100, 100, 255);
                }
                if (IsHintedItems[0]) _itemKirakiras[0].transform.position
                        = IsGetItems[0] ? new(-100, -100, -0.055f) : itemPositions[0].SetZ(-0.055f);
                if (IsHintedItems[1]) _itemKirakiras[1].transform.position
                        = IsGetItems[1] ? new(-100, -100, -0.055f) : itemPositions[1].SetZ(-0.055f);
                if (IsHintedItems[2]) _itemKirakiras[2].transform.position
                        = IsGetItems[2] ? new(-100, -100, -0.055f) : itemPositions[2].SetZ(-0.055f);
                if (IsHintedItems[3]) _itemKirakiras[3].transform.position
                        = IsGetItems[3] ? new(-100, -100, -0.055f) : itemPositions[3].SetZ(-0.055f);
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

        // 書斎の棚を調べる
        public void CheckRack()
        {
            // 既に調べてあるなら何もしない
            if (IsCheckedRack) return;
            // もうこのメソッドの処理は行わない
            IsCheckedRack = true;

            StopCoroutine(ShowDirectionLog_Cor);
            ShowDirectionLog_Cor = null;

            // アイテムのヒントをもらっている状態にする
            IsHintedItems = IsHintedItems.Map(e => true).ToArray();

            // 書斎の棚のきらきらを非表示にする
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
