using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("ScriptableObject/CounterType"))]

public class CounterTypeSO : ScriptableObject
{
    public CounterModel counterModel;
    public List<GameObject> counterType = new List<GameObject>();
    public bool isCounterSelected;

    public void SettingCounterSize(float length, float hight, float depth)
    {
        counterModel.length = length;
        counterModel.hight = hight;
        counterModel.depth = depth;
    }

    public void SetCounterRotation(Vector3 rotation, Vector3 position)
    {
        counterModel.rotaton = rotation;
        counterModel.rotaton = position;
    }

    public void SettingTexture(Texture2D texture)
    {
        counterModel.texture = texture;
    }



}
