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
    private GameObject _instanciateBasin;
    public LayerMask layerMask;

    private Color cubeMat;


   
    private void Start()
    {
        
        Input.multiTouchEnabled = false;
    }

    void Update()
    {
        if(currentBasin != null)
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
            {
                Debug.Log("hit counter layer : ");
                if(raycastHit.collider.tag  == "Basin")
                {
                    Debug.Log("yes it is s basin");
                }
                ////currentBasin.transform.position = new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z);
                Vector3 targetPosition = new Vector3(raycastHit.point.x, currentBasin.transform.position.y, raycastHit.point.z);
                currentBasin.transform.position = Vector3.Lerp(currentBasin.transform.position, targetPosition, Time.deltaTime * speed);
            }
        }
        
       

        //if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        //{

        //    _instanciateBasin = Instantiate(currentBasin, currentBasin.transform.position, Quaternion.identity);
        //    _instanciateBasin.transform.parent = currentBasin.transform.parent;

        //}

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
           // _isSelected = true;
        }

        //if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
        //{

        //    _isSelected = false;
        //    Destroy(currentBasin);
        //    currentBasin = _instanciateBasin;
        //}


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
