using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorldUiScaler : MonoBehaviour

{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject worldUiObject;
    private GameObject selectedWorldUiObject;
    private float dist;
    [SerializeField] private Vector3 InitialScale;
    [SerializeField] private Slider WidthSlider;
    [SerializeField] private Plywoodcontroller plywoodcontroller;
    [SerializeField] private BasinMovement basinMovement;
    public float counterSize;
    

    float widthSliderInitialVal;
    float widthSliderNewVal;
    float circleIndicatorInitialVal;

    private void Start()
    {
        selectedWorldUiObject = worldUiObject.transform.GetChild(0).gameObject;
        //basinMovement.OnGameobjectSelected += BasinMovement_OnGameobjectSelected;
        widthSliderInitialVal = WidthSlider.value;
        circleIndicatorInitialVal = selectedWorldUiObject.transform.localScale.x;
        InitialScale = CalculatingSizeOfWorldUiCanvas();
        
    }

    private void BasinMovement_OnGameobjectSelected(object sender, SelectedObject e)
    {
        
        if (e == SelectedObject.counter)
        {
            selectedWorldUiObject = worldUiObject.transform.GetChild(0).gameObject;
        }
        else if (e == SelectedObject.basin)
        {
            selectedWorldUiObject = worldUiObject.transform.GetChild(1).gameObject;
        }
    }

    void Update()
    {
        
        CalculatingSizeOfWorldUiCanvas();
    }

    private Vector3 CalculatingSizeOfWorldUiCanvas()
    {
        dist = Vector3.Distance(selectedWorldUiObject.transform.position, mainCamera.transform.position);
       
        Canvas worldUiCanvas = selectedWorldUiObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Canvas>();

        Vector3 vect;

        if (dist < 11.0f)
        {
            vect = (Vector3.one * dist / 3)/2;
            //Debug.Log("DCAC 0 : " + dist + "  " + vect.x + " " + InitialScale.x);
            GetAndResizeWorldUIControlPanelButtons(vect);
        }
        else {
            vect = (Vector3.one * dist / 3) / 50;
            //Debug.Log("DCAC 1 : " + dist + "  " + vect.x + " " + InitialScale.x);
            GetAllTheWorldUiCanvas(vect);
        }

        

        if(basinMovement.selectedObject == SelectedObject.counter)
        {
            SettingTextFeildActiveAndDeactive();
        }

        return vect;
    }

    private void SettingTextFeildActiveAndDeactive()
    {
        if (dist >= 15)
        {
            plywoodcontroller.DeActiveAllTheTextFields();
        }
        if (dist < 15 && dist > 14)
        {
            plywoodcontroller.SetTextFieldsActive();
        }
    }

    void GetAllTheWorldUiCanvas(Vector3 vect)
    {
        foreach(Transform canvas in selectedWorldUiObject.transform)
        {
           
            canvas.transform.localScale = vect;
           // worldUiObject.transform.GetChild(0).transform.localScale = vect;
        }
    }

    void GetAndResizeWorldUIControlPanelButtons(Vector3 vect)
    {
        selectedWorldUiObject.transform.GetChild(0).transform.GetChild(0).transform.localScale = vect;
        selectedWorldUiObject.transform.GetChild(0).transform.GetChild(1).transform.localScale = vect;
        selectedWorldUiObject.transform.GetChild(0).transform.GetChild(2).transform.localScale = vect;
        
    }

    public void IncreaseTheCircleSize(GameObject CircleIndicatorGameObject)
    {
        widthSliderNewVal = WidthSlider.value;
        float circleIndicatorNewVal = (widthSliderNewVal * circleIndicatorInitialVal) / widthSliderInitialVal;
        CircleIndicatorGameObject.transform.localScale = Vector3.one * circleIndicatorNewVal;

    }

    
}
