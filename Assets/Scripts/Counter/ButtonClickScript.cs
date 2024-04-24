using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickScript : MonoBehaviour
{
    [SerializeField] private GameObject counter;
    [SerializeField] private CheckAndCreateCounterCopyScript checkAndCreateCounterCopyScript;
    [SerializeField] private BasinMovement basinMovement;


    private void Start()
    {
        for(int i = 1; i < 5; i++)
        {
            AddCounter(i);
        }
    }
    public void AddCounter(int Uid)
    {
       GameObject counter = basinMovement.SelectedGameobject.transform.parent.gameObject;
       checkAndCreateCounterCopyScript.checkAndInstantiateCounter(counter, counter, Uid);
    }

   
}
