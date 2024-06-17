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
            ""name"": ""Title"",
            ""id"": ""39449f89-3da2-4942-9a04-c091e17d7aa5"",
            ""actions"": [
                {
                    ""name"": ""Start"",
                    ""type"": ""Button"",
                    ""id"": ""f4fc855e-429d-4b89-866c-9cabc9a20ab9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Quit"",
                    ""type"": ""Button"",
                    ""id"": ""9d7826b8-4d4d-4df9-b86a-5099b514d6a1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4b4c306d-b868-4207-a7ce-0af28a8f344c"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""286950fd-4f34-48b9-bbf5-ac01cb30ba1f"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
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
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""4377bbed-336e-44b1-9c23-20735cb20ae8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""4575d88f-751e-4997-a620-a7c06e22456d"",
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
                    ""name"": ""PickUp"",
                    ""type"": ""Button"",
                    ""id"": ""521b9fbf-1a92-41e7-8ed2-83ef2d937b2e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Read"",
                    ""type"": ""Button"",
                    ""id"": ""e4cb1275-953c-47ba-a69e-6c45e48262de"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenDoor"",
                    ""type"": ""Button"",
                    ""id"": ""b1f48bdb-a5ec-48e8-8bc2-4402ed888be4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Hide"",
                    ""type"": ""Button"",
                    ""id"": ""48aa3f28-dc2d-4cf5-980b-98c547fa8cf2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Talk"",
                    ""type"": ""Button"",
                    ""id"": ""aa233f2e-e101-4cb5-9101-4c3614635a38"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Gimmick"",
                    ""type"": ""Button"",
                    ""id"": ""00e8afd3-289d-4955-827f-586872ce2b26"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=2)"",
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
                    ""name"": """",
                    ""id"": ""60dbda46-27bd-44c3-a4b2-fc1a4fbf54c5"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
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
                    ""id"": ""8d46b831-9705-45ff-86da-59947092efc2"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PickUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8116aa6f-c109-4aa2-ae13-84a58809aeb0"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Read"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a6d60063-fc30-4388-a1bd-4534b29f8abd"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenDoor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd1f4d07-1c0c-4318-9726-485aada94bf0"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7a35e541-1813-43df-82f1-66b42bbf5e18"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Talk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""71b57588-ef12-4c36-8718-64d9b5d490ce"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Gimmick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
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
        }
    ],
    ""controlSchemes"": []
}");
            // Title
            m_Title = asset.FindActionMap("Title", throwIfNotFound: true);
            m_Title_Start = m_Title.FindAction("Start", throwIfNotFound: true);
            m_Title_Quit = m_Title.FindAction("Quit", throwIfNotFound: true);
            // MainGame
            m_MainGame = asset.FindActionMap("MainGame", throwIfNotFound: true);
            m_MainGame_Move = m_MainGame.FindAction("Move", throwIfNotFound: true);
            m_MainGame_Dash = m_MainGame.FindAction("Dash", throwIfNotFound: true);
            m_MainGame_UseItem = m_MainGame.FindAction("UseItem", throwIfNotFound: true);
            m_MainGame_Pause = m_MainGame.FindAction("Pause", throwIfNotFound: true);
            m_MainGame_Menu = m_MainGame.FindAction("Menu", throwIfNotFound: true);
            m_MainGame_ScrollItem = m_MainGame.FindAction("ScrollItem", throwIfNotFound: true);
            m_MainGame_PickUp = m_MainGame.FindAction("PickUp", throwIfNotFound: true);
            m_MainGame_Read = m_MainGame.FindAction("Read", throwIfNotFound: true);
            m_MainGame_OpenDoor = m_MainGame.FindAction("OpenDoor", throwIfNotFound: true);
            m_MainGame_Hide = m_MainGame.FindAction("Hide", throwIfNotFound: true);
            m_MainGame_Talk = m_MainGame.FindAction("Talk", throwIfNotFound: true);
            m_MainGame_Gimmick = m_MainGame.FindAction("Gimmick", throwIfNotFound: true);
            // Menu
            m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
            m_Menu_Submit = m_Menu.FindAction("Submit", throwIfNotFound: true);
            m_Menu_Cancel = m_Menu.FindAction("Cancel", throwIfNotFound: true);
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

        // Title
        private readonly InputActionMap m_Title;
        private List<ITitleActions> m_TitleActionsCallbackInterfaces = new List<ITitleActions>();
        private readonly InputAction m_Title_Start;
        private readonly InputAction m_Title_Quit;
        public struct TitleActions
        {
            private @IA m_Wrapper;
            public TitleActions(@IA wrapper) { m_Wrapper = wrapper; }
            public InputAction @Start => m_Wrapper.m_Title_Start;
            public InputAction @Quit => m_Wrapper.m_Title_Quit;
            public InputActionMap Get() { return m_Wrapper.m_Title; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(TitleActions set) { return set.Get(); }
            public void AddCallbacks(ITitleActions instance)
            {
                if (instance == null || m_Wrapper.m_TitleActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_TitleActionsCallbackInterfaces.Add(instance);
                @Start.started += instance.OnStart;
                @Start.performed += instance.OnStart;
                @Start.canceled += instance.OnStart;
                @Quit.started += instance.OnQuit;
                @Quit.performed += instance.OnQuit;
                @Quit.canceled += instance.OnQuit;
            }

            private void UnregisterCallbacks(ITitleActions instance)
            {
                @Start.started -= instance.OnStart;
                @Start.performed -= instance.OnStart;
                @Start.canceled -= instance.OnStart;
                @Quit.started -= instance.OnQuit;
                @Quit.performed -= instance.OnQuit;
                @Quit.canceled -= instance.OnQuit;
            }

            public void RemoveCallbacks(ITitleActions instance)
            {
                if (m_Wrapper.m_TitleActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(ITitleActions instance)
            {
                foreach (var item in m_Wrapper.m_TitleActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_TitleActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public TitleActions @Title => new TitleActions(this);

        // MainGame
        private readonly InputActionMap m_MainGame;
        private List<IMainGameActions> m_MainGameActionsCallbackInterfaces = new List<IMainGameActions>();
        private readonly InputAction m_MainGame_Move;
        private readonly InputAction m_MainGame_Dash;
        private readonly InputAction m_MainGame_UseItem;
        private readonly InputAction m_MainGame_Pause;
        private readonly InputAction m_MainGame_Menu;
        private readonly InputAction m_MainGame_ScrollItem;
        private readonly InputAction m_MainGame_PickUp;
        private readonly InputAction m_MainGame_Read;
        private readonly InputAction m_MainGame_OpenDoor;
        private readonly InputAction m_MainGame_Hide;
        private readonly InputAction m_MainGame_Talk;
        private readonly InputAction m_MainGame_Gimmick;
        public struct MainGameActions
        {
            private @IA m_Wrapper;
            public MainGameActions(@IA wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_MainGame_Move;
            public InputAction @Dash => m_Wrapper.m_MainGame_Dash;
            public InputAction @UseItem => m_Wrapper.m_MainGame_UseItem;
            public InputAction @Pause => m_Wrapper.m_MainGame_Pause;
            public InputAction @Menu => m_Wrapper.m_MainGame_Menu;
            public InputAction @ScrollItem => m_Wrapper.m_MainGame_ScrollItem;
            public InputAction @PickUp => m_Wrapper.m_MainGame_PickUp;
            public InputAction @Read => m_Wrapper.m_MainGame_Read;
            public InputAction @OpenDoor => m_Wrapper.m_MainGame_OpenDoor;
            public InputAction @Hide => m_Wrapper.m_MainGame_Hide;
            public InputAction @Talk => m_Wrapper.m_MainGame_Talk;
            public InputAction @Gimmick => m_Wrapper.m_MainGame_Gimmick;
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
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
                @ScrollItem.started += instance.OnScrollItem;
                @ScrollItem.performed += instance.OnScrollItem;
                @ScrollItem.canceled += instance.OnScrollItem;
                @PickUp.started += instance.OnPickUp;
                @PickUp.performed += instance.OnPickUp;
                @PickUp.canceled += instance.OnPickUp;
                @Read.started += instance.OnRead;
                @Read.performed += instance.OnRead;
                @Read.canceled += instance.OnRead;
                @OpenDoor.started += instance.OnOpenDoor;
                @OpenDoor.performed += instance.OnOpenDoor;
                @OpenDoor.canceled += instance.OnOpenDoor;
                @Hide.started += instance.OnHide;
                @Hide.performed += instance.OnHide;
                @Hide.canceled += instance.OnHide;
                @Talk.started += instance.OnTalk;
                @Talk.performed += instance.OnTalk;
                @Talk.canceled += instance.OnTalk;
                @Gimmick.started += instance.OnGimmick;
                @Gimmick.performed += instance.OnGimmick;
                @Gimmick.canceled += instance.OnGimmick;
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
                @Pause.started -= instance.OnPause;
                @Pause.performed -= instance.OnPause;
                @Pause.canceled -= instance.OnPause;
                @Menu.started -= instance.OnMenu;
                @Menu.performed -= instance.OnMenu;
                @Menu.canceled -= instance.OnMenu;
                @ScrollItem.started -= instance.OnScrollItem;
                @ScrollItem.performed -= instance.OnScrollItem;
                @ScrollItem.canceled -= instance.OnScrollItem;
                @PickUp.started -= instance.OnPickUp;
                @PickUp.performed -= instance.OnPickUp;
                @PickUp.canceled -= instance.OnPickUp;
                @Read.started -= instance.OnRead;
                @Read.performed -= instance.OnRead;
                @Read.canceled -= instance.OnRead;
                @OpenDoor.started -= instance.OnOpenDoor;
                @OpenDoor.performed -= instance.OnOpenDoor;
                @OpenDoor.canceled -= instance.OnOpenDoor;
                @Hide.started -= instance.OnHide;
                @Hide.performed -= instance.OnHide;
                @Hide.canceled -= instance.OnHide;
                @Talk.started -= instance.OnTalk;
                @Talk.performed -= instance.OnTalk;
                @Talk.canceled -= instance.OnTalk;
                @Gimmick.started -= instance.OnGimmick;
                @Gimmick.performed -= instance.OnGimmick;
                @Gimmick.canceled -= instance.OnGimmick;
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

        // Menu
        private readonly InputActionMap m_Menu;
        private List<IMenuActions> m_MenuActionsCallbackInterfaces = new List<IMenuActions>();
        private readonly InputAction m_Menu_Submit;
        private readonly InputAction m_Menu_Cancel;
        public struct MenuActions
        {
            private @IA m_Wrapper;
            public MenuActions(@IA wrapper) { m_Wrapper = wrapper; }
            public InputAction @Submit => m_Wrapper.m_Menu_Submit;
            public InputAction @Cancel => m_Wrapper.m_Menu_Cancel;
            public InputActionMap Get() { return m_Wrapper.m_Menu; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
            public void AddCallbacks(IMenuActions instance)
            {
                if (instance == null || m_Wrapper.m_MenuActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_MenuActionsCallbackInterfaces.Add(instance);
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
            }

            private void UnregisterCallbacks(IMenuActions instance)
            {
                @Submit.started -= instance.OnSubmit;
                @Submit.performed -= instance.OnSubmit;
                @Submit.canceled -= instance.OnSubmit;
                @Cancel.started -= instance.OnCancel;
                @Cancel.performed -= instance.OnCancel;
                @Cancel.canceled -= instance.OnCancel;
            }

            public void RemoveCallbacks(IMenuActions instance)
            {
                if (m_Wrapper.m_MenuActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IMenuActions instance)
            {
                foreach (var item in m_Wrapper.m_MenuActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_MenuActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public MenuActions @Menu => new MenuActions(this);
        public interface ITitleActions
        {
            void OnStart(InputAction.CallbackContext context);
            void OnQuit(InputAction.CallbackContext context);
        }
        public interface IMainGameActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnDash(InputAction.CallbackContext context);
            void OnUseItem(InputAction.CallbackContext context);
            void OnPause(InputAction.CallbackContext context);
            void OnMenu(InputAction.CallbackContext context);
            void OnScrollItem(InputAction.CallbackContext context);
            void OnPickUp(InputAction.CallbackContext context);
            void OnRead(InputAction.CallbackContext context);
            void OnOpenDoor(InputAction.CallbackContext context);
            void OnHide(InputAction.CallbackContext context);
            void OnTalk(InputAction.CallbackContext context);
            void OnGimmick(InputAction.CallbackContext context);
        }
        public interface IMenuActions
        {
            void OnSubmit(InputAction.CallbackContext context);
            void OnCancel(InputAction.CallbackContext context);
        }
    }
}
