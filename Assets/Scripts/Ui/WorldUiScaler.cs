using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldUiScaler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject worldUiObject;
    private float dist;

     void Update()
    {
        dist = Vector3.Distance(worldUiObject.transform.position, mainCamera.transform.position);
        Canvas worldUiCanvas = worldUiObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Canvas>();
        Vector3 vect = (Vector3.one * dist / 3)/50;
        GetAllTheWorldUiCanvas(vect);
       // worldUiCanvas.transform.localScale = vect;
        Debug.Log("Distance between camera and worlduiObject " + dist + vect);
    }


    void GetAllTheWorldUiCanvas(Vector3 vect)
    {
        foreach(Transform canvas in worldUiObject.transform)
        {
            canvas.transform.localScale = vect;
        }
    }
}
