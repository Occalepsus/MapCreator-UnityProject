using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Donne le type du bloc (donc s'il est accessible ou non)
public enum BlocType
{
    None,
    Prop,
    Surface,
    Bridge,
    Underground,
}

[System.Serializable]
public class BlocInfo : MonoBehaviour
{
    [ReadOnly]
    public Collider hitBox;             //Référence au composant collider du bloc

    public int index;                   //Indice dans la liste MapManager.blocs
    public Vector3Int coord;            //Coordonnées dans MapManager.map
    public float rotation;
    public BlocType type;
    public int textureIdx;              //Le numéro de la texture utilisée
    public bool active = true;          //Si le bloc est modifiable ou non

    public float speed = 1;

    public int[] accessible;            //[N,E,S,W] : N = Zpos, E = Xpos, ...
    [Range(0,100)]
    public int state;

    public BlocInfo(int index,
                    Vector3Int coord,
                    float rotation,
                    BlocType type,
                    int textureIdx,
                    float speed,
                    int[] accessible,
                    int state)
    {
        this.index = index;
        this.coord = coord;
        this.rotation = rotation;
        this.type = type;
        this.textureIdx = textureIdx;
        this.speed = speed;
        this.accessible = accessible;
        this.state = state;
    }

    private void Start()
    {
        hitBox = gameObject.GetComponent<Collider>();
    }
}
