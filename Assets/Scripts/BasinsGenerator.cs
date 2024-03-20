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
    public event Action OnBasinGenrate;
    private BasinTypeSO AllBasinsSo;
    private Dictionary<string, GameObject> basins = new Dictionary<string, GameObject>();
    private Quaternion lastSelectedBasinRotation = Quaternion.Euler(Vector3.zero);
    [SerializeField] private BasinMovement basinMovement;
    [SerializeField] private BasinOverlapingController basinOverlapingController;
    [SerializeField] private RotationScript rotationScript;
    [SerializeField] private WorldCanvas worldCanvas;
    [SerializeField] private Material grayMat;
    private Material defaultObjectMat;

    void Start()
    {
        SettingBasinToDict();
        basinMovement.OnBasinStopMoving += SetObjectDefaultMat;
    }

    public void BasinGererator(string basinName)
    {
        Vector3 lastSelectedBasinPos = Vector3.zero;
        rotationScript.BasinRotationVal = 0f;
        lastSelectedBasinRotation = Quaternion.Euler(Vector3.zero);
        lastSelectedBasinPos = SettinglastSelectedBasinPos(lastSelectedBasinPos);

        Transform currentBasinObj = this.transform.Find("CounterBase").transform.Find("Basin").transform;
        //SettingBasinPosition(currentBasinObj);
        currentBasin = Instantiate(basins[basinName], CounterSO.CurrenetCounter.transform.position + lastSelectedBasinPos,lastSelectedBasinRotation);
        currentBasin.GetComponent<Collider>().isTrigger = true;
        currentBasin.name = "Basin";
        rotationScript.BasinRotationVal = Mathf.Round(rotationScript.BasinRotationVal); // here added now
        basinMovement.currentBasin = currentBasin;
        GameObject selectedDashCube = Instantiate(SelectedDashLineBasin, Vector3.zero, Quaternion.identity);
        selectedDashCube.name = "SelectedDashLineCube";
        selectedDashCube.transform.SetParent(currentBasin.transform, false);
        currentBasin.transform.parent = basinMovement.currentCounter.transform.Find("Basin").transform;
        SettingBasinSelected();
        currentBasin.transform.localPosition = new Vector3(lastSelectedBasinPos.x, 0.02f, lastSelectedBasinPos.z);
        OnBasinGenrate?.Invoke();
    }

    private Vector3 SettinglastSelectedBasinPos(Vector3 lastSelectedBasinPos)
    {
        if (basinMovement.selectedObject == SelectedObject.basin)
        {
            lastSelectedBasinPos = basinMovement.SelectedGameobject.transform.localPosition;
            lastSelectedBasinRotation = basinMovement.SelectedGameobject.transform.rotation;
            rotationScript.BasinRotationVal = basinMovement.SelectedGameobject.transform.eulerAngles.y;
            Destroy( basinMovement.SelectedGameobject.gameObject);
        }
        return lastSelectedBasinPos;
    }


    private void SettingBasinSelected()
    {
        basinMovement.DeselectingAllDashLines();
        currentBasin.transform.Find("SelectedDashLineCube").gameObject.SetActive(true);
        basinMovement.selectedObject = SelectedObject.basin;
        basinMovement.TriggerOnGameobjectSelectedEvent();
        worldCanvas.SettingWorldUiCanvasToTrue();
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
        Transform selectedGameobject = basinMovement.SelectedGameobject.transform;
        InstanciateBasin = Instantiate(selectedGameobject.gameObject,selectedGameobject.position, selectedGameobject.localRotation);
        InstanciateBasin.gameObject.name = "BasinClone";
        InstanciateBasin.transform.Find("SelectedDashLineCube").gameObject.SetActive(false);
        InstanciateBasin.transform.parent = basinMovement.currentCounter.transform.GetChild(1).transform;
        selectedGameobject.gameObject.AddComponent<BasinOverlapingController>();
        SettingGrayMatToOboject(selectedGameobject);
        basinMovement.isBasinInstanciate = true;
    }


    public void SettingGrayMatToOboject(Transform selectedGameobject)
    {
        defaultObjectMat = selectedGameobject.Find("Cube").GetComponent<MeshRenderer>().material;
        selectedGameobject.Find("Cube").GetComponent<MeshRenderer>().material = grayMat;
    }

    private void SetObjectDefaultMat()
    {
        basinMovement.SelectedGameobject.transform.Find("Cube").GetComponent<MeshRenderer>().material = defaultObjectMat;
    }

    private void SettingBasinToDict()
    {
        AllBasinsSo = (BasinTypeSO)Resources.Load("AllBasinSo");
        foreach (GameObject basin in AllBasinsSo.BasinType)
        {
            basins.Add(basin.name, basin);
        }
    }
}
