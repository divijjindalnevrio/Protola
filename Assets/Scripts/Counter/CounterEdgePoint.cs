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

    [SerializeField] private List<LineRenderer> lineRenderers;
    [SerializeField] private RotationScript rotationScript;

    [SerializeField] private List<Vector3> basinEdgePoints;
     private Vector3 basinCenterPoint;
     private List<Vector3> counterEdgePoints;


    void Start() 
    {
        basinEdgePoints = basinDashLine.basinEdgePoints;
        Debug.Log("CounterEdgePoint BasinLength 0 : " + basinEdgePoints.Count);
        basinMovement.OnGameobjectSelected += BasinMovement_OnGameobjectSelected;
        basinMovement.OnBasinMoving += BasinMovement_OnBasinMoving;
        basinsGenerator.OnBasinGenrate += BasinsGenerator_OnBasinGenrate;
        rotationScript.OnBasinRotation += RotationScript_OnBasinRotation;
    }

    private void RotationScript_OnBasinRotation()                                                     // <----- on basin rotation
    {
        Debug.Log("RotationScript_OnBasinRotation");
        GettingBasinCornerPoints();
       
    }

    private void BasinsGenerator_OnBasinGenrate()       // <----- on basin generate 
    {
        GettingCounterCornerPointsObject();
       // GettingBasinCornerPoints();
    }

    private void BasinMovement_OnGameobjectSelected(object sender, SelectedObject e)
    {
        if(e == SelectedObject.basin)
        {
            GettingCounterCornerPointsObject();
           
        }
    }

    private void BasinMovement_OnBasinMoving()
    {
        basinEdgePoints = basinDashLine.basinEdgePoints;
        Debug.Log("CounterEdgePoint BasinLength 1 : " + basinEdgePoints.Count);
        GettingBasinCornerPoints();
    }

    private void GettingCounterCornerPointsObject()
    {
        TopFourPointsObj = basinMovement.currentCounter.transform.Find("Counter").transform.Find("SelectedDashLineCube").transform.
        Find("TopLineRenderer").gameObject;
    }

    private void GettingBasinCornerPoints()
    {
        counterTopFourPoints.Clear();
        foreach (Transform child in TopFourPointsObj.transform)
        {
            //Vector3 totalval = child.transform.position + new Vector3(0, counterSizeDeformation.currentCounterPosition.y, 0);
            counterTopFourPoints.Add(child.transform.position);

            Debug.Log("GettingCounterEmptyGameobjectPointsPosition : " + child.transform.position);
        }
        basinEdgePoints = basinDashLine.basinEdgePoints;
        Debug.Log("CounterEdgePoint BasinLength 2 : " + basinEdgePoints.Count);
        basinCenterPoint = CenterOfVectors(basinEdgePoints);
        counterEdgePoints = GetCounterEdgePoints(counterTopFourPoints, basinCenterPoint);
        DrawingLineRenderer(basinEdgePoints,counterEdgePoints, basinCenterPoint);
    }

    private List<Vector3> GetCounterEdgePoints(List<Vector3> CounterEdgePositions, Vector3 basinCenterPoint) {
        float xMin = CounterEdgePositions.OrderBy(v => v.x).First().x;
        float xMax = CounterEdgePositions.OrderBy(v => v.x).Last().x;

        float zMin = CounterEdgePositions.OrderBy(v => v.z).First().z;
        float zMax = CounterEdgePositions.OrderBy(v => v.z).Last().z;

        List<Vector3> counterEdgePoints = new List<Vector3> {
            new Vector3(xMax,basinCenterPoint.y,basinCenterPoint.z),
            new Vector3(xMin,basinCenterPoint.y,basinCenterPoint.z),
            new Vector3(basinCenterPoint.x,basinCenterPoint.y,zMax),
            new Vector3(basinCenterPoint.x,basinCenterPoint.y,zMin),
            

        };

        return counterEdgePoints;
    }

    private void DrawingLineRenderer(List<Vector3> basinEdgePoints, List<Vector3> counterEdgePoints, Vector3 basinCenterPoint)
    {
        List<Vector3> BasinAndCounterEdgePoints = new List<Vector3>();
        BasinAndCounterEdgePoints.AddRange(basinEdgePoints);
        BasinAndCounterEdgePoints.AddRange(counterEdgePoints);

        FindCommonXVerticesAndDrawLine(BasinAndCounterEdgePoints, basinCenterPoint);
        FindCommonZVerticesAndDrawLine(BasinAndCounterEdgePoints, basinCenterPoint);
    }

    private void FindCommonXVerticesAndDrawLine(List<Vector3> basinEdgePoints, Vector3 basinCenterPoint)
    {
        var groupedVectors = basinEdgePoints.GroupBy(v => v.x);
        // Find the group(s) with the most occurrences
        var maxCount = groupedVectors.Max(g => g.Count());

        // Filter the groups to get only the group(s) with the most occurrences
        var mostCommonXGroups = groupedVectors.Where(g => g.Count() == maxCount);

        // Flatten the groups and extract the vectors
        List<Vector3> vectorsWithMostCommonX = mostCommonXGroups.SelectMany(g => g).ToList();

        Debug.Log($"Vectors with the most common x-axis value:");

        int smallLinerendererposition = 0;
        int bigLinerendererposition = 0;
        for (int i = 0; i < vectorsWithMostCommonX.Count; ++i)
        {
            if (vectorsWithMostCommonX[i].z < basinCenterPoint.z)
            {

                lineRenderers[2].SetPosition(smallLinerendererposition, vectorsWithMostCommonX[i]);
                smallLinerendererposition++;
                Debug.Log("FindCommonZVerticesAndDrawLine Less : " + vectorsWithMostCommonX[i]);
            }
            else
            {

                lineRenderers[3].SetPosition(bigLinerendererposition, vectorsWithMostCommonX[i]);
                bigLinerendererposition++;
                Debug.Log("FindCommonZVerticesAndDrawLine More : " + vectorsWithMostCommonX[i]);
            }

        }
    }


    private void FindCommonZVerticesAndDrawLine(List<Vector3> basinEdgePoints, Vector3 basinCenterPoint)
    {


        var groupedVectors = basinEdgePoints.GroupBy(v => v.z);
        // Find the group(s) with the most occurrences
        var maxCount = groupedVectors.Max(g => g.Count());

        // Filter the groups to get only the group(s) with the most occurrences
        var mostCommonXGroups = groupedVectors.Where(g => g.Count() == maxCount);

        // Flatten the groups and extract the vectors
        List<Vector3> vectorsWithMostCommonZ = mostCommonXGroups.SelectMany(g => g).ToList();

        Debug.Log($"Vectors with the most common x-axis value:");
        int smallLinerendererposition = 0;
        int bigLinerendererposition = 0;
        for (int i = 0; i < vectorsWithMostCommonZ.Count; ++i)
        {
            if (vectorsWithMostCommonZ[i].x < basinCenterPoint.x)
            {
                
                lineRenderers[0].SetPosition(smallLinerendererposition, vectorsWithMostCommonZ[i]);
                smallLinerendererposition++;
                Debug.Log("FindCommonZVerticesAndDrawLine Less : "+ vectorsWithMostCommonZ[i]);
            }
            else {
                
                lineRenderers[1].SetPosition(bigLinerendererposition, vectorsWithMostCommonZ[i]);
                bigLinerendererposition++;
                Debug.Log("FindCommonZVerticesAndDrawLine More : " + vectorsWithMostCommonZ[i]);
            }
           
        }
    }

    public Vector3 CenterOfVectors(List<Vector3> vectors)
    {
        Vector3 sum = Vector3.zero;
        if (vectors == null || vectors.Count == 0)
        {
            return sum;
        }

        foreach (Vector3 vec in vectors)
        {
            sum += vec;
        }
        return sum / vectors.Count;
    }

    
}
