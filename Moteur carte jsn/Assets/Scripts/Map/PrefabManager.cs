using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public struct Prefab
{
    public string name;
    public GameObject[] stateTextures;
    public int[] stateDiv;
    public BlocType texType;
    public float speed;
    public int rotation;
    public int[] accessible;

    public int StateToIndex(int state)
    {
        for (int i = 0; i < stateDiv.Length; i++)
        {
            if (stateDiv[i] > state)
            {
                return i;
            }
        }
        return stateDiv.Length;
    }

    public GameObject GetStateTexture(int state)
    {
        return stateTextures[StateToIndex(state)];
    }

    public Transform PlaceStateTextures()
    {
        Transform container = new GameObject("Container").transform;

        foreach (GameObject texture in stateTextures)
        {
            GameObject.Instantiate(texture, container).SetActive(false);
        }
        return container;
    }
}



public class PrefabManager : MonoBehaviour
{
    public string fileDirectory;
    public string texturesDirectory;

    private readonly string[] keyWords = new string[]
        { "%" };

    public Prefab[] prefabs;

    private void Start()
    {
        prefabs = ReadData();
    }

    public Prefab[] ReadData()
    {
        string[] content = File.ReadAllLines(Directory.GetCurrentDirectory() + fileDirectory);

        Prefab[] prefabs = new Prefab[content.Length];

        for (int i = 0; i < content.Length; i++)
        {
            string[] data = content[i].Split(keyWords, StringSplitOptions.None);

            prefabs[i].name = data[0];
            int texNb = int.Parse(data[1]);
            GameObject[] stateTextures = new GameObject[texNb];

            for (int j = 0; j < texNb; j++)
            {
                stateTextures[j] = Resources.Load(data[2 + j]) as GameObject;
            }
            prefabs[i].stateTextures = stateTextures;

            int[] stateDiv = new int[texNb - 1];
            for (int j = 0; j < texNb - 1; j++)
            {
                stateDiv[j] = int.Parse(data[2 + texNb + j]);
            }
            prefabs[i].stateDiv = stateDiv;

            prefabs[i].texType = (BlocType)int.Parse(data[2 * texNb + 1]);
            prefabs[i].speed = float.Parse(data[2 * texNb + 2]);
            prefabs[i].rotation = int.Parse(data[2 * texNb + 3]) % 360;

            prefabs[i].accessible = new int[] 
                { int.Parse(data[2 * texNb + 4]), 
                  int.Parse(data[2 * texNb + 5]), 
                  int.Parse(data[2 * texNb + 6]), 
                  int.Parse(data[2 * texNb + 7]) };

            //Debug.Log($"Prefab {i} chargé");
        }
        return prefabs;
    }
}
