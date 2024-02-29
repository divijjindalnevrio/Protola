using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class BasinDashLine : MonoBehaviour
{
    public GameObject CurrentBasin;
    public BasinMovement basinMovement;
    [SerializeField] private BasinsGenerator basinsGenerator;
    public List<GameObject> basinDashLinePoints = new List<GameObject>();

    public List<Vector3> basinVertices = new List<Vector3>();
    public List<Vector3> vector3s = new List<Vector3>();

    void Start()
    {
        basinsGenerator.OnBasinGenrate += BasinsGenerator_OnBasinGenrate;
    }

    private void BasinsGenerator_OnBasinGenrate()
    {
        AssignCurrentBasin();
        GetTheBasinVertices();
        RestTheSelectedDashLineBasin();
        
    }

    private void AssignCurrentBasin()
    {
        CurrentBasin = basinMovement.SelectedGameobject;
    }

    private void RestTheSelectedDashLineBasin()
    {
        GettingDashLinePoints();
        for (int j = 0; j < 8; j++)
        {
            basinDashLinePoints[j].transform.localPosition = vector3s[j];
        }
        
    }

    private void GettingDashLinePoints()
    {
        GameObject Dashline = CurrentBasin.transform.Find("SelectedDashLineCube").gameObject;
        basinDashLinePoints.Clear();
        foreach (Transform child in Dashline.transform.Find("TopLineRenderer"))
        {
            basinDashLinePoints.Add(child.gameObject);
        }

        foreach (Transform child in Dashline.transform.Find("BottomLineRenderer"))
        {
            basinDashLinePoints.Add(child.gameObject);
        }  
    }

    private void GetTheBasinVertices()
    {
        MeshFilter basinMeshFilter = CurrentBasin.transform.Find("Cube").GetComponent<MeshFilter>();
        BoxCollider b = CurrentBasin.GetComponent<BoxCollider>();

        //foreach (Vector3 index in vector3s)
        //{
        //    basinVertices.Add(basinMeshFilter.transform.TransformPoint(index));
        //}
        //basinVertices = basinVertices.Distinct().ToList();

        vector3s.Clear();
        Debug.Log("GetTheBasinVertices Center : " + b.center);
        Debug.Log("GetTheBasinVertices xLen : " + b.bounds.size.x);
        Debug.Log("GetTheBasinVertices yLen : " + b.bounds.size.y);
        Debug.Log("GetTheBasinVertices zLen : " + b.bounds.size.z);
        vector3s.Add(b.center + new Vector3(-b.bounds.size.x, -b.bounds.size.y, -b.bounds.size.z) * 0.5f);
        vector3s.Add(b.center + new Vector3(b.bounds.size.x, -b.bounds.size.y, -b.bounds.size.z) * 0.5f);
        vector3s.Add(b.center + new Vector3(b.bounds.size.x, -b.bounds.size.y, b.bounds.size.z) * 0.5f);
        vector3s.Add(b.center + new Vector3(-b.bounds.size.x, -b.bounds.size.y, b.bounds.size.z) * 0.5f);
        vector3s.Add(b.center + new Vector3(-b.bounds.size.x, b.bounds.size.y, -b.bounds.size.z) * 0.5f);
        vector3s.Add(b.center + new Vector3(b.bounds.size.x, b.bounds.size.y, -b.bounds.size.z) * 0.5f);
        vector3s.Add(b.center + new Vector3(b.bounds.size.x, b.bounds.size.y, b.bounds.size.z) * 0.5f);
        vector3s.Add(b.center + new Vector3(-b.bounds.size.x, b.bounds.size.y, b.bounds.size.z) * 0.5f);
    }
}
