using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapManager : MonoBehaviour
{
    public TileMapClass tileMapClass;
    public PrefabManager prefabManager;
    public Environnement environnement;

    public GameObject mapObject;
    public GameObject ghosts;

    [ReadOnly]
    public Vector3Int size;
    public Vector3 scale = new Vector3(1, 0.5f, 1);
    public Vector3 RealSize
    {
        get => new Vector3(size.x * scale.x, (size.y - 1) * scale.y, size.z * scale.z);
    }

    [ReadOnly]
    public Vector3Int poolCoord;
    public Transform thrown;

    public GameObject defBloc;

    [ReadOnly]
    public List<GameObject> blocs;
    [ReadOnly]
    public int[,,] map;
    private int lastIndex = 1;

    private Biome biome;
    [ReadOnly]
    public bool isCompiled;

    // Create a new map given the size and the texture
    public void StartNewMap(Vector3Int size, int texture, Biome biome)
    {
        this.size = size;
        this.biome = biome;

        CreateNewMapArray(size);

        ClearBlocsList();

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.z; j++)
            {
                PlaceNewBloc(0, new Vector3Int(i, 0, j), true);
            }
        }

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.z; j++)
            {
                PlaceNewBloc(texture, new Vector3Int(i, 1, j));
            }
        }

        tileMapClass.SetTileMap(size, biome);
        environnement.SetEnvironnement();
    }


    //Recreate a map from the info given
    public void RecreateMap(Vector3Int size, Vector3 scale, BlocInfo[] blocInfos)
    {
        this.size = size;
        this.scale = scale;

        CreateNewMapArray(size);

        ClearBlocsList();

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.z; j++)
            {
                PlaceNewBloc(0, new Vector3Int(i, 0, j), true);
            }
        }

        for (int i = 0; i < blocInfos.Length; i++)
        {
            BlocInfo info = blocInfos[i];
            
            if (ValidEmptyPos(info.coord))
            {
                PlaceDetBloc(info.textureIdx, info.coord, info.rotation, info.type, info.speed, info.accessible, info.state);
            }
        }

        tileMapClass.SetTileMap(size, new Biome());
        environnement.SetEnvironnement();
    }

    public bool ValidEmptyPos(Vector3Int coord)
    {
        bool x = 0 <= coord.x && coord.x < size.x;
        bool y = 0 <= coord.y && coord.y < size.y;
        bool z = 0 <= coord.z && coord.z < size.z;

        return x && y && z && map[coord.x, coord.y, coord.z] == -1;
    }

    public bool ValidEmptyPos(int x, int y, int z)
    {
        return ValidEmptyPos(new Vector3Int(x, y, z));
    }

    public GameObject TakeBloc(Vector3Int coord)
    {
        bool x = 0 <= coord.x && coord.x < size.x;
        bool y = 0 <= coord.y && coord.y < size.y;
        bool z = 0 <= coord.z && coord.z < size.z;

        if (x && y && z && map[coord.x, coord.y, coord.z] >= 0)
        {
            return blocs[map[coord.x, coord.y, coord.z]];
        }
        else
        {
            return defBloc;
        }
    }


    public Vector3 ToWorldPos(Vector3Int coordinates)
    {
        return new Vector3(coordinates.x * scale.x, coordinates.y * scale.y, coordinates.z * scale.z);
    }

    public void CreateNewMapArray(Vector3Int size)
    {
        int[,,] map = new int[size.x, size.y, size.z];

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                for (int z = 0; z < size.z; z++)
                {
                    map[x, y, z] = -1;
                }
            }
        }
        this.map = map;
    }

    public void CompileTileMap()
    {
        tileMapClass.SetTileMap(size, biome);
        tileMapClass.EvaluateTileMap(blocs, map);
        isCompiled = true;
    }



    //Place un nouveau bloc demandé, /!\ si forcePlace, le bloc ne sera plus accessible /!\
    public GameObject PlaceNewBloc(int texToPlace, Vector3Int coordinates, bool isFloor = false, bool forcePlace = false)
    {
        //Si la position n'est pas valide (et que l'on ne force pas le placement)
        if (!forcePlace && !ValidEmptyPos(coordinates))
        {
            throw new System.ArgumentException($"Place déjà occupée à {coordinates}");
        }

        Prefab prefab = prefabManager.prefabs[texToPlace];

        GameObject bloc = Instantiate(defBloc);
        BlocInfo blocInfo = bloc.GetComponent<BlocInfo>();

        bloc.transform.position = ToWorldPos(coordinates);

        if (!isFloor && !forcePlace) //Si le bloc est normal
        {
            ChangeTextureContainer(bloc.transform, texToPlace);
            bloc.transform.GetChild(0).eulerAngles = new Vector3(0, prefab.rotation, 0);

            blocInfo.coord = coordinates;
            blocInfo.speed = prefab.speed;
            blocInfo.rotation = prefab.rotation;
            blocInfo.textureIdx = texToPlace;
            blocInfo.type = prefab.texType;
            blocInfo.accessible = new int[4];
            System.Array.Copy(prefab.accessible, blocInfo.accessible, 4);

            bloc.transform.parent = mapObject.transform;
            blocInfo.active = true;

            bloc.name = $"Bloc {lastIndex}";
            blocs.Add(bloc);
            blocInfo.index = lastIndex;
            map[coordinates.x, coordinates.y, coordinates.z] = lastIndex;
            lastIndex++;

            ChangeTextureState(bloc.transform);
        }
        else if (isFloor) //Si le bloc est un fantôme
        {
            bloc.transform.parent = ghosts.transform;
            blocInfo.index = lastIndex;
            blocInfo.coord = coordinates;
            blocs.Add(bloc);
            map[coordinates.x, coordinates.y, coordinates.z] = lastIndex;
            blocInfo.active = false;
            //bloc.GetComponentInChildren<MeshRenderer>().enabled = false;
            bloc.name = $"Floor {lastIndex}";
            lastIndex++;
        }

        else if (forcePlace) //Si on force le placement du bloc
        {
            bloc.transform.parent = mapObject.transform;
            blocInfo.active = true;
            bloc.name = "Forced";
        }

        return bloc;
    }

    private GameObject PlaceDetBloc(int texToPlace, Vector3Int coordinates, float rotation, BlocType type, float speed, int[] accessible, int state)
    {
        GameObject bloc = Instantiate(defBloc);

        blocs.Add(bloc);
        bloc.transform.position = ToWorldPos(coordinates);
        bloc.transform.localScale = scale;
        bloc.transform.parent = mapObject.transform;
        bloc.name = $"Bloc {lastIndex}";
        map[coordinates.x, coordinates.y, coordinates.z] = lastIndex;

        //bloc.transform.GetChild(0).eulerAngles = new Vector3(0, rotation, 0);

        BlocInfo blocInfo = bloc.GetComponent<BlocInfo>();

        blocInfo.active = true;

        blocInfo.index = lastIndex;
        blocInfo.coord = coordinates;
        blocInfo.rotation = rotation;
        blocInfo.textureIdx = texToPlace;
        blocInfo.speed = speed;

        blocInfo.accessible = accessible;
        blocInfo.type = type;
        blocInfo.state = state;

        ChangeTextureContainer(bloc.transform, texToPlace);
        ChangeTextureState(bloc.transform);

        lastIndex++;

        return bloc;
    }

    //Déplace le bloc vers les coordonnées si possible
    public void Move(Transform selectedBloc, Vector3Int coordinates, bool forcePlace = false)
    {
        BlocInfo blocInfo = selectedBloc.GetComponent<BlocInfo>();

        if (!forcePlace && !ValidEmptyPos(coordinates))
        {
            throw new System.ArgumentException($"Place déjà occupée {coordinates} !");
        }
        else if (!forcePlace && selectedBloc.GetComponent<BlocInfo>().active)
        {
            Vector3Int oldPos = blocInfo.coord;
            map[oldPos.x, oldPos.y, oldPos.z] = -1;
            map[coordinates.x, coordinates.y, coordinates.z] = blocInfo.index;
        }
        
        blocInfo.coord = coordinates;
        selectedBloc.position = ToWorldPos(coordinates);
    }

    public void DeleteBloc(Transform selectedBloc)
    {
        BlocInfo blocInfo = selectedBloc.GetComponent<BlocInfo>();

        map[blocInfo.coord.x, blocInfo.coord.y, blocInfo.coord.z] = -1;
        blocInfo.coord = poolCoord;
        blocInfo.active = false;

        selectedBloc.position = poolCoord;
        selectedBloc.GetComponent<Collider>().enabled = false;
        selectedBloc.GetComponentInChildren<MeshRenderer>().enabled = false;

        Debug.Log("Deleted");
    }

    private void ClearBlocsList()
    {
        int length = blocs.Count;

        if (length >= 1)
        {
            foreach (GameObject bloc in blocs)
            {
                if (bloc.GetComponent<BlocInfo>().active)
                {
                    DeleteBloc(bloc.transform);
                }
                Debug.Log($"{bloc.name} has been cleared");
            }

            blocs.Clear();
        }

        lastIndex = 0;
    }

    public void ChangeTextureContainer(Transform selectedBloc, int texIdx, bool changeParam = false)
    {
        Transform oldTex = selectedBloc.GetChild(0);
        
        oldTex.position = poolCoord;
        oldTex.parent = thrown;
        oldTex.gameObject.SetActive(false);
        oldTex.name = "ThrownTexture";

        BlocInfo blocInfo = selectedBloc.GetComponent<BlocInfo>();
        blocInfo.textureIdx = texIdx;

        Transform newTex = prefabManager.prefabs[texIdx].PlaceStateTextures();

        newTex.parent = selectedBloc;
        newTex.SetAsFirstSibling();
        newTex.position = selectedBloc.position;
        newTex.localEulerAngles = new Vector3(0, selectedBloc.GetComponent<BlocInfo>().rotation, 0);

        if (changeParam)
        {
            blocInfo.rotation = prefabManager.prefabs[texIdx].rotation;
            blocInfo.type = prefabManager.prefabs[texIdx].texType;
            blocInfo.speed = prefabManager.prefabs[texIdx].speed;
            blocInfo.accessible = prefabManager.prefabs[texIdx].accessible;
        }
        ChangeTextureState(selectedBloc);
    }

    public void ChangeTextureState(Transform selectedBloc)
    {
        BlocInfo blocInfo = selectedBloc.GetComponent<BlocInfo>();
        int texIdx = blocInfo.textureIdx;
        int stateIdx = prefabManager.prefabs[texIdx].StateToIndex(blocInfo.state);

        for (int i = 0; i < selectedBloc.GetChild(0).childCount; i++)
        {
            selectedBloc.GetChild(0).GetChild(i).gameObject.SetActive(i == stateIdx);
        }
    }

    public void RotateBloc(Transform selectedBloc, int rotation)
    {
        Transform texture = selectedBloc.GetChild(0);
        texture.eulerAngles = new Vector3(0, rotation, 0);

        BlocInfo blocInfo = selectedBloc.GetComponent<BlocInfo>();
        blocInfo.rotation = rotation;
    }
}
