using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject currentBasin;
    public GameObject currentHole;
    public GameObject currentCounter;

    public LayerMask layerMask;
    public LayerMask CounterlayerMask;

    private bool isBasinInstanciate = false;
    private bool isCounterInstanciate = false;

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
                _isSelected = true;
                if (raycastHit.collider.tag == "Counter" && isCounterInstanciate == false)
                {
                    CounterInstanciate();
                }

                if (_instanciateCounter != null && raycastHit.collider.tag == "Counter")
                {
                    Vector3 targetPosition = new Vector3(raycastHit.point.x, currentCounter.transform.position.y, raycastHit.point.z);
                    _instanciateCounter.transform.position = Vector3.Lerp(currentCounter.transform.position, targetPosition, Time.deltaTime * Counterspeed);
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
        if(this.gameObject.transform.childCount < 1)
        {
            currentBasin = Instantiate(basin, currentCounter.transform.position, Quaternion.identity);
            currentBasin.transform.parent = this.gameObject.transform;
            Vector3 currentCounterPos = new Vector3(currentCounter.transform.localPosition.x, 0f, currentCounter.transform.localPosition.z);
            currentBasin.transform.localPosition = currentCounterPos + new Vector3(1f, 0.4031f, 0);
        }
       
    }

    public void HolwGererator()
    {
        if (currentCounter.gameObject.transform.childCount <= 1)
        {
            currentHole = Instantiate(hole, currentCounter.transform.position, Quaternion.identity);
            currentHole.transform.parent = currentCounter.gameObject.transform;
           // Vector3 currentHolePos = new Vector3(currentCounter.transform.localPosition.x, 0f, currentCounter.transform.localPosition.z);
            currentHole.transform.localPosition =   new Vector3(0, -2.7f, 0);
        }

    }

}
