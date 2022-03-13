using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIRender : MonoBehaviour
{
    public PrefabManager prefabManager;
    public UIManager UIManager;
    public UIBlocAction UIBlocAction;
    public UINewMap UINewMap;
    public CameraManager cameraManager;

    public RectTransform NMIContentViewport;
    public RectTransform BAContentViewport;
    public GameObject textureDisplayPrefab;
    public GameObject textureRenderPrefab;
    public RenderTexture defRenderTexture;

    private List<TextureDisplay> NMIdisplays;
    private List<TextureDisplay> BAdisplays;
    private List<TextureRender> renders;

    public Vector2 defRenderSize;
    [Range(1,1.5f)]
    public float largeRate;

    public Color defaultColor;
    public Color overColor;
    public Color selectedColor;

    public float defaultRotSpeed;
    public float overRotSpeed;

    [ReadOnly]
    public int selectedTexture;

    void Start()
    {
        renders = new List<TextureRender>();
        NMIdisplays = new List<TextureDisplay>();
        BAdisplays = new List<TextureDisplay>();

        for (int i = 0; i < prefabManager.prefabs.Length; i++)
        {
            GameObject render = Instantiate(textureRenderPrefab, transform);
            render.transform.localPosition = 10 * i * Vector3.left;

            TextureRender textureRender = render.GetComponent<TextureRender>();

            textureRender.rotSpeed = defaultRotSpeed;
            textureRender.SetRender(prefabManager.prefabs[i].stateTextures, prefabManager.prefabs[i].StateToIndex(UIBlocAction.blocInfo.state), this);

            renders.Add(textureRender);

            //NMI
            GameObject NMIdisplay = Instantiate(textureDisplayPrefab, NMIContentViewport);
            TextureDisplay NMItextureDisplay = NMIdisplay.GetComponent<TextureDisplay>();

            NMItextureDisplay.TR = textureRender;
            NMItextureDisplay.texIdx = i;
            NMItextureDisplay.nameDisp.text = prefabManager.prefabs[i].name;
            NMItextureDisplay.SetDisplay(this);

            NMItextureDisplay.isNMI = true;
            NMIdisplays.Add(NMItextureDisplay);

            //BA
            GameObject BAdisplay = Instantiate(textureDisplayPrefab, BAContentViewport);
            TextureDisplay BAtextureDisplay = BAdisplay.GetComponent<TextureDisplay>();

            BAtextureDisplay.TR = textureRender;
            BAtextureDisplay.texIdx = i;
            BAtextureDisplay.nameDisp.text = prefabManager.prefabs[i].name;
            BAtextureDisplay.SetDisplay(this);

            BAtextureDisplay.isNMI = false;
            BAdisplays.Add(BAtextureDisplay);
        }
    }

    public void ChangeTextureState(int state)
    {
        for (int i = 0; i < renders.Count; i++)
        {
            int stateIdx = prefabManager.prefabs[i].StateToIndex(state);
            renders[i].ChangeActive(stateIdx);
        }
    }

    public void SetSelectedTexture(int selected, bool isNMI, bool closeNMITextures, bool changeSelectedTexture = false)
    {
        for (int i = 0; i < NMIdisplays.Count; i++)
        {
            NMIdisplays[i].SelectDisplay(i == selected);
            BAdisplays[i].SelectDisplay(i == selected);
        }
        //If the display is in new map menu textures
        if (isNMI)
        {
            UIManager.UINewMap.textureIdx = selected;

            //If a texture is selected, and new map menu is opened, close the panel
            if (closeNMITextures) { UINewMap.OpenTextures(false); }
        }
        //If the selected texture is changed
        if (changeSelectedTexture) { UIBlocAction.ChangeTexture(selected); }
    }
}
