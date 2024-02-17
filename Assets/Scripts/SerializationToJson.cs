using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializationToJson : MonoBehaviour
{
   
    void Start()
    {
        
    }

    //public void Json()
    //{
    //    Vector3 rotation = currentCounter.transform.eulerAngles;
    //    Vector3 position = counterWhole.transform.position;
    //    counterTypeSO.SetCounterRotationAndPosition(rotation, position);
    //    // counterTypeSO.SettingCounterSize(length, hight, depth);
    //    string jsonFormat = JsonUtility.ToJson(counterTypeSO.counterModel);
    //    Debug.Log("jsonFormat : " + jsonFormat);
    //}

    public void CreateInstanceOfSo()
    {
        CounterTypeSO CountSo = ScriptableObject.CreateInstance<CounterTypeSO>();
        Debug.Log("CreateInstanceOfSo" + CountSo.GetType());
    }

}
