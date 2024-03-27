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
        ObjMasked.GetComponent<MeshRenderer>().materials[0].renderQueue = 3003;
        ObjMasked.GetComponent<MeshRenderer>().materials[1].renderQueue = 3002;
    }



}

