using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CounterGenerator : MonoBehaviour
{
    [SerializeField] BasinMovement basinMovement;
    [SerializeField] RotationScript rotationScript;
    public GameObject currentCounter;
    public GameObject counterWhole;
    public GameObject _instanciateCounter;
    [SerializeField] private WorldCanvas worldCanvas;
    public event Action OnCounterAdded;
    private Quaternion lastSelectedCounterRotation = Quaternion.Euler(Vector3.zero);
    [SerializeField] private Material grayMat;
    private Material defaultObjectMat;
    [SerializeField] private BasinAndCounterOverlapingController BasinAndCounterOverlapingController;
   // public List<GameObject> TotalCounterInScene = new List<GameObject>();

    void Start()
    {
        SettingCounterSelected();
        basinMovement.OnCounterStopMoving += SetObjectDefaultMat;
    }

    public void CounterInstanciate(Quaternion currentObjRotation)
    {
        GameObject currentCounter = basinMovement.currentCounter;
        _instanciateCounter = Instantiate(currentCounter, currentCounter.transform.position, currentObjRotation);
        _instanciateCounter.name = currentCounter.transform.parent.name;
        _instanciateCounter.transform.Find("Counter").transform.Find("SelectedDashLineCube").gameObject.SetActive(false);
        BasinAndCounterOverlapingController.SelectedCounterInitialColor = currentCounter.transform.GetChild(0).GetComponent<MeshRenderer>().materials[1].color;
        basinMovement.SelectedGameobject.AddComponent<BasinAndCounterOverlapingController>();
        _instanciateCounter.transform.parent = counterWhole.transform.parent;
        SettingGrayMatToOboject(currentCounter.transform);

    }

    private void SettingGrayMatToOboject(Transform selectedGameobject)
    {
        defaultObjectMat = selectedGameobject.Find("Counter").GetComponent<MeshRenderer>().material;
        Debug.Log("CHECKING_COUNTER_COLOR_CHANGE_ISSUE : COUNTER IS SELECTED");
        //BasinAndCounterOverlapingController.SelectedCounterInitialMaterial = defaultObjectMat;
        //selectedGameobject.Find("Counter").GetComponent<MeshRenderer>().materials[0] = grayMat;
    }

    private void SetObjectDefaultMat()
    {
        basinMovement.SelectedGameobject.GetComponent<MeshRenderer>().material = defaultObjectMat;
    }

    private void SettingCounterSelected()
    {
        currentCounter.transform.Find("Counter").transform.Find("SelectedDashLineCube").gameObject.SetActive(true);
        basinMovement.selectedObject = SelectedObject.counter;
        basinMovement.SelectedGameobject = currentCounter.transform.Find("Counter").gameObject;
        basinMovement.counterWhole = this.counterWhole.transform;
    }

    public void AddingCounter()
    {
        lastSelectedCounterRotation = Quaternion.Euler(Vector3.zero);
        rotationScript.CounterRotationVal = 0f;
        lastSelectedCounterRotation = SettinglastSelectedCounterRotation(lastSelectedCounterRotation);

        GameObject cunterbase = Instantiate(basinMovement.SelectedGameobject.transform.parent.gameObject, basinMovement.SelectedGameobject.transform.parent.position, lastSelectedCounterRotation);
        //TotalCounterInScene.Add(cunterbase);
        cunterbase.name = "CounterBase";
        cunterbase.transform.position = basinMovement.SelectedGameobject.transform.parent.transform.localPosition + new Vector3(5f,0f,0f);
        currentCounter.transform.Find("Counter").transform.Find("SelectedDashLineCube").gameObject.SetActive(false);
        DestroyPreviousBasins(cunterbase);
        currentCounter = cunterbase;
        basinMovement.currentCounter = cunterbase;
        cunterbase.transform.SetParent(counterWhole.transform, false);
        OnCounterAdded();
        currentCounter.transform.Find("Counter").GetComponent<MeshRenderer>().materials[0].renderQueue = 3003;
        currentCounter.transform.Find("Counter").GetComponent<MeshRenderer>().materials[1].renderQueue = 3002;
    }

    private Quaternion SettinglastSelectedCounterRotation(Quaternion rotaionval)
    {
        if (basinMovement.selectedObject == SelectedObject.counter)
        {
            rotaionval = basinMovement.SelectedGameobject.transform.rotation;
            rotationScript.CounterRotationVal = basinMovement.SelectedGameobject.transform.eulerAngles.y;
        }
        return rotaionval;
    }

    private static void DestroyPreviousBasins(GameObject cunterbase)
    {
        Transform basinParent = cunterbase.transform.Find("Basin").transform;
        if (basinParent.childCount > 0)
        {
            foreach (Transform child in basinParent)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void DisableAllBasinsCollider()
    {
        Transform basinParent = basinMovement.currentCounter.transform.Find("Basin").transform;
        foreach (Transform child in basinParent)
        {
            child.GetComponent<BoxCollider>().enabled = false;
        }
        
    }

    public void EnableAllBasinsCollider()
    {
        Transform basinParent = basinMovement.currentCounter.transform.Find("Basin").transform;
        foreach (Transform child in basinParent)
        {
            child.GetComponent<BoxCollider>().enabled = true;
        }

    }
}
