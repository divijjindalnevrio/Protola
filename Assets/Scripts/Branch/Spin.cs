using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Branch.initSession(CallbackWithBranchUniversalObject);
    }

    void CallbackWithBranchUniversalObject(BranchUniversalObject buo,
                                            BranchLinkProperties linkProps,
                                            string error)
    {
        if (error != null)
        {
            Debug.LogError("Error : "
                                    + error);
        }
        else if (linkProps.controlParams.Count > 0)
        {
            Debug.Log("Deeplink params : "
                                    + buo.ToJsonString()
                                    + linkProps.ToJsonString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        //rotate 90 degress per second
        transform.Rotate(Vector3.up * Time.deltaTime * 90);
    }
}