using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterGenerator : MonoBehaviour
{
    [SerializeField] BasinMovement basinMovement;
    public GameObject currentCounter;
    public GameObject counterWhole;
    public GameObject _instanciateCounter;
    [SerializeField] private WorldCanvas worldCanvas;

    void Start()
    {
        SettingCounterSelected();
    }

    public void CounterInstanciate()
    {
        _instanciateCounter = Instantiate(counterWhole, counterWhole.transform.position, Quaternion.identity);
        _instanciateCounter.GetComponent<BasinMovement>().enabled = false;
        Destroy(_instanciateCounter.transform.Find("WorldUiCanvas").gameObject);
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

    public void AddingCounter()
    {
        GameObject cunterbase = Instantiate(basinMovement.SelectedGameobject.transform.parent.gameObject,
        currentCounter.transform.position + new Vector3(5f, 0f, 0f), Quaternion.identity);
        DestroyPreviousBasins(cunterbase);
        cunterbase.transform.SetParent(counterWhole.transform, false);
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
}
