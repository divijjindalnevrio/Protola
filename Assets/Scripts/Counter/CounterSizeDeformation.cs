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
    [SerializeField] private BasinsGenerator basinsGenerator;
    [SerializeField] private BasinMovement basinMovement;
    [SerializeField] private SerializationToJson serializationToJson;
    public Vector3 currentCounterPosition;
    private Color colour;
    private Color basinColour;
    [SerializeField] private CounterSurfaceChanger counterSurfaceChanger;
    [SerializeField] private Plywoodcontroller plywoodcontroller;
    [SerializeField] private CheckAndCreateCounterCopyScript checkAndCreateCounterCopyScript;
    [SerializeField] private ButtonClickScript buttonClickScript;

    [SerializeField] private List<GameObject> counters = new List<GameObject>();
    [SerializeField] private List<Transform> plywoods = new List<Transform>();

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

        foreach (int i in Enumerable.Range(0, Scenemodel.allCounters.Count))
        {
            CounterModel model = Scenemodel.allCounters[i];

            if (i > 0)
            {
                buttonClickScript.AddCounter(i);
            }
            counters = checkAndCreateCounterCopyScript.ConvertingCounterDictToList();

            Transform counter = counters[i].transform;
            counter.Find("Counter").localScale = new Vector3(model.width, 0.07f, model.depth);
            counter.transform.position = new Vector3(counter.localScale.x, model.thickness, counter.localScale.z);
            counter.rotation = Quaternion.Euler(model.rotaton);
            counter.position = model.position;
            SetTheSizeValueOfCurrentCounter();

            //setting color below
            plywoods.Clear();
            Texture2D mainTexture = counterSurfaceChanger.AllTextures[model.texture];
            ColorUtility.TryParseHtmlString(model.colourHexCode, out colour);
            counter.Find("Counter").GetComponent<MeshRenderer>().materials[1].color = colour;
            counter.Find("Counter").GetComponent<MeshRenderer>().materials[0].SetTexture("_AlphaTexture", Texture2D.whiteTexture);
            ChangingPlywoodSurface(mainTexture, Texture2D.whiteTexture, "Color");

            // setting texture below

            counter.Find("Counter").GetComponent<MeshRenderer>().materials[0].SetTexture("_Texture2D", mainTexture);
            ChangingPlywoodSurface(mainTexture, Texture2D.whiteTexture, "Texture");

            //plywood length changing below
           
            GettingPlywoods(counter);
            for (int j = 0; j < plywoods.Count; j++)
            {
                plywoods[j].transform.localScale = new Vector3(1, model.plywoodTextfield[j], 1);
            }
            // basingeneration.
            Debug.Log("basin got generated :1  " + model.allbasins.Count);
            for (int b = 0; b <= model.allbasins.Count - 1; b++)
            {
                Debug.Log("basin got generated : 2");
                basinsGenerator.BasinGererator(model.allbasins[b].name);
                // basinMovement.SelectedGameobject = counter.Find("Counter").gameObject;
                GameObject currentBasin = basinMovement.SelectedGameobject;
                currentBasin.transform.position = model.allbasins[b].position;
                currentBasin.transform.rotation = Quaternion.Euler(model.allbasins[b].rotaton);

                ColorUtility.TryParseHtmlString(model.allbasins[b].colourHexCode, out basinColour);
                currentBasin.transform.Find("Cube").GetComponent<MeshRenderer>().materials[1].color = basinColour;

                Texture2D texture = counterSurfaceChanger.AllTextures[model.allbasins[b].texture];
//               
                currentBasin.transform.Find("Cube").GetComponent<MeshRenderer>().materials[0].SetTexture("_Texture2D", texture);

                if(model.allbasins[b].alphaTexture == "UnityWhite")
                {
                    currentBasin.transform.Find("Cube").GetComponent<MeshRenderer>().materials[0].SetTexture("_AlphaTexture", Texture2D.whiteTexture);
                }
                else
                {
                    Texture2D alpha = counterSurfaceChanger.AllTextures[model.allbasins[b].alphaTexture];
                    currentBasin.transform.Find("Cube").GetComponent<MeshRenderer>().materials[0].SetTexture("_AlphaTexture", alpha);
                }
               

                Debug.Log("basin got generated : " + model.allbasins[b].name + "count : " + model.allbasins.Count);
            }

            // setting alpha
            if (model.alphaTexture != "UnityWhite")
            {
                // for granulate
                Texture2D alphaTexture = counterSurfaceChanger.AllTextures[model.alphaTexture];
                counter.Find("Counter").GetComponent<MeshRenderer>().materials[0].SetTexture("_AlphaTexture", alphaTexture);

                ChangingPlywoodSurface(mainTexture, alphaTexture, "Granulate");
            }
            else
            {
                counter.Find("Counter").GetComponent<MeshRenderer>().materials[0].SetTexture("_AlphaTexture", Texture2D.whiteTexture);
                ChangingPlywoodSurface(mainTexture, Texture2D.whiteTexture, "Texture");
            }
            
           // buttonClickScript.AddCounter(i);
        }

    }

   

    private void GettingPlywoods(Transform counter)
    {
        GameObject plywoodCubeParent = counter.Find("Counter").Find("PlywoodCubes").gameObject;

        foreach (Transform plywood in plywoodCubeParent.transform)
        {
            plywoods.Add(plywood);
        }
    }

    private void ChangingPlywoodSurface(Texture2D _mainTexture, Texture2D _alphaTexture, string _surfaceChangein)
    {
        foreach (int j in Enumerable.Range(0, plywoods.Count))
        {
            if(_surfaceChangein == "Texture")
            {
                // plywood texture change
                Material mat = plywoods[j].transform.GetChild(0).GetComponent<MeshRenderer>().materials[0];
                mat.SetTexture("_Texture2D", _mainTexture);
                mat.SetTexture("_AlphaTexture", _alphaTexture);
            }

            if(_surfaceChangein == "Granulate")
            {
                Material mat = plywoods[j].transform.GetChild(0).GetComponent<MeshRenderer>().materials[0];
                mat.SetTexture("_Texture2D", _mainTexture);
                mat.SetTexture("_AlphaTexture", _alphaTexture);
                plywoods[j].transform.GetChild(0).GetComponent<MeshRenderer>().materials[1].color = colour;
            }
            if (_surfaceChangein == "Color")
            {
                // plywood color change
                plywoods[j].transform.GetChild(0).GetComponent<MeshRenderer>().materials[1].color = colour;
                Material mat = plywoods[j].transform.GetChild(0).GetComponent<MeshRenderer>().materials[0];
                mat.SetTexture("_Texture2D", _mainTexture);
                mat.SetTexture("_AlphaTexture", _alphaTexture);
            }
            
        }
    }


}
