using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpInitialScene : MonoBehaviour
{
    [SerializeField] private BasinMovement basinMovement;
    [SerializeField] private CounterSizeDeformation counterSizeDeformation;
    [SerializeField] private SerializationToJson serializationToJson;
    private GameObject currentCounter;

    void Start()
    {
        currentCounter = basinMovement.currentCounter;
    }

   
   

    public void SetTheInitialSizeOfCounterfromJson()
    {
        CounterModel model = serializationToJson.ReadingJson();
        if (model != null)
        {
            Debug.Log("SetTheInitialSizeOfCounter eneterd : " + model.depth);
            Transform counter = currentCounter.transform.Find("Counter").transform;
            counter.localScale = new Vector3(model.width, counter.localScale.y, model.depth);
            counterSizeDeformation.SetTheSizeValueOfCurrentCounter();

        }

    }
}
