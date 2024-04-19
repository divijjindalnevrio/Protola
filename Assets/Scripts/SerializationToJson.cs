using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SerializationToJson : MonoBehaviour
{
    // [SerializeField] private GameObject currentCounter;
    [SerializeField] private BasinMovement basinMovement;
    private CounterTypeSO counterTypeSO;
    [SerializeField] private TextAsset jsonFile;
    [SerializeField] private CounterSurfaceChanger counterSurfaceChanger;
    [SerializeField] private Plywoodcontroller plywoodcontroller;
    

    void Start()
    {
        counterSurfaceChanger.SetAllTexturesToDict();
        counterTypeSO = (CounterTypeSO)Resources.Load("Counter");
       
    }

    //creating json at the end
    public void CreatingJsonFile()              
    {
        Vector3 rotation = basinMovement.currentCounter.transform.eulerAngles;
        Vector3 position = basinMovement.currentCounter.transform.position;

        counterTypeSO.SetCounterRotationAndPosition(rotation, position);
        Transform currentCounter = basinMovement.currentCounter.transform.Find("Counter").transform;
        float thickness= basinMovement.currentCounter.transform.position.y;
        counterTypeSO.SettingCounterSize(currentCounter.localScale.x, thickness, currentCounter.localScale.z);
        string colorHexCode = ColorUtility.ToHtmlStringRGBA(currentCounter.GetComponent<MeshRenderer>().materials[1].color);
        counterTypeSO.SetTheColor(colorHexCode);
        string jsonFormat = JsonUtility.ToJson(counterTypeSO.counterModel);

        Material textureMat = currentCounter.GetComponent<MeshRenderer>().materials[0];
        string mainTexture = textureMat.GetTexture("_Texture2D").name;
        string AlphaTexture = textureMat.GetTexture("_AlphaTexture").name;
        counterTypeSO.SettingTexture(mainTexture, AlphaTexture);

        counterTypeSO.SetPlywoodLength(plywoodcontroller.AllPlywoodCubes);
        Debug.Log("there is a masin texture : " + mainTexture + AlphaTexture);

        File.WriteAllText(Application.dataPath + "/ saveJson.json", jsonFormat);
       


    }

    public void CreateInstanceOfSo()
    {
        CounterTypeSO CountSo = ScriptableObject.CreateInstance<CounterTypeSO>();
        Debug.Log("CreateInstanceOfSo" + CountSo.GetType());
    }

    public CounterModel ReadingJson()
    {
        CounterModel counter = new CounterModel();
        string jsonFileString = jsonFile.ToString();
        counter = JsonUtility.FromJson<CounterModel>(jsonFileString);
        return counter;
    }


   

}
