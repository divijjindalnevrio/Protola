using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public enum SelectedObject { none,basin, counter, hole }
public class BasinMovement : MonoBehaviour
{
    [SerializeField] private UiModel uiModel;
    [SerializeField] private Camera mainCam;
    [SerializeField] private float speed = 1;
    [SerializeField] private float Counterspeed = 1;


    public static bool _isSelected;
    public GameObject currentCounter;
    public GameObject SelectedDashLineCube;
    public GameObject SelectedDashLineBasin;
    public GameObject SelectedGameobject;
    private Transform currentBasin;
    public Transform counterWhole;
    public LayerMask layerMask;
    public LayerMask CounterlayerMask;

    public bool isBasinInstanciate = false;

    private bool isCounterSelected = false;

    private bool isInstanciateBasinMoved = false;
    public bool isPointerOverUI = false;
    public bool isUiCanvasIsOpen = false;
    public SelectedObject selectedObject;

    public Material defaultMat;
    [SerializeField] private BasinsGenerator basinsGenerator;
    [SerializeField] private CounterGenerator counterGenerator;
    [SerializeField] private RotationScript rotationScript;

    [SerializeField] private CounterTypeSO counterTypeSO;


    public event EventHandler<SelectedObject> OnGameobjectSelected;

    private void Start()
    {
        basinsGenerator.OnBasinGenrate += SettingCurrentBasin;
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
                   
                    SinkMovement(raycastHit); 
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
                // counterWhole = counterGenerator.counterWhole.transform;
                selectedObject = SelectedObject.counter;
                DeselectingAllDashLines();
                 SelectedGameobject = raycastHit.collider.gameObject;
                OnGameobjectSelected.Invoke(this, selectedObject);
                if (basinsGenerator.InstanciateBasin != null)
                {
                    Destroy(basinsGenerator.InstanciateBasin);
                }
                currentCounter.transform.GetChild(0).transform.Find("SelectedDashLineCube").gameObject.SetActive(true);
            }
            else if (raycastHit.collider.tag == "Basin")
            {
                selectedObject = SelectedObject.basin;
                DeselectingAllDashLines();
                rotationScript.BasinRotationVal = 0f;
                SelectedGameobject = raycastHit.collider.gameObject;
                SelectedGameobject.transform.localPosition = new Vector3(SelectedGameobject.transform.localPosition.x, SelectedGameobject.transform.localPosition.y + .0010f, SelectedGameobject.transform.localPosition.z);
                OnGameobjectSelected.Invoke(this, selectedObject);
                SelectedGameobject.transform.Find("SelectedDashLineCube").gameObject.SetActive(true);
            }
        }

        return raycastHit;
    }

    private RaycastHit CounterMovement(RaycastHit raycastHit)
    {

        if (raycastHit.collider.tag == "Counter" && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            _isSelected = true;
            Vector3 targetPosition = new Vector3(raycastHit.point.x, counterWhole.position.y, raycastHit.point.z);
            counterWhole.position = Vector3.Lerp(counterWhole.position, targetPosition, Time.deltaTime * Counterspeed);
        }

        if (Input.GetTouch(0).phase == TouchPhase.Began && raycastHit.collider.tag == "Grid")
        {
            selectedObject = SelectedObject.none;
            currentCounter.transform.GetChild(0).transform.Find("SelectedDashLineCube").gameObject.SetActive(false);
        }

        return raycastHit;
    }

    private void SinkMovement(RaycastHit rayHit)
    {
        _isSelected = true;

        if (isInstanciateBasinMoved && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            isInstanciateBasinMoved = false;
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
            Vector3 targetPosition = new Vector3(rayHit.point.x, SelectedGameobject.transform.position.y, rayHit.point.z);
            SelectedGameobject.transform.position = Vector3.Lerp(SelectedGameobject.transform.position, targetPosition, Time.deltaTime * speed);
        }
        if (rayHit.collider.tag == "Grid" && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            selectedObject = SelectedObject.none;
            SelectedGameobject.transform.Find("SelectedDashLineCube").gameObject.SetActive(false);
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

    //private void ChangingSizeOfCounter()
    //{
    //    width = widthSlider.value;
    //    thickness = thicknessSlider.value;
    //    depth = depthSlider.value;
    //    currentCounter.transform.GetChild(0).transform.localScale = new Vector3(width, currentCounter.transform.GetChild(0).transform.localScale.y, depth);
    //    currentCounter.transform.position = new Vector3(currentCounter.transform.position.x, thickness, currentCounter.transform.position.z);

    //}

    public void DeselectingAllDashLines()
    {
        if (SelectedGameobject != null)
        {
            SelectedGameobject.transform.Find("SelectedDashLineCube").gameObject.SetActive(false);
        }

    }

    private void SettingCurrentBasin()
    {
        currentBasin = basinsGenerator.currentBasin.transform;
    }

   
}
