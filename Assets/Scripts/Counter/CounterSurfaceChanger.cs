using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterSurfaceChanger : MonoBehaviour
{
    [SerializeField] private List<Material> counterMat = new List<Material>();

    [SerializeField] private List<Texture> counterTex = new List<Texture>();
    [SerializeField] private List<Texture> counterGranulateTex = new List<Texture>();
    [SerializeField] private List<Texture> counterGranulateTexMap = new List<Texture>();
    [SerializeField] private Texture colorDefaultTex;
    [SerializeField] private BasinMovement basinMovement;
    [SerializeField] private MainUiController mainUiController;
    [SerializeField] private UiModel uiModel;
    [SerializeField] private Plywoodcontroller plywoodcontroller;
    private Color lastSelectedColor = Color.white;
    private Color basinlastSelectedColor = Color.white;
    public List<Color> colors = new List<Color>();

    void Start()
    {
        
    }

    public void ChangingCounterSurfaceTexture(int material)
    {
        GameObject selectedObjcet = basinMovement.SelectedGameobject;
        if(selectedObjcet == null) { return; }

        if (selectedObjcet.CompareTag("Basin"))
        {
            selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().material.color = Color.white;
            selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().material.mainTexture = counterTex[material];
        }

        else
        {
            selectedObjcet.transform.GetComponent<MeshRenderer>().materials[1].color = Color.white;
            selectedObjcet.transform.GetComponent<MeshRenderer>().materials[0].mainTexture = counterTex[material];
            ChangingThePlywoodSurface(material);
            Debug.Log("Counter color got changed : ");
            selectedObjcet.transform.GetComponent<MeshRenderer>().material.renderQueue = 3002;
        }
        
    }

    public void ChangingSurfaceGranulateTexture(int material)
    {
        GameObject selectedObjcet = basinMovement.SelectedGameobject;
        if (selectedObjcet == null) { return; }

        if (selectedObjcet.CompareTag("Basin"))
        {
            selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().material.mainTexture = counterGranulateTex[material];
            selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().material.color = basinlastSelectedColor;
        }

        else
        {
            // for counter
            selectedObjcet.transform.GetComponent<MeshRenderer>().materials[0].SetTexture("Texture2D", counterGranulateTex[material]);
            //Debug.Log("granulate textures here : " + selectedObjcet.transform.GetComponent<MeshRenderer>().materials[0].GetTexture("Texture2D").name);
            selectedObjcet.transform.GetComponent<MeshRenderer>().materials[0].SetTexture("AlphaTexture", counterGranulateTexMap[material]);
            selectedObjcet.transform.GetComponent<MeshRenderer>().material.color = lastSelectedColor;
            Debug.Log("granulate textures here : " + material);
            // for plywood 
            foreach (GameObject obje in plywoodcontroller.AllPlywoodCubes)
            {
                obje.transform.GetChild(0).GetComponent<MeshRenderer>().material.mainTexture = counterGranulateTex[material];
                obje.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = lastSelectedColor;
            }

        }

    }

    public void ChangingCounterSurfaceColor(int color)
    {
        GameObject selectedObjcet = basinMovement.SelectedGameobject;
        if (selectedObjcet == null) { return; }

        if (selectedObjcet.CompareTag("Basin"))
        {
            selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().material.mainTexture = colorDefaultTex;

            selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().material.color = colors[color];
            basinlastSelectedColor = colors[color];
        }
        else
        {
            // for counter
            selectedObjcet.transform.GetComponent<MeshRenderer>().material.mainTexture = colorDefaultTex;

            selectedObjcet.transform.GetComponent<MeshRenderer>().materials[1].color = colors[color];
            // for plywood 
            foreach (GameObject obje in plywoodcontroller.AllPlywoodCubes)
            {
                obje.transform.GetChild(0).GetComponent<MeshRenderer>().material.mainTexture = colorDefaultTex;
                obje.transform.GetChild(0).GetComponent<MeshRenderer>().materials[1].color = colors[color];
            }

            lastSelectedColor = colors[color];

        }

    }


    private void ChangingThePlywoodSurface(int material)
    {
        foreach (GameObject obje in plywoodcontroller.AllPlywoodCubes)
        {
            obje.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].color = Color.white;
            obje.transform.GetChild(0).GetComponent<MeshRenderer>().material.mainTexture = counterTex[material];
        }
    }
}
