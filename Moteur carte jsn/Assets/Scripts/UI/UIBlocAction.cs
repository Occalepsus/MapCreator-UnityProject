using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBlocAction : MonoBehaviour
{
    public UIManager UIManager;
    public UIRender UIRender;
    public SelectBloc selectBloc;
    public MapManager mapManager;


    [Header("Quick Action")]
    public Button blocOptionsButton;
    public Button xp;
    public Button xn;
    public Button yp;
    public Button yn;
    public Button zp;
    public Button zn;

    public Button newBlocButton;

    public TMP_InputField xCoord;
    public TMP_InputField yCoord;
    public TMP_InputField zCoord;

    public Slider rotationSlider;
    public TMP_InputField rotationValue;

    public Slider vitesseSlider;
    public TMP_InputField vitesseValue;

    [Header("Options action")]
    public TMP_Dropdown type;

    public Slider etatSlider;
    public TMP_InputField etatValue;

    public Button NAccButton;
    public TMP_Text NAccText;
    public Button EAccButton;
    public TMP_Text EAccText;
    public Button SAccButton;
    public TMP_Text SAccText;
    public Button WAccButton;
    public TMP_Text WAccText;

    public Toggle autoChange;

    private Transform selectedBloc;
    [ReadOnly]
    public BlocInfo blocInfo;

    public void SelectNewBloc(Transform selectedBloc)
    {
        SelectNewBloc(selectedBloc, selectedBloc.GetComponent<BlocInfo>());
    }

    public void SelectNewBloc(Transform selectedBloc, BlocInfo blocInfo)
    {
        this.selectedBloc = selectedBloc;
        this.blocInfo = blocInfo;

        UIManager.blocActionInterfaceOpened = true;

        if (blocInfo.active)
        {
            UIManager.OpenBlocInterface(true);

            Vector3Int coord = blocInfo.coord;
            UpdateCoordinates(coord);

            rotationSlider.value = blocInfo.rotation;
            rotationValue.text = blocInfo.rotation.ToString();

            vitesseSlider.value = blocInfo.speed;
            vitesseValue.text = blocInfo.speed.ToString();

            type.value = (int)blocInfo.type;

            etatSlider.value = blocInfo.state;
            etatValue.text = blocInfo.state.ToString();

            NAccText.text = blocInfo.accessible[0].ToString();
            EAccText.text = blocInfo.accessible[1].ToString();
            SAccText.text = blocInfo.accessible[2].ToString();
            WAccText.text = blocInfo.accessible[3].ToString();

            UIRender.SetSelectedTexture(blocInfo.textureIdx, false, false, false);

            newBlocButton.enabled = mapManager.ValidEmptyPos(coord.x, coord.y + 1, coord.z);
        }
        else { UIManager.Deselect(); }
    }


    private void UpdateCoordinates(Vector3Int coord)
    {
        xp.enabled = mapManager.ValidEmptyPos(coord.x + 1, coord.y, coord.z);
        xn.enabled = mapManager.ValidEmptyPos(coord.x - 1, coord.y, coord.z);
        yp.enabled = mapManager.ValidEmptyPos(coord.x, coord.y + 1, coord.z);
        yn.enabled = mapManager.ValidEmptyPos(coord.x, coord.y - 1, coord.z);
        zp.enabled = mapManager.ValidEmptyPos(coord.x, coord.y, coord.z + 1);
        zn.enabled = mapManager.ValidEmptyPos(coord.x, coord.y, coord.z - 1);

        xCoord.text = (coord.x + 1).ToString();
        yCoord.text = coord.y.ToString();
        zCoord.text = (coord.z + 1).ToString();
    }



    //Programmes modifiant les valeurs sur l'interface

    public void MoveOnePosition(string dir)
    {
        Vector3Int to = blocInfo.coord;
        if (dir == "xpos") { to.x++; }
        else if (dir == "xneg") { to.x--; }
        else if (dir == "ypos") { to.y++; }
        else if (dir == "yneg") { to.y--; }
        else if (dir == "zpos") { to.z++; }
        else if (dir == "zneg") { to.z--; }
        mapManager.Move(selectedBloc, to);

        UpdateCoordinates(to);
    }


    public void MoveMultiplePosition()
    {
        int x = int.Parse(xCoord.text) - 1;
        int y = int.Parse(yCoord.text);
        int z = int.Parse(zCoord.text) - 1;

        if (mapManager.ValidEmptyPos(x, y, z))
        {
            Vector3Int coord = new Vector3Int(x, y, z);
            mapManager.Move(selectBloc.selectedBloc.transform, coord);

            UpdateCoordinates(coord);
        }
        else
        {
            //try
            {
                xCoord.text = (blocInfo.coord.x + 1).ToString();
                yCoord.text = blocInfo.coord.y.ToString();
                zCoord.text = (blocInfo.coord.z + 1).ToString();
            }
            //catch (System.NullReferenceException) { };
        }
    }

    public void AddNewBloc()
    {
        Vector3Int to = blocInfo.coord;
        to.y++;
        selectBloc.SelectNewBloc(mapManager.PlaceNewBloc(UIManager.UINewMap.textureIdx, to));

        newBlocButton.enabled = mapManager.ValidEmptyPos(to.x, to.y + 1, to.z);
    }

    public void ChangeRotationValue()
    {
        int rotation = (int)rotationSlider.value;
        rotationValue.text = rotation.ToString();
        mapManager.RotateBloc(selectedBloc, rotation);
    }
    public void ChangeRotationSlider()
    {
        int rotation = int.Parse(rotationValue.text) % 360;
        Debug.Log(rotation);
        rotationValue.text = rotation.ToString();
        rotationSlider.value = rotation;
        mapManager.RotateBloc(selectedBloc, rotation);
    }

    public void ChangeVitesseValue()
    {
        float speed = vitesseSlider.value;
        vitesseValue.text = speed.ToString("F1");
        blocInfo.speed = speed;
    }
    public void ChangeVitesseSlider()
    {
        float speed = float.Parse(vitesseValue.text);
        vitesseSlider.value = speed;
        blocInfo.speed = speed;
    }


    public void ChangeType()
    {
        blocInfo.type = (BlocType)type.value;
    }

    public void ChangeStateValue()
    {
        int state = (int)etatSlider.value;
        etatValue.text = state.ToString();
        blocInfo.state = state;

        mapManager.ChangeTextureState(selectedBloc);
        UIRender.ChangeTextureState(state);
    }
    public void ChangeStateSlider()
    {
        int state = int.Parse(etatValue.text);
        state = state > 100 ? 100 : state;        //Si state est supérieur à 100, on le ramène à 100

        etatValue.text = state.ToString();
        etatSlider.value = state;
        blocInfo.state = state;

        mapManager.ChangeTextureState(selectedBloc);
        UIRender.ChangeTextureState(state);
    }

    public void ChangeTexture(int texIdx)
    {
        mapManager.ChangeTextureContainer(selectedBloc, texIdx, autoChange.isOn);
    }

    public void ChangeNaccState()
    {
        NAccText.text = CycleAccessibility(0).ToString();
    }
    public void ChangeEaccState()
    {
        EAccText.text = CycleAccessibility(1).ToString();
    }
    public void ChangeSaccState()
    {
        SAccText.text = CycleAccessibility(2).ToString();
    }
    public void ChangeWaccState()
    {
        WAccText.text = CycleAccessibility(3).ToString();
    }
    public int CycleAccessibility(int dir)
    {
        //On auguemente accessible de 1 niveau sauf si il est déjà trop haut
        blocInfo.accessible[dir] = ((blocInfo.accessible[dir] + 2) % 3) - 1;
        return blocInfo.accessible[dir];
    }
}