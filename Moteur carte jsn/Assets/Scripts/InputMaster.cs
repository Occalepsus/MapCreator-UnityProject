// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Test"",
            ""id"": ""f0b3f142-1bda-400f-82f6-db5f2465df71"",
            ""actions"": [
                {
                    ""name"": ""Test"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1e692b0d-a3ac-4fd4-a78a-c6cefcebfee8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""60efaeef-712b-4eff-be34-37f106b1394f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Map modifier"",
            ""id"": ""96613068-398e-4c61-9497-ae7114ef7197"",
            ""actions"": [
                {
                    ""name"": ""Position"",
                    ""type"": ""Value"",
                    ""id"": ""b1c3267d-9ff8-45ac-822d-d98ffc94d16c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""5a682150-dfec-465f-ac9d-d44008f39ef7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""Up"",
                    ""type"": ""Value"",
                    ""id"": ""9dcd15b3-34d8-40b8-b84f-743cc4b3fd0a"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Delete"",
                    ""type"": ""Button"",
                    ""id"": ""cbdb6b2a-8871-4275-ba93-68af9cbe020b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Add"",
                    ""type"": ""Button"",
                    ""id"": ""2c7bd25b-0403-41ac-b6ca-f384cb1869fb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""01e9c8b7-a56e-4ab8-9825-081bed68a38e"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5ffde279-61bf-4378-a7c0-9509ac9fcf83"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""448f6053-1da3-4496-ad01-ebe931091cfb"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""5521b69a-578b-44e6-bbb0-4f1ffb1ff85f"",
                    ""path"": ""<Keyboard>/pageDown"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""104412b6-987d-4595-8f33-7940b944ae98"",
                    ""path"": ""<Keyboard>/pageUp"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""19e46e5a-f1bb-41ed-ad03-f5fb2d5764c6"",
                    ""path"": ""<Keyboard>/delete"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Delete"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a6f56753-6f3b-4551-9e03-0985c98ad4d8"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Add"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""57ac16c1-f9eb-4182-9809-196a76c42746"",
            ""actions"": [
                {
                    ""name"": ""Close"",
                    ""type"": ""Button"",
                    ""id"": ""93ad9ae1-da10-4830-a3e3-0e196aa446f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Load"",
                    ""type"": ""Button"",
                    ""id"": ""c8161f5e-16be-41a9-aa6f-bacb74d41265"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Save"",
                    ""type"": ""Button"",
                    ""id"": ""aab47456-04dc-46d2-83b0-3f20dbc634cd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Bloc options"",
                    ""type"": ""Button"",
                    ""id"": ""914e3dfd-76de-48c5-9b88-c04ff6946d8d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cdddb766-20e6-468e-bf67-04feb8e83a78"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Close"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7519633-5bb7-4755-b19d-5dfd3f093221"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Load"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8dad481b-a84e-4216-8eec-9699cfc7be79"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Save"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""93e39024-6aa2-443e-8fcf-5770406c26da"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Bloc options"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Camera"",
            ""id"": ""f591a0a3-0eef-4db4-98a0-b58403ecc715"",
            ""actions"": [
                {
                    ""name"": ""Pan clic"",
                    ""type"": ""Button"",
                    ""id"": ""e9912b0e-07f9-475f-9548-20ab4a42fca9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reset rot zoom"",
                    ""type"": ""Button"",
                    ""id"": ""b055ebca-fd60-46ad-a87c-f81310bedd37"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reset zoom"",
                    ""type"": ""Button"",
                    ""id"": ""b75d479f-678e-4300-bb72-ad12bfe1c1d3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""zoom"",
                    ""type"": ""Value"",
                    ""id"": ""c7fbb734-2b6f-4f1c-aebd-2274f0d6a32a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Delta"",
                    ""type"": ""Value"",
                    ""id"": ""7b48bfc7-3d16-4b5e-9076-67d1e7796d9e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Upper cam"",
                    ""type"": ""Button"",
                    ""id"": ""dea8c604-73e1-44b3-b4a9-1c5d91cda804"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e5152204-249b-4eb0-86ae-9aa90849ece0"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Pan clic"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0968b6c3-f001-478b-b0d1-65990f818f72"",
                    ""path"": ""<Keyboard>/#(²)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Reset rot zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""efd1c286-6129-4680-95ae-89e7e3c332b6"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Reset zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6c73ff19-0869-495e-800f-fd5d48e9e5f8"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""468ab94b-50c1-4a29-b4aa-e657dea25989"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""zoom"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""2d4bbefe-1c55-4721-9aa3-7eb48343c2d7"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""1aa24c61-97cc-4284-b39e-2308d573ba22"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""da23bba3-80de-425a-9a11-f9fd64634f07"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2(invertX=false)"",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Delta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c052738b-e0ec-4672-864c-01489ee85b67"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Upper cam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
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
        }
    ]
}");
        // Test
        m_Test = asset.FindActionMap("Test", throwIfNotFound: true);
        m_Test_Test = m_Test.FindAction("Test", throwIfNotFound: true);
        // Map modifier
        m_Mapmodifier = asset.FindActionMap("Map modifier", throwIfNotFound: true);
        m_Mapmodifier_Position = m_Mapmodifier.FindAction("Position", throwIfNotFound: true);
        m_Mapmodifier_Select = m_Mapmodifier.FindAction("Select", throwIfNotFound: true);
        m_Mapmodifier_Up = m_Mapmodifier.FindAction("Up", throwIfNotFound: true);
        m_Mapmodifier_Delete = m_Mapmodifier.FindAction("Delete", throwIfNotFound: true);
        m_Mapmodifier_Add = m_Mapmodifier.FindAction("Add", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Close = m_UI.FindAction("Close", throwIfNotFound: true);
        m_UI_Load = m_UI.FindAction("Load", throwIfNotFound: true);
        m_UI_Save = m_UI.FindAction("Save", throwIfNotFound: true);
        m_UI_Blocoptions = m_UI.FindAction("Bloc options", throwIfNotFound: true);
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_Panclic = m_Camera.FindAction("Pan clic", throwIfNotFound: true);
        m_Camera_Resetrotzoom = m_Camera.FindAction("Reset rot zoom", throwIfNotFound: true);
        m_Camera_Resetzoom = m_Camera.FindAction("Reset zoom", throwIfNotFound: true);
        m_Camera_zoom = m_Camera.FindAction("zoom", throwIfNotFound: true);
        m_Camera_Delta = m_Camera.FindAction("Delta", throwIfNotFound: true);
        m_Camera_Uppercam = m_Camera.FindAction("Upper cam", throwIfNotFound: true);
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

    // Test
    private readonly InputActionMap m_Test;
    private ITestActions m_TestActionsCallbackInterface;
    private readonly InputAction m_Test_Test;
    public struct TestActions
    {
        private @InputMaster m_Wrapper;
        public TestActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Test => m_Wrapper.m_Test_Test;
        public InputActionMap Get() { return m_Wrapper.m_Test; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TestActions set) { return set.Get(); }
        public void SetCallbacks(ITestActions instance)
        {
            if (m_Wrapper.m_TestActionsCallbackInterface != null)
            {
                @Test.started -= m_Wrapper.m_TestActionsCallbackInterface.OnTest;
                @Test.performed -= m_Wrapper.m_TestActionsCallbackInterface.OnTest;
                @Test.canceled -= m_Wrapper.m_TestActionsCallbackInterface.OnTest;
            }
            m_Wrapper.m_TestActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Test.started += instance.OnTest;
                @Test.performed += instance.OnTest;
                @Test.canceled += instance.OnTest;
            }
        }
    }
    public TestActions @Test => new TestActions(this);

    // Map modifier
    private readonly InputActionMap m_Mapmodifier;
    private IMapmodifierActions m_MapmodifierActionsCallbackInterface;
    private readonly InputAction m_Mapmodifier_Position;
    private readonly InputAction m_Mapmodifier_Select;
    private readonly InputAction m_Mapmodifier_Up;
    private readonly InputAction m_Mapmodifier_Delete;
    private readonly InputAction m_Mapmodifier_Add;
    public struct MapmodifierActions
    {
        private @InputMaster m_Wrapper;
        public MapmodifierActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Position => m_Wrapper.m_Mapmodifier_Position;
        public InputAction @Select => m_Wrapper.m_Mapmodifier_Select;
        public InputAction @Up => m_Wrapper.m_Mapmodifier_Up;
        public InputAction @Delete => m_Wrapper.m_Mapmodifier_Delete;
        public InputAction @Add => m_Wrapper.m_Mapmodifier_Add;
        public InputActionMap Get() { return m_Wrapper.m_Mapmodifier; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MapmodifierActions set) { return set.Get(); }
        public void SetCallbacks(IMapmodifierActions instance)
        {
            if (m_Wrapper.m_MapmodifierActionsCallbackInterface != null)
            {
                @Position.started -= m_Wrapper.m_MapmodifierActionsCallbackInterface.OnPosition;
                @Position.performed -= m_Wrapper.m_MapmodifierActionsCallbackInterface.OnPosition;
                @Position.canceled -= m_Wrapper.m_MapmodifierActionsCallbackInterface.OnPosition;
                @Select.started -= m_Wrapper.m_MapmodifierActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_MapmodifierActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_MapmodifierActionsCallbackInterface.OnSelect;
                @Up.started -= m_Wrapper.m_MapmodifierActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_MapmodifierActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_MapmodifierActionsCallbackInterface.OnUp;
                @Delete.started -= m_Wrapper.m_MapmodifierActionsCallbackInterface.OnDelete;
                @Delete.performed -= m_Wrapper.m_MapmodifierActionsCallbackInterface.OnDelete;
                @Delete.canceled -= m_Wrapper.m_MapmodifierActionsCallbackInterface.OnDelete;
                @Add.started -= m_Wrapper.m_MapmodifierActionsCallbackInterface.OnAdd;
                @Add.performed -= m_Wrapper.m_MapmodifierActionsCallbackInterface.OnAdd;
                @Add.canceled -= m_Wrapper.m_MapmodifierActionsCallbackInterface.OnAdd;
            }
            m_Wrapper.m_MapmodifierActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Position.started += instance.OnPosition;
                @Position.performed += instance.OnPosition;
                @Position.canceled += instance.OnPosition;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @Delete.started += instance.OnDelete;
                @Delete.performed += instance.OnDelete;
                @Delete.canceled += instance.OnDelete;
                @Add.started += instance.OnAdd;
                @Add.performed += instance.OnAdd;
                @Add.canceled += instance.OnAdd;
            }
        }
    }
    public MapmodifierActions @Mapmodifier => new MapmodifierActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Close;
    private readonly InputAction m_UI_Load;
    private readonly InputAction m_UI_Save;
    private readonly InputAction m_UI_Blocoptions;
    public struct UIActions
    {
        private @InputMaster m_Wrapper;
        public UIActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Close => m_Wrapper.m_UI_Close;
        public InputAction @Load => m_Wrapper.m_UI_Load;
        public InputAction @Save => m_Wrapper.m_UI_Save;
        public InputAction @Blocoptions => m_Wrapper.m_UI_Blocoptions;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Close.started -= m_Wrapper.m_UIActionsCallbackInterface.OnClose;
                @Close.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnClose;
                @Close.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnClose;
                @Load.started -= m_Wrapper.m_UIActionsCallbackInterface.OnLoad;
                @Load.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnLoad;
                @Load.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnLoad;
                @Save.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSave;
                @Save.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSave;
                @Save.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSave;
                @Blocoptions.started -= m_Wrapper.m_UIActionsCallbackInterface.OnBlocoptions;
                @Blocoptions.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnBlocoptions;
                @Blocoptions.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnBlocoptions;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Close.started += instance.OnClose;
                @Close.performed += instance.OnClose;
                @Close.canceled += instance.OnClose;
                @Load.started += instance.OnLoad;
                @Load.performed += instance.OnLoad;
                @Load.canceled += instance.OnLoad;
                @Save.started += instance.OnSave;
                @Save.performed += instance.OnSave;
                @Save.canceled += instance.OnSave;
                @Blocoptions.started += instance.OnBlocoptions;
                @Blocoptions.performed += instance.OnBlocoptions;
                @Blocoptions.canceled += instance.OnBlocoptions;
            }
        }
    }
    public UIActions @UI => new UIActions(this);

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_Panclic;
    private readonly InputAction m_Camera_Resetrotzoom;
    private readonly InputAction m_Camera_Resetzoom;
    private readonly InputAction m_Camera_zoom;
    private readonly InputAction m_Camera_Delta;
    private readonly InputAction m_Camera_Uppercam;
    public struct CameraActions
    {
        private @InputMaster m_Wrapper;
        public CameraActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Panclic => m_Wrapper.m_Camera_Panclic;
        public InputAction @Resetrotzoom => m_Wrapper.m_Camera_Resetrotzoom;
        public InputAction @Resetzoom => m_Wrapper.m_Camera_Resetzoom;
        public InputAction @zoom => m_Wrapper.m_Camera_zoom;
        public InputAction @Delta => m_Wrapper.m_Camera_Delta;
        public InputAction @Uppercam => m_Wrapper.m_Camera_Uppercam;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                @Panclic.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnPanclic;
                @Panclic.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnPanclic;
                @Panclic.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnPanclic;
                @Resetrotzoom.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnResetrotzoom;
                @Resetrotzoom.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnResetrotzoom;
                @Resetrotzoom.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnResetrotzoom;
                @Resetzoom.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnResetzoom;
                @Resetzoom.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnResetzoom;
                @Resetzoom.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnResetzoom;
                @zoom.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
                @zoom.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
                @zoom.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
                @Delta.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnDelta;
                @Delta.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnDelta;
                @Delta.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnDelta;
                @Uppercam.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnUppercam;
                @Uppercam.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnUppercam;
                @Uppercam.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnUppercam;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Panclic.started += instance.OnPanclic;
                @Panclic.performed += instance.OnPanclic;
                @Panclic.canceled += instance.OnPanclic;
                @Resetrotzoom.started += instance.OnResetrotzoom;
                @Resetrotzoom.performed += instance.OnResetrotzoom;
                @Resetrotzoom.canceled += instance.OnResetrotzoom;
                @Resetzoom.started += instance.OnResetzoom;
                @Resetzoom.performed += instance.OnResetzoom;
                @Resetzoom.canceled += instance.OnResetzoom;
                @zoom.started += instance.OnZoom;
                @zoom.performed += instance.OnZoom;
                @zoom.canceled += instance.OnZoom;
                @Delta.started += instance.OnDelta;
                @Delta.performed += instance.OnDelta;
                @Delta.canceled += instance.OnDelta;
                @Uppercam.started += instance.OnUppercam;
                @Uppercam.performed += instance.OnUppercam;
                @Uppercam.canceled += instance.OnUppercam;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    public interface ITestActions
    {
        void OnTest(InputAction.CallbackContext context);
    }
    public interface IMapmodifierActions
    {
        void OnPosition(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnUp(InputAction.CallbackContext context);
        void OnDelete(InputAction.CallbackContext context);
        void OnAdd(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnClose(InputAction.CallbackContext context);
        void OnLoad(InputAction.CallbackContext context);
        void OnSave(InputAction.CallbackContext context);
        void OnBlocoptions(InputAction.CallbackContext context);
    }
    public interface ICameraActions
    {
        void OnPanclic(InputAction.CallbackContext context);
        void OnResetrotzoom(InputAction.CallbackContext context);
        void OnResetzoom(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnDelta(InputAction.CallbackContext context);
        void OnUppercam(InputAction.CallbackContext context);
    }
}
