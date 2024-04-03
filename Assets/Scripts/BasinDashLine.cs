using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Unity.Mathematics;

public class BasinDashLine : MonoBehaviour
{
    public GameObject CurrentBasin;
    public BoxCollider b;
    public BasinMovement basinMovement;
    [SerializeField] private RotationScript rotationScript;
    [SerializeField] private BasinsGenerator basinsGenerator;
    [SerializeField] private GameObject basinMeasurementLineRenderer;
    public List<GameObject> basinDashLinePoints = new List<GameObject>();

    public List<Vector3> basinVertices = new List<Vector3>();
    public List<Vector3> vector3s = new List<Vector3>();

    public List<Vector2> vector2s = new List<Vector2>();
    public List<Vector3> basinEdgePoints = new List<Vector3>();

    void Start()
    {
        basinsGenerator.OnBasinGenrate += BasinsGenerator_OnBasinGenrate;
        basinMovement.OnBasinMoving += BasinMovement_OnBasinMoving;
        basinMovement.OnBasinStopMoving += BasinMovement_OnBasinStopMoving;
        basinMovement.OnGameobjectSelected += BasinMovement_OnGameobjectSelected;
        rotationScript.OnBasinRotation += RotationScript_OnBasinRotation;
    }

    private void BasinMovement_OnBasinStopMoving()
    {
        AssignBasinAndGetBasinVertices();
        fun();
    }

    private void BasinMovement_OnGameobjectSelected(object sender, SelectedObject e)
    {
        if(e == SelectedObject.basin)
        {
            rotationScript.BasinRotationVal = Mathf.FloorToInt(rotationScript.BasinRotationVal);

            BasinMeasurementLineRendererSetToActive();
            AssignBasinAndGetBasinVertices();
        }
        else
        {
            rotationScript.BasinRotationVal = Mathf.FloorToInt(rotationScript.BasinRotationVal);
            Debug.Log("rotationScript.BasinRotationVal : 1 " + rotationScript.BasinRotationVal);
            BasinMeasurementLineRendererSetToFalse();
        }
    }

    private void RotationScript_OnBasinRotation()
    {
        fun();

    }

    private void fun()
    {
        
        if (rotationScript.BasinRotationVal == 90 || rotationScript.BasinRotationVal == -90 || rotationScript.BasinRotationVal == 270 || rotationScript.BasinRotationVal == -270)
        {
            Debug.Log("RotationScript_OnBasinRotation ABC TRUE");                   // on basin vertical 
            GetTheBasinVertices(true);
        }
        else
        {
            Debug.Log("RotationScript_OnBasinRotation ABC FALSE");
            GetTheBasinVertices();
        }
    }

    private void BasinMovement_OnBasinMoving()
    {
        AssignBasinAndGetBasinVertices();
    }

    private void BasinsGenerator_OnBasinGenrate()
    {
        AssignBasinAndGetBasinVertices();
    }

    public void AssignBasinAndGetBasinVertices()
    {
        AssignCurrentBasin();
        //GetTheBasinVertices();
        fun();
        RestTheSelectedDashLineBasin();
    }
    private void AssignCurrentBasin()
    {
        CurrentBasin = basinMovement.currentBasin;
        b = CurrentBasin.GetComponent<BoxCollider>();
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

    public void GetTheBasinVertices(bool BasinRotated = false)
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
        CalculateBasinMidpoint(b,BasinRotated);
        
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

    private void CalculateBasinMidpoint(BoxCollider b, bool BasinRotated = false)
    {
        Vector3 basinCenterPoint = CurrentBasin.transform.position;

        Vector3 basinSize = new Vector3(b.size.x, b.size.y, b.size.z);
        Debug.Log("Basin size z : " + basinSize.z);
        basinEdgePoints.Clear();

        if (BasinRotated == false)
        {
            Debug.Log("RotationScript_OnBasinRotation ABC FALSE 2");
            basinEdgePoints.Add(new Vector3(basinCenterPoint.x, basinCenterPoint.y, basinCenterPoint.z + basinSize.z * 1f));
            basinEdgePoints.Add(new Vector3(basinCenterPoint.x, basinCenterPoint.y, (basinCenterPoint.z - basinSize.z * 1f)));
            basinEdgePoints.Add(new Vector3(basinCenterPoint.x + basinSize.x, basinCenterPoint.y, basinCenterPoint.z));
            basinEdgePoints.Add(new Vector3(basinCenterPoint.x - basinSize.x, basinCenterPoint.y, basinCenterPoint.z));
        }
        else {
            Debug.Log("RotationScript_OnBasinRotation ABC TRUE 2");
            basinEdgePoints.Add(new Vector3(basinCenterPoint.x + +basinSize.z, basinCenterPoint.y, basinCenterPoint.z ));
            basinEdgePoints.Add(new Vector3(basinCenterPoint.x - basinSize.z, basinCenterPoint.y, (basinCenterPoint.z )));
            basinEdgePoints.Add(new Vector3(basinCenterPoint.x , basinCenterPoint.y, basinCenterPoint.z + basinSize.x));
            basinEdgePoints.Add(new Vector3(basinCenterPoint.x , basinCenterPoint.y, basinCenterPoint.z - basinSize.x));
        }
        Debug.Log("BasinMovement_OnGameobjectStopMoving_CHECK CenterPoint Of Basin : " );
    }

    public void BasinMeasurementLineRendererSetToActive()
    {
        basinMeasurementLineRenderer.SetActive(true);
    }

    public void BasinMeasurementLineRendererSetToFalse()
    {
        basinMeasurementLineRenderer.SetActive(false);
    }



}
