using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterSizeDeformation : MonoBehaviour
{
    [SerializeField] private Slider widthSlider;
    [SerializeField] private Slider thicknessSlider;
    [SerializeField] private Slider depthSlider;

    [SerializeField] private float width;
    [SerializeField] private float thickness;
    [SerializeField] private float depth;
    private CounterGenerator counterGenerator;
    [SerializeField] private Transform currentCounter;
    [SerializeField] private BasinMovement basinMovement;
    public Vector3 currentCounterPosition;

    void Start()
    {
        counterGenerator = GetComponent<CounterGenerator>();
        currentCounter = counterGenerator.currentCounter.transform;
        counterGenerator.OnCounterAdded += GettingCurrentCounter;
        basinMovement.OnGameobjectSelected += BasinMovement_OnGameobjectSelected;
        ChangingSizeOfCounter();

    }

    private void BasinMovement_OnGameobjectSelected(object sender, SelectedObject e)
    {
        if(e == SelectedObject.counter)
        {
            currentCounter = basinMovement.currentCounter.transform;
            Debug.Log("CURRENT COUNTER NAME : " + currentCounter.transform.name + currentCounter.transform.Find("Counter").transform.localScale);
            SetTheSizeValueOfCurrentCounter();
        }


    }

    private void Update()
    {
       // Debug.Log("SetTheSizeValueOfCurrentCounter : counterWidth : " + currentCounter.transform.localScale);
        
        //ChangingSizeOfCounter();

    }


    public void SetTheSizeValueOfCurrentCounter()
    {
        Debug.Log("CURRENT COUNTER NAME PROBLEM ");
        Debug.Log("SetTheSizeValueOfCurrentCounter : counterWidth :");
        depthSlider.value = currentCounter.transform.Find("Counter").transform.localScale.z;
        //widthSlider.value = currentCounter.transform.Find("Counter").transform.localScale.x;
        //thicknessSlider.value = currentCounter.transform.localPosition.y;
        
    }

    public void ChangingSizeOfCounter()
    {
        width = widthSlider.value;
        thickness = thicknessSlider.value;
        depth = depthSlider.value;

       currentCounter.transform.Find("Counter").transform.localScale = new Vector3(width, currentCounter.transform.Find("Counter").transform.localScale.y, depth);
       currentCounter.position = new Vector3(currentCounter.position.x, thickness, currentCounter.position.z);
        currentCounterPosition = currentCounter.transform.localPosition;
    }

    private void GettingCurrentCounter()
    {
        
    }

    
}
