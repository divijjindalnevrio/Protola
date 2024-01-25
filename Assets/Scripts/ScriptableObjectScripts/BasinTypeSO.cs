using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("ScriptableObject/basinType"))]
public class BasinTypeSO : ScriptableObject
{
    public List<GameObject> BasinType = new List<GameObject>();
}
