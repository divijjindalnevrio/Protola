using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasinOverlapingController : MonoBehaviour
{
    [SerializeField] private BasinMovement basinMovement;
    public GameObject DetectedObject;
    public bool IsBasinOverlaping = false;

    void Start()
    {
        basinMovement = transform.root.transform.GetComponentInChildren<BasinMovement>();
        basinMovement.OnGameobjectMoving += BasinMovement_OnGameobjectMoving;
        basinMovement.OnGameobjectStopMoving += BasinMovement_OnGameobjectStopMoving;
    }

    private void BasinMovement_OnGameobjectStopMoving()
    {
        RemovingTheRigidbody(basinMovement.SelectedGameobject);
        Destroy(basinMovement.SelectedGameobject.GetComponent<BasinOverlapingController>());
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
        if(other.name != "Counter")
        {
            DetectedObject = other.gameObject;
            basinMovement.SelectedGameobject.transform.Find("Cube").GetComponent<MeshRenderer>().material.color = Color.red;
        }

        else { return; }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name != "Counter")
        {
            IsBasinOverlaping = true;
        }
        else { return; }
       
    }

    private void OnTriggerExit(Collider other)
    {
        basinMovement.SelectedGameobject.transform.Find("Cube").GetComponent<MeshRenderer>().material.color = Color.white;
        IsBasinOverlaping = false;
    }

    private void SetColliderIsTriggerOff(GameObject selectedObject)
    {
        selectedObject.GetComponent<BoxCollider>().isTrigger = false;
    }



    //private void BasinOverlap()
    //{
    //    basinMovement.SelectedGameobject.GetComponent<MeshRenderer>().material.color = Color.red;

    //}


}
