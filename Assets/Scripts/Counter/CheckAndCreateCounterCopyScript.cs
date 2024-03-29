using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CheckAndCreateCounterCopyScript : MonoBehaviour
{
    
    [SerializeField] private List<Vector3> mainCubeMaxAreaPoints;
    [SerializeField] private GameObject counterBase;
    private List<Vector3> getMainCubeMaxAreaPoints(Vector3 counter1Point, Bounds counter1Bounds, Bounds counter2Bounds) {
        List<Vector3> mainCubeMaxAreaPoints = new List<Vector3>();
        float xdelta = ((counter1Bounds.size.x / 2 + counter2Bounds.size.x / 2) + 0.01f);
        float zdelta = ((counter1Bounds.size.z / 2 + counter2Bounds.size.z / 2) + 0.01f);
        mainCubeMaxAreaPoints.Add(new Vector3(counter1Point.x + xdelta, counter1Point.y, counter1Point.z));
        mainCubeMaxAreaPoints.Add(new Vector3(counter1Point.x, counter1Point.y, counter1Point.z + zdelta));
        mainCubeMaxAreaPoints.Add(new Vector3(counter1Point.x - xdelta, counter1Point.y, counter1Point.z));
        mainCubeMaxAreaPoints.Add(new Vector3(counter1Point.x, counter1Point.y, counter1Point.z - zdelta));
        return mainCubeMaxAreaPoints;
    }


    public void checkAndInstantiateCounter(GameObject targetCounterParent, GameObject counterParent)
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
        
       

        foreach (Vector3 mainCubeMaxAreaPoint in mainCubeMaxAreaPoints) {
            hitColliders = Physics.OverlapBox(mainCubeMaxAreaPoint, transform.localScale / 2, Quaternion.identity);
            List<Collider> colliders = hitColliders.ToList();
            if (colliders.Count == 0)
            {
                GameObject newCounter = Instantiate(counterParent, mainCubeMaxAreaPoint,Quaternion.identity,counterBase.transform);
                newCounter.gameObject.name = "CounterBase" + Time.time;
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
            checkAndInstantiateCounter(nextCounter.transform.parent.gameObject, counterParent);

        }
        
        
    }
}
