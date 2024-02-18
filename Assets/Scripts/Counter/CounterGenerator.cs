using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterGenerator : MonoBehaviour
{
    [SerializeField] BasinMovement basinMovement;
    public GameObject currentCounter;
    public GameObject counterWhole;

    void Awake()
    {
        
    }
    void Start()
    {
        SettingCounterSelected();
    }

    private void CounterInstanciate()
    {
        //_instanciateCounter = Instantiate(currentCounter, currentCounter.transform.position, Quaternion.identity);
        //_instanciateCounter.transform.parent = currentCounter.transform.parent;
        //currentCounter = _instanciateCounter;
        //isCounterInstanciate = true;
        //if (_isBasinGenerate)
        //{
        //    // currentBasin = _instanciateCounter.transform.GetChild(1).transform.GetChild(0).gameObject;
        //}
    }
       private void SettingCounterSelected()
    {
        currentCounter.transform.Find("Counter").transform.Find("SelectedDashLineCube").gameObject.SetActive(true);
        basinMovement.selectedObject = SelectedObject.counter;
        basinMovement.SelectedGameobject = currentCounter.transform.Find("Counter").gameObject;
        basinMovement.counterWhole = this.counterWhole.transform;
    }
}
