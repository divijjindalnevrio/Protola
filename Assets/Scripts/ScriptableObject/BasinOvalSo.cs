using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/BasinOval")]
public class BasinOvalSo : ScriptableObject
{
    public string BasinType;
    public List<GameObject> BasinOval = new List<GameObject>();
}
