using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterSurfaceChanger : MonoBehaviour
{
    [SerializeField] private List<Material> counterMat = new List<Material>();
    [SerializeField] private List<Texture> counterTex = new List<Texture>();
    [SerializeField] private BasinMovement basinMovement;
    [SerializeField] private MainUiController mainUiController;
    [SerializeField] private UiModel uiModel;
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
            selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().material = counterMat[material];
        }

        else
        {
            selectedObjcet.transform.GetComponent<MeshRenderer>().material = counterMat[material];
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
            selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().material = counterMat[material];
        }
        else
        {
            // for counter
            selectedObjcet.transform.GetComponent<MeshRenderer>().material.mainTexture = counterTex[material];
        }

    }

    public void ChangingCounterSurfaceColor(int color)
    {
        GameObject selectedObjcet = basinMovement.SelectedGameobject;
        if (selectedObjcet == null) { return; }

        if (selectedObjcet.CompareTag("Basin"))
        {
            selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().material.mainTexture = null;
            selectedObjcet.transform.Find("Cube").GetComponent<MeshRenderer>().material.color = colors[color];
           
        }
        else
        {
            // for counter
           // selectedObjcet.transform.GetComponent<MeshRenderer>().material.mainTexture = null;
            selectedObjcet.transform.GetComponent<MeshRenderer>().material.color = colors[color];
           
        }

    }
}
