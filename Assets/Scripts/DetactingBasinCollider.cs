using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetactingBasinCollider : MonoBehaviour
{

    public GameObject CollidedObject;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Basin")
        {
            CollidedObject = other.gameObject;
            Debug.Log("OBJECT NAEM : basin " + this.gameObject.name + " " + other.name);
        }
        else
        {
            Debug.Log("OBJECT NAEM : " + other.name);
        }

    }
}
