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

    void Start()
    {
        SettingCounterSelected();

    }

    public void CounterInstanciate(Quaternion currentObjRotation)
    {
        _instanciateCounter = Instantiate(basinMovement.currentCounter, basinMovement.currentCounter.transform.position, currentObjRotation);
       // _instanciateCounter.GetComponent<BasinMovement>().enabled = false;
       // Destroy(_instanciateCounter.transform.Find("WorldUiCanvas").gameObject);
        _instanciateCounter.transform.parent = counterWhole.transform.parent;
        //currentCounter = _instanciateCounter;
        //if (_isBasinGenerate)
        //{
        //    // currentBasin = _instanciateCounter.transform.GetChild(1).transform.GetChild(0).gameObject;
        //}
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
        cunterbase.transform.position = basinMovement.SelectedGameobject.transform.parent.transform.localPosition + new Vector3(5f,0f,0f);
        currentCounter.transform.Find("Counter").transform.Find("SelectedDashLineCube").gameObject.SetActive(false);
        DestroyPreviousBasins(cunterbase);
        currentCounter = cunterbase;
        basinMovement.currentCounter = cunterbase;
        cunterbase.transform.SetParent(counterWhole.transform, false);
        OnCounterAdded();
        currentCounter.transform.Find("Counter").GetComponent<MeshRenderer>().material.renderQueue = 3002;
    }

    private Quaternion SettinglastSelectedCounterRotation(Quaternion rotaionval)
    {
        if (basinMovement.selectedObject == SelectedObject.counter)
        {
            lastSelectedCounterRotation = basinMovement.SelectedGameobject.transform.rotation;
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
}
