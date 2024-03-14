using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Plywoodcontroller : MonoBehaviour
{
    [SerializeField] private BasinMovement basinMovement;
    [SerializeField] private Transform PlywoodCube;

    [SerializeField] private List<GameObject> AllPlywoodCubes = new List<GameObject>();
    [SerializeField] private List<GameObject> PlywoodInputTextFields = new List<GameObject>();
    [SerializeField] private List<Vector3> centerPosition = new List<Vector3>();
    private Transform PlywoodInputTextFieldParentObj;


    void Start()
    {

        AssignPlywood();
        GetAllPlywoods();
        GettingAllInputFields();
        GettingAllPlywoodCubeCenterPos();

        basinMovement.OnGameobjectSelected += BasinMovement_OnGameobjectSelected;
        basinMovement.OnCounterStopMoving += BasinMovement_OnCounterStopMoving;
    }

    private void Update()
    {
        if(AllPlywoodCubes != null && PlywoodInputTextFields != null )
        {
            IncreaseThePlywoodSize();
        }
       
    }

    private void BasinMovement_OnCounterStopMoving()
    {
        AssignPlywood();
        GettingAllPlywoodCubeCenterPos();
        SetPlywoodLineRendererActive();
        SettingTextFieldToCenterPos();
    }


    private void BasinMovement_OnGameobjectSelected(object sender, SelectedObject e)
    {
       if(e == SelectedObject.counter)
        {
            AssignPlywood();
            GettingAllPlywoodCubeCenterPos();
            SetPlywoodLineRendererActive();
            SetTextFieldsActive();
            SettingTextFieldToCenterPos();

        }

        else
        {
            AssignPlywood();
            SetPlywoodLineRendererDeActive();
            if(PlywoodInputTextFields != null)
            {
                SetTextFieldsDeActive();
            }
        }
    
    }

    private void AssignPlywood()
    {
        PlywoodCube = basinMovement.currentCounter.transform.Find("Counter").transform.Find("PlywoodCubes").transform;
       
    }

    private void GettingAllInputFields()
    {
        Transform PlywoodInputTextFieldParentObj = basinMovement.currentCounter.transform.Find("Counter").transform.Find("PlywoodInputTextFiels").transform;
        foreach (Transform child in PlywoodInputTextFieldParentObj)
        {
            PlywoodInputTextFields.Add(child.gameObject);
        }
    }

    private void GettingAllPlywoodCubeCenterPos()
    {
        centerPosition.Clear();
        foreach (Transform child in PlywoodCube)
        {
            centerPosition.Add(child.transform.GetChild(0).transform.GetChild(1).transform.position);
        }
    }

    private void SetPlywoodLineRendererActive()
    {
        foreach (Transform child in PlywoodCube)
        {
            GameObject firstPos  = child.transform.GetChild(0).transform.Find("TopNPoint").gameObject;
            GameObject secondPos = child.transform.GetChild(0).transform.GetChild(2).gameObject;
            child.transform.GetChild(0).transform.Find("LineRenderer").GetComponent<LineRenderer>().SetWidth(0.05f, 0.05f);
            child.transform.GetChild(0).transform.Find("LineRenderer").GetComponent<LineRenderer>().SetPosition(0, firstPos.transform.position);
            child.transform.GetChild(0).transform.Find("LineRenderer").GetComponent<LineRenderer>().SetPosition(1, secondPos.transform.position);
            AllPlywoodCubes.Add(child.gameObject);
            //centerPosition.Add(child.transform.GetChild(1).transform.position);

        }
    }

    private void SetPlywoodLineRendererDeActive()
    {
        foreach (Transform child in PlywoodCube)
        {
            child.transform.GetChild(0).transform.Find("LineRenderer").GetComponent<LineRenderer>().SetWidth(0f, 0f);
        }
    }

    private void SettingTextFieldToCenterPos()
    {
        for (int i =0; i < PlywoodInputTextFields.Count; i++)
        {
            PlywoodInputTextFields[i].transform.position = centerPosition[i];
        }
    }


    private void SetTextFieldsActive()
    {
        foreach (GameObject child in PlywoodInputTextFields)
        {
            child.SetActive(true);
        }
    }

    private void SetTextFieldsDeActive()
    {
        foreach(GameObject child in PlywoodInputTextFields)
        {
            child.SetActive(false);
        }
    }

    private void GetAllPlywoods()
    {
        foreach (Transform child in PlywoodCube)
        {
            AllPlywoodCubes.Add(child.gameObject);
        }
    }

    private void IncreaseThePlywoodSize()
    {
        for (int i = 0; i < PlywoodInputTextFields.Count; i++)
        {
            float plywoodLength = float.Parse(PlywoodInputTextFields[i].transform.GetChild(0).GetComponent<TMP_InputField>().text);
            if(plywoodLength > 10)
            {
                plywoodLength = 10;
                PlywoodInputTextFields[i].transform.GetChild(0).GetComponent<TMP_InputField>().text = "10";
            }
            AllPlywoodCubes[i].transform.localScale = new Vector3(1, plywoodLength, 1);
        }
    }
   

}