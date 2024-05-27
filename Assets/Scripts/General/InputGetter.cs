using UnityEngine;
using UnityEngine.InputSystem;

namespace IA
{
    public class InputGetter : MonoBehaviour
    {
        #region インスタンスの管理、コールバックとのリンク、staticかつシングルトンにする
        IA _inputs;

        public static InputGetter Instance { get; set; } = null;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            _inputs = new IA();

            Link(true);
        }

        void OnDestroy()
        {
            Link(false);

            _inputs.Dispose();
        }

        void OnEnable()
        {
            _inputs.Enable();
        }

        void OnDisable()
        {
            _inputs.Disable();
        }
        #endregion

        #region 変数宣言
        public bool Title_Istart { get; private set; } = false;
        public bool Title_IsQuit { get; private set; } = false;

        public Vector2 MainGame_ValueMove { get; private set; } = Vector2.zero;
        public bool MainGame_IsDash { get; private set; } = false;
        public bool MainGame_IsUseItem { get; private set; } = false;
        public bool MainGame_IsPause { get; private set; } = false;
        public bool MainGame_IsMenu { get; private set; } = false;
        public int MainGame_ValueScrollItem { get; private set; } = 0;
        public bool MainGame_IsPickUp { get; private set; } = false;
        public bool MainGame_IsRead { get; private set; } = false;
        public bool MainGame_IsOpenDoor { get; private set; } = false;
        public bool MainGame_IsHide { get; private set; } = false;
        public bool MainGame_IsTalk { get; private set; } = false;
        public bool MainGame_IsGimmick { get; private set; } = false;

        public bool Menu_IsSubmit { get; private set; } = false;
        public bool Menu_IsCancel { get; private set; } = false;
        #endregion

        #region【LateUpdate】毎フレームの最後で、フラグを初期化する
        void LateUpdate()
        {
            if (Title_Istart) Title_Istart = false;
            if (Title_IsQuit) Title_IsQuit = false;

            if (MainGame_IsDash) MainGame_IsDash = false;
            if (MainGame_IsUseItem) MainGame_IsUseItem = false;
            if (MainGame_IsPause) MainGame_IsPause = false;
            if (MainGame_IsMenu) MainGame_IsMenu = false;
            if (MainGame_IsPickUp) MainGame_IsPickUp = false;
            if (MainGame_IsRead) MainGame_IsRead = false;
            if (MainGame_IsOpenDoor) MainGame_IsOpenDoor = false;
            if (MainGame_IsHide) MainGame_IsHide = false;
            if (MainGame_IsTalk) MainGame_IsTalk = false;
            if (MainGame_IsGimmick) MainGame_IsGimmick = false;

            if (Menu_IsSubmit) Menu_IsSubmit = false;
            if (Menu_IsCancel) Menu_IsCancel = false;
        }
        #endregion

        #region コールバックとのリンクの詳細
        void Link(bool isLink)
        {
            // インスタンス名.Map名.Action名.コールバック名
            if (isLink)
            {
                _inputs.Title.Start.performed += Title_OnStart;
                _inputs.Title.Quit.performed += Title_OnQuit;

                _inputs.MainGame.Move.started += MainGame_ReadMove;
                _inputs.MainGame.Move.performed += MainGame_ReadMove;
                _inputs.MainGame.Move.canceled += MainGame_ReadMove;
                _inputs.MainGame.Dash.performed += MainGame_OnDash;
                _inputs.MainGame.UseItem.performed += MainGame_OnUseItem;
                _inputs.MainGame.Pause.performed += MainGame_OnPause;
                _inputs.MainGame.Menu.performed += MainGame_OnMenu;
                _inputs.MainGame.ScrollItem.started += MainGame_ReadScrollItem;
                _inputs.MainGame.ScrollItem.performed += MainGame_ReadScrollItem;
                _inputs.MainGame.ScrollItem.canceled += MainGame_ReadScrollItem;
                _inputs.MainGame.PickUp.performed += MainGame_OnPickUp;
                _inputs.MainGame.Read.performed += MainGame_OnRead;
                _inputs.MainGame.OpenDoor.performed += MainGame_OnOpenDoor;
                _inputs.MainGame.Hide.performed += MainGame_OnHide;
                _inputs.MainGame.Talk.performed += MainGame_OnTalk;
                _inputs.MainGame.Gimmick.performed += MainGame_OnGimmick;

                _inputs.Menu.Submit.performed += Menu_OnSubmit;
                _inputs.Menu.Cancel.performed += Menu_OnCancel;

            }
            else
            {
                _inputs.Title.Start.performed -= Menu_OnSubmit;
                _inputs.Title.Quit.performed -= Menu_OnCancel;

                _inputs.MainGame.Move.started -= MainGame_ReadMove;
                _inputs.MainGame.Move.performed -= MainGame_ReadMove;
                _inputs.MainGame.Move.canceled -= MainGame_ReadMove;
                _inputs.MainGame.Dash.performed -= MainGame_OnDash;
                _inputs.MainGame.UseItem.performed -= MainGame_OnUseItem;
                _inputs.MainGame.Pause.performed -= MainGame_OnPause;
                _inputs.MainGame.Menu.performed -= MainGame_OnMenu;
                _inputs.MainGame.ScrollItem.started -= MainGame_ReadScrollItem;
                _inputs.MainGame.ScrollItem.performed -= MainGame_ReadScrollItem;
                _inputs.MainGame.ScrollItem.canceled -= MainGame_ReadScrollItem;
                _inputs.MainGame.PickUp.performed -= MainGame_OnPickUp;
                _inputs.MainGame.Read.performed -= MainGame_OnRead;
                _inputs.MainGame.OpenDoor.performed -= MainGame_OnOpenDoor;
                _inputs.MainGame.Hide.performed -= MainGame_OnHide;
                _inputs.MainGame.Talk.performed -= MainGame_OnTalk;
                _inputs.MainGame.Gimmick.performed -= MainGame_OnGimmick;

                _inputs.Menu.Submit.performed -= Menu_OnSubmit;
                _inputs.Menu.Cancel.performed -= Menu_OnCancel;
            }
        }
        #endregion

        #region 処理の詳細
        void Title_OnStart(InputAction.CallbackContext context) { Title_Istart = true; }
        void Title_OnQuit(InputAction.CallbackContext context) { Title_IsQuit = true; }

        void MainGame_ReadMove(InputAction.CallbackContext context) { MainGame_ValueMove = context.ReadValue<Vector2>(); }
        void MainGame_OnDash(InputAction.CallbackContext context) { MainGame_IsDash = true; }
        void MainGame_OnUseItem(InputAction.CallbackContext context) { MainGame_IsUseItem = true; }
        void MainGame_OnPause(InputAction.CallbackContext context) { MainGame_IsPause = true; }
        void MainGame_OnMenu(InputAction.CallbackContext context) { MainGame_IsMenu = true; }
        void MainGame_ReadScrollItem(InputAction.CallbackContext context)
        {
            float val = context.ReadValue<float>();
            if (val > 0f) MainGame_ValueScrollItem = 1;
            else if (val < 0f) MainGame_ValueScrollItem = -1;
            else MainGame_ValueScrollItem = 0;
        }
        void MainGame_OnPickUp(InputAction.CallbackContext context) { MainGame_IsPickUp = true; }
        void MainGame_OnRead(InputAction.CallbackContext context) { MainGame_IsRead = true; }
        void MainGame_OnOpenDoor(InputAction.CallbackContext context) { MainGame_IsOpenDoor = true; }
        void MainGame_OnHide(InputAction.CallbackContext context) { MainGame_IsHide = true; }
        void MainGame_OnTalk(InputAction.CallbackContext context) { MainGame_IsTalk = true; }
        void MainGame_OnGimmick(InputAction.CallbackContext context) { MainGame_IsGimmick = true; }

        void Menu_OnSubmit(InputAction.CallbackContext context) { Menu_IsSubmit = true; }
        void Menu_OnCancel(InputAction.CallbackContext context) { Menu_IsCancel = true; }
        #endregion
    }
}