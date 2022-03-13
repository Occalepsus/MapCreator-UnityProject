using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class BiomeDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [ReadOnly]
    public BiomeManager biomeManager;

    public RectTransform panel;
    private Image panelImage;
    public RawImage textureDisp;

    public TMP_Text nameDisp;
    public int biomeIdx;

    [ReadOnly]
    public bool isOn;

    public void SetDisplay(BiomeManager biomeManager, Texture texture)
    {
        this.biomeManager = biomeManager;

        panelImage = panel.GetComponent<Image>();
        panelImage.color = biomeManager.defaultColor;
        textureDisp.texture = texture;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        panel.sizeDelta *= biomeManager.largeRate;
        if (!isOn) { panelImage.color = biomeManager.overColor; }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        panel.sizeDelta = biomeManager.defRenderSize;
        panelImage.color = isOn ? biomeManager.selectedColor : biomeManager.defaultColor;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        panel.sizeDelta = biomeManager.defRenderSize;
        biomeManager.SetSelectedBiome(biomeIdx, true);
    }

    public void SelectDisplay(bool select)
    {
        panelImage.color = select ? biomeManager.selectedColor : biomeManager.defaultColor;
        isOn = select;
    }
}
