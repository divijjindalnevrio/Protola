using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DashLine : MonoBehaviour
{
    [SerializeField] private GameObject one;
    [SerializeField] private GameObject two;
    [SerializeField] private GameObject three;
    [SerializeField] private GameObject four;
    [SerializeField] private GameObject five;

    [SerializeField] private GameObject six;
    [SerializeField] private GameObject seven;
    [SerializeField] private GameObject eight;
    [SerializeField] private GameObject nine;
    [SerializeField] private GameObject ten;

    [SerializeField] private LineRenderer ln;
    [SerializeField] private LineRenderer lnBottom;
    [SerializeField] private LineRenderer frontLeft;
    [SerializeField] private LineRenderer frontRight;
    [SerializeField] private LineRenderer backLeft;
    [SerializeField] private LineRenderer backRight;


    void Update()
    {
        ln.SetPosition(0, one.transform.position);
        ln.SetPosition(1, two.transform.position);
        ln.SetPosition(2, three.transform.position);
        ln.SetPosition(3, four.transform.position);
        ln.SetPosition(4, one.transform.position);

        lnBottom.SetPosition(0, five.transform.position);
        lnBottom.SetPosition(1, six.transform.position);
        lnBottom.SetPosition(2, seven.transform.position);
        lnBottom.SetPosition(3, eight.transform.position);
        lnBottom.SetPosition(4, five.transform.position);

        //frontLeft
        frontLeft.SetPosition(0, one.transform.position);
        frontLeft.SetPosition(1, five.transform.position);

        //frontRight
        frontRight.SetPosition(0, two.transform.position);
        frontRight.SetPosition(1, six.transform.position);

        //backLeft
        backLeft.SetPosition(0, three.transform.position);
        backLeft.SetPosition(1, seven.transform.position);

        //backRight
        backRight.SetPosition(0, four.transform.position);
        backRight.SetPosition(1, eight.transform.position);

    }


   
}
