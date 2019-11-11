// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Controls
{
    public class PlayerControls : IInputActionCollection, IDisposable
    {
        private InputActionAsset asset;

        public PlayerControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Game"",
            ""id"": ""e9d76cc1-bddf-4e33-8041-2bd512314251"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""6af0be90-7c6c-489d-a98c-c74dcb3213cd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""WeaponAimMouse"",
                    ""type"": ""Value"",
                    ""id"": ""24334f7a-7066-499a-8cb7-3f0b8e12b82a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""6750dd07-69ef-4c90-916a-f1aae64392c8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""3263a2f8-e666-4293-ab09-1bce3f895862"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveStick"",
                    ""type"": ""Value"",
                    ""id"": ""33b4b3be-e8dd-44b0-977e-c007e825a4f4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""WeaponAimStick"",
                    ""type"": ""Button"",
                    ""id"": ""b8da82b0-2200-4b8a-824d-8e006a193fc2"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""AD"",
                    ""id"": ""ec0fd707-08ce-4b4a-9efd-0e822492c8cd"",
                    ""path"": ""1DAxis"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""92be4c4b-f7ea-4720-8ef5-b401b7d130c5"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d613de0d-219a-4635-8d7e-8f25912e1d9e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""baa90bf0-acbc-45d1-960a-300dce85aecc"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""03dae197-723a-454d-a4bf-82d5f6296387"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55d784f4-304d-4343-a566-873276d07325"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""37eca69a-52d3-4205-b1e7-d8271edc4e48"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""27512d83-005a-427c-8487-7c427456327f"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c49632d7-f783-438a-b89d-dc14a7432f79"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2e0bb8a-9c63-4e01-b7db-d268e9d25a3e"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""WeaponAimMouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38fad887-aba6-42be-b2f1-8dd3687e1793"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""WeaponAimStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Game
            m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
            m_Game_Move = m_Game.FindAction("Move", throwIfNotFound: true);
            m_Game_WeaponAimMouse = m_Game.FindAction("WeaponAimMouse", throwIfNotFound: true);
            m_Game_Fire = m_Game.FindAction("Fire", throwIfNotFound: true);
            m_Game_Jump = m_Game.FindAction("Jump", throwIfNotFound: true);
            m_Game_MoveStick = m_Game.FindAction("MoveStick", throwIfNotFound: true);
            m_Game_WeaponAimStick = m_Game.FindAction("WeaponAimStick", throwIfNotFound: true);
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

        // Game
        private readonly InputActionMap m_Game;
        private IGameActions m_GameActionsCallbackInterface;
        private readonly InputAction m_Game_Move;
        private readonly InputAction m_Game_WeaponAimMouse;
        private readonly InputAction m_Game_Fire;
        private readonly InputAction m_Game_Jump;
        private readonly InputAction m_Game_MoveStick;
        private readonly InputAction m_Game_WeaponAimStick;

        public struct GameActions
        {
            private PlayerControls m_Wrapper;

            public GameActions(PlayerControls wrapper)
            {
                m_Wrapper = wrapper;
            }

            public InputAction @Move => m_Wrapper.m_Game_Move;
            public InputAction @WeaponAimMouse => m_Wrapper.m_Game_WeaponAimMouse;
            public InputAction @Fire => m_Wrapper.m_Game_Fire;
            public InputAction @Jump => m_Wrapper.m_Game_Jump;
            public InputAction @MoveStick => m_Wrapper.m_Game_MoveStick;
            public InputAction @WeaponAimStick => m_Wrapper.m_Game_WeaponAimStick;

            public InputActionMap Get()
            {
                return m_Wrapper.m_Game;
            }

            public void Enable()
            {
                Get().Enable();
            }

            public void Disable()
            {
                Get().Disable();
            }

            public bool enabled => Get().enabled;

            public static implicit operator InputActionMap(GameActions set)
            {
                return set.Get();
            }

            public void SetCallbacks(IGameActions instance)
            {
                if (m_Wrapper.m_GameActionsCallbackInterface != null)
                {
                    Move.started -= m_Wrapper.m_GameActionsCallbackInterface.OnMove;
                    Move.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnMove;
                    Move.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnMove;
                    WeaponAimMouse.started -= m_Wrapper.m_GameActionsCallbackInterface.OnWeaponAimMouse;
                    WeaponAimMouse.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnWeaponAimMouse;
                    WeaponAimMouse.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnWeaponAimMouse;
                    Fire.started -= m_Wrapper.m_GameActionsCallbackInterface.OnFire;
                    Fire.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnFire;
                    Fire.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnFire;
                    Jump.started -= m_Wrapper.m_GameActionsCallbackInterface.OnJump;
                    Jump.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnJump;
                    Jump.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnJump;
                    MoveStick.started -= m_Wrapper.m_GameActionsCallbackInterface.OnMoveStick;
                    MoveStick.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnMoveStick;
                    MoveStick.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnMoveStick;
                    WeaponAimStick.started -= m_Wrapper.m_GameActionsCallbackInterface.OnWeaponAimStick;
                    WeaponAimStick.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnWeaponAimStick;
                    WeaponAimStick.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnWeaponAimStick;
                }

                m_Wrapper.m_GameActionsCallbackInterface = instance;
                if (instance != null)
                {
                    Move.started += instance.OnMove;
                    Move.performed += instance.OnMove;
                    Move.canceled += instance.OnMove;
                    WeaponAimMouse.started += instance.OnWeaponAimMouse;
                    WeaponAimMouse.performed += instance.OnWeaponAimMouse;
                    WeaponAimMouse.canceled += instance.OnWeaponAimMouse;
                    Fire.started += instance.OnFire;
                    Fire.performed += instance.OnFire;
                    Fire.canceled += instance.OnFire;
                    Jump.started += instance.OnJump;
                    Jump.performed += instance.OnJump;
                    Jump.canceled += instance.OnJump;
                    MoveStick.started += instance.OnMoveStick;
                    MoveStick.performed += instance.OnMoveStick;
                    MoveStick.canceled += instance.OnMoveStick;
                    WeaponAimStick.started += instance.OnWeaponAimStick;
                    WeaponAimStick.performed += instance.OnWeaponAimStick;
                    WeaponAimStick.canceled += instance.OnWeaponAimStick;
                }
            }
        }

        public GameActions @Game => new GameActions(this);
        private int m_KeyboardSchemeIndex = -1;

        public InputControlScheme KeyboardScheme
        {
            get
            {
                if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
                return asset.controlSchemes[m_KeyboardSchemeIndex];
            }
        }

        private int m_GamepadSchemeIndex = -1;

        public InputControlScheme GamepadScheme
        {
            get
            {
                if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
                return asset.controlSchemes[m_GamepadSchemeIndex];
            }
        }

        public interface IGameActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnWeaponAimMouse(InputAction.CallbackContext context);
            void OnFire(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnMoveStick(InputAction.CallbackContext context);
            void OnWeaponAimStick(InputAction.CallbackContext context);
        }
    }
}