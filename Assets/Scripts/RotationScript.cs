using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    [SerializeField] private Transform basin;
    [SerializeField] private Transform counter;
    [SerializeField] private float speed = 10;
    [SerializeField] private BasinMovement basinMovement;

    private Quaternion target ;
    private bool _isBasinButtonPressed;
    private bool _isCounterButtonPressed;
    private float rightRotation = 0f;

    private void Start()
    {
       
    }

    void Update()
    {
                                                                        //<--------------- need to refactor here

        if (basinMovement.currentBasin != null)
        {
            basin = basinMovement.currentBasin.transform;
            if (_isBasinButtonPressed == true)
            {
                basin.rotation = RotationObject(basin.gameObject);
            }
        }
        if (_isCounterButtonPressed)
        {
            counter = basinMovement.currentCounter.transform;
            counter.rotation = RotationObject(counter.gameObject);
        }

    }

    private Quaternion RotationObject(GameObject gameObject)
    {
        target = Quaternion.Euler(gameObject.transform.eulerAngles.x, rightRotation, gameObject.transform.eulerAngles.z);
        Quaternion objectRotation = Quaternion.Slerp(gameObject.transform.rotation, target, Time.deltaTime * speed);
        return objectRotation;
    }

    public void SetBasinToRotate()
    {
        _isCounterButtonPressed = false;
        _isBasinButtonPressed = true;
    }
    public void SetCounterToRotate()
    {
        _isBasinButtonPressed = false;
        _isCounterButtonPressed = true;
    }

    public void SettingRightRotateValue()
    {
        rightRotation = rightRotation + 90f;
    }

    public void SettingLefttRotateValue()
    {
        rightRotation = rightRotation - 90f;
    }
}

