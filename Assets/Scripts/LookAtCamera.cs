using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera Maincamera;
    void Start()
    {
        Maincamera = Camera.main;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        

        transform.LookAt(transform.position + Maincamera.transform.rotation * Vector3.back, Maincamera.transform.rotation * Vector3.up);
    }
}
