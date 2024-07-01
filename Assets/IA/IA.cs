//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/IA/IA.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace IA
{
    public partial class @IA: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @IA()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""IA"",
    ""maps"": [
        {
            ""name"": ""System"",
            ""id"": ""0e2336ca-122c-48d2-bb5b-f87ef7a988c8"",
            ""actions"": [
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""a956d90e-288b-42be-82ef-db29f30091ee"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""1016d3ae-2743-40c8-adbd-d2b586796e6b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7e904c8b-468e-4d88-a43e-e79dc86f14f0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""898730ed-d84c-4825-a38a-bde3b7f2ad2a"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""MainGame"",
            ""id"": ""beb74ce0-c711-40c8-aaac-d222efcb51fe"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""ee0551b9-13d0-4b03-896d-31fa2ca8d3fc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""d69dec3e-0587-4881-9d05-47df49db3902"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""UseItem"",
                    ""type"": ""Button"",
                    ""id"": ""318da8f9-0b84-48b9-8994-1fa9db534472"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ScrollItem"",
                    ""type"": ""Value"",
                    ""id"": ""bfb2fb8e-e651-4a1a-99c2-344f9bbdd66d"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""4377bbed-336e-44b1-9c23-20735cb20ae8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""88ffc70b-883d-406e-82e8-6a25f41c99cd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""5f3e0d2f-5bd0-4451-9a28-d3ae3df8bcb8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""411ca761-426f-407b-9e83-010aa757f3ce"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d268bc1f-c718-4921-add5-7c48c76b7873"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b94358b2-d196-4f9a-a277-aa4a883a0630"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4fc5b83f-27ec-4000-814a-f018c8aab0a4"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0a151047-2b5b-4895-bdc4-886a2bf10ffa"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""00322cdb-726c-416f-b58f-d576af6bd4cd"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a547ba2d-395d-4387-84d7-5d8dd69b195c"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""48cc50e2-4d3a-4334-a6d4-4e12287ba7e0"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""63aa1f60-efc6-4958-a224-54023d32d252"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2cf44a09-3017-4369-ab34-5a92abe66d5c"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""8ffceb0c-3646-4735-9efa-4ba4e2c64ad3"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""243b3a26-73e4-4e9d-8f50-9c7501fa34db"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""58702007-a759-4070-9a2c-b51a8d10cac6"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2ee55f1e-4f01-4f1a-95fd-4d789f1dbe92"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f4de4f3d-5948-4c28-922c-c66240465ee8"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""0f561b1f-ade9-409b-8552-a38c7cc0f9ec"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82cba658-9faa-4169-95ac-eabc775fe170"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""667316b7-7413-40b4-8d26-4662b3a9331f"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ae098c40-5cfb-4ca3-af17-e4459f02ce91"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""49bc7cd5-1504-484d-a313-3cb50a7e3869"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""35570110-2890-4410-9383-2aeb846bf12a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollItem"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""2bdb6c76-a268-4d53-a23c-b664a535621f"",
                    ""path"": ""<Mouse>/scroll/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e032136e-4309-4a56-96ec-ffff660a37f9"",
                    ""path"": ""<Mouse>/scroll/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""58dc4cdd-085d-4ac5-a402-364c08004b20"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5d8b161b-d925-4392-8044-f4542d34704b"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // System
            m_System = asset.FindActionMap("System", throwIfNotFound: true);
            m_System_Submit = m_System.FindAction("Submit", throwIfNotFound: true);
            m_System_Cancel = m_System.FindAction("Cancel", throwIfNotFound: true);
            // MainGame
            m_MainGame = asset.FindActionMap("MainGame", throwIfNotFound: true);
            m_MainGame_Move = m_MainGame.FindAction("Move", throwIfNotFound: true);
            m_MainGame_Dash = m_MainGame.FindAction("Dash", throwIfNotFound: true);
            m_MainGame_UseItem = m_MainGame.FindAction("UseItem", throwIfNotFound: true);
            m_MainGame_ScrollItem = m_MainGame.FindAction("ScrollItem", throwIfNotFound: true);
            m_MainGame_Pause = m_MainGame.FindAction("Pause", throwIfNotFound: true);
            m_MainGame_Up = m_MainGame.FindAction("Up", throwIfNotFound: true);
            m_MainGame_Down = m_MainGame.FindAction("Down", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }

        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // System
        private readonly InputActionMap m_System;
        private List<ISystemActions> m_SystemActionsCallbackInterfaces = new List<ISystemActions>();
        private readonly InputAction m_System_Submit;
        private readonly InputAction m_System_Cancel;
        public struct SystemActions
        {
            private @IA m_Wrapper;
            public SystemActions(@IA wrapper) { m_Wrapper = wrapper; }
            public InputAction @Submit => m_Wrapper.m_System_Submit;
            public InputAction @Cancel => m_Wrapper.m_System_Cancel;
            public InputActionMap Get() { return m_Wrapper.m_System; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(SystemActions set) { return set.Get(); }
            public void AddCallbacks(ISystemActions instance)
            {
                if (instance == null || m_Wrapper.m_SystemActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_SystemActionsCallbackInterfaces.Add(instance);
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
            }

            private void UnregisterCallbacks(ISystemActions instance)
            {
                @Submit.started -= instance.OnSubmit;
                @Submit.performed -= instance.OnSubmit;
                @Submit.canceled -= instance.OnSubmit;
                @Cancel.started -= instance.OnCancel;
                @Cancel.performed -= instance.OnCancel;
                @Cancel.canceled -= instance.OnCancel;
            }

            public void RemoveCallbacks(ISystemActions instance)
            {
                if (m_Wrapper.m_SystemActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(ISystemActions instance)
            {
                foreach (var item in m_Wrapper.m_SystemActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_SystemActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public SystemActions @System => new SystemActions(this);

        // MainGame
        private readonly InputActionMap m_MainGame;
        private List<IMainGameActions> m_MainGameActionsCallbackInterfaces = new List<IMainGameActions>();
        private readonly InputAction m_MainGame_Move;
        private readonly InputAction m_MainGame_Dash;
        private readonly InputAction m_MainGame_UseItem;
        private readonly InputAction m_MainGame_ScrollItem;
        private readonly InputAction m_MainGame_Pause;
        private readonly InputAction m_MainGame_Up;
        private readonly InputAction m_MainGame_Down;
        public struct MainGameActions
        {
            private @IA m_Wrapper;
            public MainGameActions(@IA wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_MainGame_Move;
            public InputAction @Dash => m_Wrapper.m_MainGame_Dash;
            public InputAction @UseItem => m_Wrapper.m_MainGame_UseItem;
            public InputAction @ScrollItem => m_Wrapper.m_MainGame_ScrollItem;
            public InputAction @Pause => m_Wrapper.m_MainGame_Pause;
            public InputAction @Up => m_Wrapper.m_MainGame_Up;
            public InputAction @Down => m_Wrapper.m_MainGame_Down;
            public InputActionMap Get() { return m_Wrapper.m_MainGame; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MainGameActions set) { return set.Get(); }
            public void AddCallbacks(IMainGameActions instance)
            {
                if (instance == null || m_Wrapper.m_MainGameActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_MainGameActionsCallbackInterfaces.Add(instance);
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @UseItem.started += instance.OnUseItem;
                @UseItem.performed += instance.OnUseItem;
                @UseItem.canceled += instance.OnUseItem;
                @ScrollItem.started += instance.OnScrollItem;
                @ScrollItem.performed += instance.OnScrollItem;
                @ScrollItem.canceled += instance.OnScrollItem;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
            }

            private void UnregisterCallbacks(IMainGameActions instance)
            {
                @Move.started -= instance.OnMove;
                @Move.performed -= instance.OnMove;
                @Move.canceled -= instance.OnMove;
                @Dash.started -= instance.OnDash;
                @Dash.performed -= instance.OnDash;
                @Dash.canceled -= instance.OnDash;
                @UseItem.started -= instance.OnUseItem;
                @UseItem.performed -= instance.OnUseItem;
                @UseItem.canceled -= instance.OnUseItem;
                @ScrollItem.started -= instance.OnScrollItem;
                @ScrollItem.performed -= instance.OnScrollItem;
                @ScrollItem.canceled -= instance.OnScrollItem;
                @Pause.started -= instance.OnPause;
                @Pause.performed -= instance.OnPause;
                @Pause.canceled -= instance.OnPause;
                @Up.started -= instance.OnUp;
                @Up.performed -= instance.OnUp;
                @Up.canceled -= instance.OnUp;
                @Down.started -= instance.OnDown;
                @Down.performed -= instance.OnDown;
                @Down.canceled -= instance.OnDown;
            }

            public void RemoveCallbacks(IMainGameActions instance)
            {
                if (m_Wrapper.m_MainGameActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IMainGameActions instance)
            {
                foreach (var item in m_Wrapper.m_MainGameActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_MainGameActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public MainGameActions @MainGame => new MainGameActions(this);
        public interface ISystemActions
        {
            void OnSubmit(InputAction.CallbackContext context);
            void OnCancel(InputAction.CallbackContext context);
        }
        public interface IMainGameActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnDash(InputAction.CallbackContext context);
            void OnUseItem(InputAction.CallbackContext context);
            void OnScrollItem(InputAction.CallbackContext context);
            void OnPause(InputAction.CallbackContext context);
            void OnUp(InputAction.CallbackContext context);
            void OnDown(InputAction.CallbackContext context);
        }
    }
}
