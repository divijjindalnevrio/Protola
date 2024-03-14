using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plywoodcontroller : MonoBehaviour
{
    [SerializeField] private BasinMovement basinMovement;
    [SerializeField] private Transform PlywoodCube;
    [SerializeField] private List<GameObject> PlywoodInputTextFields = new List<GameObject>();
    [SerializeField] private List<Vector3> centerPosition = new List<Vector3>();
    private Transform PlywoodInputTextFieldParentObj;


    void Start()
    {

        AssignPlywood();
        GettingAllInputFields();
        GettingAllPlywoodCubeCenterPos();

        basinMovement.OnGameobjectSelected += BasinMovement_OnGameobjectSelected;
        basinMovement.OnCounterStopMoving += BasinMovement_OnCounterStopMoving;
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
            Debug.Log("cOUNTTER GOT SELECTED : END");

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
            centerPosition.Add(child.transform.GetChild(1).transform.position);
        }
    }

    private void SetPlywoodLineRendererActive()
    {
        foreach (Transform child in PlywoodCube)
        {
            Debug.Log("it came to here : 1");
            GameObject firstPos  = child.transform.Find("TopNPoint").gameObject;
            GameObject secondPos = child.transform.GetChild(2).gameObject;
            child.transform.Find("LineRenderer").GetComponent<LineRenderer>().SetWidth(0.05f, 0.05f);
            child.transform.Find("LineRenderer").GetComponent<LineRenderer>().SetPosition(0, firstPos.transform.position);
            child.transform.Find("LineRenderer").GetComponent<LineRenderer>().SetPosition(1, secondPos.transform.position);

            //centerPosition.Add(child.transform.GetChild(1).transform.position);

        }
    }

    private void SetPlywoodLineRendererDeActive()
    {
        foreach (Transform child in PlywoodCube)
        {
          
            child.transform.Find("LineRenderer").GetComponent<LineRenderer>().SetWidth(0f, 0f);
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

   

}