using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Tile
{
    public int index;
    public Vector3Int coordinates;
    public int[] accessible;
    public int[] neighbors;
    public float speed;
    
    public Tile(int index, Vector3Int coordinates, int[] accessible, float speed)
    {
        this.index = index;
        this.coordinates = coordinates;
        this.accessible = accessible;
        neighbors = new int[] { -1, -1, -1, -1 };
        this.speed = speed;
    }
}


public class TileMapClass : MonoBehaviour
{
    public TileRenderer tileRenderer;

    public Vector3Int size;
    public Biome biome;

    public List<Tile> TileList = new List<Tile>();
    public int[,,] TileMap;
    public BlocType[,,] TypeMap;

    [ReadOnly]
    public int lastIndex;

    public void SetTileMap(Vector3Int size, Biome biome)
    {
        size.y--;
        this.size = size;
        this.biome = biome;

        TileList.Clear();
        lastIndex = 0;
        TileMap = CreateNewTileMap(size);
        TypeMap = CreateNewTypeMap(size);

        tileRenderer.gameObject.SetActive(false);

        Debug.Log("TileMap set !");
    }

    public void SetTileMap()
    {
        TileList = new List<Tile>();
        TileMap = CreateNewTileMap(size);
        TypeMap = CreateNewTypeMap(size);

        tileRenderer.gameObject.SetActive(false);

        Debug.Log("TileMap set !");
    }

    public void EvaluateTileMap(List<GameObject> blocs, int[,,] blocMap)
    {
        //Regarde tous les blocs actifs de la carte
        for (int x = 0; x < size.x; x++)
        {
            for (int y = size.y - 1; y >= 0; y--)
            {
                for (int z = 0; z < size.z; z++)
                {
                    int bloc = blocMap[x, y + 1, z];

                    if (bloc >= 0)
                    {
                        BlocInfo blocInfo = blocs[bloc].GetComponent<BlocInfo>();

                        if (blocInfo.active)
                        {
                            //Si le bloc peut être accessible
                            if (blocInfo.type == BlocType.Surface || blocInfo.type == BlocType.Bridge)
                            {

                                BlocType upType = BlocType.None;
                                if (y < size.y - 1)
                                {
                                    upType = TypeMap[x, y + 1, z];
                                }

                                //Si le bloc du dessus ne gène pas, on l'ajoute à tileList
                                if (upType == BlocType.None || upType == BlocType.Bridge || upType == BlocType.Prop)
                                {
                                    Tile tile = new Tile(lastIndex, blocInfo.coord, blocInfo.accessible, blocInfo.speed);
                                    TileList.Add(tile);
                                    TileMap[x, y, z] = lastIndex;

                                    lastIndex++;
                                }
                            }
                            TypeMap[x, y, z] = blocInfo.type;
                        }
                        else { throw new System.Exception(); }
                    }
                }
            }
        }

        foreach (Tile tile in TileList)
        {
            Neighbors(tile);
        }

        Debug.Log("TileMap evaluated !");
        tileRenderer.gameObject.SetActive(true);
        tileRenderer.DrawLines(TileList);
    }

    public static int[,,] CreateNewTileMap(Vector3Int size)
    {
        int[,,] arr = new int[size.x, size.y, size.z];

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                for (int z = 0; z < size.z; z++)
                {
                    arr[x, y, z] = -1;
                }
            }
        }
        return arr;
    }

    public static BlocType[,,] CreateNewTypeMap(Vector3Int size)
    {
        BlocType[,,] arr = new BlocType[size.x, size.y, size.z];

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                for (int z = 0; z < size.z; z++)
                {
                    arr[x, y, z] = BlocType.None;
                }
            }
        }
        return arr;
    }


    private void Neighbors(Tile tile)
    {
        Vector3Int coord = tile.coordinates;

        try
        {
            //Nord
            if (coord.x < size.x - 1)
            {
                int y = tile.accessible[0];
                //Si le voisin est accessible au nord
                if (y >= 0)
                {
                    //On récupère son indice
                    int north = TileMap[coord.x + 1, coord.y + y - 1, coord.z];

                    //Si il existe et que lui est accessible par le sud
                    if (north >= 0 && TileList[north].accessible[2] == 0)
                    {
                        tile.neighbors[0] = north;
                    }
                    //Si il n'existe pas, on cherche un potentiel voisin en dessous
                    else if (coord.y + y > 1)
                    {
                        int northD = TileMap[coord.x + 1, coord.y + y - 2, coord.z];
                        if (northD >= 0 && TileList[northD].accessible[2] == 1)
                        {
                            tile.neighbors[0] = northD;
                        }
                    }
                }
            }

            //Sud
            if (coord.x > 0)
            {
                int y = tile.accessible[2];
                if (y >= 0)
                {
                    int south = TileMap[coord.x - 1, coord.y + y - 1, coord.z];

                    if (south >= 0 && TileList[south].accessible[0] == 0)
                    {
                        tile.neighbors[2] = south;
                    }
                    else if (coord.y + y > 1)
                    {
                        int southD = TileMap[coord.x - 1, coord.y + y - 2, coord.z];
                        if (southD >= 0 && TileList[southD].accessible[0] == 1)
                        {
                            tile.neighbors[2] = southD;
                        }
                    }
                }
            }

            //Est
            if (coord.z > 0)
            {
                int y = tile.accessible[1];
                if (y >= 0)
                {
                    int east = TileMap[coord.x, coord.y + y - 1, coord.z - 1];

                    if (east >= 0 && TileList[east].accessible[3] == 0)
                    {
                        tile.neighbors[1] = east;
                    }
                    else if (coord.y + y > 1)
                    {
                        int eastD = TileMap[coord.x, coord.y + y - 2, coord.z - 1];
                        if (eastD >= 0 && TileList[eastD].accessible[3] == 1)
                        {
                            tile.neighbors[1] = eastD;
                        }
                    }
                }
            }

            //Ouest
            if (coord.z < size.z - 1)
            {
                int y = tile.accessible[3];
                if (y >= 0)
                {
                    int west = TileMap[coord.x, coord.y + y - 1, coord.z + 1];

                    if (west != -1 && TileList[west].accessible[1] == 0)
                    {
                        tile.neighbors[3] = west;
                    }
                    else if (coord.y + y > 1)
                    {
                        int westD = TileMap[coord.x, coord.y + y - 2, coord.z + 1];
                        if (westD >= 0 && TileList[westD].accessible[1] == 1)
                        {
                            tile.neighbors[3] = westD;
                        }
                    }
                }
            }
        }
        catch (System.ArgumentOutOfRangeException) { Debug.Log("J'ai buggé, pardon... : " + tile.index.ToString()); }
    }
}



