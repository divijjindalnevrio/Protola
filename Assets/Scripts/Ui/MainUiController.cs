using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainUiController : MonoBehaviour
{
    private UiModel uiModel;
    private GameObject sliders;
    private GameObject ModelShapesContent;


    void Start()
    {
        // assign ui elements to dictonary!
        uiModel = GetComponent<UiModel>();
        uiModel.AddUiElementsToDictonary();
        sliders = uiModel.UiButtonsAndElementsDict["SurfaceSizeContent"].transform.Find("Sliders").gameObject;
        ModelShapesContent = uiModel.UiButtonsAndElementsDict["SinkModelContent"].transform.Find("ModelShapesContent ").gameObject;
        InitialButtoUiState();
    }

    public void InitialButtoUiState()
    {
        uiModel.SetAllUiElementsToFalse(null);
        uiModel.UiButtonsAndElementsDict["SurfaceSizeAndColorButtonsTop"].SetActive(true);
        uiModel.UiButtonsAndElementsDict["SurfaceSizeAndColorButtonsTop"].transform.Find("SurfaceSize").gameObject.GetComponent<Button>().Select();
        uiModel.UiButtonsAndElementsDict["SurfaceSizeContent"].SetActive(true);
        uiModel.UiButtonsAndElementsDict["Background"].SetActive(true);
        SetAllSlidersToFalse();
        uiModel.UiButtonsAndElementsDict["SurfaceSizeContent"].transform.Find("Sliders").transform.Find(uiModel.lastSliderActiveName).gameObject.SetActive(true);
    }

    public void SettingSliderToActive(string sliderName)
    {
        switch (sliderName)
        {
            case "Depth":
                SetAllSlidersToFalse();
                sliders.transform.Find("Depth").gameObject.SetActive(true);
                uiModel.lastSliderActiveName = "Depth";
                break;

            case "Width":
                SetAllSlidersToFalse();
                sliders.transform.Find("Width").gameObject.SetActive(true);
                uiModel.lastSliderActiveName = "Width";
                break;

            case "Thickness":
                SetAllSlidersToFalse();
                sliders.transform.Find("Thickness").gameObject.SetActive(true);
                uiModel.lastSliderActiveName = "Thickness";
                break;
        }
    }

    public void SettingSinkShapeContentToActive(string sinkShape)
    {
        switch (sinkShape)
        {
            case "Round":
                SetAllSinkShapeContentToFalse();
                ModelShapesContent.transform.Find("Round").gameObject.SetActive(true);
                uiModel.lastModelShapeActiveName = "Round";
                break;

            case "Oval":
                SetAllSinkShapeContentToFalse();
                ModelShapesContent.transform.Find("Oval").gameObject.SetActive(true);
                uiModel.lastModelShapeActiveName = "Oval";
                break;

            case "Block":
                SetAllSinkShapeContentToFalse();
                ModelShapesContent.transform.Find("Block").gameObject.SetActive(true);
                uiModel.lastModelShapeActiveName = "Block";
                break;

            case "Kehto":
                SetAllSinkShapeContentToFalse();
                ModelShapesContent.transform.Find("Kehto").gameObject.SetActive(true);
                uiModel.lastModelShapeActiveName = "Kehto";
                break;
        }
    }

    public void GoingBackToPreviousUiState()
    {
        switch (uiModel.TopActiveButton)
        {
            case "SurfaceSize":
                InitialButtoUiState();
                uiModel.TopActiveButton = "SurfaceSize";
                break;

            case "SurfaceColor":
                uiModel.UiButtonsAndElementsDict["SurfaceSizeAndColorButtonsTop"].SetActive(true);
                SelectingSurfaceColor();
                uiModel.TopActiveButton = "SurfaceSize";
                break;

            case "AddObject":
                uiModel.UiButtonsAndElementsDict["SurfaceSizeAndColorButtonsTop"].SetActive(true);
                AddObject();
                uiModel.TopActiveButton = "SurfaceSize";
                break;
        }

    }

    private void SetAllSlidersToFalse()
    {
        foreach (Transform child in sliders.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void SetAllSinkShapeContentToFalse()
    {
        foreach (Transform child in ModelShapesContent.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void SelectingSurfaceColor()
    {
        SetBackgroundAndContenetActive("SurfaceSizeAndColorButtonsTop");
        GameObject surfaceColorContent = uiModel.UiButtonsAndElementsDict["SurfaceColorContent"];
        surfaceColorContent.SetActive(true);  
    }
    public void SelectingSinkSurfaceColor()
    {
        SetBackgroundAndContenetActive("SinkButtonsTop");
        uiModel.UiButtonsAndElementsDict["SurfaceColorContent"].SetActive(true);
    }

    public void PickColor()
    {
        SetBackgroundAndContenetActive("PalaceAndBaseColorButtonsTop");
        uiModel.UiButtonsAndElementsDict["PalaceAndBaseColorButtonsTop"].SetActive(true);
        uiModel.UiButtonsAndElementsDict["PalaceColorsContent"].transform.gameObject.SetActive(true);
    }

    public void Granulates()
    {
        SetBackgroundAndContenetActive("PalaceAndBaseColorButtonsTop");
        uiModel.UiButtonsAndElementsDict["GranulatesContent"].transform.gameObject.SetActive(true);

    }
    public void BaseColor()
    {
        SetBackgroundAndContenetActive("PalaceAndBaseColorButtonsTop");
        uiModel.UiButtonsAndElementsDict["BaseColorsContent"].SetActive(true);

    }

    public void AddObject()
    {
        SetBackgroundAndContenetActive("SurfaceSizeAndColorButtonsTop");
        uiModel.UiButtonsAndElementsDict["AddObjectContent"].SetActive(true);
    }
        
    public void ShowBasinModelUi()
    {
        SetBackgroundAndContenetActive("SinkButtonsTop");   // active top buttons here 
        uiModel.UiButtonsAndElementsDict["SinkButtonsTop"].transform.gameObject.SetActive(true);
        uiModel.UiButtonsAndElementsDict["SinkModelContent"].SetActive(true);
        SettingSinkShapeContentToActive(uiModel.lastModelShapeActiveName);                       // <--- checking here the last active model shape
    }

    private void SetBackgroundAndContenetActive(string contenet)
    {
        uiModel.SetAllUiElementsToFalse(contenet);
        uiModel.UiButtonsAndElementsDict["Background"].SetActive(true);
    }


    ///////
    ///

    private void PreviousSurfaceColor()
    {
        uiModel.UiButtonsAndElementsDict["SurfaceColorContent"].gameObject.SetActive(true);
        GameObject surfaceColorContent = uiModel.UiButtonsAndElementsDict["SurfaceColorContent"];
        surfaceColorContent.SetActive(true);

    }
}