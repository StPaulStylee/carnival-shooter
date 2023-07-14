//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/InputActions/GameControls.inputactions
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

public partial class @GameControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameControls"",
    ""maps"": [
        {
            ""name"": ""GameController"",
            ""id"": ""2dfce4ed-be3d-44dc-9f97-e2e837bfbffb"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""3f1c1efb-8b45-4d1d-8c9c-1699ba202244"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""bab40887-2a4d-4bbb-b8f2-2d1198ae3c36"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""967fb87d-131b-428d-a668-5511375d7c60"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8eb54e13-8aaa-45da-8f15-c4e942b11f13"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d9549c4f-98d4-4caa-b70b-4a8332862890"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a28c0d3f-cf47-4161-a511-11d614a4b8e1"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse and Keyboard"",
            ""bindingGroup"": ""Mouse and Keyboard"",
            ""devices"": []
        }
    ]
}");
        // GameController
        m_GameController = asset.FindActionMap("GameController", throwIfNotFound: true);
        m_GameController_Look = m_GameController.FindAction("Look", throwIfNotFound: true);
        m_GameController_Fire = m_GameController.FindAction("Fire", throwIfNotFound: true);
        m_GameController_Reload = m_GameController.FindAction("Reload", throwIfNotFound: true);
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

    // GameController
    private readonly InputActionMap m_GameController;
    private List<IGameControllerActions> m_GameControllerActionsCallbackInterfaces = new List<IGameControllerActions>();
    private readonly InputAction m_GameController_Look;
    private readonly InputAction m_GameController_Fire;
    private readonly InputAction m_GameController_Reload;
    public struct GameControllerActions
    {
        private @GameControls m_Wrapper;
        public GameControllerActions(@GameControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_GameController_Look;
        public InputAction @Fire => m_Wrapper.m_GameController_Fire;
        public InputAction @Reload => m_Wrapper.m_GameController_Reload;
        public InputActionMap Get() { return m_Wrapper.m_GameController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameControllerActions set) { return set.Get(); }
        public void AddCallbacks(IGameControllerActions instance)
        {
            if (instance == null || m_Wrapper.m_GameControllerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameControllerActionsCallbackInterfaces.Add(instance);
            @Look.started += instance.OnLook;
            @Look.performed += instance.OnLook;
            @Look.canceled += instance.OnLook;
            @Fire.started += instance.OnFire;
            @Fire.performed += instance.OnFire;
            @Fire.canceled += instance.OnFire;
            @Reload.started += instance.OnReload;
            @Reload.performed += instance.OnReload;
            @Reload.canceled += instance.OnReload;
        }

        private void UnregisterCallbacks(IGameControllerActions instance)
        {
            @Look.started -= instance.OnLook;
            @Look.performed -= instance.OnLook;
            @Look.canceled -= instance.OnLook;
            @Fire.started -= instance.OnFire;
            @Fire.performed -= instance.OnFire;
            @Fire.canceled -= instance.OnFire;
            @Reload.started -= instance.OnReload;
            @Reload.performed -= instance.OnReload;
            @Reload.canceled -= instance.OnReload;
        }

        public void RemoveCallbacks(IGameControllerActions instance)
        {
            if (m_Wrapper.m_GameControllerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameControllerActions instance)
        {
            foreach (var item in m_Wrapper.m_GameControllerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameControllerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameControllerActions @GameController => new GameControllerActions(this);
    private int m_MouseandKeyboardSchemeIndex = -1;
    public InputControlScheme MouseandKeyboardScheme
    {
        get
        {
            if (m_MouseandKeyboardSchemeIndex == -1) m_MouseandKeyboardSchemeIndex = asset.FindControlSchemeIndex("Mouse and Keyboard");
            return asset.controlSchemes[m_MouseandKeyboardSchemeIndex];
        }
    }
    public interface IGameControllerActions
    {
        void OnLook(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
    }
}
