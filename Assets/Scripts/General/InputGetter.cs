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
        public Vector2 MainGame_ValueMove { get; private set; } = Vector2.zero;
        public bool MainGame_IsDash { get; private set; } = false;
        public bool MainGame_IsUseItem { get; private set; } = false;
        public int MainGame_ValueScrollItem { get; private set; } = 0;
        public bool MainGame_IsPause { get; private set; } = false;
        public bool MainGame_IsUp { get; private set; } = false;
        public bool MainGame_IsDown { get; private set; } = false;

        public bool System_IsSubmit { get; private set; } = false;
        public bool System_IsCancel { get; private set; } = false;
        public bool System_IsCredit { get; set; } = false;
        #endregion

        #region【LateUpdate】毎フレームの最後で、フラグを初期化する
        void LateUpdate()
        {
            if (MainGame_IsUseItem) MainGame_IsUseItem = false;
            if (MainGame_IsPause) MainGame_IsPause = false;
            if (MainGame_IsUp) MainGame_IsUp = false;
            if (MainGame_IsDown) MainGame_IsDown = false;

            if (System_IsSubmit) System_IsSubmit = false;
            if (System_IsCancel) System_IsCancel = false;
            if (System_IsCredit) System_IsCredit = false;
        }
        #endregion

        #region コールバックとのリンクの詳細
        void Link(bool isLink)
        {
            // インスタンス名.Map名.Action名.コールバック名
            if (isLink)
            {
                _inputs.MainGame.Move.started += MainGame_ReadMove;
                _inputs.MainGame.Move.performed += MainGame_ReadMove;
                _inputs.MainGame.Move.canceled += MainGame_ReadMove;
                _inputs.MainGame.Dash.performed += MainGame_OnDashDown;
                _inputs.MainGame.Dash.canceled += MainGame_OnDashUp;
                _inputs.MainGame.UseItem.performed += MainGame_OnUseItem;
                _inputs.MainGame.ScrollItem.started += MainGame_ReadScrollItem;
                _inputs.MainGame.ScrollItem.performed += MainGame_ReadScrollItem;
                _inputs.MainGame.ScrollItem.canceled += MainGame_ReadScrollItem;
                _inputs.MainGame.Pause.performed += MainGame_OnPause;
                _inputs.MainGame.Up.performed += MainGame_OnUp;
                _inputs.MainGame.Down.performed += MainGame_OnDown;

                _inputs.System.Submit.performed += System_OnSubmit;
                _inputs.System.Cancel.performed += System_OnCancel;
                _inputs.System.Credit.performed += System_OnCredit;
            }
            else
            {
                _inputs.MainGame.Move.started -= MainGame_ReadMove;
                _inputs.MainGame.Move.performed -= MainGame_ReadMove;
                _inputs.MainGame.Move.canceled -= MainGame_ReadMove;
                _inputs.MainGame.Dash.performed -= MainGame_OnDashDown;
                _inputs.MainGame.Dash.canceled -= MainGame_OnDashUp;
                _inputs.MainGame.UseItem.performed -= MainGame_OnUseItem;
                _inputs.MainGame.ScrollItem.started -= MainGame_ReadScrollItem;
                _inputs.MainGame.ScrollItem.performed -= MainGame_ReadScrollItem;
                _inputs.MainGame.ScrollItem.canceled -= MainGame_ReadScrollItem;
                _inputs.MainGame.Pause.performed -= MainGame_OnPause;
                _inputs.MainGame.Up.performed -= MainGame_OnUp;
                _inputs.MainGame.Down.performed -= MainGame_OnDown;

                _inputs.System.Submit.performed -= System_OnSubmit;
                _inputs.System.Cancel.performed -= System_OnCancel;
                _inputs.System.Credit.performed -= System_OnCredit;
            }
        }
        #endregion

        #region 処理の詳細
        void MainGame_ReadMove(InputAction.CallbackContext context) { MainGame_ValueMove = context.ReadValue<Vector2>(); }
        void MainGame_OnDashDown(InputAction.CallbackContext context) { MainGame_IsDash = true; }
        void MainGame_OnDashUp(InputAction.CallbackContext context) { MainGame_IsDash = false; }
        void MainGame_OnUseItem(InputAction.CallbackContext context) { MainGame_IsUseItem = true; }
        void MainGame_ReadScrollItem(InputAction.CallbackContext context)
        {
            float val = context.ReadValue<float>();
            if (val > 0f) MainGame_ValueScrollItem = 1;
            else if (val < 0f) MainGame_ValueScrollItem = -1;
            else MainGame_ValueScrollItem = 0;
        }
        void MainGame_OnPause(InputAction.CallbackContext context) { MainGame_IsPause = true; }
        void MainGame_OnUp(InputAction.CallbackContext context) { MainGame_IsUp = true; }
        void MainGame_OnDown(InputAction.CallbackContext context) { MainGame_IsDown = true; }

        void System_OnSubmit(InputAction.CallbackContext context) { System_IsSubmit = true; }
        void System_OnCancel(InputAction.CallbackContext context) { System_IsCancel = true; }
        void System_OnCredit(InputAction.CallbackContext context) { System_IsCredit = true; }
        #endregion
    }
}