using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// This struct manage cloud unit position in world
/// </summary>
[System.Serializable]
public struct CloudUnit
{
    /// <summary>
    /// Coordinates of the cloud in the batch
    /// </summary>
    public Vector2Int index;
    /// <summary>
    /// Coordinates of the cloud in the world
    /// </summary>
    public Vector3 pos;
    public Vector3 scale;
    public Quaternion rot;

    public Vector2Int globalIndex;

    public CloudUnit(Vector2Int index, Vector3 pos, Quaternion rot, Vector3 scale, Vector2Int globalIndex)
    {
        this.index = index;
        this.pos = pos;
        this.rot = rot;
        this.scale = scale;
        this.globalIndex = globalIndex;
    }

    public Matrix4x4 tranformData
    {
        get => Matrix4x4.TRS(pos, rot, scale);
    }

    public void SetScale(Vector3 scale)
    {
        this.scale = scale;
    }

    /// <summary>
    /// Is the cloud big enough to be drawn
    /// </summary>
    /// <param name="minSize">The minimal size the cloud must have</param>
    public bool IsBigEnough(float minSize)
    {
        return scale.y > minSize;
    }
}

public struct CloudBatch
{
    public Vector3 originPos;
    public Vector2Int size;
    public Vector2 deltaUnit;

    public Vector2Int index;

    public CloudUnit[,] cloudsToUpdate;

    public Vector3 centerPos
    {
        get => new Vector3(originPos.x + size.x * deltaUnit.x, originPos.y, originPos.z + size.y * deltaUnit.y);
        set
        {
            originPos.x = value.x - size.x * deltaUnit.x;
            originPos.y = value.y;
            originPos.z = value.z - size.y * deltaUnit.y;
        }
    }


    public CloudBatch(Vector3 originPos, Vector2Int size, Vector2 deltaUnit, Vector2Int index)
    {
        this.size = size;
        this.deltaUnit = deltaUnit;

        this.originPos = originPos;

        this.index = index;

        cloudsToUpdate = new CloudUnit[size.x, size.y];

        SetClouds();
    }
    
    /// <summary>
    /// Get the distance of the batch from the center
    /// </summary>
    /// <param name="center">Slt</param>
    /// <returns></returns>
    public float DistanceFromCenter(Vector3 center)
    {
        return Vector3.Distance(centerPos, center);
    }

    /// <summary>
    /// Set the CloudUnits of the batch
    /// </summary>
    private void SetClouds()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Vector3 cloudPos = originPos + new Vector3(i * deltaUnit.x, 0, j * deltaUnit.y);
                Vector2Int globalIndex = new Vector2Int(i, j) + Vector2Int.Scale(index, size);
                cloudsToUpdate[i, j] = new CloudUnit(new Vector2Int(i, j), cloudPos, Quaternion.identity, Vector3.one, globalIndex);
            }
        }
    }

    /// <summary>
    /// Returns the cloud that have to be drawn
    /// </summary>
    public List<CloudUnit> DrawClouds(CloudFade cloudFade, Vector2 perlinPos, Vector2 perlinScale, Vector3 defScale, float minSize, AnimationCurve cloudsShape)
    {
        List<CloudUnit> toDraw = new List<CloudUnit>();
        foreach (CloudUnit cloud in cloudsToUpdate)
        {
            Vector3 scale = defScale
                * cloudsShape.Evaluate(Mathf.PerlinNoise(
                    perlinPos.x + cloud.index.x * perlinScale.x,
                    perlinPos.y + cloud.index.y * perlinScale.y))
                * cloudFade.AdaptSize(cloud.pos);

            cloud.SetScale(scale);
            if (cloud.IsBigEnough(minSize))
            {
                toDraw.Add(cloud);
            }
        }
        return toDraw;
    }
}

public class CloudsSystem : MonoBehaviour
{
    public CloudFade cloudFade;
    public CloudMask cloudMask;
    private bool doMask;
    public CloudAdaptCam CAC;
    public Mesh cloudMesh;
    public Material cloudMaterial;

    [ReadOnly]
    public int batchRendered;

    /// <summary>
    /// The plane of where the clouds are on the map
    /// </summary>
    [ReadOnly]
    public Plane cloudPlane;
    [ReadOnly]
    public Vector3 centerPos;

    public Vector2Int batchSize;
    public Vector2Int batchNumber;
    public Vector2 deltaUnit;
    [ReadOnly]
    public Vector2 planeSize;
    private CloudBatch[,] batches;

    public Vector3 defScale;
    /// <summary>
    /// The minimum scale that cloud will be drawn
    /// </summary>
    public float minScale;

    [ReadOnly]
    public Vector2 perlinSpeed;
    //[ReadOnly]
    public Vector2 perlinScale;
    [ReadOnly]
    public Vector2 perlinPos;

    public AnimationCurve cloudsShape;

    private List<List<CloudUnit>> cloudsToUpdate;

    public void SetSky(Vector3 mapCenterPos, Vector2Int batchNumber, Vector2 planeSize, float mapSize)
    {
        perlinSpeed = Random.insideUnitCircle * 0.05f;
        perlinPos = Random.insideUnitCircle * 10000;

        cloudsToUpdate = new List<List<CloudUnit>>();

        this.batchNumber = batchNumber;

        centerPos = mapCenterPos;
        cloudPlane = new Plane(Vector3.up, -mapCenterPos.y);
        this.planeSize = planeSize;

        Vector3 origin = centerPos - new Vector3(
            batchNumber.x * deltaUnit.x * batchSize.x / 2,
            0,
            batchNumber.y * deltaUnit.y * batchSize.y / 2);

        batches = new CloudBatch[batchNumber.x, batchNumber.y];

        for (int i = 0; i < batchNumber.x; i++)
        {
            for (int j = 0; j < batchNumber.y; j++)
            {
                Vector3 batchOrigin = origin + new Vector3(
                    i * batchSize.x * deltaUnit.x,
                    0,
                    j * batchSize.y * deltaUnit.y);
                batches[i, j] = new CloudBatch(batchOrigin, batchSize, deltaUnit, new Vector2Int(i, j));
            }
        }

        doMask = cloudMask != null && cloudMask.enabled;
        if (doMask) { cloudMask.SetMask(new Vector3(batchSize.x * deltaUnit.x, 0, batchSize.y * deltaUnit.y ) / 2, mapSize); }
    }


    private void Update()
    {
        batchRendered = 0;

        perlinPos += Time.deltaTime * perlinSpeed;

        doMask = cloudMask.enabled;
        GetClouds();

        foreach (var batch in cloudsToUpdate)
        {
            Graphics.DrawMeshInstanced(cloudMesh, 0, cloudMaterial, batch.Select(a => a.tranformData).ToList());
        }
    }

    private void GetClouds()
    {
        cloudsToUpdate.Clear();
        Vector2 deltaPerlin = Vector2.Scale(perlinScale, batchSize);

        for (int i = 0; i < batchNumber.x; i++)
        {
            for (int j = 0; j < batchNumber.y; j++)
            {
                if (!doMask || cloudMask.IsBatchVisible(batches[i, j]))
                {
                    batchRendered++;

                    Vector2 batchPerlinPos = perlinPos + new Vector2(i * deltaPerlin.x, j * deltaPerlin.y);

                    cloudsToUpdate.Add(batches[i, j].DrawClouds(
                        cloudFade,
                        batchPerlinPos,
                        perlinScale,
                        defScale * CAC.scaleMult,
                        minScale,
                        cloudsShape));
                }                
            }
        }
    }
}
