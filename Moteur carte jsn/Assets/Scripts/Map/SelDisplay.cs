using System.Collections;
using UnityEngine;


public class SelDisplay : MonoBehaviour
{
    public MapManager mapManager;

    public GameObject renderingPrefab;
    public Transform renderingBloc;

    public void ChangeSelection(Transform selectedBloc)
    {
        renderingBloc.parent = selectedBloc;
        renderingBloc.localPosition = Vector3.zero;
    }
}