using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
   
    [SerializeField] private Transform rotateObj;
    [SerializeField] private float speed = 10;
    [SerializeField] private BasinMovement basinMovement;

    private Quaternion target ;
    private bool _isRotateButtonPressed;
    private float rightRotation = 0f;

    private void Start()
    {
       
    }

    void Update()
    {
                                                                            //<--------------- need to refactor here

       
        if (basinMovement.SelectedGameobject != null && _isRotateButtonPressed && basinMovement.selectedObject != SelectedObject.none)
        {
            rotateObj = basinMovement.SelectedGameobject.transform;
            rotateObj.rotation = RotationObject(rotateObj.gameObject);
        }
         
    }

    private Quaternion RotationObject(GameObject gameObject)
    {
        target = Quaternion.Euler(gameObject.transform.eulerAngles.x, rightRotation, gameObject.transform.eulerAngles.z);
        Quaternion objectRotation = Quaternion.Slerp(gameObject.transform.rotation, target, Time.deltaTime * speed);
        return objectRotation;
    }

  
    public void SettingRightRotateValue()
    {
        if(basinMovement.selectedObject != SelectedObject.none)
        {
            rightRotation = rightRotation + 90f;
        }
       
    }

    public void SettingLefttRotateValue()
    {
        if (basinMovement.selectedObject != SelectedObject.none)
        {
            rightRotation = rightRotation - 90f;
        }
    }

    public void setRotateButtonToTrue()
    {
        _isRotateButtonPressed = true;
    }
        
}

