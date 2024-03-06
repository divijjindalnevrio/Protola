using System.Collections;
using System.Collections.Generic;
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
        TopFourPointsObj = basinMovement.SelectedGameobject.transform.Find("SelectedDashLineCube").transform.Find("TopLineRenderer").gameObject;
    }

    private void GettingCounterEmptyGameobjectPointsPosition()
    {
        counterTopFourPoints.Clear();
        foreach (Transform child in TopFourPointsObj.transform)
        {
            Debug.Log("current counter y position  + " + counterSizeDeformation.currentCounterPosition.y);

            Vector3 totalval = child.transform.position + new Vector3(0, counterSizeDeformation.currentCounterPosition.y, 0);
            counterTopFourPoints.Add(totalval);

            Debug.Log("All valu here  " + totalval + " " + basinDashLine.basinEdgePoints[0]);
        }

    }
}
