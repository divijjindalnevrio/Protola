using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiController : MonoBehaviour
{
    public BasinMovement basinMovement;

    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(IsPointerOverUIObject())
        {
            BasinMovement._isSelected = true;
            basinMovement.isPointerOverUI = true;
            Debug.Log("pointerisovermainui");
        }
        else
        {
            basinMovement.isPointerOverUI = false;
        }
    }
    private void OnMouseEnter()
    {
       // Debug.Log("there you go we onmouseEnter");
    }

    public bool IsPointerOverUIObject()      //Called to check if the pointer is over a ui object
    {
        bool value = false;
        for (int i = 0; i < Input.touches.Length; i++)
        {
            int Index = i;
            value = EventSystem.current.IsPointerOverGameObject(Input.GetTouch(Index).fingerId);
            if (value)
                break;
        }
        return value;
    }
}

