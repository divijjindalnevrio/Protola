using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;

public class Spin : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        try
        {
            Branch.initSession(CallbackWithBranchUniversalObject);
            Debug.Log("ShareDeepLink : Spin : ");
        }
        catch (Exception e)
        {
            Debug.Log("ShareDeepLink : Spin : " + e);
        }

    }


    public void CallbackWithBranchUniversalObject(BranchUniversalObject buo,
                                            BranchLinkProperties linkProps,
                                            string error)
    {
        Debug.Log("Deeplink params entered:  " + linkProps);
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


}