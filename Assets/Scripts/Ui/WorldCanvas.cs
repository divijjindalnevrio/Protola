using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldCanvas : MonoBehaviour
{
    private GameObject WorldUiCanvasButtons;
    [SerializeField] private BasinMovement basinMovement;
    [SerializeField] private CounterGenerator counterGenerator;

    void Start()
    {
        WorldUiCanvasButtons = transform.Find("AllWorldButtons").gameObject;
        basinMovement.OnGameobjectSelected += BasinMovement_OnGameobjectSelected;
        basinMovement.OnGameobjectMoving += SettingWorldUiCanvasToFalse;
        basinMovement.OnGameobjectStopMoving += SettingWorldUiCanvasToTrue;
        basinMovement.OnCounterStopMoving += SettingWorldUiCanvasToTrue;
        basinMovement.OnBasinStopMoving += SettingWorldUiCanvasToTrue;
        counterGenerator.OnCounterAdded += SettingWorldUiCanvasToTrue;
    }

    private void BasinMovement_OnGameobjectSelected(object sender, SelectedObject e)
    {
       if(e != SelectedObject.none)
        {
            SettingWorldUiCanvasToTrue();
        }
        else
        {
            SettingWorldUiCanvasToFalse();
        }

    }

    public void SettingWorldUiCanvasToTrue()
    {
        WorldUiCanvasButtons.SetActive(true);
        WorldUiCanvasButtons.transform.parent.position = basinMovement.currentCounter.transform.position;
    }

    public void SettingWorldUiCanvasToFalse()
    {
        WorldUiCanvasButtons.SetActive(false);
    }


}
