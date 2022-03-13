using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TextureDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private UIRender UIRender;
    [ReadOnly]
    public TextureRender TR;

    public RectTransform panel;
    private Image panelImage;
    public RawImage textureDisp;

    public TMP_Text nameDisp;
    public int texIdx;

    [ReadOnly]
    public bool isOn;
    [ReadOnly]
    public bool isNMI;


    public void SetDisplay(UIRender UI)
    {
        UIRender = UI;

        panelImage = panel.GetComponent<Image>();
        panelImage.color = UIRender.defaultColor;
        textureDisp.texture = TR._camera.activeTexture;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        panel.sizeDelta *= UIRender.largeRate;
        if (!isOn) { panelImage.color = UIRender.overColor; }        

        TR.rotSpeed = UIRender.overRotSpeed;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        panel.sizeDelta = UIRender.defRenderSize;
        panelImage.color = isOn ? UIRender.selectedColor : UIRender.defaultColor;

        TR.rotSpeed = UIRender.defaultRotSpeed;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        panel.sizeDelta = UIRender.defRenderSize;
        UIRender.SetSelectedTexture(texIdx, isNMI, isNMI, !isNMI);
    }

    public void SelectDisplay(bool select)
    {
        panelImage.color = select ? UIRender.selectedColor : UIRender.defaultColor;
        isOn = select;
    }
}
