using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plywoodcontroller : MonoBehaviour
{
    [SerializeField] private BasinMovement basinMovement;
    [SerializeField] private Transform PlywoodCube;
    
    void Start()
    {
        basinMovement.OnGameobjectSelected += BasinMovement_OnGameobjectSelected;
    }

    private void BasinMovement_OnGameobjectSelected(object sender, SelectedObject e)
    {
       if(e == SelectedObject.counter)
        {
            AssignPlywood();
            SetPlywoodLineRendererActive();
        }
        else { return; }
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
            Debug.Log("it came to here : 2 " + firstPos.transform.position);
            //Vector3 firstPos = child.transform.Find("TopNPoint").transform.position;
            // Vector3 secondPos = child.transform.Find("DownNPoint").transform.position;

            child.transform.Find("LineRenderer").GetComponent<LineRenderer>().SetPosition(0, firstPos.transform.position);
            child.transform.Find("LineRenderer").GetComponent<LineRenderer>().SetPosition(1, secondPos.transform.position);
        }
    }
}