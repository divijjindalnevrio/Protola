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
    private bool _isBasinInstanciate = false;
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

    private Color cubeMat;
    private void Start()
    {
        //Input.multiTouchEnabled = false;
    }

    void Update()
    {
        // BasinMovementAndGerenartion();
        CounterMovementAndGerenartion();

        if (Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _isSelected = false;
        }
        ChangingSizeOfCounter();
    }

    private void BasinMovementAndGerenartion()
    {
        if (currentBasin != null && Input.touchCount == 1)
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
            {
               
                if (raycastHit.collider.tag == "Basin" && isBasinInstanciate == false)
                {
                    BasinInstanciate();
                }

                if (_instanciateBasin != null && raycastHit.collider.tag == "Counter")
                {
                    _isSelected = true;
                    Vector3 targetPosition = new Vector3(raycastHit.point.x, currentBasin.transform.position.y, raycastHit.point.z);
                    _instanciateBasin.transform.position = Vector3.Lerp(currentBasin.transform.position, targetPosition, Time.deltaTime * speed);
                }

            }

        }

    }




    private void CounterMovementAndGerenartion()
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
                    currentCounter.GetComponent<MeshRenderer>().material.color = Color.cyan;
                }

                if (isCounterSelected && raycastHit.collider.tag == "Counter" &&  Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    _isSelected = true;
                    Vector3 targetPosition = new Vector3(raycastHit.point.x, counterWhole.transform.position.y, raycastHit.point.z);
                    counterWhole.transform.position = Vector3.Lerp(counterWhole.transform.position, targetPosition, Time.deltaTime * Counterspeed);
                }

                if (isCounterSelected && Input.GetTouch(0).phase == TouchPhase.Ended &&  raycastHit.collider.tag == "Grid")
                {
                    isCounterSelected = false;
                    currentCounter.GetComponent<MeshRenderer>().material.color = Color.white;
                }

                /////
                ///
                SinkMovement(raycastHit);



            }
           
        }

}

    private void SinkMovement(RaycastHit rayHit)
    {

        if (rayHit.collider.tag == "Basin" && isBasinSelected == false && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            BasinInstanciate();
            isBasinSelected = true;
            Debug.Log("basin got selected");
            currentBasin.transform.GetChild(0).transform.GetChild(1).GetComponent<MeshRenderer>().material.color = Color.red;

        }

        if (isBasinSelected && rayHit.collider.tag == "Counter" && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            _isSelected = true;
            Vector3 targetPosition = new Vector3(rayHit.point.x, currentBasin.transform.position.y, rayHit.point.z);
            _instanciateBasin.transform.position = Vector3.Lerp(currentBasin.transform.position, targetPosition, Time.deltaTime * speed);
        }

        if(isBasinSelected && _instanciateBasin != null && Input.GetTouch(0).phase == TouchPhase.Ended && rayHit.collider.CompareTag("Counter"))
        {
           // Destroy((current.transform.parent.Find("Basin").gameObject));
            Debug.Log("basin got Deleted");
        }

        if (isBasinSelected && Input.GetTouch(0).phase == TouchPhase.Ended && rayHit.collider.tag == "Grid")
        {
            isBasinSelected = false;
            currentBasin.transform.GetChild(0).transform.GetChild(1).GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }


    private void BasinInstanciate()
    {
        _instanciateBasin = Instantiate(currentBasin, currentBasin.transform.position, Quaternion.identity);
        _instanciateBasin.transform.parent = counterWhole.gameObject.transform.GetChild(1).transform;
        currentBasin = _instanciateBasin;
        isBasinInstanciate = true;
    }


    private void CounterInstanciate()
    {
        _instanciateCounter = Instantiate(currentCounter, currentCounter.transform.position, Quaternion.identity);
        Destroy(currentCounter.gameObject);
        _instanciateCounter.transform.parent = currentCounter.transform.parent;
        currentCounter = _instanciateCounter;
        isCounterInstanciate = true;
    }



    public void BasinGererator()
    {
        if(_isBasinInstanciate == false)
        {
            currentBasin = Instantiate(basin, currentCounter.transform.position, Quaternion.identity);
            currentBasin.transform.parent = counterWhole.gameObject.transform.GetChild(1).transform;
            Vector3 currentCounterPos = new Vector3(currentCounter.transform.localPosition.x, 0f, currentCounter.transform.localPosition.z);
            currentBasin.transform.localPosition = currentCounterPos + new Vector3(1f, 0.291f, 0);
            _isBasinInstanciate = true;
        }
       
    }

    public void HolwGererator()
    {
        if (_isHoleInstanciate == false)
        {
            currentHole = Instantiate(hole, currentCounter.transform.position, Quaternion.identity);
            currentHole.transform.parent = currentCounter.gameObject.transform.parent;
           // Vector3 currentHolePos = new Vector3(currentCounter.transform.localPosition.x, 0f, currentCounter.transform.localPosition.z);
            currentHole.transform.localPosition =   new Vector3(0, -0.12f, 0);
            _isHoleInstanciate = true;
        }

    }


    private void ChangingSizeOfCounter()
    {
        float length = lengthSlider.value;
        float hight =  hightSlider.value;
        float depth =  depthSlider.value;
        currentCounter.transform.localScale = new Vector3(length, currentCounter.transform.localScale.y, depth);
        counterWhole.transform.position = new Vector3(counterWhole.transform.position.x, hight, counterWhole.transform.position.z);

    }
}
