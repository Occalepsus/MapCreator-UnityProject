using System.Collections.Generic;
using UnityEngine;

public class TextureRender : MonoBehaviour
{
    private UIRender UIRender;

    public Transform cameraContainer;
    public Camera _camera;
    private RenderTexture renderTexture;
    public Transform container;
    private List<GameObject> textures;

    [ReadOnly]
    public float rotSpeed;

    public void SetRender(GameObject[] stateTextures, int active, UIRender UI)
    {
        UIRender = UI;
        textures = new List<GameObject>();

        renderTexture = new RenderTexture(UIRender.defRenderTexture);
        _camera.targetTexture = renderTexture;

        for (int i = 0; i < stateTextures.Length; i++)
        {
            GameObject texture = Instantiate(stateTextures[i],
                                             container);
            textures.Add(texture);
            SetFamilyLayer(texture.transform);
        }
        ChangeActive(active);
    }

    private void Update()
    {
        container.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
        cameraContainer.localRotation = UIRender.cameraManager.container.localRotation;

        renderTexture.Release();
    }

    public void ChangeActive(int active)
    {
        for (int i = 0; i < textures.Count; i++)
        {
            textures[i].SetActive(i == active);
        }
    }

    private void SetFamilyLayer(Transform @object)
    {
        @object.gameObject.layer = 12;

        Transform[] children = new Transform[@object.transform.childCount];
        for (int i = 0; i < children.Length; i++)
        {
            children[i] = @object.transform.GetChild(i);
        }

        foreach (Transform child in children)
        {
            SetFamilyLayer(child);
        }
    }
}