using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CloudFade : MonoBehaviour
{
    public CloudsSystem cloudsSystem;
    public SelectBloc selectBloc;


    public float pointSize;

    public AnimationCurve shadeCurve;

    private bool isCursorOverClouds;

    private Vector3 mousePosInClouds;


    private void Update()
    {
        isCursorOverClouds = cloudsSystem.cloudPlane.Raycast(selectBloc.ray, out float dist);

        if (isCursorOverClouds)
        {
            mousePosInClouds = selectBloc.ray.GetPoint(dist);
        }
    }

    public float AdaptSize(Vector3 position)
    {
        float sizeMult = 1;

        if (isCursorOverClouds)
        {
            float distFromCursor = Vector3.Distance(position, mousePosInClouds);

            //Si le point est proche du curseur (et si le curseur est présent)
            if (distFromCursor < pointSize)
            {
                sizeMult *= shadeCurve.Evaluate(distFromCursor / pointSize);
            }
        }
        return sizeMult;
    }
}
