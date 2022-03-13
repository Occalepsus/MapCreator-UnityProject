using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public struct Biome
{
    /// <summary>
    /// The index of this biome
    /// </summary>
    public int index;
    public string name;
    public GameObject biomeBackground;

    public float sunDeclinaison;
    public float sunHoraire;
    /// <summary>
    /// The color of the sun
    /// </summary>
    public Color sunColor;
}

public class BiomeManager : MonoBehaviour
{
    public Environnement environnement;
    public UINewMap UINewMap;

    public TMPro.TMP_Text biomeName;

    public Transform backgroundsParent;

    public RectTransform viewport;

    public string biomeDataPath;
    private readonly string[] keyWords = new string[]
    { "%" };

    [ReadOnly]
    public int selected;

    public Vector2 defRenderSize;
    [Range(1, 1.5f)]
    public float largeRate;

    public Color defaultColor;
    public Color overColor;
    public Color selectedColor;

    [ReadOnly]
    public List<Biome> biomes;

    public GameObject displayPrefab;
    private List<BiomeDisplay> biomeDisplays;

    private void Start()
    {
        biomes = new List<Biome>();
        biomeDisplays = new List<BiomeDisplay>();

        string[] content = File.ReadAllLines(Directory.GetCurrentDirectory() + biomeDataPath);

        for (int i = 1; i < content.Length; i++)
        {
            string[] line = content[i].Split(keyWords, System.StringSplitOptions.None);

            GameObject bg = line[2] != "" 
                ? Instantiate(Resources.Load(line[2]) as GameObject) 
                : new GameObject($"Def background : {i}");

            Texture icon = line[8] != ""
                ? Resources.Load(line[8]) as Texture
                : Resources.Load("question-mark") as Texture;

            Biome biome = new Biome
            {
                index = int.Parse(line[0]),
                name = line[1],

                biomeBackground = bg,

                sunDeclinaison = int.Parse(line[3]),
                sunHoraire = int.Parse(line[4]),
                               
                sunColor = new Color
                {
                    r = int.Parse(line[5]) / 255f,
                    g = int.Parse(line[6]) / 255f,
                    b = int.Parse(line[7]) / 255f,
                    a = 1
                }
            };
            biome.biomeBackground.transform.parent = backgroundsParent;
            biome.biomeBackground.SetActive(false);

            biomes.Add(biome);

            GameObject display = Instantiate(displayPrefab, viewport);
            BiomeDisplay biomeDisplay = display.GetComponent<BiomeDisplay>();

            biomeDisplay.biomeIdx = biome.index;
            biomeDisplay.nameDisp.text = biome.name;
            biomeDisplay.SetDisplay(this, icon);

            biomeDisplays.Add(biomeDisplay);
        }

        biomeName.text = biomes[selected].name;
    }

    public void SetSelectedBiome(int biomeIdx, bool closeNMIBiomes)
    {
        selected = biomeIdx;
        environnement.SetBackground(biomeIdx);

        for (int i = 0; i < biomeDisplays.Count; i++)
        {
            biomeDisplays[i].SelectDisplay(i == selected);
        }

        if (closeNMIBiomes)
        {
            UINewMap.OpenBiomes(false);
            biomeName.text = biomes[selected].name;
        }
    }
}
