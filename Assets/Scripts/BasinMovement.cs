using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasinMovement : MonoBehaviour
{
    [SerializeField] private GameObject basin;
    [SerializeField] private GameObject hole;

    [SerializeField] private Camera mainCam;
    [SerializeField] private float speed = 1;
    [SerializeField] private float Counterspeed = 1;
    [SerializeField] private GameObject counterWhole;

    private GameObject _instanciateBasin;
    private GameObject _instanciateCounter;

    public static bool _isSelected;
    private bool _isBasinGenerate = false;
    private bool _isHoleInstanciate = false;
    public GameObject currentBasin;
    public GameObject currentHole;
    public GameObject currentCounter;

    public LayerMask layerMask;
    public LayerMask CounterlayerMask;

    private bool isBasinInstanciate = false;
    private bool isCounterInstanciate = false;
    private bool isCounterSelected = false;
    private bool isBasinSelected = false;

    [SerializeField] private Slider lengthSlider;
    [SerializeField] private Slider hightSlider;
    [SerializeField] private Slider depthSlider;

    private void Start()
    {
        
    }

    void Update()
    {
  
        CounterAndSinkMovementAndGerenartion();

        if (Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _isSelected = false;
        }
        ChangingSizeOfCounter();
    }

    private void CounterAndSinkMovementAndGerenartion()
    {
        if (Input.touchCount == 1)
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, CounterlayerMask))
            {

                
                if (raycastHit.collider.tag == "Counter" && isCounterInstanciate == false)
                {
                    CounterInstanciate();
                }

                if (_instanciateCounter != null && raycastHit.collider.tag == "Counter" && isCounterSelected == false && Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    isCounterSelected = true;
                    currentCounter.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.cyan;
                }

                if (isCounterSelected && raycastHit.collider.tag == "Counter" && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    _isSelected = true;
                    Vector3 targetPosition = new Vector3(raycastHit.point.x, counterWhole.transform.position.y, raycastHit.point.z);
                    counterWhole.transform.position = Vector3.Lerp(counterWhole.transform.position, targetPosition, Time.deltaTime * Counterspeed);
                }

                if (isCounterSelected && Input.GetTouch(0).phase == TouchPhase.Ended && raycastHit.collider.tag == "Grid")
                {
                    isCounterSelected = false;
                    currentCounter.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.white;
                }

                /////
                ///
                SinkMovement(raycastHit);



            }
            else {

            }
           
        }

}

    private void SinkMovement(RaycastHit rayHit)
    {
        if (isBasinSelected && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            isBasinSelected = false;
            currentBasin.transform.GetChild(0).transform.GetChild(1).GetComponent<MeshRenderer>().material.color = Color.white;
            int BasinCount = currentCounter.transform.GetChild(1).transform.childCount;
            Destroy(currentCounter.transform.GetChild(1).transform.GetChild(0).gameObject);
            Debug.Log("Basin count : " + BasinCount);

        }
        if (rayHit.collider.tag == "Basin" && isBasinSelected == false && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            BasinInstanciate();
            isBasinSelected = true;
            currentBasin.transform.GetChild(0).transform.GetChild(1).GetComponent<MeshRenderer>().material.color = Color.red;

        }

        if (isBasinSelected && rayHit.collider.tag == "Counter" && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            _isSelected = true;
            Vector3 targetPosition = new Vector3(rayHit.point.x, currentBasin.transform.position.y, rayHit.point.z);
            _instanciateBasin.transform.position = Vector3.Lerp(currentBasin.transform.position, targetPosition, Time.deltaTime * speed);
        }

        //if(isBasinSelected && _instanciateBasin != null && Input.GetTouch(0).phase == TouchPhase.Ended && rayHit.collider.CompareTag("Counter"))
        //{
        //   // Destroy((current.transform.parent.Find("Basin").gameObject));
        //    Debug.Log("basin got Deleted");
        //}

        
    }


    private void BasinInstanciate()
    {
        _instanciateBasin = Instantiate(currentBasin, currentBasin.transform.position, Quaternion.identity);
        _instanciateBasin.transform.parent = currentCounter.transform.GetChild(1).transform;
        currentBasin = _instanciateBasin;
        //isBasinInstanciate = true;
    }


    private void CounterInstanciate()
    {
        _instanciateCounter = Instantiate(currentCounter, currentCounter.transform.position, Quaternion.identity);
        Destroy(currentCounter.gameObject);
        _instanciateCounter.transform.parent = currentCounter.transform.parent;
        currentCounter = _instanciateCounter;
        isCounterInstanciate = true;
        if(_isBasinGenerate)
        {
            currentBasin = _instanciateCounter.transform.GetChild(1).transform.GetChild(0).gameObject;
        }
    }



    public void BasinGererator()
    {
        if(_isBasinGenerate == false)
        {
            currentBasin = Instantiate(basin, currentCounter.transform.position, Quaternion.identity);
            currentBasin.transform.parent = currentCounter.transform.GetChild(1).transform;
            Vector3 currentCounterPos = new Vector3(currentCounter.transform.localPosition.x, 0f, currentCounter.transform.localPosition.z);
            currentBasin.transform.localPosition = currentCounterPos + new Vector3(1f, 0.3f, 0);
            _isBasinGenerate = true;
        }
       
    }

    public void HolwGererator()
    {
        if (_isHoleInstanciate == false)
        {
            currentHole = Instantiate(hole, currentCounter.transform.position, Quaternion.identity);
            currentHole.transform.parent = currentCounter.transform;
            currentHole.transform.localPosition =   new Vector3(0, -0.1247f, 0);
            _isHoleInstanciate = true;
        }
            
    }


    private void ChangingSizeOfCounter()
    {
        float length = lengthSlider.value;
        float hight =  hightSlider.value;
        float depth =  depthSlider.value;
        currentCounter.transform.GetChild(0).transform.localScale = new Vector3(length, currentCounter.transform.GetChild(0).transform.localScale.y, depth);
        currentCounter.transform.position = new Vector3(currentCounter.transform.position.x, hight, currentCounter.transform.position.z);

    }

    public void DeleteWholeCounter()
    {
        Destroy(counterWhole);
    }
}
