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
    private string str;

    public void SettingCounterSize(float width, float thickness, float depth)
    {
        counterModel.width = width;
        counterModel.depth = depth;
        counterModel.thickness = thickness;
    }

    public void SetCounterRotationAndPosition(Vector3 rotation, Vector3 position)
    {
        counterModel.rotaton = rotation;
        counterModel.position = position;
    }

    public void SettingTexture(string texture, string alphaTexture)
    {
        counterModel.texture = texture;
        counterModel.alphaTexture = alphaTexture;
    }
    public void SetTheColor(string hexCode)
    {
        counterModel.colourHexCode = hexCode.Insert(0, "#");
    }

    public void SetPlywoodLength(List<GameObject> plywoods, float length)
    {
        counterModel.plywoodTextfield.Clear();
        for (int i = 0; i < plywoods.Count; i++)
        {
            counterModel.plywoodTextfield.Add(length);
        }
    }

    public void AssignBasinList()
    {

    }
}   
