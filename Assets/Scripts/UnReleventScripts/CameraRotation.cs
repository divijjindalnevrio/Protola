using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private GameObject _followGameObject;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(0, 0, 0);
        float speed = 50f;

        if(Input.GetKeyDown(KeyCode.A))
        {
            direction.x = -1f;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            direction.x = +1f;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            direction.y = +1f;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            direction.y = -1f;
        }

        _followGameObject.transform.position += direction * speed * Time.deltaTime;
    }
}
