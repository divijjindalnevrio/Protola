using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RotationScript : MonoBehaviour
{
   
    [SerializeField] private Transform rotateObj;
    [SerializeField] private float speed = 10;
    [SerializeField] private BasinMovement basinMovement;

    private Quaternion target ;
    private bool _isRotateButtonPressed;
    public float BasinRotationVal = 0f;
    public float CounterRotationVal = 0f;
    public event Action OnBasinRotation;

    private void Start()
    {
       
    }

    void Update()
    {
        //<--------------- need to refactor here


        if (basinMovement.SelectedGameobject != null && _isRotateButtonPressed && basinMovement.selectedObject != SelectedObject.none)
        {
            if (basinMovement.selectedObject == SelectedObject.counter)
            {
                rotateObj = basinMovement.SelectedGameobject.transform.parent;
                rotateObj.rotation = RotationObject(rotateObj.gameObject, CounterRotationVal);
            }
            if (basinMovement.selectedObject == SelectedObject.basin)
            {
                rotateObj = basinMovement.SelectedGameobject.transform;
                Debug.Log("BASINROTATION value :" + rotateObj.transform.eulerAngles);
                rotateObj.rotation = RotationObject(rotateObj.gameObject, BasinRotationVal);
               
            }
            
        }

    }

    private Quaternion RotationObject(GameObject gameObject, float RotationVal)
    {
        target = Quaternion.Euler(gameObject.transform.eulerAngles.x, RotationVal, gameObject.transform.eulerAngles.z);
        Quaternion objectRotation = Quaternion.Slerp(gameObject.transform.rotation, target, Time.deltaTime * speed);
        return objectRotation;
    }

  
    public void SettingRightRotateValue()                  //<--------------- need to refactor here
    {
        if(basinMovement.selectedObject != SelectedObject.none)
        {
            if(basinMovement.selectedObject == SelectedObject.basin)
            {
                BasinRotationVal = BasinRotationVal + 90f;
                if (BasinRotationVal > 360)
                {
                    BasinRotationVal = 90f;
                }
            }
            else
            {
                CounterRotationVal = CounterRotationVal + 90f;
                if (CounterRotationVal > 360)
                {
                    CounterRotationVal = 90f;
                }
            }
            
        }
       
    }

    public void SettingLefttRotateValue()
    {
        if (basinMovement.selectedObject != SelectedObject.none)
        {
            if (basinMovement.selectedObject == SelectedObject.basin)
            {
                BasinRotationVal = BasinRotationVal - 90f;
                if (BasinRotationVal < -360)
                {
                    BasinRotationVal = -90f;
                }
            }
            else
            {
                CounterRotationVal = CounterRotationVal - 90f;
                if (CounterRotationVal < -360)
                {
                    CounterRotationVal = -90f;
                }
            }

        }


    }

    public void setRotateButtonToTrue()
    {
        _isRotateButtonPressed = true;
    }

    public void CallingBasinRotationEvent()
    {
        OnBasinRotation();
    }


    
        
}

