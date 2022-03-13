using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraManager : MonoBehaviour
{
    public CameraAnimator CA;
    public MapManager mapManager;
    public UIManager UIManager;
    public InputMaster controls;
    public CloudAdaptCam CAC;

    public Camera _camera;
    public Transform container;

    private Vector3 defaultSlide;
    private Vector3 defaultPos;
    public Vector3 defaultRotation;
    public Vector3 upRoation;

    public Vector2 focusSlide;

    [ReadOnly]
    public bool isPanning = false;
    [ReadOnly]
    public bool isMoving = false;
    [ReadOnly]
    public bool isUp;

    [Range(1,1.20f)]
    public float orthSizeMult;

    [Header("Paramètres du pan")]
    [Range(1, 30)]
    public float xPanSensibility;           //Les sensibilités de rotation de la caméra
    [Range(1, 30)]
    public float yPanSensibility;
    [Range(1, 100)]
    public float seuil;

    [Header("Paramètres du zoom")]
    [Range(0.1f,10)]
    public float zoomSensibility;           //La sensibilité de zoom de la caméra
    public Vector2 zoomLimitsScale;
    private Vector2 zoomLimits;
    [ReadOnly]
    public bool isZoomAdjusting;
    [Range(0, 5f)]
    public float focusedSize;
    [ReadOnly]
    public bool isFocused;

    private float upperSize;

    private float gDiag;
    private float beta;

    private const float vDist = 50;

    private Vector3 lastPos;
    [ReadOnly]
    public Quaternion lastRot;


    private void Awake()
    {
        controls = new InputMaster();

        controls.Camera.Uppercam.started += _ => ViewUp(true);
        controls.Camera.Uppercam.canceled += _ => ViewUp(false);

        controls.Camera.zoom.started += _ => { isZoomAdjusting = (isMoving || isFocused); };
        //controls.Camera.zoom.canceled += _ => { isZoomAdjusting = false; };

        controls.Camera.Panclic.started += _ => { isPanning = true; };
        controls.Camera.Panclic.canceled += _ => { isPanning = false; };

        controls.Camera.Delta.performed += ctx =>
        {
            if (isPanning && !isUp && !isMoving)
            {
                PanCam(ctx.ReadValue<Vector2>());

                if (isZoomAdjusting && !isFocused)
                {
                    _camera.orthographicSize = AdjustingCamSize(container.eulerAngles.x);
                }
            }
        };

        controls.Camera.Resetrotzoom.performed += _ => ResetZoom(true);
        controls.Camera.Resetzoom.performed += _ => ResetZoom(false);
    }

    public void SetCamera()
    {
        controls.Camera.Enable();

        defaultSlide = new Vector3(0, 0, -vDist);
        _camera.transform.localPosition = defaultSlide;
        _camera.enabled = true;
        container.eulerAngles = defaultRotation;

        defaultPos = new Vector3((mapManager.size.x - 1) * mapManager.scale.x, mapManager.size.y * mapManager.scale.y, (mapManager.size.z - 1) * mapManager.scale.z) / 2;
        container.position = defaultPos;

        //Calcule la taille de la caméra du dessus
        upperSize = mapManager.size.x > mapManager.size.z ? mapManager.size.x / 2f : mapManager.size.z / 2f;

        //Calcule les paramètres utiles au zoom
        gDiag = mapManager.RealSize.magnitude;
        beta = Mathf.Acos(mapManager.RealSize.y / gDiag);

        isZoomAdjusting = true;
        _camera.orthographicSize = AdjustingCamSize(defaultRotation.x);

        zoomLimits = zoomLimitsScale * Mathf.Max(mapManager.size.x, mapManager.size.z);

        lastPos = defaultPos;
        lastRot = Quaternion.Euler(defaultRotation);
        isFocused = false;

        //CAC.AdaptAlphaToAngle(defaultRotation.x);

        Debug.Log("Camera set !");
    }

    void Update()
    {
        if (!isZoomAdjusting)
        {
            ZoomCamera(controls.Camera.zoom.ReadValue<float>());
        }
    }

    private void ViewUp(bool goUp)
    {
        if (!isFocused && (isUp != goUp))
        {
            isUp = goUp;

            //Si la caméra doit aller au dessus
            if (goUp)
            {
                lastPos = container.position;
                lastRot = container.rotation;
                StartCoroutine(CA.MoveCamera(defaultPos, Quaternion.Euler(upRoation)));
                StartCoroutine(CA.ZoomCamera(upperSize));
            }
            //Si la camera doit reprendre sa position
            else
            {
                StartCoroutine(CA.MoveCamera(lastPos, lastRot));
                StartCoroutine(CA.ZoomCamera(AdjustingCamSize(lastRot.eulerAngles.x)));
            }
        }
    }

    /// <summary>
    /// Remet la position de la caméra à celle d'origine
    /// </summary>
    /// <param name="resetAll">ne reset pas uniquement le zoom mais aussi la rotation</param>
    private void ResetZoom(bool resetAll)
    {
        if (!isMoving)
        {
            isZoomAdjusting = true;

            if (resetAll)
            {
                isFocused = false;
                StartCoroutine(CA.MoveCamera(defaultPos, Quaternion.Euler(defaultRotation)));
                StartCoroutine(CA.ZoomCamera(AdjustingCamSize(defaultRotation.x)));
            }
            else { StartCoroutine(CA.ZoomCamera(AdjustingCamSize(container.eulerAngles.x))); }
        }
    }

    //Déplace la caméra
    private void PanCam(Vector2 delta)
    {
        Vector3 rot = container.localEulerAngles;

        rot.y += delta.x * xPanSensibility * Time.deltaTime;
        float x = rot.x + delta.y * yPanSensibility * Time.deltaTime;

        //Si on est hors des seuils limites, on change l'angle selon x
        if (x <= 90 - seuil && x >= seuil)
        {
            rot.x = x;
        }
        else { rot.x = x < seuil ? seuil : 90 - seuil; }

        container.eulerAngles = rot;

        //CAC.AdaptAlphaToAngle(rot.x);
    }

    //Donne la taille de zoom adaptatif
    private float AdjustingCamSize(float alpha) //alpha en degrés
    {
        alpha *= Mathf.Deg2Rad;
        return (gDiag / 2) * Mathf.Cos(beta - alpha) * orthSizeMult;
    }

    public void ZoomCamera(float speed)
    {
        if (_camera.orthographicSize > zoomLimits.x && speed > 0)
        {
            _camera.orthographicSize -= speed * zoomSensibility * Time.deltaTime;
        }
        else if (_camera.orthographicSize < zoomLimits.y && speed < 0)
        {
            _camera.orthographicSize -= speed * zoomSensibility * Time.deltaTime;
        }

        //CAC.AdaptAlphaToSize(_camera.orthographicSize);
    }

    public void FocusCamera(Vector3 coord)
    {
        isFocused = true;
        isZoomAdjusting = false;

        lastPos = container.position;
        lastRot = container.rotation;

        StartCoroutine(CA.ZoomCamera(focusedSize));
        StartCoroutine(CA.MoveCamera(coord, Quaternion.Euler(defaultRotation)));
        StartCoroutine(CA.SlideCamera((Vector3)focusSlide + defaultSlide));
        //Debug.Log("Focused");
    }

    public void UnfocusCamera()
    {
        if (isFocused)
        {
            StartCoroutine(CA.MoveCamera(lastPos, lastRot));
        }

        isFocused = false;

        StartCoroutine(CA.ZoomCamera(AdjustingCamSize(lastRot.eulerAngles.x)));
        StartCoroutine(CA.SlideCamera(defaultSlide));
    }

    public void UpdateFocus(Vector3 coord)
    {
        StartCoroutine(CA.MoveCamera(coord, container.rotation));
    }

    public void SetRotation(Quaternion rot)
    {
        lastRot = container.rotation;
        container.rotation = rot;
    }
}
