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
                _isSelected = true;
                if (raycastHit.collider.tag == "Basin" && isBasinInstanciate == false)
                {
                    BasinInstanciate();
                }

                if (_instanciateBasin != null && raycastHit.collider.tag == "Counter")
                {
                    Vector3 targetPosition = new Vector3(raycastHit.point.x, currentBasin.transform.position.y, raycastHit.point.z);
                    _instanciateBasin.transform.position = Vector3.Lerp(currentBasin.transform.position, targetPosition, Time.deltaTime * speed);
                }

            }

        }

    }


    private void BasinInstanciate()
    {
        _instanciateBasin = Instantiate(currentBasin, currentBasin.transform.position, Quaternion.identity);
        _instanciateBasin.transform.parent = currentBasin.transform.parent;
        currentBasin = _instanciateBasin;
        isBasinInstanciate = true;
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
                    Vector3 targetPosition = new Vector3(raycastHit.point.x, currentCounter.transform.position.y, raycastHit.point.z);
                    _instanciateCounter.transform.parent.position = Vector3.Lerp(currentCounter.transform.position, targetPosition, Time.deltaTime * Counterspeed);
                }

                if (isCounterSelected && Input.GetTouch(0).phase == TouchPhase.Ended &&  raycastHit.collider.tag == "Grid")
                {
                    isCounterSelected = false;
                    currentCounter.GetComponent<MeshRenderer>().material.color = Color.white;
                }

            }
           
        }

       

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
            currentBasin.transform.parent = currentCounter.gameObject.transform.parent;
            Vector3 currentCounterPos = new Vector3(currentCounter.transform.localPosition.x, 0f, currentCounter.transform.localPosition.z);
            currentBasin.transform.localPosition = currentCounterPos + new Vector3(1f, 0.296f, 0);
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
        currentCounter.transform.parent.position = new Vector3(currentCounter.transform.parent.position.x, hight, currentCounter.transform.parent.position.z);

    }
}
