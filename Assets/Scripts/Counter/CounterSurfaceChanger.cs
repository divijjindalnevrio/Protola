using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterSurfaceChanger : MonoBehaviour
{
    [SerializeField] private List<Material> counterMat = new List<Material>();

    [SerializeField] private List<Texture> counterTex = new List<Texture>();
    [SerializeField] private List<Texture> counterGranulateTex = new List<Texture>();
    [SerializeField] private Texture colorDefaultTex;
    [SerializeField] private BasinMovement basinMovement;
    [SerializeField] private MainUiController mainUiController;
    [SerializeField] private UiModel uiModel;
    private Color lastSelectedColor = Color.white;
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
            selectedObjcet.transform.GetComponent<MeshRenderer>().material.color = Color.white;
            selectedObjcet.transform.GetComponent<MeshRenderer>().material.mainTexture = counterTex[material];
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
            selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().material.color = lastSelectedColor;
        }

        else
        {
            // for counter
            selectedObjcet.transform.GetComponent<MeshRenderer>().material.mainTexture = counterGranulateTex[material];
            selectedObjcet.transform.GetComponent<MeshRenderer>().material.color = lastSelectedColor;
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
           
        }
        else
        {
            // for counter
            // selectedObjcet.transform.GetComponent<MeshRenderer>().material.mainTexture = null;
            selectedObjcet.transform.GetComponent<MeshRenderer>().material.mainTexture = colorDefaultTex;
            selectedObjcet.transform.GetComponent<MeshRenderer>().material.color = colors[color];
            lastSelectedColor = colors[color];


        }

    }
}
