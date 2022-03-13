using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UINewMap : MonoBehaviour
{
    public UIManager UIManager;
    public MapManager mapManager;
    public CameraManager cameraManager;
    public BiomeManager biomeManager;

    public GameObject container;
    public TMP_InputField xSize;
    public TMP_InputField ySize;
    public TMP_InputField zSize;
    public Button biome;
    public Button textures;
    public Button startButton;
    public Button quit;

    public GameObject NMITextures;
    [ReadOnly]
    public bool texturesOpened;
    [ReadOnly]
    public int textureIdx;

    public GameObject NMIBiomes;
    [ReadOnly]
    public bool biomesOpened;

    void Start()
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        //foreach (Biome biome in (Biome[])System.Enum.GetValues(typeof(Biome)))
        //{
        //    options.Add(new TMP_Dropdown.OptionData(biome.ToString()));
        //}
        //biome.AddOptions(options);

        OpenTextures(false);
    }

    public void StartNewMap()
    {
        Vector3Int size = new Vector3Int(int.Parse(xSize.text), int.Parse(ySize.text) + 1, int.Parse(zSize.text));
        //Biome biome = (Biome)this.biome.value;

        mapManager.StartNewMap(size, textureIdx, new Biome());

        cameraManager.SetCamera();

        UIManager.SetMenu(UIMenuLayer.closed);
        UIManager.MapActive = true;

        OpenTextures(false);
    }

    public void OpenTextures()
    {
        OpenTextures(!texturesOpened);
    }

    public void OpenTextures(bool open)
    {
        if (open) { UIManager.UIRender.SetSelectedTexture(textureIdx, true, false); }
        NMITextures.SetActive(open);
        container.SetActive(!open);
        texturesOpened = open;
    }
    
    public void OpenBiomes()
    {
        OpenBiomes(!biomesOpened);
    }

    public void OpenBiomes(bool open)
    {
        //Si on ouvre le menu, alors on actualise l'élément sélectionné
        if(open) { biomeManager.SetSelectedBiome(biomeManager.selected, false); }
        NMIBiomes.SetActive(open);
        container.SetActive(!open);
        biomesOpened = open;
    }
}
