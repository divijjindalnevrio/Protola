using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public enum SelectedObject {none,basin, counter, hole}
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
    public GameObject currentBasin;
    [SerializeField] private Vector3 LastPositionSelectedObject;

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
    [SerializeField] private Material DefaultColor;
    [SerializeField] private Material shaderMat;
    [SerializeField] private Material trasparentMat;
    [SerializeField] private BasinsGenerator basinsGenerator;
    [SerializeField] private CounterGenerator counterGenerator;
    [SerializeField] private RotationScript rotationScript;
    [SerializeField] private CounterTypeSO counterTypeSO;
    [SerializeField] private BasinBound basinBound;
    [SerializeField] private BasinDashLine basinDashLine;
    [SerializeField] private BasinAndCounterOverlapingController BasinAndCounterOverlapingController;
    [SerializeField] private Plywoodcontroller plywoodcontroller;


    public event EventHandler<SelectedObject> OnGameobjectSelected;
    public event Action OnGameobjectMoving;
    public event Action OnGameobjectStopMoving;
    public event Action OnCounterStopMoving;
    public event Action OnBasinStopMoving;
    public event Action OnBasinMoving;
    public event Action OnCounterMoving;
    private float touchTime;

    private void Start()
    {
       // basinsGenerator.OnBasinGenrate += SettingCurrentBasin;
        counterGenerator.OnCounterAdded += SettingCurrentSelectedObject;
        OnGameobjectSelected?.Invoke(this, selectedObject);

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
        if (Input.GetTouch(0).phase == TouchPhase.Began && rotationScript.isGameobjectRotation == false)
        {
            Debug.Log("it entered here : ");
             touchTime = Time.time;
            Debug.Log("it entered here : " + touchTime);

            if (raycastHit.collider.tag == "Counter")
            {
                selectedObject = SelectedObject.counter;
                DeselectingAllDashLines();
                SelectedGameobject = raycastHit.collider.gameObject;
                rotationScript.CounterRotationVal = 0f + Mathf.FloorToInt(SelectedGameobject.transform.eulerAngles.y);
               
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
                currentBasin = SelectedGameobject;
                rotationScript.BasinRotationVal = 0f + Mathf.FloorToInt(SelectedGameobject.transform.eulerAngles.y);
                SelectedGameobject.transform.localPosition = new Vector3(SelectedGameobject.transform.localPosition.x, SelectedGameobject.transform.localPosition.y + .0010f, SelectedGameobject.transform.localPosition.z);
                GettingCounterMeshFilter();
                OnGameobjectSelected.Invoke(this, selectedObject);
                SelectedGameobject.transform.Find("SelectedDashLineCube").gameObject.SetActive(true);
                basinBound.GetMaxAndMinXPosition();
            }
            //else if(raycastHit.collider.tag == "Grid" && Input.GetTouch(0).phase == TouchPhase.Ended)
            //{
            //    if(Time.time - touchTime <= 0.5f)
            //    {
            //        Debug.Log("SelectedObject.none triggerd : 1");
            //        selectedObject = SelectedObject.none;
            //        SelectedGameobject.transform.Find("SelectedDashLineCube").gameObject.SetActive(false);
            //        OnGameobjectSelected.Invoke(this, selectedObject);
            //    }
            //    else { return raycastHit; }
            //}
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
            bool _IsGameobjectOverlaping = SelectedGameobject.GetComponent<BasinAndCounterOverlapingController>().IsGameobjectOverlaping;
            if (_IsGameobjectOverlaping)
            {
                currentCounter.transform.position = LastPositionSelectedObject;
            }
            //OnGameobjectStopMoving();
            //OnCounterStopMoving();

            // Assigning the materials to counter.
            List<Material> matlist = new List<Material> { shaderMat, DefaultColor };
            SelectedGameobject.gameObject.GetComponent<MeshRenderer>().SetMaterials(matlist);
            plywoodcontroller.SetTheDefaultPlywoodMaterial(shaderMat, DefaultColor);

            SelectedGameobject.GetComponent<MeshRenderer>().materials[1].color = BasinAndCounterOverlapingController.SelectedCounterInitialColor;
            OnGameobjectStopMoving();
            OnCounterStopMoving();
        }

        if (raycastHit.collider.tag == "Counter" && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            _isSelected = true;
            if (isCounterInstanciate == false)
            {
                counterGenerator.CounterInstanciate(SelectedGameobject.transform.rotation);
                LastPositionSelectedObject = SelectedGameobject.transform.position;
           
                isCounterInstanciate = true;
            }
            isInstanciateCounterMoved = true;
            counterGenerator.DisableAllBasinsCollider();
            Vector3 targetPosition = new Vector3(raycastHit.point.x, currentCounter.transform.position.y, raycastHit.point.z);
            currentCounter.transform.position = Vector3.Lerp(currentCounter.transform.position, targetPosition, Time.deltaTime * Counterspeed);

            // Assigning the materials to counter.
            if (SelectedGameobject.GetComponent<BasinAndCounterOverlapingController>().IsGameobjectOverlaping == false)
            {
                List<Material> matlist = new List<Material> { trasparentMat, trasparentMat };
                SelectedGameobject.gameObject.GetComponent<MeshRenderer>().SetMaterials(matlist);
            }

            OnCounterMoving();
            OnGameobjectMoving();

        }

        if (Input.GetTouch(0).phase == TouchPhase.Ended && raycastHit.collider.tag == "Grid")
        {
            Debug.Log("SelectedObject.none triggerd : " + touchTime + " " + Time.time);
            float diference = Time.time - touchTime;
            if (diference <= 0.2f)
            {
                selectedObject = SelectedObject.none;
                OnGameobjectSelected.Invoke(this, selectedObject);
                currentCounter.transform.GetChild(0).transform.Find("SelectedDashLineCube").gameObject.SetActive(false);
            }
            else { return raycastHit; }
           

        }

        return raycastHit;
    }

    private void SinkMovement(RaycastHit rayHit)
    {
       

        if (isInstanciateBasinMoved && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            isInstanciateBasinMoved = false;
            Destroy(basinsGenerator.InstanciateBasin);
            isBasinInstanciate = false;
            basinCanMove = false;
            
            bool _IsGameobjectOverlaping = SelectedGameobject.GetComponent<BasinAndCounterOverlapingController>().IsGameobjectOverlaping;

            if(_IsGameobjectOverlaping)
            {
                SelectedGameobject.transform.position = LastPositionSelectedObject;
            }
            basinDashLine.GetTheBasinVertices();
            List<Material> matlist = new List<Material> { shaderMat, DefaultColor };
            SelectedGameobject.transform.Find("Cube").GetComponent<MeshRenderer>().SetMaterials(matlist);

            OnBasinStopMoving();
            OnGameobjectStopMoving();
            
        }
        ///

        if (rayHit.collider.tag == "Basin" && Input.GetTouch(0).phase == TouchPhase.Moved && selectedObject == SelectedObject.basin)
        {
            basinCanMove = true;
        }

        if(basinCanMove)
        {
            _isSelected = true;

            if (isBasinInstanciate == false)
            {
                LastPositionSelectedObject = SelectedGameobject.transform.position;     // storing last selectdObject position.
                basinsGenerator.BasinInstanciate();
            }
            isInstanciateBasinMoved = true;
            rotationScript.BasinRotationVal = Mathf.Round(rotationScript.BasinRotationVal);
            Vector3 targetPosition = new Vector3(basinBound.ClapOnXAxis(rayHit, SelectedGameobject.transform).x, SelectedGameobject.transform.position.y, basinBound.ClapOnXAxis(rayHit, SelectedGameobject.transform).z);
            SelectedGameobject.transform.position = Vector3.Lerp(SelectedGameobject.transform.position, targetPosition, Time.deltaTime * speed);

            if(SelectedGameobject.GetComponent<BasinAndCounterOverlapingController>().IsGameobjectOverlaping == false)
            {
                List<Material> matlist = new List<Material> { trasparentMat, trasparentMat };
                SelectedGameobject.transform.Find("Cube").GetComponent<MeshRenderer>().SetMaterials(matlist);
            }

            OnGameobjectMoving();
            OnBasinMoving();
        }
        ////
      
        if (rayHit.collider.tag == "Grid" && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            float diference = Time.time - touchTime;
            if (diference <= 0.2f)
            {
                Debug.Log("SelectedObject.none triggerd : 3");
                selectedObject = SelectedObject.none;
                OnGameobjectSelected.Invoke(this, selectedObject);
                SelectedGameobject.transform.Find("SelectedDashLineCube").gameObject.SetActive(false);
            }
            else { return; }



        }

    }

    public void DeselectingAllDashLines()
    {
        if (SelectedGameobject != null)
        {
            SelectedGameobject.transform.Find("SelectedDashLineCube").gameObject.SetActive(false);
        }

    }


    private void SettingCurrentSelectedObject()
    {
        // setting added counter to selected
        currentCounter = counterGenerator.currentCounter;
        SelectedGameobject = counterGenerator.currentCounter.transform.Find("Counter").gameObject;
    }

    public void TriggerOnGameobjectSelectedEvent()
    {
        OnGameobjectSelected(this, selectedObject);
    }

    private void GettingCounterMeshFilter()
    {
        if (currentBasin != null)
        {
            GameObject counterBaseObj = currentBasin.transform.parent.transform.parent.gameObject;
            basinBound.counterMeshFilter = counterBaseObj.transform.Find("Counter").GetComponent<MeshFilter>();
            currentCounter = counterBaseObj;
            Debug.Log("rootObjName is here : " + basinBound.counterMeshFilter.gameObject.name);
        }
    }

    private void MoveSelectedObjectPreviousPosition()
    {
        SelectedGameobject.transform.position = new Vector3(0, 0, 0);
    }
}
