using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum UIMenuLayer
{
    closed,
    newMap,
    load,
    save,
    export,
}


public class UIManager : MonoBehaviour
{
    public MapManager mapManager;
    public SelectBloc selectBloc;
    public SaveLoadMap saveLoadMap;
    public CameraManager cameraManager;
    public InputMaster controls;

    public UIMenuLayer layer;

    [Header("Dependencies")]
    public UIRender UIRender;
    public UINewMap UINewMap;
    public UIBlocAction UIBlocAction;

    //Boutons bas gauche
    [Header("Access Buttons")]
    public GameObject abContainer;
    public Button openNewMap;
    public Button openLoadMap;
    public Button openSaveMap;
    public Button openExportInterface;

    //Les composants du menu newmap
    [Header("Menu Interface")]
    public GameObject newMapInterface;

    [ReadOnly]
    public bool newMapInterfaceOpened = true;
    [ReadOnly]
    public int defTextureIdx = 0;

    [Header("Quick action Interface")]
    public GameObject actionInterface;
    public GameObject QAContainer;
    public GameObject ActionInterfaceTexture;
    public float QASize;
    [ReadOnly]
    public bool blocActionInterfaceOpened;

    //Les composants de l'interface des options du bloc
    [Header("Option action Interface")]
    public GameObject OAContainer;
    public float FullSize;
    [ReadOnly]
    public bool expanded;

    [Header("Texture Interface")]
    public GameObject textureInterface;

    //Les composants de l'interface save
    [Header("Save Interface")]
    public GameObject saveInterface;
    public TMP_InputField saveName;
    public Button saveButton;
    [ReadOnly]
    public bool saveInterfaceOpened;

    //Les composants de l'interface load
    [Header("Load Interface")]
    public GameObject loadInterface;
    public TMP_Dropdown fileList;
    public Button loadButton;
    [ReadOnly]
    public bool loadInterfaceOpened;

    [Header("Export Interface")]
    public GameObject exportInterface;
    public Button compileMap;
    public Button export;
    public TMP_InputField exportName;
    [ReadOnly]
    public bool exportInterfaceOpened;


    public TMP_Dropdown.OptionData selectionner;

    private bool _mapActive;
    public bool MapActive
    {
        get => _mapActive;
        set
        {
            _mapActive = value;
            openSaveMap.enabled = value;
            openExportInterface.enabled = value;
        }
    }

    private void Awake()
    {
        controls = new InputMaster();
        controls.UI.Enable();

        controls.UI.Close.canceled += _ => Close();
        controls.UI.Load.canceled += _ => SetMenu(2);
        controls.UI.Save.canceled += _ => SetMenu(3);

        controls.UI.Blocoptions.started += _ =>
        {
            if (blocActionInterfaceOpened)
            {
                ExpandActionInterface();
            }
        };
    }

    void Start()
    {
        SetMenu(UIMenuLayer.newMap);
        MapActive = false;
    }

    public void SetMenu(int layer)
    {
        SetMenu((UIMenuLayer)layer);
    }

    public void SetMenu(UIMenuLayer layer)
    {
        if (layer == UIMenuLayer.closed)
        {
            selectBloc.gameObject.SetActive(true);
            controls.Camera.Enable();

            //Every menu is closed
            newMapInterface.SetActive(false);
            loadInterface.SetActive(false);
            saveInterface.SetActive(false);
            exportInterface.SetActive(false);

            newMapInterfaceOpened = false;
            loadInterfaceOpened = false;
            saveInterfaceOpened = false;
            exportInterfaceOpened = false;
        }

        //If export
        else if (layer == UIMenuLayer.export)
        {
            selectBloc.gameObject.SetActive(false);
            Deselect();

            newMapInterface.SetActive(false);
            loadInterface.SetActive(false);
            saveInterface.SetActive(false);
            exportInterface.SetActive(true);

            newMapInterfaceOpened = false;
            loadInterfaceOpened = false;
            saveInterfaceOpened = false;
            exportInterfaceOpened = true;
        }
        
        //If new map / load / save
        else
        {
            selectBloc.gameObject.SetActive(false);
            Deselect();
            controls.Camera.Disable();

            if (layer == UIMenuLayer.newMap)
            {
                newMapInterface.SetActive(true);
                loadInterface.SetActive(false);
                saveInterface.SetActive(false);
                exportInterface.SetActive(false);

                newMapInterfaceOpened = true;
                loadInterfaceOpened = false;
                saveInterfaceOpened = false;
                exportInterfaceOpened = false;
            }

            else if (layer == UIMenuLayer.load)
            {
                newMapInterface.SetActive(false);
                loadInterface.SetActive(true);
                saveInterface.SetActive(false);
                exportInterface.SetActive(false);

                newMapInterfaceOpened = false;
                loadInterfaceOpened = true;
                saveInterfaceOpened = false;
                exportInterfaceOpened = false;

                SetLoadInterface();
            }

            else if (layer == UIMenuLayer.save)
            {
                newMapInterface.SetActive(false);
                loadInterface.SetActive(false);
                saveInterface.SetActive(true);
                exportInterface.SetActive(false);

                newMapInterfaceOpened = false;
                loadInterfaceOpened = false;
                saveInterfaceOpened = true;
                exportInterfaceOpened = false;
            }
        }
    }

    //Partie Save/Load/Export

    private void SetLoadInterface()
    {
        //Clear the list before filling the new list
        fileList.options.Clear();

        //Set each map which is in save folder
        List<string> files = saveLoadMap.GetFileNames();

        List<TMP_Dropdown.OptionData> names = new List<TMP_Dropdown.OptionData>(new[] { selectionner });
        foreach (string name in files)
        {
            names.Add(new TMP_Dropdown.OptionData(name));
        }
        fileList.AddOptions(names);

        TestLoadChoice();
    }

    private void OpenExportInterface(bool open)
    {
        if (MapActive)
        {
            if (open) { export.enabled = false; }
            else { mapManager.tileMapClass.gameObject.SetActive(false); }
        }
        Debug.Assert(!MapActive, "Warning, you are trying to open Export interface whereas the map is not active");
    }

    private void TestLoadChoice()
    {
        loadButton.interactable = fileList.value > 0;
    }

    public void Load()
    {
        string name = fileList.captionText.text + ".txt";
        saveLoadMap.Load(name, out _);

        SetMenu(UIMenuLayer.closed);

        cameraManager.SetCamera();

        MapActive = true;
    }

    public void TestSaveText()
    {
        saveButton.enabled = TestFileName(saveName.text);
    }

    public void TestExportText()
    {
        export.enabled = TestFileName(exportName.text) && mapManager.isCompiled;
    }


    private bool TestFileName(string name)
    {
        bool isNotEmpty = name != "";
        
        char[] charArr = name.ToCharArray();
        bool hasNoIllegalChar = true;
        foreach (char letter in charArr)
        {
            foreach (char illegal in saveLoadMap.forbidden)
            {
                hasNoIllegalChar &= letter != illegal;
            }
        }
        return isNotEmpty && hasNoIllegalChar;
    }

    public void Save()
    {
        string name = saveName.text;
        saveLoadMap.Save(name);
        SetMenu(UIMenuLayer.closed);
    }

    public void CompileMap()
    {
        mapManager.CompileTileMap();
        TestExportText();
    }

    public void ExportTileMap()
    {
        string name = exportName.text;
        saveLoadMap.Export(name);
        OpenExportInterface(false);
    }


    //Programmes gérant l'interface du bloc

    public void OpenBlocInterface(bool open)
    {
        actionInterface.SetActive(open);
        if (expanded) { ExpandActionInterface(false); }

        blocActionInterfaceOpened = open;
    }

    public void Deselect()
    {
        actionInterface.SetActive(false);
        textureInterface.SetActive(false);
        ExpandActionInterface(false);

        blocActionInterfaceOpened = false;
    }

    public void ExpandActionInterface()
    {
        ExpandActionInterface(!expanded);
    }

    public void ExpandActionInterface(bool expand)
    {
        OAContainer.SetActive(expand);
        textureInterface.SetActive(expand);
        expanded = expand;

        float size = expand ? FullSize : QASize;
        ActionInterfaceTexture.GetComponent<RectTransform>().sizeDelta = new Vector2(400, size);

        if (expand) { cameraManager.FocusCamera(mapManager.ToWorldPos(UIBlocAction.blocInfo.coord)); }
        else { cameraManager.UnfocusCamera(); }
    }


    //Divers

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Unity quit");
    }

    public void Close()
    {
        if (MapActive)
        {
            if (UINewMap.texturesOpened) { UINewMap.OpenTextures(false); }
            else if (newMapInterfaceOpened) { SetMenu(UIMenuLayer.closed); }
            else if (saveInterfaceOpened) { SetMenu(UIMenuLayer.newMap); }
            else if (loadInterfaceOpened) { SetMenu(UIMenuLayer.newMap); }
            else if (exportInterfaceOpened) { SetMenu(UIMenuLayer.newMap); }
            else if (expanded) { ExpandActionInterface(false); }
            else { SetMenu(UIMenuLayer.newMap); Deselect(); }
        }
        else
        {
            if (UINewMap.texturesOpened) { UINewMap.OpenTextures(false); }
            else if (loadInterfaceOpened) { SetMenu(UIMenuLayer.newMap); }
            //OpenNMI(true);
        }
    }
}
