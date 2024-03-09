using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSceneElements : MonoBehaviour
{
    private BasinsGenerator basinsGenerator;
    private BasinMovement basinMovement;
    private CounterGenerator counterGenerator;
    [SerializeField] private WorldCanvas worldCanvas;

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
            Destroy(basinMovement.currentBasin);
            basinMovement.selectedObject = SelectedObject.none;
            basinMovement.TriggerOnGameobjectSelectedEvent();
            worldCanvas.SettingWorldUiCanvasToFalse();
        }
        if (basinMovement.selectedObject == SelectedObject.counter)
        {
            Destroy(basinMovement.SelectedGameobject.transform.parent.gameObject);
            basinMovement.selectedObject = SelectedObject.none;
            worldCanvas.SettingWorldUiCanvasToFalse();
        }
    }
}
