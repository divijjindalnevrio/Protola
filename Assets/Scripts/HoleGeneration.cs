using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleGeneration : MonoBehaviour
{
    public GameObject Hole;
    public GameObject currentHole;
    [SerializeField] BasinMovement basinMovement;
    public bool IsHoleInstanciate = false;

    void Start()
    {
        
    }

    public void HolwGererator()
    {
        if (IsHoleInstanciate == false)
        {
            currentHole = Instantiate(Hole, basinMovement.currentCounter.transform.position, Quaternion.identity);
            currentHole.transform.parent = basinMovement.currentCounter.transform;
            currentHole.transform.localPosition = new Vector3(0, -0.1247f, 0);
            IsHoleInstanciate = true;
        }

    }
}
