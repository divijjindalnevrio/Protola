using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProcessDeepLinkMngr : MonoBehaviour
{
    public static ProcessDeepLinkMngr Instance { get; private set; }
    private void Awake()
    {
        Debug.Log("onDeepLinkActivated : 1");
        if (Instance == null)
        {
            Debug.Log("onDeepLinkActivated : 2");
            Instance = this;
            Application.deepLinkActivated += onDeepLinkActivated;
            if (!String.IsNullOrEmpty(Application.absoluteURL))
            {
                Debug.Log("onDeepLinkActivated : 3");
                onDeepLinkActivated(Application.absoluteURL);
            }
            // Initialize DeepLink Manager global variable.
           
            DontDestroyOnLoad(gameObject);
            Debug.Log("onDeepLinkActivated : 4");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }

    private void onDeepLinkActivated(string url)
    {
        Debug.Log("onDeepLinkActivated : " + url);
        string json = url.Replace("https://protola.nevrio.tech/", "");
        Debug.Log("onDeepLinkActivated : " + json);
        byte[] decodedBytes = Convert.FromBase64String(json);
        string decodedText = Encoding.UTF8.GetString(decodedBytes);
        Debug.Log("onDeepLinkActivated : " + decodedText);
    }
}

