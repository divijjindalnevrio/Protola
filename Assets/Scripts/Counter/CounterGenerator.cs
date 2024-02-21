using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterGenerator : MonoBehaviour
{
    [SerializeField] BasinMovement basinMovement;
    public GameObject currentCounter;
    public GameObject counterWhole;
    public GameObject _instanciateCounter;

    void Start()
    {
        SettingCounterSelected();
    }

    public void CounterInstanciate()
    {
        _instanciateCounter = Instantiate(counterWhole, counterWhole.transform.position, Quaternion.identity);
        _instanciateCounter.GetComponent<BasinMovement>().enabled = false;
        _instanciateCounter.transform.parent = counterWhole.transform.parent;
        //currentCounter = _instanciateCounter;
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
