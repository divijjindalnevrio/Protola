using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CounterModel
{
    public float width;
    public float depth;
    public float thickness;
    public Vector3 rotaton;
    public Vector3 position;
    public string texture;
    public string alphaTexture;
    public string colourHexCode;
    public List<float> plywoodTextfield = new List<float>();
    public List<BasinModel> allbasins = new List<BasinModel>();


    public void SettingCounterSize(float width, float thickness, float depth)
    {
        this.width = width;
        this.depth = depth;
        this.thickness = thickness;
    }

    public void SetCounterRotationAndPosition(Vector3 rotation, Vector3 position)
    {
        this.rotaton = rotation;
        this.position = position;
    }

    public void SettingTexture(string texture, string alphaTexture)
    {
        this.texture = texture;
        this.alphaTexture = alphaTexture;
    }
    public void SetTheColor(string hexCode)
    {
        this.colourHexCode = hexCode.Insert(0, "#");
    }

    public void SetPlywoodLength(List<GameObject> plywoods)
    {
        //this.plywoodTextfield.Clear();
        for (int i = 0; i < plywoods.Count; i++)
        {
            this.plywoodTextfield.Add(plywoods[i].transform.localScale.y);
        }
        //foreach(GameObject childPlywood in plywoods)
        //{
        //    plywoodTextfield.Add(childPlywood.transform.localScale.y);
        //}
    }

    public void AssignBasinList(List<BasinModel> basinModels)
    {
        allbasins = basinModels;
    }
}
    