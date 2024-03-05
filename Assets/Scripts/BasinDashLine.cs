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

    public List<Vector2> vector2s = new List<Vector2>();

    void Start()
    {
        basinsGenerator.OnBasinGenrate += BasinsGenerator_OnBasinGenrate;
        basinMovement.OnGameobjectMoving += BasinMovement_OnGameobjectMoving;
    }

    private void BasinMovement_OnGameobjectMoving()
    {
        if(CurrentBasin != null)
        {
            AssignBasinAndGetBasinVertices();
        }
        
    }

    private void BasinsGenerator_OnBasinGenrate()
    {
        AssignBasinAndGetBasinVertices();
    }

    private void AssignBasinAndGetBasinVertices()
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

        vector3s.Clear();
        Debug.Log("GetTheBasinVertices Center : " + b.center);

        vector3s.Add(b.center + new Vector3(-b.size.x * 0.6f, -b.size.y * 1f, -b.size.z * 1f));
        vector3s.Add(b.center + new Vector3(b.size.x * 0.6f, -b.size.y * 1f, -b.size.z * 1f));
        vector3s.Add(b.center + new Vector3(b.size.x * 0.6f, -b.size.y * 1f, b.size.z * 1f));
        vector3s.Add(b.center + new Vector3(-b.size.x * 0.6f, -b.size.y * 1f, b.size.z * 1f));
        vector3s.Add(b.center + new Vector3(-b.size.x * 0.6f, b.size.y * 1f, -b.size.z * 1f));
        vector3s.Add(b.center + new Vector3(b.size.x * 0.6f, b.size.y * 1f, -b.size.z * 1f));
        vector3s.Add(b.center + new Vector3(b.size.x * 0.6f, b.size.y * 1f, b.size.z * 1f));
        vector3s.Add(b.center + new Vector3(-b.size.x * 0.6f, b.size.y * 1f, b.size.z * 1f));
        ConvertingToVectorTwo();
        CalculateBasinMidpoint(b);
        
    }

    private void ConvertingToVectorTwo()
    {
        vector2s.Clear();
        foreach (Vector3 val in vector3s)
        {
            var vector2Val = new Vector3(val.x, val.z);
            vector2s.Add(vector2Val);
        }
        vector2s = vector2s.Distinct().ToList();
    }

    private void CalculateBasinMidpoint(BoxCollider b)
    {
        Debug.Log("Basin posiion here :" + basinMovement.SelectedGameobject.transform.position);

        Vector3 basinCenterPoint = CurrentBasin.transform.position;
        Debug.Log("basinCenterPoint : " + CurrentBasin.transform.position);
        Vector3 basinSize = new Vector3(b.size.x, b.size.y, b.size.z);
        Vector3 basinEdgePointOne = new Vector3(basinCenterPoint.x, basinCenterPoint.y, basinCenterPoint.z + basinSize.z * 1f);
        Vector3 basinEdgePointTwo = new Vector3(basinCenterPoint.x, basinCenterPoint.y, - (basinCenterPoint.z + basinSize.z * 1f));

        Vector3 basinEdgePointThree = new Vector3(basinCenterPoint.x + basinSize.x, basinCenterPoint.y, basinCenterPoint.z);
        Vector3 basinEdgePointFour  =  new Vector3(basinCenterPoint.x - basinSize.x, basinCenterPoint.y, basinCenterPoint.z);

        Debug.Log("basinCenterPointAll basinEdgePointsVal :" + basinEdgePointOne + basinEdgePointTwo + basinEdgePointThree + basinEdgePointFour);

    }



}
