using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    [SerializeField] private Transform basin;
    [SerializeField] private float speed = 10;
    private Quaternion target ;
    private bool _isRightButtonPressed;
    private float rightRotation = 0f;


    void Update()
    {
        if(_isRightButtonPressed == true)
        {
            basin.rotation = RotationObject(basin.gameObject);
        }
        basin.rotation = RotationObject(basin.gameObject);
    }

    private Quaternion RotationObject(GameObject gameObject)
    {
        target = Quaternion.Euler(gameObject.transform.eulerAngles.x, rightRotation, gameObject.transform.eulerAngles.z);
        Quaternion objectRotation = Quaternion.Slerp(gameObject.transform.rotation, target, Time.deltaTime * speed);
        return objectRotation;
    }

    public void SetTriggerOn()
    {
        _isRightButtonPressed = true;
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

