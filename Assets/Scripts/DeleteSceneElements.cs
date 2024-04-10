using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSceneElements : MonoBehaviour
{
    private BasinsGenerator basinsGenerator;
    private BasinMovement basinMovement;
    private CounterGenerator counterGenerator;
    [SerializeField] private WorldCanvas worldCanvas;
    [SerializeField] private CheckAndCreateCounterCopyScript checkAndCreateCounterCopyScript;
    [SerializeField] private GameObject SceneReloadButton;


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
            GameObject selectedObject = basinMovement.SelectedGameobject.transform.parent.gameObject;
            Destroy(selectedObject);
            RemovingCounterFromDict(selectedObject);
            basinMovement.selectedObject = SelectedObject.none;
            worldCanvas.SettingWorldUiCanvasToFalse();
            basinMovement.counterWhole.transform.Find("PlywoodInputTextFiels").gameObject.SetActive(false);
        }
    }


    private void RemovingCounterFromDict(GameObject obj)
    {
        checkAndCreateCounterCopyScript.TotalCounterInScene.Remove(obj.name);

        if (checkAndCreateCounterCopyScript.TotalCounterInScene.Count == 0)
        {
            SceneReloadButton.SetActive(true);
        }
    }
    
}
