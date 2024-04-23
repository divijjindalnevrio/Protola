using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("ScriptableObject/basinType"))]
public class BasinTypeSO : ScriptableObject
{
    public BasinModel basinModel;
    public List<GameObject> BasinType = new List<GameObject>();
    

    public void SetBasinRotationAndPosition(Vector3 rotation, Vector3 position)
    {
        basinModel.rotaton = rotation;
        basinModel.position = position;
    }

    public void SettingTexture(string texture, string alphaTexture)
    {
        basinModel.texture = texture;
        basinModel.alphaTexture = alphaTexture;
    }
    public void SetTheColor(string hexCode)
    {
        basinModel.colourHexCode = hexCode.Insert(0, "#");
    }
}
