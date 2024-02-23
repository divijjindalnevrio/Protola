using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskObject : MonoBehaviour
{
    public GameObject ObjMasked;

    private void OnEnable()
    {
        ObjMasked = GameObject.FindGameObjectWithTag("Counter");
        Debug.Log("topRootObjectname : " + this.transform.root.name);
    }

    void Start()
    {
        ObjMasked.GetComponent<MeshRenderer>().material.renderQueue = 3002;
    }



}

