using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private Plywoodcontroller plywoodcontroller;
    [SerializeField] private CheckAndCreateCounterCopyScript checkAndCreateCounterCopyScript;
    [SerializeField] private ButtonClickScript buttonClickScript;

    [SerializeField] private List<GameObject> counters = new List<GameObject>();

    void Start()
    {
        plywoodcontroller.AssignPlywood();
        plywoodcontroller.GetAllPlywoods();

        counterGenerator = GetComponent<CounterGenerator>();
        currentCounter = counterGenerator.currentCounter.transform;
        counterGenerator.OnCounterAdded += GettingCurrentCounter;
        basinMovement.OnGameobjectSelected += BasinMovement_OnGameobjectSelected;
        ChangingSizeOfCounter();
        ConstructSceneFromSceneModel();
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
        depthSlider.SetValueWithoutNotify(currentCounter.transform.Find("Counter").transform.localScale.z);
        widthSlider.SetValueWithoutNotify(currentCounter.transform.Find("Counter").transform.localScale.x);
        thicknessSlider.SetValueWithoutNotify(currentCounter.transform.position.y);
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

    public void ConstructSceneFromSceneModel()
    {
        SceneModel Scenemodel = serializationToJson.DeserializingJson();
        for(int i = 0; i < Scenemodel.allCounters.Count - 1; i++)
        {
            buttonClickScript.AddCounter(i);
        }
        counters = checkAndCreateCounterCopyScript.ConvertingCounterDictToList();

        foreach (int i in Enumerable.Range(0, Scenemodel.allCounters.Count))
        {
            CounterModel model = Scenemodel.allCounters[i];
            Debug.Log("Each Counter value 101: " + counters[i].name);

            Transform counter = counters[i].transform;
            counter.Find("Counter").localScale = new Vector3(model.width, 0.07f, model.depth);
            counter.transform.position = new Vector3(counter.localScale.x, model.thickness, counter.localScale.z);
            counter.rotation = Quaternion.Euler(model.rotaton);
            counter.position = model.position;
            SetTheSizeValueOfCurrentCounter();
            Debug.Log("ConstructSceneFromSceneModel ALL texture value : " + counterSurfaceChanger.AllTextures.Count);
            //setting color below

            ColorUtility.TryParseHtmlString(model.colourHexCode, out colour);
            counter.Find("Counter").GetComponent<MeshRenderer>().materials[1].color = colour;
            counter.Find("Counter").GetComponent<MeshRenderer>().materials[0].SetTexture("_AlphaTexture", Texture2D.whiteTexture);

            // setting texture below
         
            Texture2D mainTexture = counterSurfaceChanger.AllTextures[model.texture];
            counter.Find("Counter").GetComponent<MeshRenderer>().materials[0].SetTexture("_Texture2D", mainTexture);

            //plywood length changing below

            GameObject plywoodCubeParent = counter.Find("Counter").Find("PlywoodCubes").gameObject;
            List<Transform> plywoods = new List<Transform>();

            foreach(Transform textField in plywoodCubeParent.transform)
            {
                plywoods.Add(textField);
            }

            foreach (int j in Enumerable.Range(0, plywoods.Count))
            {
                plywoods[j].transform.localScale = new Vector3(1, model.plywoodTextfield[j], 1);
            }


            // setting alpha
            if (model.alphaTexture != "UnityWhite")
            {
                // for granulate
                Texture2D alphaTexture = counterSurfaceChanger.AllTextures[model.alphaTexture];
                counter.Find("Counter").GetComponent<MeshRenderer>().materials[0].SetTexture("_AlphaTexture", alphaTexture);

                SetTheDefaultPlywoodMaterial(mainTexture, counterSurfaceChanger.AllTextures[model.alphaTexture]);
            }
            else
            {
                counter.Find("Counter").GetComponent<MeshRenderer>().materials[0].SetTexture("_AlphaTexture", Texture2D.whiteTexture);

                SetTheDefaultPlywoodMaterial(mainTexture, Texture2D.whiteTexture);
            }
        }

    }

    private void SetTheDefaultPlywoodMaterial(Texture2D texture, Texture2D alpha)
    {
        foreach (GameObject plywood in plywoodcontroller.AllPlywoodCubes)
        {
            plywood.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].SetTexture("_Texture2D", texture);
            plywood.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].SetTexture("_AlphaTexture", alpha);
            plywood.transform.GetChild(0).GetComponent<MeshRenderer>().materials[1].color = colour;
        }

    }


}
