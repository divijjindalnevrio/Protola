using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasinAndCounterOverlapingController : MonoBehaviour
{
    [SerializeField] private BasinMovement basinMovement;
    public GameObject DetectedObject;
    public bool IsGameobjectOverlaping = false;
    public static Color SelectedCounterInitialColor;

    void Start()
    {
        basinMovement = transform.root.transform.GetComponentInChildren<BasinMovement>();
        basinMovement.OnGameobjectMoving += BasinMovement_OnGameobjectMoving;
        basinMovement.OnGameobjectStopMoving += BasinMovement_OnGameobjectStopMoving;
    }

    private void BasinMovement_OnGameobjectStopMoving()
    {
        RemovingTheRigidbody(basinMovement.SelectedGameobject);
        Destroy(basinMovement.SelectedGameobject.GetComponent<BasinAndCounterOverlapingController>());
    }

    private void BasinMovement_OnGameobjectMoving()
    {
        AddingRigidbodyToSelectedObject(basinMovement.SelectedGameobject);
       // SetColliderIsTriggerOff(basinMovement.SelectedGameobject);
    }

    private void AddingRigidbodyToSelectedObject(GameObject selectedGameobject)
    {
        selectedGameobject.AddComponent<Rigidbody>();
        Rigidbody rigidBody = selectedGameobject.GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;
        rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
    }
     
    private void RemovingTheRigidbody(GameObject selectedGameobject)
    {
        Destroy(selectedGameobject.GetComponent<Rigidbody>());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(basinMovement.SelectedGameobject.tag == "Counter")
        {
            if(other.transform.parent.name.Contains("CounterBase") && other.transform.parent.name != basinMovement.SelectedGameobject.transform.parent.name)
            {
                Debug.Log("BasinAndCounterOverlapingController CHECK");
                try
                {
                    
                    basinMovement.SelectedGameobject.GetComponent<MeshRenderer>().materials[0].SetColor("_BaseMap", Color.red);
                    basinMovement.SelectedGameobject.GetComponent<MeshRenderer>().materials[1].SetColor("_BaseMap", Color.red);
                   
                }
                catch (Exception e) {
                    Debug.Log("BasinAndCounterOverlapingController CHECK ERR : "+e);
                }
                

            }
            else {return;}
            

        }

        else if(basinMovement.SelectedGameobject.tag == "Basin")
        {
            if (other.name != "Counter" && other.name != "BasinClone")
            {
                DetectedObject = other.gameObject;
                basinMovement.SelectedGameobject.transform.Find("Cube").GetComponent<MeshRenderer>().material.color = Color.red;
            }
            else {return;}
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (basinMovement.SelectedGameobject.tag == "Counter")
        {
            if (other.transform.parent.name.Contains("CounterBase") && other.transform.parent.name != basinMovement.SelectedGameobject.transform.parent.name)
            {
                Debug.Log("cOUNTER GOT TRIGGERED : counter");
                IsGameobjectOverlaping = true;
            }
            else { return; }
           
        }

        else if (basinMovement.SelectedGameobject.tag == "Basin")
        {
            if (other.name != "Counter" && other.name != "BasinClone")
            {
                IsGameobjectOverlaping = true;
            }
            else { return; }
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (basinMovement.SelectedGameobject.tag == "Counter")
        {
            
            basinMovement.SelectedGameobject.GetComponent<MeshRenderer>().materials[1].color = Color.white;
            IsGameobjectOverlaping = false;

        }

        else if (basinMovement.SelectedGameobject.tag == "Basin")
        {
            basinMovement.SelectedGameobject.transform.Find("Cube").GetComponent<MeshRenderer>().material.color = Color.white;
            IsGameobjectOverlaping = false;
        }
       
    }

    



}
