using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudAdaptCam : MonoBehaviour
{
    public CameraManager cameraManager;

    [ReadOnly]
    public float scaleMult = 1;

    [Header("Cam size")]
    /// <summary>
    /// The smallest size the clouds are visible
    /// </summary>
    public float nearCamSize;
    /// <summary>
    /// The size which cloud are becoming transparent
    /// </summary>
    public float farCamSize;

    [Header("Cam angle")]
    /// <summary>
    /// The angle which clouds will become transparent
    /// </summary>
    public float startCamAngle;
    /// <summary>
    /// The angle which clouds are totally transparent
    /// </summary>
    public float minCamAngle;

    public AnimationCurve curve;

    private void Update()
    {
        scaleMult = Mathf.Min(
            AdaptAlphaToSize(cameraManager._camera.orthographicSize),
            AdaptAlphaToAngle(cameraManager.container.eulerAngles.x));
    }


    private float AdaptAlphaToSize(float size)
    {
        if (size < farCamSize)
        {
           return curve.Evaluate(Interpolate(nearCamSize, farCamSize, size));
        }
        else { return 1; }
    }
    
    private float AdaptAlphaToAngle(float angle)
    {
        return curve.Evaluate(Interpolate(minCamAngle, startCamAngle, angle));
    }

    private float Interpolate(float minValue, float maxValue, float value)
    {
        return (minValue - value) / (minValue - maxValue);
    }
}
