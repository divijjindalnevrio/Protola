using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CheckAndCreateCounterCopyScript : MonoBehaviour
{
    [SerializeField] private List<Vector3> mainCubeMaxAreaPoints;
    [SerializeField] private GameObject counterBase;
    [SerializeField] private BasinMovement basinMovement;
    [SerializeField] private WorldCanvas worldCanvas;
    public Dictionary<string, GameObject> TotalCounterInScene = new Dictionary<string, GameObject>();

    private void Awake()
    {
        GameObject InitialCounter = basinMovement.counterWhole.Find("CounterBase").gameObject;
        TotalCounterInScene.Add(InitialCounter.name, InitialCounter);
    }
    private void Start()
    {
    }
    private List<Vector3> getMainCubeMaxAreaPoints(Vector3 counter1Point, Bounds counter1Bounds, Bounds counter2Bounds)
    {
        List<Vector3> mainCubeMaxAreaPoints = new List<Vector3>();
        float xdelta = ((counter1Bounds.size.x / 2 + counter2Bounds.size.x / 2) + 0.01f);
        float zdelta = ((counter1Bounds.size.z / 2 + counter2Bounds.size.z / 2) + 0.01f);
        mainCubeMaxAreaPoints.Add(new Vector3(counter1Point.x + xdelta, counter1Point.y, counter1Point.z));
        mainCubeMaxAreaPoints.Add(new Vector3(counter1Point.x, counter1Point.y, counter1Point.z + zdelta));
        mainCubeMaxAreaPoints.Add(new Vector3(counter1Point.x - xdelta, counter1Point.y, counter1Point.z));
        mainCubeMaxAreaPoints.Add(new Vector3(counter1Point.x, counter1Point.y, counter1Point.z - zdelta));
        return mainCubeMaxAreaPoints;
    }


    public void checkAndInstantiateCounter(GameObject targetCounterParent, GameObject counterParent, int Uid)
    {
        GameObject targetCounter = targetCounterParent.transform.GetChild(0).gameObject;
        GameObject counter = counterParent.transform.GetChild(0).gameObject;
        Collider[] hitColliders;
        GameObject nextCounter = null;
        bool cubeInstanciated = false;
        Vector3 targetCounterPoint = targetCounter.transform.position;
        Bounds targetCounterBounds = targetCounter.transform.GetComponent<MeshRenderer>().bounds;
        Bounds counterBounds = counter.transform.GetComponent<MeshRenderer>().bounds;
        mainCubeMaxAreaPoints = getMainCubeMaxAreaPoints(targetCounterPoint, targetCounterBounds, counterBounds);

        GameObject newCounter = null;

        foreach (Vector3 mainCubeMaxAreaPoint in mainCubeMaxAreaPoints) {
            hitColliders = Physics.OverlapBox(mainCubeMaxAreaPoint, transform.localScale / 2, Quaternion.identity);
            List<Collider> colliders = hitColliders.ToList();
            if (colliders.Count == 0)
            {
                newCounter = Instantiate(counterParent, mainCubeMaxAreaPoint,Quaternion.identity,counterBase.transform);
                newCounter.gameObject.name = "CounterBase" + Time.time + Uid;
                TotalCounterInScene.Add(newCounter.name, newCounter);
                Debug.Log("TotalCounterInScene : " + TotalCounterInScene[newCounter.name].name + TotalCounterInScene.Count);
                Debug.Log("Intsantiated at : "+ mainCubeMaxAreaPoint);
                cubeInstanciated = true;
                break;
            }
            else {
                foreach (Collider collider in colliders)
                {
                    Debug.Log(collider.name);
                    if (nextCounter == null) {
                        nextCounter = collider.gameObject;
                    }
                }
            }
        }
        if (cubeInstanciated == false) {
            checkAndInstantiateCounter(nextCounter.transform.parent.gameObject, counterParent, 1);

        }

        DestroyPreviousBasins(newCounter);
        SettingCounterSelected(newCounter);
        newCounter.transform.Find("Counter").GetComponent<MeshRenderer>().materials[0].renderQueue = 3003;
        newCounter.transform.Find("Counter").GetComponent<MeshRenderer>().materials[1].renderQueue = 3002;

    }

    private static void DestroyPreviousBasins(GameObject cunterbase)
    {
        Transform basinParent = cunterbase.transform.Find("Basin").transform;
        if (basinParent.childCount > 0)
        {
            foreach (Transform child in basinParent)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void SettingCounterSelected(GameObject newCounter)
    {
        basinMovement.DeselectingAllDashLines();
        newCounter.transform.Find("Counter").transform.Find("SelectedDashLineCube").gameObject.SetActive(true);
        basinMovement.selectedObject = SelectedObject.counter;
        basinMovement.currentCounter = newCounter;
        basinMovement.SelectedGameobject = newCounter.transform.Find("Counter").gameObject;
        basinMovement.TriggerOnGameobjectSelectedEvent();
        worldCanvas.SettingWorldUiCanvasToTrue();
       
    }

    public List<GameObject> ConvertingCounterDictToList()
    {
        List<GameObject> allCounters = new List<GameObject>();
        allCounters = TotalCounterInScene.Values.ToList();
        return allCounters;
    }
}
