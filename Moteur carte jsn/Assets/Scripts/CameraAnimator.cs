using System.Collections;
using UnityEngine;


public class CameraAnimator : MonoBehaviour
{
    public MapManager mapManager;
    public CameraManager cameraManager;

    public AnimationCurve TransCurve;
    public AnimationCurve RotCurve;
    public AnimationCurve ZoomCurve;
    public AnimationCurve SlideCurve;

    public float duration;

    public IEnumerator MoveCamera(Vector3 endPos, Quaternion endRot)
    {
        cameraManager.isMoving = true;

        Vector3 startPos = cameraManager.container.position;
        Quaternion startRot = cameraManager.container.rotation;

        float t = 0;
        while (t <= duration)
        {
            cameraManager.container.position = Vector3.Lerp(startPos, endPos, TransCurve.Evaluate(t / duration));
            cameraManager.container.rotation = Quaternion.Lerp(startRot, endRot, RotCurve.Evaluate(t / duration));

            t += Time.deltaTime;
            yield return null;
        }
        cameraManager.isMoving = false;
        //Debug.Log("Moved");
    }

    public IEnumerator ZoomCamera(float endSize)
    {
        cameraManager.isMoving = true;

        float startSize = cameraManager._camera.orthographicSize;

        float t = 0;
        while (t <= duration)
        {
            cameraManager._camera.orthographicSize = Mathf.Lerp(startSize, endSize, ZoomCurve.Evaluate(t / duration));
            
            t += Time.deltaTime;
            yield return null;
        }
        cameraManager.isMoving = false;
        //Debug.Log("Zoomed");
    }

    public IEnumerator SlideCamera(Vector3 endSlide)
    {
        cameraManager.isMoving = true;

        Vector3 startSlide = cameraManager._camera.transform.localPosition;

        float t = 0;
        while (t <= duration)
        {
            cameraManager._camera.transform.localPosition = Vector3.Lerp(startSlide, endSlide, SlideCurve.Evaluate(t / duration));

            t += Time.deltaTime;
            yield return null;
        }
        cameraManager.isMoving = false;
    }
}