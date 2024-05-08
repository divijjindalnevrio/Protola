using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeepLinkControler : MonoBehaviour
{
    private object universalObjectDatauniversalObject;
    private object linkProperties;
    public string shareData;
    BranchUniversalObject universalObjectData = new BranchUniversalObject();
    BranchLinkProperties linkPropertiesData = new BranchLinkProperties();

    void Start()
    {
       
    }

    public void ShareDeepLink()
    {
        AssignDataToLink();
        CreatDeepLink();
        Debug.Log("DeepLinkControler : 1");
        Branch.getShortURL(universalObjectData, linkPropertiesData, (parameters, error) => {
            if (error != null)
            {
                Debug.LogError("Branch Error: " + error);
            }
            else
            {
                Debug.Log("Branch.getShortURL shared params: " + parameters.ToString());
            }
            //{
            //    Debug.LogError("CallbackWithBranchUniversalObject Branch.shareLink failed: " + error);
            //    Debug.Log("DeepLinkControler : 2");
            //}
        });
 
    }

    private void AssignDataToLink()
    {
        universalObjectData.contentIndexMode = 1;
        //Identifier that helps Branch dedupe across many instances of the same content.
        universalObjectData.canonicalIdentifier = "id12345";
        // OG title
        universalObjectData.title = "id12345 title";
        // OG Description
        universalObjectData.contentDescription = "My awesome piece of content!";
        // OG Image
        universalObjectData.imageUrl = "https://s3-us-west-1.amazonaws.com/branchhost/mosaic_og.png";
        // User defined key value pair
        universalObjectData.metadata.AddCustomMetadata("foo", "bar");

    }

    private void CreatDeepLink()
    {
        linkPropertiesData.tags.Add("tag1");
        linkPropertiesData.tags.Add("tag2");
        // Feature link is associated with. Eg. Sharing
        linkPropertiesData.feature = "share";
        // The channel where you plan on sharing the link Eg.Facebook, Twitter, SMS etc
        linkPropertiesData.channel = "Protola App";
        linkPropertiesData.stage = "2";
        // Parameters used to control Link behavior
        linkPropertiesData.controlParams.Add("_url", "http://example.com");
    }

    public void CallbackWithBranchUniversalObject(BranchUniversalObject universalObject, BranchLinkProperties linkProperties, string error)
    {
        Debug.Log("Branch initialization completed: ");
        Debug.Log("Universal Object: " + universalObject.ToJsonString());
        Debug.Log("Link Properties: " + linkProperties.ToJsonString());
    }


}
   
    