using Cysharp.Threading.Tasks;
using Ex;
using General;
using IA;
using SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainGame
{
    public enum EventState
    {
        Normal,
        ItemCompleted,
        BreakerDown,
        ShoggothRaise,
        LastShoggoth,
        End
    }

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

        [NonSerialized]
        public EventState EventState = EventState.Normal;

        [SerializeField] private PlayerController playerController;

        [SerializeField] Image textBack;
        [SerializeField] TextMeshProUGUI textMeshProUGUI;
        [SerializeField] TextMeshProUGUI ExplosionTimeText;

        [SerializeField] GameObject finalHint;
        [SerializeField] Image redImage;

        [SerializeField] TextMeshProUGUI floorText;

        [SerializeField] private ExtraShogghth extraShogghth;
        [SerializeField] private ItemOutlineTrigger itemOutlineTrigger;
        [SerializeField, Header("アイテムの設置候補場所\n(z座標はきらきらと同じにする)")] private ItemPoints itemPoints;
        [SerializeField] private FloorChangePoints floorChangePoints;
        [SerializeField] private BreakerPoints breakerPoints;
        [SerializeField] private FencePoints fencePoints;
        [SerializeField] private ToiletPoints toiletPoints;
        public FencePoints FencePoints => fencePoints;
        [SerializeField, Header("全てのショゴスの招集場所(x,yのみ)")] private Transform shoggothFinalPoint;

        [SerializeField] private MainToGameClear mainToGameClear;

        [NonSerialized] public HashSet<Vector2Int> PathPositions = new();
        [NonSerialized] public List<HashSet<Vector2Int>> EnemyStokingPositions = new(); // 0が1F、2がB2F
        [NonSerialized] public GameObject Player;
        [NonSerialized] public GameObject[] Enemys = new GameObject[6];
        [NonSerialized] public List<GameObject> ExtraShoggoth = new();
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

        [SerializeField] private AudioSource lockedDoorSE;
        [SerializeField] private AudioSource potionSE;
        [SerializeField] private AudioSource chaseBGM;
        [SerializeField] private AudioSource elevator1FSE;
        [SerializeField] private AudioSource elevatorB1FSE;
        [SerializeField] private AudioSource elevatorB2FSE;
        [SerializeField] private AudioSource breakerOffSE;
        [SerializeField] private AudioSource breakerOnSE;
        [SerializeField] private AudioSource ironFenceCloseSE;
        [SerializeField] private AudioSource glassBreakSE;

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

            15.0f.SecWaitAndDo(() => StartCoroutine(
                ShowLogWaitingTime(SO_UIConsoleText.Entity.ShowDirectionLog, SO_General.Entity.LogFadeDur)), destroyCancellationToken).Forget();
        }

        void Update()
        {
            if (InputGetter.Instance.SystenmSubmit.Bool)
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

            if (EventState == EventState.ItemCompleted)
            {
                if (IsGetItems.All(true) && Player.transform.position.x < 75 && Player.transform.position.y < 75)
                {
                    EventState = EventState.BreakerDown;
                    playerController.Light2D.intensity = SO_Player.Entity.LightIntensityOnBreakerOff;
                    fencePoints.Arrange();
                    StartCoroutine(ShowLogWaitingSubmit(SO_UIConsoleText.Entity.BreakerLog));
                    breakerOffSE.Raise(SO_Sound.Entity.BreakerOffSE, SType.SE);
                    0.5f.SecWaitAndDo(() => ironFenceCloseSE.Raise(SO_Sound.Entity.IronFenceCloseSE, SType.SE), destroyCancellationToken).Forget();
                }
            }
            else if (EventState == EventState.ShoggothRaise)
            {
                if (Player.transform.position.x < 75 && Player.transform.position.y < 75)
                {
                    EventState = EventState.LastShoggoth;
                    redImage.enabled = true;
                }
            }
            else if (EventState == EventState.LastShoggoth)
            {
                if (!(Player.transform.position.x < 75 && Player.transform.position.y < 75))
                {
                    EventState = EventState.ShoggothRaise;
                    redImage.enabled = false;
                }
            }

            if (InputGetter.Instance.DebugAction1.Bool) DebugTeleport(new(9, 106, -1));
            else if (InputGetter.Instance.DebugAction2.Bool) DebugTeleport(new(35, 125, -1));
            else if (InputGetter.Instance.DebugAction3.Bool) DebugTeleport(new(109, 11, -1));
            else if (InputGetter.Instance.DebugAction4.Bool) DebugTeleport(new(126, 30, -1));


            // トイレに入った実績達成
            CheckToiletEntry();
        }

        void DebugTeleport(Vector3 pos)
        {
            // 敵の発覚状態を解除する
            foreach (EnemyMove _enemy in EnemyMoves)
            {
                _enemy.StopChaseTime = 0;
                _enemy.IsChasing = false;
                _enemy.SelectNewStokingPoint();
            }

            PlayerMove.transform.position = pos;
        }

        bool InteractCheck_IsInteractable = true;
        void InteractCheck()
        {
            if (EventState == EventState.End) return;
            if (Time.timeScale == 0) return;
            if (!InteractCheck_IsInteractable) return;

            #region インタラクトの検知
            Vector3 pos = PlayerMove.transform.position;
            DIR dir = PlayerMove.LookingDir;

            if (floorChangePoints.InteractCheck(PlayerMove, out Vector3 v, out bool isElevator))
            {
                if ((EventState is EventState.BreakerDown or EventState.End) && isElevator) return;

                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                SO_General.Entity.InteractDur.SecWaitAndDo(() => InteractCheck_IsInteractable = true, ct).Forget();

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

                if (isElevator is true)
                {
                    var so = SO_Sound.Entity;
                    AudioSource source = v.x >= 100 ? elevatorB1FSE : v.y >= 100 ? elevatorB2FSE : elevator1FSE;
                    AudioClip clip = v.x >= 100 ? so.ElevatorB1FSE : v.y >= 100 ? so.ElevatorB2FSE : so.Elevator1FSE;
                    source.Raise(clip, SType.SE);
                }
            }
            else if (CHECK_POSITIONS.Any(e => PlayerMove.IsInteractableAgainst(e)))
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                SO_General.Entity.InteractDur.SecWaitAndDo(() => InteractCheck_IsInteractable = true, ct).Forget();

                // 書斎の棚を調べる
                CheckRack();
            }
            else if (CheckItemInteract(out int idx))
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                SO_General.Entity.InteractDur.SecWaitAndDo(() => InteractCheck_IsInteractable = true, ct).Forget();

                // アイテムを入手
                if (IsHintedItems[idx])
                {
                    IsGetItems[idx] = true;
                }
            }
            else if (breakerPoints.IsInteractableAgainstAny(PlayerMove))
            {
                if (EventState == EventState.BreakerDown)
                {
                    EventState = EventState.ShoggothRaise;

                    StartExplosionCount(destroyCancellationToken).Forget();

                    // 敵の発覚状態を解除して、招集する
                    foreach (EnemyMove _enemy in EnemyMoves)
                    {
                        _enemy.ChangeFloorThenDo(EnemyMove.FLOOR.F1, () =>
                        {
                            _enemy.StopChaseTime = 0;
                            _enemy.IsChasing = false;

                            Vector3 enemyPos = _enemy.transform.position;
                            enemyPos.x = shoggothFinalPoint.position.x;
                            enemyPos.y = shoggothFinalPoint.position.y;
                            _enemy.transform.position = enemyPos;

                            _enemy.SelectNewStokingPoint();
                        }, destroyCancellationToken).Forget();
                    }

                    fencePoints.Dearrange();
                    playerController.Light2D.intensity = SO_Player.Entity.LightIntensityDefault;
                    breakerOnSE.Raise(SO_Sound.Entity.BreakerOnSE, SType.SE);

                    extraShogghth.Raise();
                    0.5f.SecWaitAndDo(() => glassBreakSE.Raise(SO_Sound.Entity.GlassBreakSE, SType.SE), destroyCancellationToken).Forget();
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
                        SO_General.Entity.InteractDur.SecWaitAndDo(() => InteractCheck_IsInteractable = true, ct).Forget();

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


        private async UniTaskVoid SceneChange(CancellationToken ct)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: ct);
            SceneManager.LoadScene(SO_SceneName.Entity.Explosion);
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
            if (EventState != EventState.LastShoggoth) return;

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
                if (EventState != EventState.End)
                {
                    EventState = EventState.End;
                    mainToGameClear.Clear();
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
                    StartCoroutine(ShowLogWaitingSubmit(SO_UIConsoleText.Entity.ItemCompletedLog));

                    if (EventState == EventState.Normal) EventState = EventState.ItemCompleted;
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

        // 書斎の棚を調べる
        public void CheckRack()
        {
            // 既に調べてあるなら何もしない
            if (IsCheckedRack) return;
            // もうこのメソッドの処理は行わない
            IsCheckedRack = true;

            // アイテムのヒントをもらっている状態にする
            IsHintedItems = IsHintedItems.Map(e => true).ToArray();

            // 書斎の棚のきらきらを非表示にする
            foreach (GameObject e in _checkKirakiras)
            {
                e.transform.position = new(-100, -100, -0.055f);
            }

            StartCoroutine(ShowLogWaitingSubmit(SO_UIConsoleText.Entity.EscapeTeachLog));
        }

        private IEnumerator ShowLogWaitingTime(string text, float seconds)
        {
            textBack.enabled = true;
            textMeshProUGUI.text = text;

            yield return new WaitForSeconds(seconds);

            textMeshProUGUI.text = "";
            textBack.enabled = false;
        }

        private IEnumerator ShowLogWaitingSubmit(string text)
        {
            Time.timeScale = 0.0f;
            textBack.enabled = true;
            textMeshProUGUI.text = text;

            while (true)
            {
                yield return null;

                if (InputGetter.Instance.SystenmSubmit.Bool)
                {
                    textMeshProUGUI.text = "";
                    textBack.enabled = false;
                    Time.timeScale = 1.0f;
                    yield break;
                }
            }
        }

        private void CheckToiletEntry()
        {
            if (SaveDataHolder.Instance.SaveData.HasEnteredToilet is true) return;
            if (toiletPoints.IsInAny(Player.transform.position) is false) return;
            SaveDataHolder.Instance.SaveData.HasEnteredToilet = true;
        }

        private async UniTaskVoid StartExplosionCount(CancellationToken ct)
        {
            ExplosionTimeText.enabled = true;
            float t = SO_General.Entity.ExplosionDur;
            while (true)
            {
                await UniTask.NextFrame(ct);
                t -= Time.deltaTime;
                if (t <= 0)
                {
                    SceneManager.LoadScene(SO_SceneName.Entity.Explosion);
                    return;
                }
                ExplosionTimeText.text = ToNormalizedText(t);
            }

            static string ToNormalizedText(float second)
            {
                int intSecond = (int)second;
                int min = intSecond / 60;
                int sec = intSecond % 60;
                return $"爆発まで　{min}:{sec:00}";
            }
        }
    }
}
