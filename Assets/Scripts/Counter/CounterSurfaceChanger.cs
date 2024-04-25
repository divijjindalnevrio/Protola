using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CounterSurfaceChanger : MonoBehaviour
{
    [SerializeField] private List<Material> counterMat = new List<Material>();
    [SerializeField] private List<Texture2D> counterTex = new List<Texture2D>();
    [SerializeField] private List<Texture2D> counterGranulateTex = new List<Texture2D>();
    [SerializeField] private List<Texture2D> counterGranulateTexMap = new List<Texture2D>();
    [SerializeField] private Texture2D colorDefaultTex;
    [SerializeField] private BasinMovement basinMovement;
    [SerializeField] private MainUiController mainUiController;
    [SerializeField] private UiModel uiModel;
    [SerializeField] private Plywoodcontroller plywoodcontroller;

    [SerializeField] private Texture2D DefaultColorTexture;
    [SerializeField] private Texture2D DefaultColorTextureMap;
    private GameObject selectedObjcet;
    private Color lastSelectedColor = Color.white;
    private Color basinlastSelectedColor = Color.white;
    public List<Color> colors = new List<Color>();
    public Dictionary<string, Texture2D> AllTextures = new Dictionary<string, Texture2D>();
    public List<Texture2D> allTextures = new List<Texture2D>();

    private void Awake()
    {
        SetAllTexturesToDict();
    }

    void Start()
    {
        basinMovement.OnCounterStopMoving += BasinMovement_OnCounterStopMoving;
        SetTheMaterialPriority();
    }

    private void BasinMovement_OnCounterStopMoving()
    {
        SetTheMaterialPriority();
    }

    public void ChangingCounterSurfaceTexture(int material)
    {
        selectedObjcet = basinMovement.SelectedGameobject;

        if (selectedObjcet == null) { return; }

        if (selectedObjcet.CompareTag("Basin"))
        {
            selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().materials[1].color = Color.white;
            Material mat =  selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().materials[0];
            mat.SetTexture("_Texture2D", counterTex[material]);
            mat.SetTexture("_AlphaTexture", Texture2D.whiteTexture);
            
        }

        else
        {
            selectedObjcet.transform.GetComponent<MeshRenderer>().materials[1].color = Color.white;
            Material mat =  selectedObjcet.transform.GetComponent<MeshRenderer>().materials[0];
            mat.SetTexture("_Texture2D", counterTex[material]);
            mat.SetTexture("_AlphaTexture", Texture2D.whiteTexture);
            ChangingThePlywoodSurface(material);

            selectedObjcet.transform.GetComponent<MeshRenderer>().materials[0].renderQueue = 3003;
            selectedObjcet.transform.GetComponent<MeshRenderer>().materials[1].renderQueue = 3002;
        }
        
    }

    public void ChangingSurfaceGranulateTexture(int material)
    {
        selectedObjcet = basinMovement.SelectedGameobject;
        if (selectedObjcet == null) { return; }

        if (selectedObjcet.CompareTag("Basin"))
        {
             Material mat = selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().materials[0];
            mat.SetTexture("_Texture2D", counterGranulateTex[material]);
            mat.SetTexture("_AlphaTexture", counterGranulateTexMap[material]);
            //selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().materials[1].color = lastSelectedColor;
            DefaultColorTexture = counterGranulateTex[material];
            DefaultColorTextureMap = counterGranulateTexMap[material];
        }

        else
        {
            // for counter

            Material mat = selectedObjcet.transform.GetComponent<MeshRenderer>().materials[0];
            mat.SetTexture("_Texture2D", counterGranulateTex[material]);
            mat.SetTexture("_AlphaTexture", counterGranulateTexMap[material]);
            selectedObjcet.transform.GetComponent<MeshRenderer>().material.color = lastSelectedColor;
            DefaultColorTexture = counterGranulateTex[material];
            DefaultColorTextureMap = counterGranulateTexMap[material];
        
            // for plywood 
            foreach (GameObject obje in plywoodcontroller.AllPlywoodCubes)
            {
                obje.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].SetTexture("_Texture2D",counterGranulateTex[material]);
                obje.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].SetTexture("_AlphaTexture", counterGranulateTexMap[material]);
                obje.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = lastSelectedColor;
            }

        }

    }

    public void ChangingCounterSurfaceColor(int color)
    {
        selectedObjcet = basinMovement.SelectedGameobject;
        if (selectedObjcet == null) { return; }

        if (selectedObjcet.CompareTag("Basin"))
        {
            selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().material.mainTexture = colorDefaultTex;
            selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().material.color = colors[color];
            basinlastSelectedColor = colors[color];
            Material mat = selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().materials[0];
            mat.SetTexture("__Texture2D", DefaultColorTexture);
            mat.SetTexture("_AlphaTexture", DefaultColorTextureMap);
            selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().materials[1].color = colors[color];
        }
        else
        {
            // for counter
            Material mat = selectedObjcet.transform.GetComponent<MeshRenderer>().materials[0];
            mat.SetTexture("__Texture2D", DefaultColorTexture);
            mat.SetTexture("_AlphaTexture", DefaultColorTextureMap);
            selectedObjcet.transform.GetComponent<MeshRenderer>().materials[1].color = colors[color];
            // for plywood 
            foreach (GameObject obje in plywoodcontroller.AllPlywoodCubes)
            {
                Material plywoodMat = obje.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0];
                plywoodMat.SetTexture("__Texture2D", DefaultColorTexture);
                plywoodMat.SetTexture("_AlphaTexture", DefaultColorTextureMap);
                obje.transform.GetChild(0).GetComponent<MeshRenderer>().materials[1].color = colors[color];
            }

            lastSelectedColor = colors[color];

        }

    }


    private void ChangingThePlywoodSurface(int material)
    {
        foreach (GameObject obje in plywoodcontroller.AllPlywoodCubes)
        {
            Material mat = obje.transform.GetChild(0).GetComponent<MeshRenderer>().materials[1];
            mat.color = Color.white;
            obje.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].SetTexture("_Texture2D", counterTex[material]);
            obje.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].SetTexture("_AlphaTexture", Texture2D.whiteTexture);

            //obje.transform.GetChild(0).GetComponent<MeshRenderer>().materials[1].
        }
    }

    private void SetTheMaterialPriority()
    {
        selectedObjcet = basinMovement.SelectedGameobject;
        selectedObjcet.transform.GetComponent<MeshRenderer>().materials[0].renderQueue = 3003;
        selectedObjcet.transform.GetComponent<MeshRenderer>().materials[1].renderQueue = 3002;
    }


    public void SetAllTexturesToDict()
    {
       // List<Texture2D> allTextures = new List<Texture2D>();
        allTextures = counterTex.Concat(counterGranulateTex).Concat(counterGranulateTexMap).ToList();
        foreach (Texture2D texture in allTextures)
        {
            AllTextures.Add(texture.name, texture);
        }
        Debug.Log("SetAllTexturesToDict values all : " + AllTextures.Count);
    }
   
}
