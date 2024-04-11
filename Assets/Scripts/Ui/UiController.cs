using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiController : MonoBehaviour, IPointerEnterHandler
{
    public BasinMovement basinMovement;
    public bool inputFieldSelected = false;

    void Start()
    {
        
    }

    
    void Update()
    {
        if(inputFieldSelected == false)
        {
            if (IsPointerOverUIObject())
            {
                //BasinMovement._isSelected = true;
                basinMovement.isPointerOverUI = true;



            }
            else
            {
                basinMovement.isPointerOverUI = false;
            }

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
      
        BasinMovement._isSelected = false;
        inputFieldSelected = true;
    }

    public void SetInputFieldSelectedToFalse()
    {
        Debug.Log(" SetInputFieldSelected GOT TRIGGERED : ");
        BasinMovement._isSelected = false;
        inputFieldSelected = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Nameabc: " + eventData.pointerCurrentRaycast.gameObject.name);
        //Debug.Break();
    }
}

