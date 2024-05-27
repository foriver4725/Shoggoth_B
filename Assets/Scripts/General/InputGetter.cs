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
        public bool IsSubmit { get; private set; } = false;
        public bool IsCancel {  get; private set; } = false;
        public Vector2 ValueMove { get; private set; } = Vector2.zero;
        public bool IsHold { get; private set; } = false;
        #endregion

        #region【LateUpdate】毎フレームの最後で、フラグを初期化する
        void LateUpdate()
        {
            if (IsSubmit) IsSubmit = false;
            if (IsCancel) IsCancel = false;
            if (IsHold) IsHold = false;
        }
        #endregion

        #region コールバックとのリンクの詳細
        void Link(bool isLink)
        {
            // インスタンス名.Map名.Action名.コールバック名
            if (isLink)
            {
                _inputs.Test.Submit.performed += OnSubmit;

                _inputs.Test.Cancel.performed += OnCancel;

                _inputs.Test.Move.started += ReadMove;
                _inputs.Test.Move.performed += ReadMove;
                _inputs.Test.Move.canceled += ReadMove;

                _inputs.Test.Hold.performed += OnHold;
            }
            else
            {
                _inputs.Test.Submit.performed -= OnSubmit;

                _inputs.Test.Cancel.performed -= OnCancel;

                _inputs.Test.Move.started -= ReadMove;
                _inputs.Test.Move.performed -= ReadMove;
                _inputs.Test.Move.canceled -= ReadMove;

                _inputs.Test.Hold.performed -= OnHold;
            }
        }
        #endregion

        #region 処理の詳細
        void OnSubmit(InputAction.CallbackContext context)
        {
            IsSubmit = true;
        }

        void OnCancel(InputAction.CallbackContext context)
        {
            IsCancel = true;
        }

        void ReadMove(InputAction.CallbackContext context)
        {
            ValueMove = context.ReadValue<Vector2>();
        }

        void OnHold(InputAction.CallbackContext context)
        {
            IsHold = true;
        }
        #endregion
    }
}