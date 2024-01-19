using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasinMovement : MonoBehaviour
{
    [SerializeField] private GameObject basin;
    [SerializeField] private Camera mainCam;
    [SerializeField] private float speed = 1;

    public static bool _isSelected;
    public GameObject currentBasin;
    [SerializeField] private GameObject _instanciateBasin;
    public LayerMask layerMask;
    private bool isBasinHit = false;
    private bool isBasinSelect = false;

    private Color cubeMat;
    private void Start()
    {
        
        //Input.multiTouchEnabled = false;
    }

    void Update()
    {
        if(currentBasin != null && Input.touchCount == 1)
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
            {
                _isSelected = true;
                Debug.Log("hit counter layer : ");
                if(raycastHit.collider.tag == "Basin" && isBasinHit == false)
                {
                    Debug.Log("yes it is s basin");
                    _instanciateBasin = Instantiate(currentBasin, currentBasin.transform.position, Quaternion.identity);
                    _instanciateBasin.transform.parent = currentBasin.transform.parent;
                    currentBasin = _instanciateBasin;
                    isBasinHit = true;
                }

                if(_instanciateBasin != null && raycastHit.collider.tag == "Counter")
                {
                        Debug.Log("counter is selecting now ");
                        Vector3 targetPosition = new Vector3(raycastHit.point.x, currentBasin.transform.position.y, raycastHit.point.z);
                        _instanciateBasin.transform.position = Vector3.Lerp(currentBasin.transform.position, targetPosition, Time.deltaTime * speed);

                   
                }
               
            }
            
        }


        //if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        //{

        //    _instanciateBasin = Instantiate(currentBasin, currentBasin.transform.position, Quaternion.identity);
        //    _instanciateBasin.transform.parent = currentBasin.transform.parent;

        //}

        //if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        //{
        //   // _isSelected = true;
        //}

        if (Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _isSelected = false;
        }

    }

    public void BasinGererator()
    {
        if(this.gameObject.transform.childCount <= 1)
        {
            currentBasin = Instantiate(basin, transform.position, Quaternion.identity);
            currentBasin.transform.parent = this.gameObject.transform;
            currentBasin.transform.localPosition = new Vector3(1f, 0.4031f, 0);
        }
       
    }

    
}
