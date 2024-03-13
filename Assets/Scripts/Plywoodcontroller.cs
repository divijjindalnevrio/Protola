using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plywoodcontroller : MonoBehaviour
{
    [SerializeField] private BasinMovement basinMovement;
    [SerializeField] private Transform PlywoodCube;
    private float LineWidth;
    
    void Start()
    {
        basinMovement.OnGameobjectSelected += BasinMovement_OnGameobjectSelected;
        basinMovement.OnCounterStopMoving += BasinMovement_OnCounterStopMoving;
    }

    private void BasinMovement_OnCounterStopMoving()
    {
        AssignPlywood();
        SetPlywoodLineRendererActive();
    }


    private void BasinMovement_OnGameobjectSelected(object sender, SelectedObject e)
    {
       if(e == SelectedObject.counter)
        {
            AssignPlywood();
            SetPlywoodLineRendererActive();
        }

        else
        {
            AssignPlywood();
            SetPlywoodLineRendererDeActive();
        }
    
    }


    private void AssignPlywood()
    {
        Debug.Log("it came to here : ");
        PlywoodCube = basinMovement.currentCounter.transform.Find("Counter").transform.Find("PlywoodCubes").transform;
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
        }
    }

    private void SetPlywoodLineRendererDeActive()
    {
        foreach (Transform child in PlywoodCube)
        {
            //child.transform.Find("LineRenderer").GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);
            child.transform.Find("LineRenderer").GetComponent<LineRenderer>().SetWidth(0f, 0f);
        }
    }
}