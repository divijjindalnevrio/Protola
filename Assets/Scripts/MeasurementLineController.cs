using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MeasurementLineController : MonoBehaviour
{
    [SerializeField] private BasinMovement basinMovement;
    [SerializeField] private BasinDashLine basinDashLine;
    [SerializeField] private CounterEdgePoint counterEdgePoint;
    [SerializeField] private List<LineRenderer> measurementLines = new List<LineRenderer>();
    [SerializeField] private List<float> LineLength = new List<float>();

    [SerializeField] private List<GameObject> measurementLinesInputFeilds = new List<GameObject>();
    [SerializeField] private GameObject MeasurementLineUi;

    void Start()
    {
        
    }

    private void Update()
    {
        CalculatingMeasurementLineLength();
    }

    private void CalculatingMeasurementLineLength()
    {
        if(basinMovement.selectedObject == SelectedObject.basin)
        {
            LineLength.Clear();
            float measurementLinesXValOne = measurementLines[0].GetPosition(0).x - measurementLines[0].GetPosition(1).x;
            float measurementLinesXValTwo = measurementLines[1].GetPosition(0).x - measurementLines[1].GetPosition(1).x;
            float measurementLinesZValOne = measurementLines[2].GetPosition(0).z - measurementLines[2].GetPosition(1).z;
            float measurementLinesZValTwo = measurementLines[3].GetPosition(0).z - measurementLines[3].GetPosition(1).z;

            LineLength.Add(Mathf.Abs(measurementLinesXValOne * 100));
            LineLength.Add(Mathf.Abs(measurementLinesXValTwo * 100));
            LineLength.Add(Mathf.Abs(measurementLinesZValOne * 100));
            LineLength.Add(Mathf.Abs(measurementLinesZValTwo * 100));
            SettingTextFeildPosition(measurementLinesXValOne, measurementLinesXValTwo, measurementLinesZValOne, measurementLinesZValTwo);
            AssignMeasurementLineLengthToFeild();
           
        }

    }

    private void SettingTextFeildPosition(float XValOne, float XValTwo, float ZValOne, float ZValTwo)
    {
        MeasurementLineUi.SetActive(true);
        measurementLinesInputFeilds[3].transform.position = basinDashLine.basinEdgePoints[0] + new Vector3(0, 0.2f, ZValOne/2);      // common in z axis
        measurementLinesInputFeilds[2].transform.position = basinDashLine.basinEdgePoints[1] + new Vector3(0, 0.2f, ZValTwo/2);

        measurementLinesInputFeilds[1].transform.position = basinDashLine.basinEdgePoints[2] + new Vector3(XValTwo/2 , 0.2f, 0);         // common in x axis
        measurementLinesInputFeilds[0].transform.position = basinDashLine.basinEdgePoints[3] + new Vector3(XValOne/2, 0.2f, 0);
    }

    private void AssignMeasurementLineLengthToFeild()
    {
       for(int i = 0; i < LineLength.Count; i++)
        {
            measurementLinesInputFeilds[i].transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = LineLength[i].ToString();
        }

       
    }



}
