using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RotationScript : MonoBehaviour
{
   
    [SerializeField] private Transform rotateObj;
    [SerializeField] private float speed = 12;
    [SerializeField] private BasinMovement basinMovement;

    private Quaternion target ;
    private bool _isRotateButtonPressed;
    public float BasinRotationVal   = 0f;
    public float CounterRotationVal = 0f;
    public event Action OnBasinRotation;
    public event Action OnCounterRotation;
    public event Action OnCounterRotationStop;
 
    private void Start()
    {
       
    }

    IEnumerator RotateTo(float time)
    {

        if (basinMovement.selectedObject == SelectedObject.counter)
        {
            float t = 0;

            while (t < .5f)
            {
                yield return null;
                t += Time.deltaTime / time;

                rotateObj = basinMovement.currentCounter.transform;
                rotateObj.rotation = RotationObject(rotateObj.gameObject, CounterRotationVal);
                OnCounterRotation?.Invoke();
            }
            Debug.Log("rotation got stoped here :  counter");
            OnCounterRotationStop?.Invoke();
        }

        if (basinMovement.selectedObject == SelectedObject.basin)
        {
            float t = 0;

            while (t < .5f)
            {
                yield return null;
                t += Time.deltaTime / time;
                // transform.position = Vector3.Lerp(start, end, t);
                rotateObj = basinMovement.currentBasin.transform;
                rotateObj.rotation = RotationObject(rotateObj.gameObject, BasinRotationVal);
                OnBasinRotation();
            }
            rotateObj.rotation = Quaternion.Euler(new Vector3(rotateObj.rotation.x, BasinRotationVal, rotateObj.rotation.z));



        }

       
    }
    //

    private Quaternion RotationObject(GameObject gameObject, float RotationVal)
    {
        target = Quaternion.Euler(gameObject.transform.eulerAngles.x, RotationVal, gameObject.transform.eulerAngles.z);
        Quaternion objectRotation = Quaternion.Slerp(gameObject.transform.rotation, target, Time.deltaTime * speed);
        return objectRotation;
    }


    public void SettingRightRotateValue()                  //<--------------- need to refactor here
    {
        //if (basinMovement.selectedObject != SelectedObject.none)
        //{
        //    if (basinMovement.selectedObject == SelectedObject.basin)
        //    {
        //        BasinRotationVal = BasinRotationVal + 90f;
        //        if (BasinRotationVal > 360)
        //        {
        //            BasinRotationVal = 90f;
        //        }
        //    }
        //    else
        //    {
        //        CounterRotationVal = CounterRotationVal + 90f;
        //        if (CounterRotationVal > 360)
        //        {
        //            CounterRotationVal = 90f;
        //        }
        //    }

        //}


        if (basinMovement.selectedObject == SelectedObject.counter)
        {
            CounterRotationVal = CounterRotationVal + 90f;
            if (CounterRotationVal > 360)
            {
                CounterRotationVal = 90f;
            }
            StartCoroutine(RotateTo(1));
        }

        if (basinMovement.selectedObject == SelectedObject.basin)
        {

            BasinRotationVal = Mathf.Round(BasinRotationVal + 90f);

            
            if (BasinRotationVal > 360)
            {
                BasinRotationVal = 90f;
            }
            StartCoroutine(RotateTo(1));
            Debug.Log("it is got triggered : ");
        }

       
        else { }


    }

    public void SettingLefttRotateValue()
    {
        //if (basinMovement.selectedObject != SelectedObject.none)
        //{
        //    if (basinMovement.selectedObject == SelectedObject.basin)
        //    {
        //        BasinRotationVal = BasinRotationVal - 90f;
        //        if (BasinRotationVal < -360)
        //        {
        //            BasinRotationVal = -90f;
        //        }
        //    }
        //    else
        //    {
        //        CounterRotationVal = CounterRotationVal - 90f;
        //        if (CounterRotationVal < -360)
        //        {
        //            CounterRotationVal = -90f;
        //        }
        //    }

        //}

        if (basinMovement.selectedObject == SelectedObject.counter)
        {
            CounterRotationVal = CounterRotationVal - 90f;
            if (CounterRotationVal < -360)
            {
                CounterRotationVal = -90f;
            }
            StartCoroutine(RotateTo(1f));
        }

        if (basinMovement.selectedObject == SelectedObject.basin)
            {

                BasinRotationVal = Mathf.Round(BasinRotationVal - 90f);
                if (BasinRotationVal < -360)
                {
                    BasinRotationVal = -90f;
                }
                StartCoroutine(RotateTo(1));
                Debug.Log("it is got triggered : ");
            }
            
        else { }
       


    }

    public void setRotateButtonToTrue()
    {
        _isRotateButtonPressed = true;
    }

    public void CallingBasinRotationEvent()
    {
        //OnBasinRotation();
    }


    
        
}

