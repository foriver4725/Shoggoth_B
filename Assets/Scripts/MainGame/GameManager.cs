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
using static UnityEngine.EventSystems.EventTrigger;

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

        public TextMeshProUGUI textMeshProUGUI;

        [NonSerialized] public HashSet<Vector2Int> PathPositions = new();
        [NonSerialized] public List<HashSet<Vector2Int>> EnemyStokingPositions = new(); // 0が1F、2がB2F
        [NonSerialized] public GameObject Player;
        [NonSerialized] public GameObject[] Enemys = new GameObject[6];
        private PlayerMove _player;
        private EnemyMove[] _enemys = new EnemyMove[6];

        // 書斎の、調べられる棚の場所
        static readonly private Vector3[] CHECK__POSITIONS
            = new Vector3[4] { new(18, 104, -0.055f), new(19, 104, -0.055f), new(20, 104, -0.055f), new(21, 104, -0.055f) };
        // 書斎の棚を調べているか
        [NonSerialized] public bool IsCheckedRack = false;

        // アイテムの場所
        // 濃硝酸1つ(0)、濃塩酸3つ(1,2,3)
        static readonly private Vector3[] ITEM__POSITIONS
            = new Vector3[4] { new(25, 110, -0.055f), new(133, 32, -0.055f), new(106, 6, -0.055f), new(35, 136, -0.055f) };
        // アイテム取得状況(4つ集めると脱出可能)
        [NonSerialized] public bool[] IsGetItems = new bool[4] { false, false, false, false };
        // アイテム取得状況のヒントをもらっているか(falseなら、光らないし取得できない)
        [NonSerialized] public bool[] IsHintedItems = new bool[4] { false, false, false, false };
        // アイテムのきらきら
        [SerializeField] private GameObject[] _kirakiras = new GameObject[4];
        // アイテム取得状況に対応するImage
        [SerializeField] private Image[] _preItemImages = new Image[4];
        // アイテム取得状況に対応するImageの親のGameObject
        [SerializeField] private GameObject _preItemImageParent;
        // 王水のImage
        [SerializeField] private Image _ousuiImage;

        [SerializeField] private AudioSource _onGameBGM;

        [SerializeField] AudioSource potionSE;

        private CancellationToken ct;

        

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

            // アイテムImage達を更新
            UpdateItemImages();
        }

        bool InteractCheck_IsInteractable = true;
        void InteractCheck()
        {
            // インタラクト不可なら何もしない
            if (!InteractCheck_IsInteractable) return;

            #region インタラクトの検知
            Vector3 pos = _player.transform.position;
            DIR dir = _player.LookingDir;

            if (pos == new Vector3(0, 37, -1) && dir == DIR.UP)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // 敵の発覚状態を解除する
                foreach (EnemyMove _enemy in _enemys)
                {
                    _enemy.StopChaseTime = 0;
                    _enemy.IsChasing = false;
                    _enemy.ChaseAS.Stop();
                    _enemy.SelectNewStokingPoint();
                }

                // B1に行く
                MapMoveB1F();
                _player.transform.position = new(101, 36, -1);
            }
            else if (pos == new Vector3(1, 37, -1) && dir == DIR.UP)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // 敵の発覚状態を解除する
                foreach (EnemyMove _enemy in _enemys)
                {
                    _enemy.StopChaseTime = 0;
                    _enemy.IsChasing = false;
                    _enemy.ChaseAS.Stop();
                    _enemy.SelectNewStokingPoint();
                }

                // B1に行く
                MapMoveB1F();
                _player.transform.position = new(101, 36, -1);
            }
            else if (pos == new Vector3(100, 37, -1) && dir == DIR.UP)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // 敵の発覚状態を解除する
                foreach (EnemyMove _enemy in _enemys)
                {
                    _enemy.StopChaseTime = 0;
                    _enemy.IsChasing = false;
                    _enemy.ChaseAS.Stop();
                    _enemy.SelectNewStokingPoint();
                }

                // 1に行く
                MapMove1F();
                _player.transform.position = new(1, 36, -1);
            }
            else if (pos == new Vector3(101, 37, -1) && dir == DIR.UP)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // 敵の発覚状態を解除する
                foreach (EnemyMove _enemy in _enemys)
                {
                    _enemy.StopChaseTime = 0;
                    _enemy.IsChasing = false;
                    _enemy.ChaseAS.Stop();
                    _enemy.SelectNewStokingPoint();
                }

                // B2に行く
                MapMoveB2F();
                _player.transform.position = new(1, 136, -1);
            }
            else if (pos == new Vector3(0, 137, -1) && dir == DIR.UP)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // 敵の発覚状態を解除する
                foreach (EnemyMove _enemy in _enemys)
                {
                    _enemy.StopChaseTime = 0;
                    _enemy.IsChasing = false;
                    _enemy.ChaseAS.Stop();
                    _enemy.SelectNewStokingPoint();
                }

                // B1に行く
                MapMoveB1F();
                _player.transform.position = new(101, 36, -1);
            }
            else if (pos == new Vector3(1, 137, -1) && dir == DIR.UP)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // 敵の発覚状態を解除する
                foreach (EnemyMove _enemy in _enemys)
                {
                    _enemy.StopChaseTime = 0;
                    _enemy.IsChasing = false;
                    _enemy.ChaseAS.Stop();
                    _enemy.SelectNewStokingPoint();
                }

                // B1に行く
                MapMoveB1F();
                _player.transform.position = new(101, 36, -1);
            }

            else if (pos == new Vector3(CHECK__POSITIONS[0].x, CHECK__POSITIONS[0].y, -1) + Vector3.left && dir == DIR.RIGHT)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // 書斎の棚を調べる
                CheckRack();
            }
            else if (pos == new Vector3(CHECK__POSITIONS[0].x, CHECK__POSITIONS[0].y, -1) + Vector3.up && dir == DIR.DOWN)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // 書斎の棚を調べる
                CheckRack();
            }
            else if (pos == new Vector3(CHECK__POSITIONS[0].x, CHECK__POSITIONS[0].y, -1) + Vector3.down && dir == DIR.UP)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // 書斎の棚を調べる
                CheckRack();
            }
            else if (pos == new Vector3(CHECK__POSITIONS[1].x, CHECK__POSITIONS[1].y, -1) + Vector3.up && dir == DIR.DOWN)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // 書斎の棚を調べる
                CheckRack();
            }
            else if (pos == new Vector3(CHECK__POSITIONS[1].x, CHECK__POSITIONS[1].y, -1) + Vector3.down && dir == DIR.UP)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // 書斎の棚を調べる
                CheckRack();
            }
            else if (pos == new Vector3(CHECK__POSITIONS[2].x, CHECK__POSITIONS[2].y, -1) + Vector3.up && dir == DIR.DOWN)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // 書斎の棚を調べる
                CheckRack();
            }
            else if (pos == new Vector3(CHECK__POSITIONS[2].x, CHECK__POSITIONS[2].y, -1) + Vector3.down && dir == DIR.UP)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // 書斎の棚を調べる
                CheckRack();
            }
            else if (pos == new Vector3(CHECK__POSITIONS[3].x, CHECK__POSITIONS[3].y, -1) + Vector3.right && dir == DIR.LEFT)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // 書斎の棚を調べる
                CheckRack();
            }
            else if (pos == new Vector3(CHECK__POSITIONS[3].x, CHECK__POSITIONS[3].y, -1) + Vector3.up && dir == DIR.DOWN)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // 書斎の棚を調べる
                CheckRack();
            }
            else if (pos == new Vector3(CHECK__POSITIONS[3].x, CHECK__POSITIONS[3].y, -1) + Vector3.down && dir == DIR.UP)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // 書斎の棚を調べる
                CheckRack();
            }

            else if (pos == new Vector3(ITEM__POSITIONS[0].x, ITEM__POSITIONS[0].y, -1) + Vector3.right && dir == DIR.LEFT)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // アイテム0を入手
                EscapeIndex4();
                if (IsHintedItems[0]) IsGetItems[0] = true;
            }
            else if (pos == new Vector3(ITEM__POSITIONS[1].x, ITEM__POSITIONS[1].y, -1) + Vector3.left && dir == DIR.RIGHT)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // アイテム1を入手
                EscapeIndex4();
                if (IsHintedItems[1]) IsGetItems[1] = true;
            }
            else if (pos == new Vector3(ITEM__POSITIONS[2].x, ITEM__POSITIONS[2].y, -1) + Vector3.down && dir == DIR.UP)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // アイテム2を入手
                EscapeIndex4();
                if (IsHintedItems[2]) IsGetItems[2] = true;
            }
            else if (pos == new Vector3(ITEM__POSITIONS[3].x, ITEM__POSITIONS[3].y, -1) + Vector3.up && dir == DIR.DOWN)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                // アイテム3を入手
                EscapeIndex4();
                if (IsHintedItems[3]) IsGetItems[3] = true;
            }

            else if (pos == new Vector3(15, 0, -1) && dir == DIR.DOWN)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                CheckEscape();
            }
            else if (pos == new Vector3(16, 0, -1) && dir == DIR.DOWN)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                CheckEscape();
            }
            else if (pos == new Vector3(17, 0, -1) && dir == DIR.DOWN)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                CheckEscape();
            }
            else if (pos == new Vector3(18, 0, -1) && dir == DIR.DOWN)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                CheckEscape();
            }
            else if (pos == new Vector3(19, 0, -1) && dir == DIR.DOWN)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                CheckEscape();
            }
            else if (pos == new Vector3(20, 0, -1) && dir == DIR.DOWN)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                CheckEscape();
            }
            else if (pos == new Vector3(21, 0, -1) && dir == DIR.DOWN)
            {
                // クールタイムが明けるまでインタラクト出来ないようにし...
                InteractCheck_IsInteractable = false;
                // クールタイムのカウントを開始する
                Async.AfterWaited(() => InteractCheck_IsInteractable = true, SO_General.Entity.InteractDur, ct).Forget();

                CheckEscape();
            }
            #endregion
        }

        // ドアを壊したかどうか
        bool CheckEscape_IsDoorBroken = false;
        void CheckEscape()
        {
            // 必要アイテムが揃っていないなら何もしない
            if (!All(IsGetItems, true))
            {
                LockDoor();
                return;
            }

                // 必要アイテムが揃っているなら...

                // 最初のインタラクトではドアを壊す
                if (!CheckEscape_IsDoorBroken)
            {
                CheckEscape_IsDoorBroken = true;
                potionSE.Raise(SO_Sound.Entity.UsePotionSE, SType.SE);
                BreakDoor();
            }
            // 次のインタラクトでは脱出する
            else
            {
                Debug.Log("脱出！");
            }
        }

        // アイテムImage達を更新
        void UpdateItemImages()
        {
            // 4つのアイテムをコンプしているなら
            if (All(IsGetItems, true))
            {
                _preItemImageParent.SetActive(false);
                _ousuiImage.enabled = true;
                foreach (GameObject e in _kirakiras)
                {
                    e.transform.position = new(-100, -100, -0.055f);
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

        public void ShoggothLook()
        {
            textMeshProUGUI.text = SO_UIConsoleText.Entity.ShoggothLog[0];
            ResetUIText();
        }
        public void ShoggothEscape()
        {
            textMeshProUGUI.text = SO_UIConsoleText.Entity.ShoggothLog[1];
            ResetUIText();
        }
        public void ShoggothDamage()
        {
            textMeshProUGUI.text = SO_UIConsoleText.Entity.ShoggothLog[2];
            ResetUIText();
        }
        public void MapMove1F()
        {
            textMeshProUGUI.text = SO_UIConsoleText.Entity.MapLog[0];
            ResetUIText();
        }
        public void MapMoveB1F()
        {
            textMeshProUGUI.text = SO_UIConsoleText.Entity.MapLog[1];
            ResetUIText();
        }
        public void MapMoveB2F()
        {
            textMeshProUGUI.text = SO_UIConsoleText.Entity.MapLog[2];
            ResetUIText();
        }
        public void EscapeIndex()
        {
            textMeshProUGUI.text = SO_UIConsoleText.Entity.IndexLog[0];
            ResetUIText();
        }
        public void EscapeIndex2()
        {
            textMeshProUGUI.text = SO_UIConsoleText.Entity.IndexLog[1];
            ResetUIText();
        }
        public void EscapeIndex3()
        {
            textMeshProUGUI.text = SO_UIConsoleText.Entity.IndexLog[2];
            ResetUIText();
        }
        public void EscapeIndex4()
        {
            textMeshProUGUI.text = SO_UIConsoleText.Entity.IndexLog[3];
            ResetUIText();
        }
        public void LockDoor()
        {
            textMeshProUGUI.text = SO_UIConsoleText.Entity.IndexLog[4];
            ResetUIText();
        }
        public void BreakDoor()
        {
            textMeshProUGUI.text = SO_UIConsoleText.Entity.IndexLog[5];
            ResetUIText();
        }
        public void IndexShoggoth()
        {
            textMeshProUGUI.text = SO_UIConsoleText.Entity.IndexLog[8];
            ResetUIText();
        }
        public void IndexShoggoth2()
        {
            textMeshProUGUI.text = SO_UIConsoleText.Entity.IndexLog[9];
            ResetUIText();
        }

        // 書斎の棚を調べる
        public void CheckRack()
        {

            // 既に調べてあるなら何もしない
            if (IsCheckedRack) return;



            // 諸々の処理をここに書く...

           
            textMeshProUGUI.text = SO_UIConsoleText.Entity.IndexLog[0];
            textMeshProUGUI.text = SO_UIConsoleText.Entity.IndexLog[1];
            textMeshProUGUI.text = SO_UIConsoleText.Entity.IndexLog[2];



            // もうこのメソッドの処理は行わない
            IsCheckedRack = true;
            ResetUIText();
            // アイテムのヒントをもらっている状態にする
            IsHintedItems[0] = true;
            IsHintedItems[1] = true;
            IsHintedItems[2] = true;
            IsHintedItems[3] = true;
        }

        public void ResetUIText()
        {


               // textMeshProUGUI.text = "メッセージログ";

            
        }
    }
}
