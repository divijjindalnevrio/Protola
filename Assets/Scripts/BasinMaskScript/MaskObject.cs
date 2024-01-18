using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskObject : MonoBehaviour
{
    public GameObject ObjMasked;

    private void OnEnable()
    {
        ObjMasked = GameObject.FindGameObjectWithTag("Counter");
    }
    void Start()
    {
        ObjMasked.GetComponent<MeshRenderer>().material.renderQueue = 3002;
    }


    //private void RawFun()
    //{
    //    for (int i = 0; i < ObjMasked.Length; i++)
    //    {
    //        ObjMasked[i].GetComponent<MeshRenderer>().material.renderQueue = 3002;
    //    }
    //}

}

