using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUiController : MonoBehaviour
{
    private UiModel uiModel;
    private GameObject sliders;

    void Start()
    {
        // assign ui elements to dictonary!
        uiModel = GetComponent<UiModel>();
        uiModel.AddUiElementsToDictonary();
        sliders = uiModel.UiBuoonsAndElementsDict["SurfaceSizeContent"].transform.Find("Sliders").gameObject;
        InitialButtoUiState();

        Debug.Log("AddUiElementsToDictonary : " + uiModel.UiBuoonsAndElementsDict["SurfaceSizeAndColorButtonsTop"].name  + uiModel.UiBuoonsAndElementsDict.Count);
    }

    public void InitialButtoUiState()
    {
        uiModel.UiBuoonsAndElementsDict["SurfaceSizeAndColorButtonsTop"].SetActive(true);
        uiModel.UiBuoonsAndElementsDict["SurfaceSizeContent"].SetActive(true);
        uiModel.UiBuoonsAndElementsDict["Background"].SetActive(true);
        uiModel.UiBuoonsAndElementsDict["SurfaceSizeContent"].transform.Find("Sliders").transform.Find("Width").gameObject.SetActive(true);
    }

    public void SettingSliderToActive(string sliderName)
    {
        switch(sliderName)
        {
            case "Depth":
                SetAllSlidersToFalse();
                uiModel.UiBuoonsAndElementsDict["SurfaceSizeContent"].transform.Find("Sliders").transform.Find("Depth").gameObject.SetActive(true);
                break;

            case "Width":
                SetAllSlidersToFalse();
                uiModel.UiBuoonsAndElementsDict["SurfaceSizeContent"].transform.Find("Sliders").transform.Find("Width").gameObject.SetActive(true);
                break;

            case "Thickness":
                SetAllSlidersToFalse();
                uiModel.UiBuoonsAndElementsDict["SurfaceSizeContent"].transform.Find("Sliders").transform.Find("Thickness").gameObject.SetActive(true);
                break;
        }
    }

    private void SetAllSlidersToFalse()
    {
        foreach(Transform child in sliders.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
