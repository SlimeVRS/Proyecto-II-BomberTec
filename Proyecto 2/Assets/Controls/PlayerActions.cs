// GENERATED AUTOMATICALLY FROM 'Assets/Controls/PlayerActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""Player_1"",
            ""id"": ""78cae1cd-4bec-4761-b486-f9cd415b6176"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8ff84034-ee84-44f6-b440-188a4da80fd0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlaceBomb"",
                    ""type"": ""Button"",
                    ""id"": ""d1eb7e24-04ec-4c74-a6bf-968791767f1d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""28857ba9-0428-4790-951c-3a049d21b636"",
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
                    ""id"": ""c9d7aa1f-74fc-4f3b-bebd-8bccbe422968"",
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
                    ""id"": ""cb43a478-adec-405c-9c50-7098b97fc7ee"",
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
                    ""id"": ""dc1dccd2-32bb-4fb2-9489-3859e8ccf6f0"",
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
                    ""id"": ""7eaf3e75-89bc-49e9-a599-861b3858920f"",
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
                    ""id"": ""3e3e9ac1-56bf-479c-bb92-7467a0227033"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlaceBomb"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player_1
        m_Player_1 = asset.FindActionMap("Player_1", throwIfNotFound: true);
        m_Player_1_Move = m_Player_1.FindAction("Move", throwIfNotFound: true);
        m_Player_1_PlaceBomb = m_Player_1.FindAction("PlaceBomb", throwIfNotFound: true);
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

    // Player_1
    private readonly InputActionMap m_Player_1;
    private IPlayer_1Actions m_Player_1ActionsCallbackInterface;
    private readonly InputAction m_Player_1_Move;
    private readonly InputAction m_Player_1_PlaceBomb;
    public struct Player_1Actions
    {
        private @PlayerActions m_Wrapper;
        public Player_1Actions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_1_Move;
        public InputAction @PlaceBomb => m_Wrapper.m_Player_1_PlaceBomb;
        public InputActionMap Get() { return m_Wrapper.m_Player_1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player_1Actions set) { return set.Get(); }
        public void SetCallbacks(IPlayer_1Actions instance)
        {
            if (m_Wrapper.m_Player_1ActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_Player_1ActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_Player_1ActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_Player_1ActionsCallbackInterface.OnMove;
                @PlaceBomb.started -= m_Wrapper.m_Player_1ActionsCallbackInterface.OnPlaceBomb;
                @PlaceBomb.performed -= m_Wrapper.m_Player_1ActionsCallbackInterface.OnPlaceBomb;
                @PlaceBomb.canceled -= m_Wrapper.m_Player_1ActionsCallbackInterface.OnPlaceBomb;
            }
            m_Wrapper.m_Player_1ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @PlaceBomb.started += instance.OnPlaceBomb;
                @PlaceBomb.performed += instance.OnPlaceBomb;
                @PlaceBomb.canceled += instance.OnPlaceBomb;
            }
        }
    }
    public Player_1Actions @Player_1 => new Player_1Actions(this);
    public interface IPlayer_1Actions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnPlaceBomb(InputAction.CallbackContext context);
    }
}
