using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldCanvas : MonoBehaviour
{
    [SerializeField] GameObject worldUiObject;
    private GameObject CountersWorldUiCanvasButtons;
    private GameObject BasinWorldUiCanvasButtons;
    [SerializeField] private BasinMovement basinMovement;
    [SerializeField] private CounterGenerator counterGenerator;

    void Start()
    {
        CountersWorldUiCanvasButtons = worldUiObject.transform.GetChild(0).gameObject;
        BasinWorldUiCanvasButtons = worldUiObject.transform.GetChild(1).gameObject;
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
            if(e == SelectedObject.basin)
            {
                BasinWorldUiCanvasButtons.SetActive(true);
                CountersWorldUiCanvasButtons.SetActive(false);
                BasinWorldUiCanvasButtons.transform.parent.position = basinMovement.SelectedGameobject.transform.position;
                
            }
            else
            {
                BasinWorldUiCanvasButtons.SetActive(false);
                CountersWorldUiCanvasButtons.SetActive(true);
                
                CountersWorldUiCanvasButtons.transform.parent.position = basinMovement.currentCounter.transform.position;
            }
            
        }
        else
        {
            SettingWorldUiCanvasToFalse();
        }

    }



    public void SettingWorldUiCanvasToTrue()
    {
        CountersWorldUiCanvasButtons.SetActive(true);
        CountersWorldUiCanvasButtons.transform.parent.position = basinMovement.currentCounter.transform.position;

    }

    public void SettingWorldUiCanvasToFalse()
    {
        CountersWorldUiCanvasButtons.SetActive(false);
    }


}
