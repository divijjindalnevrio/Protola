using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SerializationToJson : MonoBehaviour
{
    // [SerializeField] private GameObject currentCounter;
    [SerializeField] private BasinMovement basinMovement;
    [SerializeField] private TextAsset jsonFile;
    [SerializeField] private CounterSurfaceChanger counterSurfaceChanger;
    [SerializeField] private Plywoodcontroller plywoodcontroller;
    [SerializeField] private CheckAndCreateCounterCopyScript checkAndCreateCounterCopyScript;
    [SerializeField] private List<GameObject> allCounters = new List<GameObject>();
    [SerializeField] private List<string> counterJson = new List<string>();
    private float plywoodLenth = 0f;

   
    
  
    void Start()
    {
        counterSurfaceChanger.SetAllTexturesToDict();
        //DeserializingJson();
    }
        
    //creating json at the end
    public void CreatingJsonFile()              
    {
        GettingCounterList();
        SceneModel sceneModel = new SceneModel();
        foreach (GameObject counter in allCounters)
        {
            Debug.Log("running this foreachloop 1 " + allCounters.Count);
            CounterModel counterModel = new CounterModel();

            Vector3 rotation = counter.transform.eulerAngles;
            Vector3 position = counter.transform.position;

            counterModel.SetCounterRotationAndPosition(rotation, position);
            Transform currentCounter = counter.transform.Find("Counter").transform;
            float thickness = counter.transform.position.y;
            counterModel.SettingCounterSize(currentCounter.localScale.x, thickness, currentCounter.localScale.z);
            string colorHexCode = ColorUtility.ToHtmlStringRGBA(currentCounter.GetComponent<MeshRenderer>().materials[1].color);
            counterModel.SetTheColor(colorHexCode);

            Material textureMat = currentCounter.GetComponent<MeshRenderer>().materials[0];
            string mainTexture = textureMat.GetTexture("_Texture2D").name;
            string AlphaTexture = textureMat.GetTexture("_AlphaTexture").name;
            counterModel.SettingTexture(mainTexture, AlphaTexture);
            GetAllPlywoodsLength(currentCounter, counterModel);

            
            Transform basins  =  counter.transform.Find("Basin").transform;
            List<BasinModel> basinList = new List<BasinModel>();
            foreach (Transform basin in basins)
            {
                BasinModel basinModel = new BasinModel();
                Vector3 basinRotation = basin.eulerAngles;

                Material basinMat = basin.transform.Find("Cube").GetComponent<MeshRenderer>().materials[0];
                string basinmainTexture  = basinMat.GetTexture("_Texture2D").name;
                string basinAlphaTexture = basinMat.GetTexture("_AlphaTexture").name;
                                                                                                
                Color basinColor = basin.transform.Find("Cube").GetComponent<MeshRenderer>().materials[1].color;
                string ColorHexCode = ColorUtility.ToHtmlStringRGBA(basinColor);

                basinModel.SetBasinRotationAndPosition(basinRotation, basin.transform.position);
                basinModel.SettingTexture(basinmainTexture, basinAlphaTexture);
                basinModel.SetTheColor(ColorHexCode);
                basinList.Add(basinModel);

            }

            Debug.Log("total number of basin in basinList :" + basinList.Count);
            counterModel.AssignBasinList(basinList);
            
            sceneModel.allCounters.Add(counterModel);
            
        }

        File.WriteAllText(Application.dataPath + "/saveJson.json", JsonUtility.ToJson(sceneModel, true));
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

    private void GettingCounterList()
    {
        allCounters = checkAndCreateCounterCopyScript.ConvertingCounterDictToList();
    }

    public void GetAllPlywoodsLength(Transform plywoodCube, CounterModel counterModel)
    {
        plywoodLenth = plywoodLenth + 1f;
        List<GameObject> AllPlywoodCubes = new List<GameObject>();
        foreach (Transform child in plywoodCube)
        {
            AllPlywoodCubes.Add(child.gameObject);
        }
        counterModel.SetPlywoodLength(AllPlywoodCubes, plywoodLenth);
    }

    public SceneModel DeserializingJson()
    {
        SceneModel sceneModel = new SceneModel();
        string jsonString = jsonFile.ToString();
        sceneModel =  JsonUtility.FromJson<SceneModel>(jsonString);
        Debug.Log("DeserializingJson : " + sceneModel.allCounters.Count);
        return sceneModel;
    }


  


}

