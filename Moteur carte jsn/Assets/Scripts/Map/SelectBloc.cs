using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBloc : MonoBehaviour
{
    public MapManager mapManager;
    public CameraManager cameraManager;
    public UIBlocAction UIBlocAction;
    public InputMaster controls;
    public SelDisplay selDisplay;

    public GameObject defaultBloc;

    [ReadOnly]
    public Ray ray;
    private RaycastHit hitData;

    private GameObject pointedBloc;
    private GameObject targetBloc;
    [ReadOnly]
    public GameObject selectedBloc;

    [ReadOnly]
    public bool held = false;
    private bool isOverInterface = false;

    [ReadOnly]
    public BlocInfo sBlocInfo;
    private BlocInfo pBlocInfo;
    private Vector3Int coordinates;
    private Vector3Int pointedCoordinates;


    private void Awake()
    {
        controls = new InputMaster();

        //Sélection d'un bloc, et si le clic reste enfoncé
        controls.Mapmodifier.Select.started += _ =>
        {
            if (!isOverInterface)
            {
                SelectNewBloc(pointedBloc);
            }
        };
        controls.Mapmodifier.Select.performed += _ => held = true;
        controls.Mapmodifier.Select.canceled += _ => held = false;

        //Si on veut ajouter un bloc
        controls.Mapmodifier.Add.started += _ =>
        {
            if (!held)
            {
                Vector3Int pos = sBlocInfo.coord;
                pos.y++;

                if (mapManager.ValidEmptyPos(pos))
                {
                    SelectNewBloc(mapManager.PlaceNewBloc(UIBlocAction.UIManager.UINewMap.textureIdx, pos));
                }
            }
        };

        //Mettre dans la pool le bloc sélectionné
        controls.Mapmodifier.Delete.started += _ =>
        {
            if (!held)
            {
                Vector3Int pos = sBlocInfo.coord;

                if (sBlocInfo.active)
                {
                    mapManager.DeleteBloc(selectedBloc.transform);

                    do { pos.y--; }
                    while (mapManager.map[pos.x, pos.y, pos.z] < 0);

                    SelectNewBloc(mapManager.TakeBloc(pos));
                }
            }
        };

        //Monter ou descendre le bloc
        controls.Mapmodifier.Up.started += ctx =>
        {
            if (!held)
            {
                Vector3Int pos = sBlocInfo.coord;
                pos.y += (int)ctx.ReadValue<float>();

                if (mapManager.ValidEmptyPos(pos))
                {
                    mapManager.Move(selectedBloc.transform, pos);

                    SelectNewBloc(selectedBloc, false);
                }
            }
        };
    }

    private void OnEnable()
    {
        pointedBloc = defaultBloc;
        targetBloc = defaultBloc;
        SelectNewBloc(defaultBloc);

        controls.Mapmodifier.Enable();
    }

    private void OnDisable()
    {
        controls.Mapmodifier.Disable();
    }


    void Update()
    {
        isOverInterface = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();

        if (!isOverInterface)
        {
            //Calcul du bloc pointé par le curseur
            ray = Camera.main.ScreenPointToRay(controls.Mapmodifier.Position.ReadValue<Vector2>());

            pointedBloc = PointedBloc();

            pointedCoordinates = pBlocInfo.coord;

            if (held && coordinates != pointedCoordinates && pointedBloc != targetBloc && !cameraManager.isFocused && sBlocInfo.active)
            {
                sBlocInfo.hitBox.enabled = false;

                targetBloc = pointedBloc;
                coordinates = pointedCoordinates;
                Vector3Int to = new Vector3Int(coordinates.x, coordinates.y + 1, coordinates.z);

                if (mapManager.ValidEmptyPos(to))
                {
                    mapManager.Move(selectedBloc.transform, to);
                    SelectNewBloc(selectedBloc, false);
                } 
            }

            if (!held)
            {
                sBlocInfo.hitBox.enabled = true;
            }
        }
    }


    private GameObject PointedBloc()
    {
        Physics.Raycast(ray, out hitData, 100);
        try
        {
            pointedBloc = hitData.transform.gameObject;
            pBlocInfo = pointedBloc.GetComponentInChildren<BlocInfo>();
            return pointedBloc;
        }
        catch (System.NullReferenceException)
        {
            pBlocInfo = defaultBloc.GetComponentInChildren<BlocInfo>();
            return defaultBloc;
        }
    }

    public void SelectNewBloc(GameObject bloc, bool updateSelDisplay = true, bool upDateUI = true)
    {
        selectedBloc = bloc;

        sBlocInfo = selectedBloc.GetComponent<BlocInfo>();
        coordinates = sBlocInfo.coord;

        //Debug.Log((sBlocInfo.index, coordinates));

        if (updateSelDisplay) { selDisplay.ChangeSelection(selectedBloc.transform); }

        if (upDateUI) { UIBlocAction.SelectNewBloc(selectedBloc.transform, sBlocInfo); }
    }
}
