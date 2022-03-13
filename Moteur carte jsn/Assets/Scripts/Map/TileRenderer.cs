using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Orientation
{
    xAxis,
    zAxis,
}

public struct TileLine
{
    private MapManager mapManager;

    public Vector3Int coordinates;

    private List<bool> areAllowed;
    private List<Vector3> positions;
    private List<Quaternion> rotations;
    private List<Vector3> scales;

    private int _count;
    public int Count
    {
        get => _count;
    }

    private Quaternion xRot;
    private Quaternion zRot;
    private Vector3 defScale;

    public TileLine(MapManager mapManager, Vector3Int coordinates, Vector3 defScale)
    {
        this.mapManager = mapManager;
        this.coordinates = coordinates;

        areAllowed = new List<bool>();
        positions = new List<Vector3>();
        rotations = new List<Quaternion>();
        scales = new List<Vector3>();
        _count = 0;

        xRot = Quaternion.identity;
        zRot = Quaternion.Euler(new Vector3(0, 90, 0));
        this.defScale = defScale;
    }
    
    public void AddLine(bool isAllowed, bool isUp, bool placeDownward, Orientation orientation)
    {
        areAllowed.Add(isAllowed);

        Vector3 position = Vector3.Scale(coordinates, mapManager.scale);
        Quaternion rotation;
        if (placeDownward)
        {
            if (orientation == Orientation.xAxis)
            {
                rotation = xRot;
                position.z -= mapManager.scale.z / 2;
            }
            else
            {
                rotation = zRot;
                position.x -= mapManager.scale.x / 2;
            }
        }
        else
        {
            if (orientation == Orientation.xAxis)
            {
                rotation = xRot;
                position.z += mapManager.scale.z / 2;
            }
            else
            {
                rotation = zRot;
                position.x += mapManager.scale.x / 2;
            }
        }

        if (isUp) { position.y += mapManager.scale.y * 3 / 2; }
        else { position.y += mapManager.scale.y / 2; }

        positions.Add(position);
        rotations.Add(rotation);
        scales.Add(defScale);

        _count++;
    }

    public bool GetAllow(int i)
    {
        return areAllowed[i];
    }

    public Matrix4x4 GetTRS(int i)
    {
        return Matrix4x4.TRS(positions[i], rotations[i], scales[i]);
    }
}

public class TileRenderer : MonoBehaviour
{
    public TileMapClass tileMapClass;
    public MapManager mapManager;
    public Mesh mesh;

    public List<TileLine> tileLines;

    private List<List<Matrix4x4>> allowedLines;
    private List<List<Matrix4x4>> blocedLines;

    public Material allowMat;
    public Material blocMat;

    public Vector3 lineScale;

    public void DrawLines(List<Tile> tileList)
    {
        allowedLines = new List<List<Matrix4x4>>();
        blocedLines = new List<List<Matrix4x4>>();

        bool[] lined = new bool[tileMapClass.lastIndex];
        for (int i = 0; i < lined.Length; i++)
        {
            lined[i] = false;
        }

        int qA = 0;
        int qB = 0;

        List<Matrix4x4> allowedLineBatch = new List<Matrix4x4>();
        List<Matrix4x4> blocedLineBatch = new List<Matrix4x4>();

        foreach (Tile tile in tileList)
        {
            TileLine tileLine = new TileLine(mapManager, tile.coordinates, lineScale);

            int n = tile.neighbors[0];
            int e = tile.neighbors[1];
            int s = tile.neighbors[2];
            int w = tile.neighbors[3];

            if (n >= 0)
            {
                //Si le voisin existe et n'est pas marqué
                if (!lined[n]) { tileLine.AddLine(true, tile.accessible[0] == 1, false, Orientation.zAxis); }
            }
            //Si le voisin n'existe pas OU n'est pas marqué
            else { tileLine.AddLine(false, tile.accessible[0] == 1, false, Orientation.zAxis); }

            if (s >= 0)
            {
                if (!lined[s]) { tileLine.AddLine(true, tile.accessible[1] == 1, true, Orientation.zAxis); }
            }
            else { tileLine.AddLine(false, tile.accessible[1] == 1, true, Orientation.zAxis); }

            if (e >= 0)
            {
                if (!lined[e]) { tileLine.AddLine(true, tile.accessible[2] == 1, true, Orientation.xAxis); }
            }
            else { tileLine.AddLine(false, tile.accessible[2] == 1, true, Orientation.xAxis); }

            if (w >= 0)
            {
                if (!lined[w]) { tileLine.AddLine(true, tile.accessible[3] == 1, false, Orientation.xAxis); }
            }
            else { tileLine.AddLine(false, tile.accessible[3] == 1, false, Orientation.xAxis); }

            lined[tile.index] = true;

            if (qA % 1023 == 0) { allowedLines.Add(allowedLineBatch); allowedLineBatch.Clear(); }
            if (qB % 1023 == 0) { blocedLines.Add(blocedLineBatch); blocedLineBatch.Clear(); }

            for (int i = 0; i < tileLine.Count; i++)
            {
                if (tileLine.GetAllow(i))
                {
                    qA++;
                    allowedLineBatch.Add(tileLine.GetTRS(i));
                }
                else
                {
                    qB++;
                    blocedLineBatch.Add(tileLine.GetTRS(i));
                }
            }
        }
        if (qA % 1023 != 0) { allowedLines.Add(allowedLineBatch); }
        if (qB % 1023 != 0) { blocedLines.Add(blocedLineBatch); }
    }

    private void Update()
    {
        foreach (List<Matrix4x4> allowed in allowedLines)
        {
            Graphics.DrawMeshInstanced(mesh, 0, allowMat, allowed);
        }
        foreach (List<Matrix4x4> bloced in blocedLines)
        {
            Graphics.DrawMeshInstanced(mesh, 0, blocMat, bloced);
        }
    }

}
