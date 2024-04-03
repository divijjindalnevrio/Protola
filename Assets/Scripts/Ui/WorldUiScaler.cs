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
        widthSliderInitialVal = WidthSlider.value;
        circleIndicatorInitialVal = worldUiObject.transform.localScale.x;
        InitialScale = CalculatingSizeOfWorldUiCanvas();
        
    }

    void Update()
    {
        CalculatingSizeOfWorldUiCanvas();
    }

    private Vector3 CalculatingSizeOfWorldUiCanvas()
    {
        dist = Vector3.Distance(worldUiObject.transform.position, mainCamera.transform.position);
        Debug.Log("Distance counter and camera : " + dist);
        Canvas worldUiCanvas = worldUiObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Canvas>();
        Vector3 vect = (Vector3.one * dist / 3) / 50;
        if (vect.x >= InitialScale.x && vect.x <= 0.13f)
        {
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
        foreach(Transform canvas in worldUiObject.transform)
        {
            canvas.transform.localScale = vect;
           // worldUiObject.transform.GetChild(0).transform.localScale = vect;
        }
    }

    public void IncreaseTheCircleSize(GameObject CircleIndicatorGameObject)
    {
        widthSliderNewVal = WidthSlider.value;
        float circleIndicatorNewVal = (widthSliderNewVal * circleIndicatorInitialVal) / widthSliderInitialVal;
        CircleIndicatorGameObject.transform.localScale = Vector3.one * circleIndicatorNewVal;

    }

    
}
