using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeepLinkControler : MonoBehaviour
{
    private object universalObject;
    private object linkProperties;
    public string shareData;

    void Start()
    {
        SendDataOnDeepLink();
        CreateDeepLink();
    }

   
    void Update()
    {
        
    }

    private void SendDataOnDeepLink()
    {
        BranchUniversalObject universalObject = new BranchUniversalObject();

        // OG Description
        universalObject.contentDescription = "My awesome piece of content!";



    }

    private void CreateDeepLink()
    {
        BranchLinkProperties linkProperties = new BranchLinkProperties();

        // Parameters used to control Link behavior
        linkProperties.controlParams.Add("$mobile_url", "http://85kjc.app.link/Protola");
    }

    //private void GenerateBranchLink()
    //{
    //    Branch.getShortURL(universalObject, linkProperties, (parameters, error) =>
    //    {
    //    if (error != null)
    //    {
    //        Debug.LogError("Branch.getShortURL failed: " + error);
    //    }
    //    else if (params != parameters) {
    //        Debug.Log("Branch.getShortURL shared params: " + parameters["Mobile_url"].ToString());
    //    }
    //});
    //}

    public void ShareDeppLink()
    {
        Branch.shareLink(universalObject, linkProperties, "Sharing link: ", (parameters, error) => {
            if (error != null)
            {
                Debug.LogError("Branch.shareLink failed: " + error);
            }
            else if (parameters != null)
            {
                Debug.Log("Branch.shareLink: " + parameters["sharedLink"].ToString() + " " + parameters["sharedChannel"].ToString());
                shareData = parameters["sharedLink"].ToString() + " " + parameters["sharedChannel"].ToString();
            }
        });
    }

  
    public void CallbackWithBranchUniversalObject(BranchUniversalObject universalObject, BranchLinkProperties linkProperties, string error)
    {
        if (error != null)
        {
            Debug.LogError("Branch Error: " + error);
        }
        else
        {
            Debug.Log("Branch initialization completed: ");
            Debug.Log("Universal Object: " + universalObject.ToJsonString());
            Debug.Log("Link Properties: " + linkProperties.ToJsonString());
        }
    }
}
