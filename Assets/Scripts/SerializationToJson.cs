using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SerializationToJson : MonoBehaviour
{
    // [SerializeField] private GameObject currentCounter;
    [SerializeField] private BasinMovement basinMovement;
    private CounterTypeSO counterTypeSO;
    [SerializeField] private TextAsset jsonFile;
    [SerializeField] private CounterSurfaceChanger counterSurfaceChanger;
    [SerializeField] private Plywoodcontroller plywoodcontroller;
    [SerializeField] private CheckAndCreateCounterCopyScript checkAndCreateCounterCopyScript;
    private List<GameObject> allCounters = new List<GameObject>();
    [SerializeField] private List<string> counterJson = new List<string>();
    private string jsonAllData;
    private float plywoodLenth = 0f;

    void Start()
    {
        counterSurfaceChanger.SetAllTexturesToDict();
        counterTypeSO = (CounterTypeSO)Resources.Load("Counter");
       
    }
        
    //creating json at the end
    public void CreatingJsonFile()              
    {
        ConvertingCounterDictToList();

        foreach(GameObject counter in allCounters)
        {

            Vector3 rotation = counter.transform.eulerAngles;
            Vector3 position = counter.transform.position;

            counterTypeSO.SetCounterRotationAndPosition(rotation, position);
            Transform currentCounter = counter.transform.Find("Counter").transform;
            float thickness = counter.transform.position.y;
            counterTypeSO.SettingCounterSize(currentCounter.localScale.x, thickness, currentCounter.localScale.z);
            string colorHexCode = ColorUtility.ToHtmlStringRGBA(currentCounter.GetComponent<MeshRenderer>().materials[1].color);
            counterTypeSO.SetTheColor(colorHexCode);

            Material textureMat = currentCounter.GetComponent<MeshRenderer>().materials[0];
            string mainTexture = textureMat.GetTexture("_Texture2D").name;
            string AlphaTexture = textureMat.GetTexture("_AlphaTexture").name;
            counterTypeSO.SettingTexture(mainTexture, AlphaTexture);

            GetAllPlywoods(currentCounter);
            counterJson.Add(JsonUtility.ToJson(counterTypeSO.counterModel));
            
        }

        string jsonData = string.Join(" ", counterJson);
     
        File.WriteAllText(Application.dataPath + "/saveJson.json", jsonData);

        //Vector3 rotation = basinMovement.currentCounter.transform.eulerAngles;
        //Vector3 position = basinMovement.currentCounter.transform.position;

        //counterTypeSO.SetCounterRotationAndPosition(rotation, position);
        //Transform currentCounter = basinMovement.currentCounter.transform.Find("Counter").transform;
        //float thickness= basinMovement.currentCounter.transform.position.y;
        //counterTypeSO.SettingCounterSize(currentCounter.localScale.x, thickness, currentCounter.localScale.z);
        //string colorHexCode = ColorUtility.ToHtmlStringRGBA(currentCounter.GetComponent<MeshRenderer>().materials[1].color);
        //counterTypeSO.SetTheColor(colorHexCode);

        //Material textureMat = currentCounter.GetComponent<MeshRenderer>().materials[0];
        //string mainTexture = textureMat.GetTexture("_Texture2D").name;
        //string AlphaTexture = textureMat.GetTexture("_AlphaTexture").name;
        //counterTypeSO.SettingTexture(mainTexture, AlphaTexture);

        //counterTypeSO.SetPlywoodLength(plywoodcontroller.AllPlywoodCubes);
        //string jsonFormat = JsonUtility.ToJson(counterTypeSO.counterModel);
        //File.WriteAllText(Application.dataPath + "/ saveJson.json", jsonFormat);



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

    private void ConvertingCounterDictToList()
    {
        allCounters = checkAndCreateCounterCopyScript.TotalCounterInScene.Values.ToList();
    }

    public void GetAllPlywoods(Transform plywoodCube)
    {
        plywoodLenth = plywoodLenth + 1f;
        List<GameObject> AllPlywoodCubes = new List<GameObject>();
        foreach (Transform child in plywoodCube)
        {
            AllPlywoodCubes.Add(child.gameObject);
        }
        counterTypeSO.SetPlywoodLength(AllPlywoodCubes, plywoodLenth);
    }


}
