using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasinOverlapingController : MonoBehaviour
{
   [SerializeField] private BasinMovement basinMovement;

    void Start()
    {
        basinMovement.OnGameobjectMoving += BasinMovement_OnGameobjectMoving;
        basinMovement.OnGameobjectStopMoving += BasinMovement_OnGameobjectStopMoving;
    }

    private void BasinMovement_OnGameobjectStopMoving()
    {
        RemovingTheRigidbody(basinMovement.SelectedGameobject);
    }

    private void BasinMovement_OnGameobjectMoving()
    {
        AddingRigidbodyToSelectedObject(basinMovement.SelectedGameobject);
    }

    private void AddingRigidbodyToSelectedObject(GameObject selectedGameobject)
    {
        selectedGameobject.AddComponent<Rigidbody>();
        Rigidbody rigidBody = selectedGameobject.GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;
        rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        Debug.Log("rigidbody get set here ");
    }

    private void RemovingTheRigidbody(GameObject selectedGameobject)
    {
        Destroy(selectedGameobject.GetComponent<Rigidbody>());
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OBJECT NAEM : " + other.name);
    }
   
}
