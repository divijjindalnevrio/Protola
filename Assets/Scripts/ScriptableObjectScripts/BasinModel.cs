using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasinModel 
{
    public Vector3 rotaton;
    public Vector3 position;
    public string texture;
    public string alphaTexture;
    public string colourHexCode;

    public void SetBasinRotationAndPosition(Vector3 rotation, Vector3 position)
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
}
