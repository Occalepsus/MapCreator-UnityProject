using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Background
{
    public GameObject background;
    public float distance;
}

public class Environnement : MonoBehaviour
{
    public CloudsSystem cloudsSystem;
    public MapManager mapManager;
    public BiomeManager biomeManager;

    public Light dirLight;

    /// <summary>
    /// The height of the clouds over the highest layer of MapManager
    /// </summary>
    public float cloudHeight;

    /// <summary>
    /// The size ration of the cloud plane compared to the size of the map
    /// </summary>
    [Range(5, 12f)]
    public float sizeRatio;

    [ReadOnly]
    public Vector2Int batchNumber;

    /// <summary>
    /// The x-axis component of the rotation of the sun
    /// </summary>
    public float Declinaison
    {
        get => dirLight.transform.eulerAngles.x;
        set
        {
            if (0 <= Declinaison && Declinaison <= 90)
            {
                Vector3 rot = dirLight.transform.eulerAngles;
                rot.x = value;
                dirLight.transform.eulerAngles = rot;
            }
            else { throw new System.Exception("Declinaison must be between 0 and 90 degrees"); }
        }
    }

    /// <summary>
    /// The y-axis component of the rotation of the sun
    /// </summary>
    public float Horaire
    {
        get => dirLight.transform.eulerAngles.y % 360;
        set
        {
            Vector3 rot = dirLight.transform.eulerAngles;
            rot.y = value;
            dirLight.transform.eulerAngles = rot;
        }
    }


    /// <summary>
    /// Sets the environnement
    /// </summary>
    public void SetEnvironnement()
    {
        gameObject.SetActive(true);

        Vector3 centerPos = new Vector3(
            (mapManager.size.x - 1) * mapManager.scale.x / 2,
            cloudHeight + mapManager.RealSize.y,
            (mapManager.size.z - 1) * mapManager.scale.z / 2);

        Vector2 planeSize = sizeRatio * new Vector2(mapManager.RealSize.x, mapManager.RealSize.z);
        /*Vector2Int */batchNumber = new Vector2Int(
            Mathf.CeilToInt(planeSize.x / (cloudsSystem.deltaUnit.x * cloudsSystem.batchSize.x)),
            Mathf.CeilToInt(planeSize.y / (cloudsSystem.deltaUnit.y * cloudsSystem.batchSize.y)));

        Declinaison = biomeManager.biomes[biomeManager.selected].sunDeclinaison;
        Horaire = biomeManager.biomes[biomeManager.selected].sunHoraire;

        dirLight.color = biomeManager.biomes[biomeManager.selected].sunColor;

        Debug.Log($"Biome {biomeManager.biomes[biomeManager.selected].name} set !");

        cloudsSystem.SetSky(centerPos, batchNumber, planeSize, Mathf.Max(mapManager.RealSize.x, mapManager.RealSize.z));
    }

    public void SetBackground(int index)
    {

    }
}
