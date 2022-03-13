using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;

/// <summary>
/// The corresponding points of the camera in the world
/// </summary>
public struct MaskAnchor
{
    public Vector2Int camPos;
    private Plane plane;
    private Ray ray;
    private float dist;

    public MaskAnchor(Vector2Int camPos, Plane plane)
    {
        this.camPos = camPos;
        this.plane = plane;
        ray = new Ray();
        dist = 0;
    }

    public Vector3 worldPos
    {
        get
        {
            ray = Camera.main.ScreenPointToRay((Vector2)camPos);
            /*if*/
            _ = (plane.Raycast(ray, out dist));
            {
                return ray.GetPoint(dist);
            }
            //else { throw new System.Exception("Camera ray do not cross the cloud plane"); }
        }
    }
}

public class CloudMask : MonoBehaviour
{
    public CloudsSystem cloudsSystem;

    [ReadOnly]
    public Vector3 cloudOrigin;

    private Vector3 camOffset;
    private Vector3 centerPos;
    private MaskAnchor centerAnchor;
    private MaskAnchor[] angleAnchors;
    private Vector2Int[] angleCoords;

    private float maskRadius;
    private float offSize;

    public Light scenelight;
    private bool doLight;
    private Vector3 lightProjPoint;
    private float size;

    public void SetMask(Vector3 camOffset)
    {
        this.camOffset = camOffset;

        int x = Camera.main.pixelWidth;
        int y = Camera.main.pixelHeight;

        angleCoords = new Vector2Int[]
        {
            new Vector2Int(0, 0),
            new Vector2Int(x, 0),
            new Vector2Int(0, y),
            new Vector2Int(x, y),
        };

        centerAnchor = new MaskAnchor(new Vector2Int(x / 2, y / 2), cloudsSystem.cloudPlane);
        angleAnchors = new MaskAnchor[4];
        for (int i = 0; i < 4; i++)
        {
            angleAnchors[i] = new MaskAnchor(angleCoords[i], cloudsSystem.cloudPlane);
        }

        offSize = Mathf.Max(
            cloudsSystem.batchSize.x * cloudsSystem.deltaUnit.x,
            cloudsSystem.batchSize.y * cloudsSystem.deltaUnit.y)
            /2;
    }

    public void SetMask(Vector3 camOffset, float sunMaskSize)
    {
        SetMask(camOffset);

        doLight = scenelight != null;

        if (doLight)
        {
            Vector3 heading = scenelight.transform.forward;
            float scale = cloudsSystem.centerPos.y / heading.y;
            lightProjPoint = cloudsSystem.centerPos + new Vector3(heading.x, 0, heading.z) * scale;
            size = sunMaskSize;
        }
        else
        {
            Debug.LogWarning("No light has been assigned to mask, cloud shadows can dissapear");
        }
    }

    void Update()
    {
        centerPos = centerAnchor.worldPos;

        maskRadius = 0;

        foreach (MaskAnchor anchor in angleAnchors)
        {
            float dist = Vector3.Distance(anchor.worldPos, centerPos);

            if (dist > maskRadius)
            {
                maskRadius = dist;
            }
        }
    }

    private bool IsBatchUnderLight(CloudBatch batch)
    {
        return Vector3.Distance(lightProjPoint, batch.centerPos - camOffset) <= size + offSize;
    }

    public bool IsBatchVisible(CloudBatch batch)
    {
        if (doLight)
        {
            return Vector3.Distance(centerPos, batch.centerPos - camOffset) <= maskRadius + offSize || IsBatchUnderLight(batch);
        }
        else
        {
            return Vector3.Distance(centerPos, batch.centerPos - camOffset) <= maskRadius + offSize;
        }
    }
}
