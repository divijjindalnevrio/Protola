using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(menuName = ("ScriptableObject/CounterType"))]

public class CounterTypeSO : ScriptableObject
{
    public CounterModel counterModel;
    public GameObject CurrenetCounter;
    public List<GameObject> counterType = new List<GameObject>();
   
}   
