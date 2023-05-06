//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Input System/Input System.inputactions
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

public partial class @InputSystem : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputSystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input System"",
    ""maps"": [
        {
            ""name"": ""Monster"",
            ""id"": ""1afd09b3-2d4d-4753-b78f-9640a921caf0"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""ced44954-cc0f-45db-b2b2-8648d027421e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""4a07761b-cc2a-4c10-b2a6-445bbed88df4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""f536f0c9-50a1-40d5-ba24-1382f33bb654"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Consume"",
                    ""type"": ""Button"",
                    ""id"": ""40a56fd3-dfba-4577-9345-a16fabfbe8d7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PushPull"",
                    ""type"": ""Button"",
                    ""id"": ""74b14e10-88be-4512-ac1b-00639c34b81c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Swap"",
                    ""type"": ""Button"",
                    ""id"": ""ec3c31bb-6c85-4bba-98fa-96f142061120"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""acec5b15-9184-43d8-82cb-ac08c207bd27"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ddec04d5-d33f-4ab4-87c9-05a11bda5608"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""1e423876-c2c2-45ed-aa6d-fe557a1099f2"",
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
                    ""id"": ""3e7cca90-9df5-46d5-883d-48571609894e"",
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
                    ""id"": ""4decc339-2fb5-4180-9359-4b79968fd394"",
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
                    ""id"": ""376f2c5d-e188-4a10-9688-2cdf525dfe3f"",
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
                    ""id"": ""1a35ac44-5d02-4c12-95fa-22b3bf855d37"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""729b08a8-78e6-45f4-91f4-d2392cef4768"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0404d8df-04a2-4b00-99c8-8ffd68ceb150"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""de451e09-1dfc-4156-a41b-6a8cf0bce84d"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""89cbb368-ba0d-40da-8b0e-1b5c5aee528d"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""552bb20f-7031-4963-b0e8-395565fdde57"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Consume"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12289c39-adf9-4490-b31a-d88e00e82526"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Consume"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48976cfd-3f79-4436-8ffd-c8bbfa39a0d4"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PushPull"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8ebdd84f-6d84-4cde-93e4-52b841d53419"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PushPull"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc239673-7f12-49c9-b096-28b36db99e7c"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c008e37a-545d-45bc-9e8c-10a19947ad5c"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""efd584ff-a796-4cf7-b75f-a488c0d3d3bf"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41afa0fb-ae4d-412a-a62e-eee7959d8b73"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Monster
        m_Monster = asset.FindActionMap("Monster", throwIfNotFound: true);
        m_Monster_Move = m_Monster.FindAction("Move", throwIfNotFound: true);
        m_Monster_Jump = m_Monster.FindAction("Jump", throwIfNotFound: true);
        m_Monster_Aim = m_Monster.FindAction("Aim", throwIfNotFound: true);
        m_Monster_Consume = m_Monster.FindAction("Consume", throwIfNotFound: true);
        m_Monster_PushPull = m_Monster.FindAction("PushPull", throwIfNotFound: true);
        m_Monster_Swap = m_Monster.FindAction("Swap", throwIfNotFound: true);
        m_Monster_Shoot = m_Monster.FindAction("Shoot", throwIfNotFound: true);
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

    // Monster
    private readonly InputActionMap m_Monster;
    private IMonsterActions m_MonsterActionsCallbackInterface;
    private readonly InputAction m_Monster_Move;
    private readonly InputAction m_Monster_Jump;
    private readonly InputAction m_Monster_Aim;
    private readonly InputAction m_Monster_Consume;
    private readonly InputAction m_Monster_PushPull;
    private readonly InputAction m_Monster_Swap;
    private readonly InputAction m_Monster_Shoot;
    public struct MonsterActions
    {
        private @InputSystem m_Wrapper;
        public MonsterActions(@InputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Monster_Move;
        public InputAction @Jump => m_Wrapper.m_Monster_Jump;
        public InputAction @Aim => m_Wrapper.m_Monster_Aim;
        public InputAction @Consume => m_Wrapper.m_Monster_Consume;
        public InputAction @PushPull => m_Wrapper.m_Monster_PushPull;
        public InputAction @Swap => m_Wrapper.m_Monster_Swap;
        public InputAction @Shoot => m_Wrapper.m_Monster_Shoot;
        public InputActionMap Get() { return m_Wrapper.m_Monster; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MonsterActions set) { return set.Get(); }
        public void SetCallbacks(IMonsterActions instance)
        {
            if (m_Wrapper.m_MonsterActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_MonsterActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MonsterActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MonsterActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_MonsterActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_MonsterActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_MonsterActionsCallbackInterface.OnJump;
                @Aim.started -= m_Wrapper.m_MonsterActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_MonsterActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_MonsterActionsCallbackInterface.OnAim;
                @Consume.started -= m_Wrapper.m_MonsterActionsCallbackInterface.OnConsume;
                @Consume.performed -= m_Wrapper.m_MonsterActionsCallbackInterface.OnConsume;
                @Consume.canceled -= m_Wrapper.m_MonsterActionsCallbackInterface.OnConsume;
                @PushPull.started -= m_Wrapper.m_MonsterActionsCallbackInterface.OnPushPull;
                @PushPull.performed -= m_Wrapper.m_MonsterActionsCallbackInterface.OnPushPull;
                @PushPull.canceled -= m_Wrapper.m_MonsterActionsCallbackInterface.OnPushPull;
                @Swap.started -= m_Wrapper.m_MonsterActionsCallbackInterface.OnSwap;
                @Swap.performed -= m_Wrapper.m_MonsterActionsCallbackInterface.OnSwap;
                @Swap.canceled -= m_Wrapper.m_MonsterActionsCallbackInterface.OnSwap;
                @Shoot.started -= m_Wrapper.m_MonsterActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_MonsterActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_MonsterActionsCallbackInterface.OnShoot;
            }
            m_Wrapper.m_MonsterActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @Consume.started += instance.OnConsume;
                @Consume.performed += instance.OnConsume;
                @Consume.canceled += instance.OnConsume;
                @PushPull.started += instance.OnPushPull;
                @PushPull.performed += instance.OnPushPull;
                @PushPull.canceled += instance.OnPushPull;
                @Swap.started += instance.OnSwap;
                @Swap.performed += instance.OnSwap;
                @Swap.canceled += instance.OnSwap;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
            }
        }
    }
    public MonsterActions @Monster => new MonsterActions(this);
    public interface IMonsterActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnConsume(InputAction.CallbackContext context);
        void OnPushPull(InputAction.CallbackContext context);
        void OnSwap(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
    }
}
