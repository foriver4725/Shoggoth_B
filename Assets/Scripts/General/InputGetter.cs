using UnityEngine;
using UnityEngine.InputSystem;

namespace IA
{
    public class InputGetter : MonoBehaviour
    {
        #region �C���X�^���X�̊Ǘ��A�R�[���o�b�N�Ƃ̃����N�Astatic���V���O���g���ɂ���
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

        #region �ϐ��錾
        public bool IsSubmit { get; private set; } = false;
        public bool IsCancel {  get; private set; } = false;
        public Vector2 ValueMove { get; private set; } = Vector2.zero;
        public bool IsHold { get; private set; } = false;
        #endregion

        #region�yLateUpdate�z���t���[���̍Ō�ŁA�t���O������������
        void LateUpdate()
        {
            if (IsSubmit) IsSubmit = false;
            if (IsCancel) IsCancel = false;
            if (IsHold) IsHold = false;
        }
        #endregion

        #region �R�[���o�b�N�Ƃ̃����N�̏ڍ�
        void Link(bool isLink)
        {
            // �C���X�^���X��.Map��.Action��.�R�[���o�b�N��
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

        #region �����̏ڍ�
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