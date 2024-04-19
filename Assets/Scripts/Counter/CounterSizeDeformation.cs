using System;
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
    [SerializeField] private SerializationToJson serializationToJson;
    public Vector3 currentCounterPosition;
    [SerializeField] private Color colour;
    [SerializeField] private CounterSurfaceChanger counterSurfaceChanger;


    void Start()
    {
        counterGenerator = GetComponent<CounterGenerator>();
        currentCounter = counterGenerator.currentCounter.transform;
        counterGenerator.OnCounterAdded += GettingCurrentCounter;
        basinMovement.OnGameobjectSelected += BasinMovement_OnGameobjectSelected;
        SetTheInitialCounterfromJson();
        ChangingSizeOfCounter();

    }

    private void BasinMovement_OnGameobjectSelected(object sender, SelectedObject e)
    {
        if(e == SelectedObject.counter)
        {
            GettingCurrentCounter();
            SetTheSizeValueOfCurrentCounter();
        }

    }

    public void SetTheSizeValueOfCurrentCounter()
    {
       
       // depthSlider.value = currentCounter.transform.Find("Counter").transform.localScale.z;
        depthSlider.SetValueWithoutNotify(currentCounter.transform.Find("Counter").transform.localScale.z);
        widthSlider.SetValueWithoutNotify(currentCounter.transform.Find("Counter").transform.localScale.x);
        thicknessSlider.SetValueWithoutNotify(currentCounter.transform.position.y);

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
        currentCounter = basinMovement.currentCounter.transform;
    }

    public void SetTheInitialCounterfromJson()
    {
        CounterModel model = serializationToJson.ReadingJson();
        if(model != null)
        {
            Debug.Log("SetTheInitialSizeOfCounter eneterd : " + model.depth);
            Transform counter = currentCounter.transform.Find("Counter").transform;
            counter.localScale = new Vector3(model.width, counter.localScale.y, model.depth);
            basinMovement.currentCounter.transform.position = new Vector3(currentCounter.localScale.x, model.thickness, currentCounter.localScale.z);
            currentCounter.rotation = Quaternion.Euler(model.rotaton);
            currentCounter.position = model.position;

            //setting color below
           
           
            ColorUtility.TryParseHtmlString(model.colourHexCode, out colour);
            currentCounter.Find("Counter").GetComponent<MeshRenderer>().materials[1].color = colour;
            currentCounter.Find("Counter").GetComponent<MeshRenderer>().materials[0].SetTexture("_AlphaTexture", Texture2D.whiteTexture);
            SetTheSizeValueOfCurrentCounter();

            // setting texture below

            Texture2D mainTexture = counterSurfaceChanger.AllTextures[model.texture];
            currentCounter.Find("Counter").GetComponent<MeshRenderer>().materials[0].SetTexture("_Texture2D", mainTexture);

            // setting alpha
            if(model.alphaTexture != "UnityWhite")
            {
                // for granulate
                Texture2D alphaTexture = counterSurfaceChanger.AllTextures[model.alphaTexture];                                         
                currentCounter.Find("Counter").GetComponent<MeshRenderer>().materials[0].SetTexture("_AlphaTexture", alphaTexture);
            }
            else
            {
                currentCounter.Find("Counter").GetComponent<MeshRenderer>().materials[0].SetTexture("_AlphaTexture", Texture2D.whiteTexture);
            }



        }
       
    }

  
}
