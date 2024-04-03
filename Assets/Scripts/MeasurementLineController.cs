using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MeasurementLineController : MonoBehaviour
{
    [SerializeField] private BasinMovement basinMovement;
    [SerializeField] private RotationScript rotationScript;
    [SerializeField] private BasinsGenerator BasinsGenerator;
    [SerializeField] private CounterEdgePoint counterEdgePoint;
    [SerializeField] private List<LineRenderer> measurementLines = new List<LineRenderer>();

    [SerializeField] private List<GameObject> measurementLinesInputFeilds = new List<GameObject>();
    [SerializeField] private GameObject MeasurementLineUi;

    [SerializeField] private List<float> TextFieldValue = new List<float>();
    [SerializeField] private 

    void Start()
    {
        basinMovement.OnBasinMoving += BasinMovement_OnBasinMoving;
        BasinsGenerator.OnBasinGenrate += BasinsGenerator_OnBasinGenrate;
        basinMovement.OnBasinStopMoving += BasinMovement_OnBasinStopMoving;
        basinMovement.OnGameobjectSelected += BasinMovement_OnGameobjectSelected;
        basinMovement.OnGameobjectStopMoving += BasinMovement_OnGameobjectStopMoving;
        rotationScript.OnBasinRotation += RotationScript_OnBasinRotation;
    }

    private void RotationScript_OnBasinRotation()
    {
        CalculatingMeasurementLineLength();
    }

    private void BasinMovement_OnGameobjectStopMoving()
    {
        CalculatingMeasurementLineLength();
      
    }

    private void BasinMovement_OnBasinStopMoving()
    {
        GetInputTextField();
        
    }

    private void BasinMovement_OnGameobjectSelected(object sender, SelectedObject e)
    {
        if (e == SelectedObject.basin)
        {
            if (MeasurementLineUi != null)
            {
                MeasurementLineUi.transform.GetChild(0).gameObject.SetActive(false);
            }
            MeasurementLineUi = basinMovement.currentCounter.transform.Find("MeasurementLineUi").gameObject;
            MeasurementLineUi.transform.GetChild(0).gameObject.SetActive(true);
            GetAllTheMeasurementLinesInputFeilds();
            CalculatingMeasurementLineLength();
        }

        else
        {
           if(MeasurementLineUi != null)
            {
                MeasurementLineUi.transform.GetChild(0).gameObject.SetActive(false);
            }
           
            MeasurementLineUi = basinMovement.currentCounter.transform.Find("MeasurementLineUi").gameObject;
            MeasurementLineUi.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void BasinsGenerator_OnBasinGenrate()
    {
        CalculatingMeasurementLineLength();
        GetInputTextField();

    }

    private void BasinMovement_OnBasinMoving()
    {
        CalculatingMeasurementLineLength();
    }


    private void CalculatingMeasurementLineLength()
    {
        if(basinMovement.selectedObject == SelectedObject.basin)
        {
            for (int i = 0; i < measurementLines.Count; i++) {
                Vector3 LineLengthVector = measurementLines[i].GetPosition(0) - measurementLines[i].GetPosition(1);
                float LineLength = Mathf.Max(Mathf.Abs(LineLengthVector.x), Mathf.Abs(LineLengthVector.y), Mathf.Abs(LineLengthVector.z));
                measurementLinesInputFeilds[i].transform.GetChild(0).gameObject.GetComponent<TMP_InputField>().text = (LineLength).ToString();   //* 100

                 measurementLinesInputFeilds[i].transform.position = new Vector3(measurementLines[i].GetPosition(0).x, measurementLinesInputFeilds[i].transform.position.y, measurementLines[i].GetPosition(0).z);

                if (Mathf.Abs(LineLengthVector.x) > Mathf.Abs(LineLengthVector.z))
                {
                    measurementLinesInputFeilds[i].transform.position += new Vector3(0.4f, 0f, 0f);
                    if(measurementLines[i].GetPosition(0).x > measurementLines[i].GetPosition(1).x)
                    {
                        measurementLinesInputFeilds[i].transform.position += new Vector3(- 1f, 0f, 0f);
                    }
                }
                else
                {
                    measurementLinesInputFeilds[i].transform.position += new Vector3(0, 0f, 0.4f);
                    if (measurementLines[i].GetPosition(0).z > measurementLines[i].GetPosition(1).z)
                    {
                        measurementLinesInputFeilds[i].transform.position += new Vector3(0f, 0f, -.5f);
                    }
                }

            }

        }

    }

    private void GetInputTextField()
    {
        TextFieldValue.Clear();
        for (int i = 0; i < measurementLines.Count; i++)
        {
            TextFieldValue.Add(float.Parse(measurementLinesInputFeilds[i].transform.GetChild(0).gameObject.GetComponent<TMP_InputField>().text));
        }

    }

    private void GetAllTheMeasurementLinesInputFeilds()
    {
        measurementLinesInputFeilds.Clear();
        GameObject measurementUiTextField = MeasurementLineUi.transform.GetChild(0).gameObject;
        foreach (Transform textField in measurementUiTextField.transform)
        {
            measurementLinesInputFeilds.Add(textField.gameObject);
        }
    }




}
