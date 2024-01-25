using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/BasinBlock")]
public class BasinBlockSo : ScriptableObject
{
    public string BasinType;
    public List<GameObject> BasinBlockSO = new List<GameObject>();
}
