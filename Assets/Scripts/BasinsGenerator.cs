using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BasinsGenerator : MonoBehaviour
{
    public GameObject Basin;
    public GameObject SelectedDashLineBasin;
    public CounterTypeSO CounterSO;
    public GameObject currentBasin;
    public GameObject InstanciateBasin;
    public bool IsBasinGenerated = false;
    [SerializeField] private BasinMovement basinMovement;

    public event Action OnBasinGenrate;
    void Start()
    {
        
    }

    public void BasinGererator()
    {
        Transform currentBasinObj = this.transform.Find("CounterBase").transform.Find("Basin").transform;
        SettingBasinPosition(currentBasinObj);

        currentBasin = Instantiate(Basin, CounterSO.CurrenetCounter.transform.position, Quaternion.identity);
        currentBasin.name = "Basin";
        GameObject selectedDashCube = Instantiate(SelectedDashLineBasin, Vector3.zero, Quaternion.identity);
        selectedDashCube.name = "SelectedDashLineBasin";
        selectedDashCube.transform.SetParent(currentBasin.transform, false);
        currentBasin.transform.parent = currentBasinObj.transform;
        SettingBasinSelected();
        currentBasin.transform.localPosition = new Vector3(0f, -1.47f, 0);
        OnBasinGenrate?.Invoke();
    }

    private void SettingBasinSelected()
    {
        basinMovement.DeselectingAllDashLines();
        currentBasin.transform.Find("SelectedDashLineBasin").gameObject.SetActive(true);
        basinMovement.selectedObject = SelectedObject.basin;
        basinMovement.SelectedGameobject = currentBasin;
    }
    private void SettingBasinPosition(Transform currentBasinObj)
    {
        Transform currentCounterObj = this.transform.Find("CounterBase").transform.Find("Counter").transform;
        Vector3 basinPos = new Vector3(0f, currentCounterObj.position.y / 2, 0f);
        currentBasinObj.localPosition = basinPos;

    }

    public void BasinInstanciate()
    {
        InstanciateBasin = Instantiate(currentBasin, currentBasin.transform.position, currentBasin.transform.localRotation);
        InstanciateBasin.transform.Find("SelectedDashLineBasin").gameObject.SetActive(false);
        InstanciateBasin.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.color = basinMovement.defaultMat.color;
        InstanciateBasin.transform.parent = basinMovement.currentCounter.transform.GetChild(1).transform;
        //currentBasin = _instanciateBasin;
        basinMovement.isBasinInstanciate = true;
    }


}
