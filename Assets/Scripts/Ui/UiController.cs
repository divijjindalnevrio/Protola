using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BasinMovement basinMovement;
    public bool inputFieldSelected = false;
    [SerializeField] private bool IsPointerOnMainUi = false;

    void Start()
    {
        
    }

    
    void Update()
    {

        if (IsPointerOverUIObject())
        {
            if (IsPointerOnMainUi)
            {
                BasinMovement._isSelected = true;                   // rotation stop
                //basinMovement.isPointerOverUI = false;
                basinMovement.isPointerOverUI = true;          // raycast detection off
            }
            else
            {
                BasinMovement._isSelected = false;
                basinMovement.isPointerOverUI = true;
            }

        }
        else
        {
            basinMovement.isPointerOverUI = false;
            BasinMovement._isSelected = false;
        }

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

    public void SetInputFieldSelected()
    {
      
        //BasinMovement._isSelected = false;
        //inputFieldSelected = true;
    }

    public void SetInputFieldSelectedToFalse()
    {
        //Debug.Log(" SetInputFieldSelected GOT TRIGGERED : ");
        //BasinMovement._isSelected = false;
        //inputFieldSelected = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Nameabc: " + eventData.pointerCurrentRaycast.gameObject.transform.root.name);
        string rootObj = "UiCanvas";
        if (eventData.pointerCurrentRaycast.gameObject.transform.root.name == rootObj)
        {
            IsPointerOnMainUi = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        BasinMovement._isSelected = false;
        basinMovement.isPointerOverUI = false;
        IsPointerOnMainUi = false;
    }
}

