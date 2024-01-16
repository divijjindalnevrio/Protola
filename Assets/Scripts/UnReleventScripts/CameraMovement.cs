using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private float distanceToTarget = 10;
   

    private Vector3 cameraPosition;
    private Plane Plane;

    private Vector3 previousPosition;

    private void Start()
    {
       // Input.multiTouchEnabled = false;
        cameraPosition = target.transform.position;
    }

    private void Update()
    {
        //Update Plane
        if (Input.touchCount >= 1)
            Plane.SetNormalAndPosition(transform.up, transform.position);

        var Delta1 = Vector3.zero;
        var Delta2 = Vector3.zero;

        //Scroll
        if (Input.touchCount >= 1)
        {
            Delta1 = PlanePositionDelta(Input.GetTouch(0));
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
                cam.transform.Translate(Delta1, Space.World);
           
        }

        ////////////////////////////////////////////////////
        if (Input.touchCount == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
                Debug.Log("previousPosition : " + previousPosition);
            }
            else if (Input.touchCount > 0)
            {

                Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
                Vector3 direction = previousPosition - newPosition;

                float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
                float rotationAroundXAxis = direction.y * 180; // camera moves vertically

                cam.transform.position = cameraPosition;

                cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
                cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);


                cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

                previousPosition = newPosition;
            }
        }

        if (Input.touchCount >= 2)
        {
            var pos1 = PlanePosition(Input.GetTouch(0).position);
            var pos2 = PlanePosition(Input.GetTouch(1).position);
            var pos1b = PlanePosition(Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition);
            var pos2b = PlanePosition(Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);

            //calc zoom
            var zoom = Vector3.Distance(pos1, pos2) /
                       Vector3.Distance(pos1b, pos2b);

            //edge case
            if (zoom == 0 || zoom > 10)
                return;

            //Move cam amount the mid ray
            cam.transform.position = Vector3.LerpUnclamped(pos1, cam.transform.position, 1 / zoom);

            

        }

        
    }


    protected Vector3 PlanePositionDelta(Touch touch)
    {
        //not moved
        if (touch.phase != TouchPhase.Moved)
            return Vector3.zero;

        //delta
        var rayBefore = cam.ScreenPointToRay(touch.position - touch.deltaPosition);
        var rayNow = cam.ScreenPointToRay(touch.position);
        if (Plane.Raycast(rayBefore, out var enterBefore) && Plane.Raycast(rayNow, out var enterNow))
            return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);

        //not on plane
        return Vector3.zero;
    }

    protected Vector3 PlanePosition(Vector2 screenPos)
    {
        //position
        var rayNow = cam.ScreenPointToRay(screenPos);
        if (Plane.Raycast(rayNow, out var enterNow))
            return rayNow.GetPoint(enterNow);

        return Vector3.zero;
    }


    //private void CameraZoomInOut()
    //{
    //    float FovincreaseValue = 10f;
        
    //    if (Input.mouseScrollDelta.y > 0)
    //    {
    //        float targetValue = cam.GetComponent<Camera>().fieldOfView + FovincreaseValue;
    //         zoomVal =  Mathf.Clamp(targetValue, 20f, 90f);
 
    //    }
    //    if (Input.mouseScrollDelta.y < 0)
    //    {
    //        float targetValue = cam.GetComponent<Camera>().fieldOfView - FovincreaseValue;
    //        zoomVal = Mathf.Clamp(targetValue, 20f, 90f);
    //    }


    //    cam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(cam.GetComponent<Camera>().fieldOfView, zoomVal, Time.deltaTime * 20f);


    //}
}