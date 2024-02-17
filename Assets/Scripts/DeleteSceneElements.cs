using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSceneElements : MonoBehaviour
{
    private BasinsGenerator basinsGenerator;
    private BasinMovement basinMovement;
    private CounterGenerator counterGenerator;

    void Start()
    {
        basinMovement = transform.Find("CounterWhole").GetComponent<BasinMovement>();
        basinsGenerator = transform.Find("CounterWhole").GetComponent<BasinsGenerator>();
        counterGenerator = GetComponent<CounterGenerator>();
    }

    public void RemoveSelectedBasinAndCounter()
    {
        if (basinMovement.selectedObject == SelectedObject.basin)
        {
            Destroy(basinsGenerator.currentBasin.gameObject);
        }
        if (basinMovement.selectedObject == SelectedObject.counter)
        {
            Destroy(counterGenerator.counterWhole.gameObject);
            basinsGenerator.IsBasinGenerated = false;
        }

    }
}
