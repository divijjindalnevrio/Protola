using UnityEngine;

public class TouchCameraControl : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 2f;
    public float pinchSpeed = 2f;

    void Update()
    {
        HandleTouchInput();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // Handle camera movement
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            Vector3 moveDirection = new Vector3(touchDeltaPosition.x, 0f, touchDeltaPosition.y).normalized;
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        }

        if (Input.touchCount == 2)
        {
            // Handle camera rotation and pinch (zoom)
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

            float prevTouchDeltaMag = (touch0PrevPos - touch1PrevPos).magnitude;
            float touchDeltaMag = (touch0.position - touch1.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // Handle pinch (zoom)
            float pinchAmount = deltaMagnitudeDiff * pinchSpeed;
            Camera.main.fieldOfView += pinchAmount;

            // Handle rotation
            float rotationAngle = Vector2.Angle(touch0.deltaPosition, touch1.deltaPosition);
            Vector3 cross = Vector3.Cross(touch0.deltaPosition, touch1.deltaPosition);

            if (cross.z > 0)
                rotationAngle = -rotationAngle;

            transform.Rotate(Vector3.up, rotationAngle * rotationSpeed);
        }
    }
}
