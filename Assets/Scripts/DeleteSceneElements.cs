using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSceneElements : MonoBehaviour
{
    [SerializeField] private BasinsGenerator basinsGenerator;
    [SerializeField] private BasinMovement basinMovement;

    void Start()
    {
        basinMovement = GetComponent<BasinMovement>();
        basinsGenerator = GetComponent<BasinsGenerator>();
    }

    public void RemoveSelectedBasinAndCounter()
    {
        if (basinMovement.selectedObject == SelectedObject.basin)
        {
            Destroy(basinsGenerator.currentBasin.gameObject);
        }
        if (basinMovement.selectedObject == SelectedObject.counter)
        {
            Destroy(basinMovement.counterWhole.gameObject);
            basinsGenerator.IsBasinGenerated = false;

        }

    }
}
