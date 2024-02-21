using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldCanvas : MonoBehaviour
{
    public GameObject WorldUiCanvas;
    [SerializeField] private BasinMovement basinMovement;

    void Start()
    {
        basinMovement.OnGameobjectSelected += BasinMovement_OnGameobjectSelected;
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
        WorldUiCanvas.SetActive(true);
    }

    public void SettingWorldUiCanvasToFalse()
    {
        WorldUiCanvas.SetActive(false);
    }


}
