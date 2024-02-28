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
        //GetTheBasinVertices();
        RestTheSelectedDashLineBasin();
        
    }

    private void AssignCurrentBasin()
    {
        CurrentBasin = basinMovement.SelectedGameobject;
    }

    private void RestTheSelectedDashLineBasin()
    {
        GettingDashLinePoints();
        foreach(Vector3 child in vector3s)
        {
           foreach(GameObject obj in basinDashLinePoints)
            {
                obj.transform.localPosition = child;
                Debug.Log("CHECK VAL HERE : " + obj.transform.localPosition);
            }
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
        vector3s.Add(b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) * 0.5f);
        vector3s.Add(b.center + new Vector3(b.size.x, -b.size.y, -b.size.z) * 0.5f);
        vector3s.Add(b.center + new Vector3(b.size.x, -b.size.y, b.size.z) * 0.5f);
        vector3s.Add(b.center + new Vector3(-b.size.x, -b.size.y, b.size.z) * 0.5f);
        vector3s.Add(b.center + new Vector3(-b.size.x, b.size.y, -b.size.z) * 0.5f);
        vector3s.Add(b.center + new Vector3(b.size.x, b.size.y, -b.size.z) * 0.5f);
        vector3s.Add(b.center + new Vector3(b.size.x, b.size.y, b.size.z) * 0.5f);
        vector3s.Add(b.center + new Vector3(-b.size.x, b.size.y, b.size.z) * 0.5f);
    }
}
