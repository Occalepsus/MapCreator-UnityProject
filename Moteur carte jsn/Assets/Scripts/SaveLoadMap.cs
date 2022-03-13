using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveLoadMap : MonoBehaviour
{
    public MapManager mapManager;
    public TileMapClass tileMapClass;

    public string saveDirectory;
    public string exportDirectory;
    private readonly string separator = "%";
    public readonly char[] forbidden = { '<', '>', ':', '"', '/', '\\', '|', '?', '*', '.' };


    public void Save(string name)
    {
        List<GameObject> blocs = mapManager.blocs;

        List<string> content = new List<string> { EncodeMapInfo() };
        foreach (GameObject bloc in blocs)
        {
            BlocInfo blocInfo = bloc.GetComponent<BlocInfo>();
            if (blocInfo.active)
            {
                content.Add(EncodeBlocInfo(blocInfo)); 
            }
        }

        File.WriteAllLines(Directory.GetCurrentDirectory() + saveDirectory + name + ".txt", content);
        Debug.Log("Map has been successfully saved");
    }

    public void Export(string name)
    {
        List<GameObject> blocs = mapManager.blocs;
        List<Tile> tiles = tileMapClass.TileList;

        List<string> content = new List<string>() { EncodeMapInfo() + "%" + blocs.Count.ToString() };
        foreach (GameObject bloc in blocs)
        {
            BlocInfo blocInfo = bloc.GetComponent<BlocInfo>();
            if (blocInfo.active)
            {
                string[] line = new string[]
                {
                blocInfo.index.ToString(),
                blocInfo.coord.ToString(),
                blocInfo.rotation.ToString(),
                blocInfo.textureIdx.ToString(),
                blocInfo.state.ToString()
                };
                content.Add(string.Join(separator, line));
            }
        }
        foreach (Tile tile in tiles)
        {
            string[] line = new string[]
            {
                tile.index.ToString(),
                tile.coordinates.ToString(),
                ArrayToString(tile.accessible),
                ArrayToString(tile.neighbors),
                tile.speed.ToString(),
            };
            content.Add(string.Join(separator, line));
        }
        File.WriteAllLines(Directory.GetCurrentDirectory() + exportDirectory + name + ".txt", content);
        Debug.Log("Map has been successfully exported");
    }

    private string EncodeMapInfo()
    {
        string[] line = new string[]
        {
            mapManager.size.ToString(),
            mapManager.scale.ToString(),
        };

        return string.Join(separator, line);
    }

    private string EncodeBlocInfo(BlocInfo blocInfo)
    {
        string[] line = new string[]
        {
            blocInfo.index.ToString(),          //0
            blocInfo.coord.ToString(),          //1
            blocInfo.rotation.ToString(),       //2
            ((int)blocInfo.type).ToString(),    //3
            blocInfo.textureIdx.ToString(),     //4
            blocInfo.speed.ToString(),          //5
            ArrayToString(blocInfo.accessible), //6
            blocInfo.state.ToString(),          //7
        };

        return string.Join(separator, line);
    }


    public void Load(string name, out Vector3Int size)
    {
        string[] content = File.ReadAllLines(Directory.GetCurrentDirectory() + saveDirectory + name);

        string[] mapInfo = content[0].Split(new[] { separator }, StringSplitOptions.None);
        size = StringToVector3Int(mapInfo[0]);

        Vector3 scale = StringToVector3(mapInfo[1]);

        for (int i = 1; i < content.Length; i++)
        {
            BlocInfo blocInfo = gameObject.AddComponent<BlocInfo>();

            string[] info = content[i].Split(new[] { separator }, StringSplitOptions.None);

            blocInfo.index = int.Parse(info[0]);
            blocInfo.coord = StringToVector3Int(info[1]);
            blocInfo.rotation = float.Parse(info[2]);
            blocInfo.type = (BlocType)int.Parse(info[3]);
            blocInfo.textureIdx = int.Parse(info[4]);
            blocInfo.speed = float.Parse(info[5]);
            blocInfo.accessible = StringToIntArr(info[6]);
            blocInfo.state = int.Parse(info[7]);
        }

        BlocInfo[] blocInfos = gameObject.GetComponents<BlocInfo>();

        mapManager.RecreateMap(size, scale, blocInfos);

        foreach (BlocInfo blocInfo in blocInfos)
        {
            Destroy(blocInfo);
        }

        Debug.Log("Map loaded");
    }

    public List<string> GetFileNames()
    {
        List<string> names = new List<string>();

        DirectoryInfo info = new DirectoryInfo(Directory.GetCurrentDirectory() + saveDirectory);
        FileInfo[] fileInfo = info.GetFiles();

        foreach (FileInfo file in fileInfo)
        {
            string[] slicedName = file.Name.Split(new char[] { '.' });

            if (slicedName.Length == 2 && slicedName[1] == "txt")
            {
                names.Add(slicedName[0]);
                //Debug.Log(slicedName[0]);
            }
        }
        return names;
    }

    public static Vector3Int StringToVector3Int(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }
        else { throw new Exception("String not in a right format"); }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3Int result = new Vector3Int(
            int.Parse(sArray[0]),
            int.Parse(sArray[1]),
            int.Parse(sArray[2]));

        return result;
    }
    
    public static Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }
        else { throw new Exception("String not in a right format"); }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            SemiParse(sArray[0]),
            SemiParse(sArray[1]),
            SemiParse(sArray[2]));

        return result;
    }

    public static int[] StringToIntArr(string sArr)
    {
        // Remove the parentheses
        if (sArr.StartsWith("(") && sArr.EndsWith(")"))
        {
            sArr = sArr.Substring(1, sArr.Length - 2);
        }

        // split the items
        string[] sArray = sArr.Split(',');

        // store as a int[]
        int[] array = new int[sArray.Length];
        for (int i = 0; i < sArray.Length; i++)
        {
            array[i] = int.Parse(sArray[i]);
        }

        return array;
    }

    public static string ArrayToString<T>(T[] array)
    {
        string sArr = "(";
        for (int i = 0; i < array.Length; i++)
        {
            sArr += array[i].ToString();
            if (i != array.Length - 1) { sArr += ','; }
        }
        sArr += ")";
        return sArr;
    }

    public static float SemiParse(string number)
    {
        int ent = number.IndexOf('.');
        int dec = number.Length - ent - 1;
        return int.Parse(number.Substring(0, ent))
            + (Mathf.Pow(10, -dec) * int.Parse(number.Substring(ent + 1, dec)));
    }
}
