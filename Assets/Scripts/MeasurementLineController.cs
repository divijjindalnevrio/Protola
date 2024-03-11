using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasurementLineController : MonoBehaviour
{
    [SerializeField] private BasinMovement basinMovement;
    [SerializeField] private CounterEdgePoint counterEdgePoint;
    [SerializeField] private List<LineRenderer> measurementLines = new List<LineRenderer>();
    [SerializeField] private List<float> LineLength = new List<float>();
   
    void Start()
    {
        
    }

    private void Update()
    {
        CalculatingMeasurementLineLength();
    }

    private void CalculatingMeasurementLineLength()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            LineLength.Clear();
            LineLength.Add((measurementLines[0].GetPosition(0).x - measurementLines[0].GetPosition(1).x) * 100);
            LineLength.Add((measurementLines[1].GetPosition(0).x - measurementLines[1].GetPosition(1).x) * 100);
            LineLength.Add((measurementLines[2].GetPosition(0).z - measurementLines[2].GetPosition(1).z) * 100);
            LineLength.Add((measurementLines[3].GetPosition(0).z - measurementLines[3].GetPosition(1).z) * 100);

        }

      
    }



}
