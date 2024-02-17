using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SelectedObject { none,basin, counter, hole }
public class BasinMovement : MonoBehaviour
{
    

    [SerializeField] private UiModel uiModel;
    [SerializeField] private GameObject hole;

    [SerializeField] private Camera mainCam;
    [SerializeField] private float speed = 1;
    [SerializeField] private float Counterspeed = 1;
    public GameObject counterWhole;

    

    public static bool _isSelected;
    private bool _isBasinGenerate = false;
    private bool _isHoleInstanciate = false;
    public GameObject currentCounter;
    public GameObject SelectedDashLineCube;
    public GameObject SelectedDashLineBasin;
    public GameObject SelectedGameobject;
    private Transform currentBasin;

    public LayerMask layerMask;
    public LayerMask CounterlayerMask;

    public bool isBasinInstanciate = false;
    private bool isCounterInstanciate = false;
    private bool isCounterSelected = false;
    private bool isBasinSelected = false;
    private bool isInstanciateBasinMoved = false;
    public bool isPointerOverUI = false;
    public bool isUiCanvasIsOpen = false;
    public SelectedObject selectedObject;

    [SerializeField] private Slider widthSlider;
    [SerializeField] private Slider thicknessSlider;
    [SerializeField] private Slider depthSlider;
    public Material defaultMat;
    [SerializeField] private BasinsGenerator basinsGenerator;

    [SerializeField] private CounterTypeSO counterTypeSO;

    private float width;
    private float thickness;
    private float depth;

    public event EventHandler<SelectedObject> OnGameobjectSelected;

    private void Start()
    {
        
    }

    void Update()
    {

        if (!isPointerOverUI)
        {
            CounterAndSinkMovementAndGerenartion();
        }
        if (Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _isSelected = false;
        }

    }
  
    private void CounterAndSinkMovementAndGerenartion()
    {
       
        if (Input.touchCount == 1)
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, CounterlayerMask))
            {

                CheckAndUpdateSelectedElement(raycastHit);
                if (selectedObject == SelectedObject.counter)
                {
                    CounterMovement(raycastHit);
                    
                }

                else if (selectedObject == SelectedObject.basin)
                {
                   
                    SinkMovement(raycastHit , currentBasin); 
                }

                else { }
            }

        }

    }

    private RaycastHit CheckAndUpdateSelectedElement(RaycastHit raycastHit)
    {
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            if (raycastHit.collider.tag == "Counter")
            {
               // isCounterSelected = true;
                selectedObject = SelectedObject.counter;
                SelectedGameobject = currentCounter;
                DeselectingAllDashLines();
                OnGameobjectSelected.Invoke(this, selectedObject);
                currentCounter.transform.GetChild(0).transform.Find("SelectedDashLineCube").gameObject.SetActive(true);
            }
            else if (raycastHit.collider.tag == "Basin")
            {
                // isBasinSelected = true;
                currentBasin = basinsGenerator.currentBasin.transform;
                selectedObject = SelectedObject.basin;
                SelectedGameobject = basinsGenerator.currentBasin;
                currentBasin.localPosition = new Vector3(currentBasin.localPosition.x, currentBasin.localPosition.y + .0010f, currentBasin.localPosition.z);
                DeselectingAllDashLines();
                OnGameobjectSelected.Invoke(this, selectedObject);
                currentBasin.Find("SelectedDashLineBasin").gameObject.SetActive(true);
            }
        }

        return raycastHit;
    }

    private RaycastHit CounterMovement(RaycastHit raycastHit)
    {

        if (raycastHit.collider.tag == "Counter" && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            _isSelected = true;
            Vector3 targetPosition = new Vector3(raycastHit.point.x, counterWhole.transform.position.y, raycastHit.point.z);
            counterWhole.transform.position = Vector3.Lerp(counterWhole.transform.position, targetPosition, Time.deltaTime * Counterspeed);
        }

        if (Input.GetTouch(0).phase == TouchPhase.Began && raycastHit.collider.tag == "Grid")
        {
            selectedObject = SelectedObject.none;
            currentCounter.transform.GetChild(0).transform.Find("SelectedDashLineCube").gameObject.SetActive(false);


        }

        return raycastHit;
    }

    private void SinkMovement(RaycastHit rayHit, Transform currentBasin)
    {
        _isSelected = true;

        if (isInstanciateBasinMoved && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            isInstanciateBasinMoved = false;
           // currentBasin.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.color = defaultMat.color;
            Destroy(basinsGenerator.InstanciateBasin);
            isBasinInstanciate = false;
        }

        if (rayHit.collider.tag == "Counter" && Input.GetTouch(0).phase == TouchPhase.Moved && isCounterSelected != true)
        {
            if (isBasinInstanciate == false)
            {
                basinsGenerator.BasinInstanciate();
            }
            isInstanciateBasinMoved = true;
            Vector3 targetPosition = new Vector3(rayHit.point.x, currentBasin.position.y, rayHit.point.z);
            currentBasin.position = Vector3.Lerp(currentBasin.position, targetPosition, Time.deltaTime * speed);
        }
        if (rayHit.collider.tag == "Grid" && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            selectedObject = SelectedObject.none;
            currentBasin.Find("SelectedDashLineBasin").gameObject.SetActive(false);
        }

    }

    //private void CounterInstanciate()
    //{
    //    _instanciateCounter = Instantiate(currentCounter, currentCounter.transform.position, Quaternion.identity);
    //    _instanciateCounter.transform.parent = currentCounter.transform.parent;
    //    currentCounter = _instanciateCounter;
    //    isCounterInstanciate = true;
    //    if(_isBasinGenerate)
    //    {
    //       // currentBasin = _instanciateCounter.transform.GetChild(1).transform.GetChild(0).gameObject;          < ---- Need to see in here.
    //    }
    //}

    private void ChangingSizeOfCounter()
    {
        width = widthSlider.value;
        thickness = thicknessSlider.value;
        depth =  depthSlider.value;
        currentCounter.transform.GetChild(0).transform.localScale = new Vector3(width, currentCounter.transform.GetChild(0).transform.localScale.y, depth);
        currentCounter.transform.position = new Vector3(currentCounter.transform.position.x, thickness, currentCounter.transform.position.z);

    }

    private void DeselectingAllDashLines()
    {
        if (basinsGenerator.currentBasin && currentCounter != null)
        {
            basinsGenerator.currentBasin.transform.Find("SelectedDashLineBasin").gameObject.SetActive(false);
            currentCounter.transform.GetChild(0).transform.Find("SelectedDashLineCube").gameObject.SetActive(false);
        }

    }

}
