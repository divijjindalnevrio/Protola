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
    public bool isCounterInstanciate = false;

    private bool isCounterSelected = false;

    private bool isInstanciateBasinMoved = false;
    private bool isInstanciateCounterMoved = false;
    private bool basinCanMove = false;

    public bool isPointerOverUI = false;
    public bool isUiCanvasIsOpen = false;
    public SelectedObject selectedObject;
    
    public Material defaultMat;
    [SerializeField] private BasinsGenerator basinsGenerator;
    [SerializeField] private CounterGenerator counterGenerator;
    [SerializeField] private RotationScript rotationScript;
    [SerializeField] private CounterTypeSO counterTypeSO;
    [SerializeField] private BasinBound basinBound;
 


    public event EventHandler<SelectedObject> OnGameobjectSelected;
    public event Action OnGameobjectMoving;
    public event Action OnCounterStopMoving;
    public event Action OnBasinStopMoving;
    public event Action OnBasinMoving;
    public event Action OnCounterMoving;

    private void Start()
    {
        basinsGenerator.OnBasinGenrate += SettingCurrentBasin;
        counterGenerator.OnCounterAdded += SettingCurrentSelectedObject;
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
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (raycastHit.collider.tag == "Counter")
            {
                selectedObject = SelectedObject.counter;
                DeselectingAllDashLines();
                SelectedGameobject = raycastHit.collider.gameObject;
                rotationScript.CounterRotationVal = 0f + SelectedGameobject.transform.eulerAngles.y;
                currentCounter = SelectedGameobject.transform.parent.gameObject;
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
                SelectedGameobject = raycastHit.collider.gameObject;
                rotationScript.BasinRotationVal = 0f + SelectedGameobject.transform.eulerAngles.y;
                SelectedGameobject.transform.localPosition = new Vector3(SelectedGameobject.transform.localPosition.x, SelectedGameobject.transform.localPosition.y + .0010f, SelectedGameobject.transform.localPosition.z);
                OnGameobjectSelected.Invoke(this, selectedObject);
                SelectedGameobject.transform.Find("SelectedDashLineCube").gameObject.SetActive(true);
                basinBound.GetMaxAndMinXPosition();
            }
            else if(raycastHit.collider.tag == "Grid")
            {
                selectedObject = SelectedObject.none;
                SelectedGameobject.transform.Find("SelectedDashLineCube").gameObject.SetActive(false);
                OnGameobjectSelected.Invoke(this, selectedObject);
            }
        }

        return raycastHit;
    }

    private RaycastHit CounterMovement(RaycastHit raycastHit)
    {
        if (isInstanciateCounterMoved && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            isInstanciateCounterMoved = false;
            Destroy(counterGenerator._instanciateCounter);
            isCounterInstanciate = false;
            counterGenerator.EnableAllBasinsCollider();
            OnCounterStopMoving();
        }

        if (raycastHit.collider.tag == "Counter" && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            _isSelected = true;
            if (isCounterInstanciate == false)
            {
                counterGenerator.CounterInstanciate(SelectedGameobject.transform.rotation);
                isCounterInstanciate = true;
            }
            isInstanciateCounterMoved = true;
            counterGenerator.DisableAllBasinsCollider();
            Vector3 targetPosition = new Vector3(raycastHit.point.x, counterWhole.position.y, raycastHit.point.z);
            currentCounter.transform.position = Vector3.Lerp(counterWhole.position, targetPosition, Time.deltaTime * Counterspeed);
            OnGameobjectMoving();
            OnCounterMoving();

        }

        if (Input.GetTouch(0).phase == TouchPhase.Began && raycastHit.collider.tag == "Grid")
        {
            selectedObject = SelectedObject.none;
            OnGameobjectSelected.Invoke(this, selectedObject);
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
            basinCanMove = false;
            OnBasinStopMoving();
        }
        ///

        if (rayHit.collider.tag == "Basin" && Input.GetTouch(0).phase == TouchPhase.Moved && selectedObject == SelectedObject.basin)
        {
            basinCanMove = true;
        }

        if(basinCanMove)
        {
            if (isBasinInstanciate == false)
            {
                basinsGenerator.BasinInstanciate();
            }
            isInstanciateBasinMoved = true;
            Vector3 targetPosition = new Vector3(basinBound.ClapOnXAxis(rayHit, SelectedGameobject.transform).x, SelectedGameobject.transform.position.y, basinBound.ClapOnXAxis(rayHit, SelectedGameobject.transform).z);
            SelectedGameobject.transform.position = Vector3.Lerp(SelectedGameobject.transform.position, targetPosition, Time.deltaTime * speed);
            OnGameobjectMoving();
            OnBasinMoving();
        }
        ////
      
        if (rayHit.collider.tag == "Grid" && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            selectedObject = SelectedObject.none;
            OnGameobjectSelected.Invoke(this, selectedObject);
            SelectedGameobject.transform.Find("SelectedDashLineCube").gameObject.SetActive(false);
        }

    }

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
    private void SettingCurrentSelectedObject()
    {
        // setting added counter to selected
        currentCounter = counterGenerator.currentCounter;
        SelectedGameobject = counterGenerator.currentCounter.transform.Find("Counter").gameObject;
    }

   
}
