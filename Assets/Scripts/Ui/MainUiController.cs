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
    private Transform SurfaceSizeAndColorButtonsTop;
    private Transform palaceAndBaseColorButtonsTop;
    private Transform sinkButtonsTop;
    private Transform baseColorsContent;
    [SerializeField] UiButtonsGenerator uiButtonsGenerator;



    void Start()
    {
        // assign ui elements to dictonary!
        uiModel = GetComponent<UiModel>();
        uiModel.AddUiElementsToDictonary();
        sliders = uiModel.UiButtonsAndElementsDict["SurfaceSizeContent"].transform.Find("Sliders").gameObject;
        ModelShapesContent = uiModel.UiButtonsAndElementsDict["SinkModelContent"].transform.Find("ModelShapesContent ").gameObject;
        GettingAllTopMainThreeUiButtons();
        InitialButtoUiState();
       // GeneratingPalaceButtons();
    }

    private void GettingAllTopMainThreeUiButtons()
    {
        SurfaceSizeAndColorButtonsTop = uiModel.UiButtonsAndElementsDict["SurfaceSizeAndColorButtonsTop"].transform;
        palaceAndBaseColorButtonsTop = uiModel.UiButtonsAndElementsDict["PalaceAndBaseColorButtonsTop"].transform;
        sinkButtonsTop = uiModel.UiButtonsAndElementsDict["SinkButtonsTop"].transform;
        baseColorsContent = uiModel.UiButtonsAndElementsDict["BaseColorsContent"].transform;
    }
    private void GeneratingPalaceButtons()
    {
        Transform textureButtonTrans = baseColorsContent.transform.Find("SelectAndPick").transform.Find("TexturesButtons").transform;
        uiButtonsGenerator.ButtonGenerator(9, uiModel.PalaceColorTemplate, textureButtonTrans);
        uiButtonsGenerator.AddColorToPalaceTextureButtons();
    }


    public void InitialButtoUiState()
    {
        uiModel.SetAllUiElementsToFalse(null);
        SurfaceSizeAndColorButtonsTop.gameObject.SetActive(true);
        DeselectAllButtons(SurfaceSizeAndColorButtonsTop);
        SurfaceSizeAndColorButtonsTop.Find("SurfaceSize").transform.Find("Background")
        .GetComponent<Image>().color = Color.cyan;
        uiModel.UiButtonsAndElementsDict["SurfaceSizeContent"].SetActive(true);
        uiModel.UiButtonsAndElementsDict["Background"].SetActive(true);
       // SetAllSlidersToFalse();
        //uiModel.UiButtonsAndElementsDict["SurfaceSizeContent"].transform.Find("Sliders").transform.Find(uiModel.lastSliderActiveName).gameObject.SetActive(true);
        uiModel.UiButtonsAndElementsDict["SurfaceSizeContent"].transform.Find("Buttons").transform.Find(uiModel.lastSliderActiveName)
        .GetComponent<Button>().Select();
            
    }

    public void SettingSliderToActive(string sliderName)
    {
        switch (sliderName)
        {
            case "Depth":
                SetAllSlidersToFalse();
                sliders.transform.Find("Depth").gameObject.SetActive(true);
                uiModel.lastSliderActiveName = "Depth";
                SurfaceSizeAndColorButtonsTop.Find("SurfaceSize").transform
                .Find("Background").GetComponent<Image>().color = Color.cyan;
                break;

            case "Width":
                SetAllSlidersToFalse();
                sliders.transform.Find("Width").gameObject.SetActive(true);
                uiModel.lastSliderActiveName = "Width";
                SurfaceSizeAndColorButtonsTop.Find("SurfaceSize").transform
                .Find("Background").GetComponent<Image>().color = Color.cyan;
                break;

            case "Thickness":
                SetAllSlidersToFalse();
                sliders.transform.Find("Thickness").gameObject.SetActive(true);
                uiModel.lastSliderActiveName = "Thickness";
                SurfaceSizeAndColorButtonsTop.Find("SurfaceSize").transform
               .Find("Background").GetComponent<Image>().color = Color.cyan;
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
                SurfaceSizeAndColorButtonsTop.gameObject.SetActive(true);
                SelectingSurfaceColor();
                uiModel.TopActiveButton = "SurfaceSize";
                break;

            case "AddObject":
                SurfaceSizeAndColorButtonsTop.gameObject.SetActive(true);
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
        DeselectAllButtons(SurfaceSizeAndColorButtonsTop);
        SurfaceSizeAndColorButtonsTop.Find("SurfaceColor").transform.Find("Background")
        .GetComponent<Image>().color = Color.cyan;
        SetBackgroundAndContenetActive("SurfaceSizeAndColorButtonsTop");
        GameObject surfaceColorContent = uiModel.UiButtonsAndElementsDict["SurfaceColorContent"];
        surfaceColorContent.SetActive(true);  
    }
    public void SelectingSinkSurfaceColor()
    {
        DeselectAllButtons(sinkButtonsTop);
        sinkButtonsTop.Find("SinkColor").transform.Find("Background")
        .GetComponent<Image>().color = Color.cyan;
        SetBackgroundAndContenetActive("SinkButtonsTop");
        uiModel.UiButtonsAndElementsDict["SurfaceColorContent"].SetActive(true);
    }

    public void PickColor()
    {
        DeselectAllButtons(palaceAndBaseColorButtonsTop);
        palaceAndBaseColorButtonsTop.Find("PalaceColor").Find("Background").GetComponent<Image>().color = Color.cyan;
        SetBackgroundAndContenetActive("PalaceAndBaseColorButtonsTop");
        uiModel.UiButtonsAndElementsDict["PalaceAndBaseColorButtonsTop"].SetActive(true);
        uiModel.UiButtonsAndElementsDict["PalaceColorsContent"].transform.gameObject.SetActive(true);
    }

    public void Granulates()
    {
        DeselectAllButtons(palaceAndBaseColorButtonsTop);
        palaceAndBaseColorButtonsTop.Find("Granulates").Find("Background").GetComponent<Image>().color = Color.cyan;
        SetBackgroundAndContenetActive("PalaceAndBaseColorButtonsTop");
        uiModel.UiButtonsAndElementsDict["GranulatesContent"].transform.gameObject.SetActive(true);

    }
    public void BaseColor()
    {
        DeselectAllButtons(palaceAndBaseColorButtonsTop);
        palaceAndBaseColorButtonsTop.Find("BaseColor").Find("Background").GetComponent<Image>().color = Color.cyan;
        SetBackgroundAndContenetActive("PalaceAndBaseColorButtonsTop");
        baseColorsContent.gameObject.SetActive(true);
       

    }

    public void AddObject()
    {
        DeselectAllButtons(SurfaceSizeAndColorButtonsTop);
        SurfaceSizeAndColorButtonsTop.Find("AddObject").transform.Find("Background")
        .GetComponent<Image>().color = Color.cyan;
        SetBackgroundAndContenetActive("SurfaceSizeAndColorButtonsTop");
        uiModel.UiButtonsAndElementsDict["AddObjectContent"].SetActive(true);
    }
        
    public void ShowBasinModelUi()
    {
        DeselectAllButtons(sinkButtonsTop);
       
        sinkButtonsTop.Find("Model").transform.Find("Background")
        .GetComponent<Image>().color = Color.cyan;
        SetBackgroundAndContenetActive("SinkButtonsTop");   // active top buttons here 
        uiModel.UiButtonsAndElementsDict["SinkButtonsTop"].transform.gameObject.SetActive(true);
        uiModel.UiButtonsAndElementsDict["SinkModelContent"].SetActive(true);
        uiModel.UiButtonsAndElementsDict["SinkModelContent"].transform.Find("Buttons").transform.Find("Round").GetComponent<Button>().Select();
        SettingSinkShapeContentToActive(uiModel.lastModelShapeActiveName);                       // <--- checking here the last active model shape
    }

    private void SetBackgroundAndContenetActive(string contenet)
    {
        uiModel.SetAllUiElementsToFalse(contenet);
        uiModel.UiButtonsAndElementsDict["Background"].SetActive(true);
    }

   
    public void HighLightButton(Transform buttonTransform, string name)
    {
        foreach (Transform child in buttonTransform)
        {
            if (child.name == name) continue;
            child.Find("SelectedState").transform.gameObject.SetActive(false);
        }

    }

    private void DeselectAllButtons(Transform topButtons)
    {
        foreach (Transform child in topButtons)
        {
            child.Find("Background").GetComponent<Image>().color = Color.white;
        }

    }

    
   
   
}