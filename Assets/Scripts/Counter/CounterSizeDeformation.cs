using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterSizeDeformation : MonoBehaviour
{
    [SerializeField] private Slider widthSlider;
    [SerializeField] private Slider thicknessSlider;
    [SerializeField] private Slider depthSlider;
    
    private float width;
    private float thickness;
    private float depth;
    private CounterGenerator counterGenerator;
    [SerializeField] private Transform currentCounter;
    [SerializeField] private BasinMovement basinMovement;
    void Start()
    {
        counterGenerator = GetComponent<CounterGenerator>();
        currentCounter = counterGenerator.currentCounter.transform;
        counterGenerator.OnCounterAdded += GettingCurrentCounter;
        basinMovement.OnGameobjectSelected += BasinMovement_OnGameobjectSelected;

    }

    private void BasinMovement_OnGameobjectSelected(object sender, SelectedObject e)
    {
        if(e == SelectedObject.counter)
        {
            GettingCurrentCounter();
        }
    }

    private void Update()
    {
        ChangingSizeOfCounter();

    }

    private void ChangingSizeOfCounter()
    {
        width = widthSlider.value;
        thickness = thicknessSlider.value;
        depth = depthSlider.value;

       currentCounter.transform.Find("Counter").transform.localScale = new Vector3(width, currentCounter.transform.Find("Counter").transform.localScale.y, depth);
       currentCounter.position = new Vector3(currentCounter.position.x, thickness, currentCounter.position.z);

    }

    private void GettingCurrentCounter()
    {
        currentCounter = basinMovement.currentCounter.transform;
    }

    
}
