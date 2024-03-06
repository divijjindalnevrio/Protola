using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CounterEdgePoint : MonoBehaviour
{
    [SerializeField] private GameObject TopFourPointsObj;
    [SerializeField] private CounterGenerator counterGenerator;
    [SerializeField] private BasinMovement basinMovement;
    [SerializeField] private CounterSizeDeformation counterSizeDeformation;
    [SerializeField] private BasinDashLine basinDashLine;
    [SerializeField] private BasinsGenerator basinsGenerator;

    [SerializeField] private List<Vector3> counterTopFourPoints = new List<Vector3>();
    [SerializeField] private List<Vector2> vector2s = new List<Vector2>();

    [SerializeField] private LineRenderer lineRenderer;

    void Start() 
    {
        basinMovement.OnGameobjectSelected += BasinMovement_OnGameobjectSelected;
        basinMovement.OnBasinMoving += BasinMovement_OnBasinMoving;
        basinsGenerator.OnBasinGenrate += BasinsGenerator_OnBasinGenrate;
    }

    private void BasinsGenerator_OnBasinGenrate()       // <----- on basin generate 
    {
        GettingCounterEmptyGameobjectPoints();
        GettingCounterEmptyGameobjectPointsPosition();
    }

    private void BasinMovement_OnGameobjectSelected(object sender, SelectedObject e)
    {
        if(e == SelectedObject.basin)
        {
            GettingCounterEmptyGameobjectPoints();
        }
    }

    private void BasinMovement_OnBasinMoving()
    {
        GettingCounterEmptyGameobjectPointsPosition();
    }

    private void GettingCounterEmptyGameobjectPoints()
    {
        TopFourPointsObj = basinMovement.currentCounter.transform.Find("Counter").transform.Find("SelectedDashLineCube").transform.
        Find("TopLineRenderer").gameObject;
    }

    private void GettingCounterEmptyGameobjectPointsPosition()
    {
        counterTopFourPoints.Clear();
        foreach (Transform child in TopFourPointsObj.transform)
        {
            //Vector3 totalval = child.transform.position + new Vector3(0, counterSizeDeformation.currentCounterPosition.y, 0);
            counterTopFourPoints.Add(child.transform.position);

            Debug.Log("GettingCounterEmptyGameobjectPointsPosition : " + child.transform.position);
        }
        GetMinMaxValues(counterTopFourPoints);
        ConvertingToVectorTwo();
        DrawingLineRenderer();
    }

    private void GetMinMaxValues(List<Vector3> CounterEdgePositions) {
        float xMin = CounterEdgePositions.OrderBy(v => v.x).First().x;
        float xMax = CounterEdgePositions.OrderBy(v => v.x).Last().x;

        float zMin = CounterEdgePositions.OrderBy(v => v.z).First().z;
        float zMax = CounterEdgePositions.OrderBy(v => v.z).Last().z;
        Debug.Log("GettingCounterEmptyGameobjectPointsPosition : " + xMin +"    "+xMax+"    "+zMin+"    "+zMax);
    }

    private void ConvertingToVectorTwo()
    {
        vector2s.Clear();
        foreach (Vector3 val in counterTopFourPoints)
        {
            var vector2Val = new Vector3(val.x, val.z);
            vector2s.Add(vector2Val);
        }
        //vector2s = vector2s.Distinct().ToList();
    }

    private void DrawingLineRenderer()
    {
        List<Vector3> basinEdgePoints = basinDashLine.basinEdgePoints;

        //if(basinEdgePoints[])
        lineRenderer.SetPosition(0, basinEdgePoints[0]);
        lineRenderer.SetPosition(1, basinEdgePoints[1]);

    }


}
