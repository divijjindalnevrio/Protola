using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickScript : MonoBehaviour
{
    [SerializeField] private GameObject counter;
    [SerializeField] private CheckAndCreateCounterCopyScript checkAndCreateCounterCopyScript;
    [SerializeField] private BasinMovement basinMovement;
    // Start is called before the first frame update
    public void AddCounter()
    {
       GameObject counter = basinMovement.SelectedGameobject.transform.parent.gameObject;
       checkAndCreateCounterCopyScript.checkAndInstantiateCounter(counter, counter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
