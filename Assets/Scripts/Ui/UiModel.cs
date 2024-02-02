using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiModel : MonoBehaviour
{
  
    public List<GameObject> UiBuoonsAndElementsList = new List<GameObject>();
    public Dictionary<string, GameObject> UiBuoonsAndElementsDict = new Dictionary<string, GameObject>();
    public string lastSliderActiveName = "Width";

    public void AddUiElementsToDictonary()
    {
        foreach (GameObject listitem in UiBuoonsAndElementsList)
        {
            UiBuoonsAndElementsDict.Add(listitem.name, listitem);
            listitem.SetActive(false);
        }
    }

    public void SetAllUiElementsToFalse(string TopButtonName)
    {
        foreach (GameObject listitem in UiBuoonsAndElementsList)
        {
            if (listitem.name == TopButtonName) continue;
            Debug.Log("SetAllUiElementsToFalse : " + listitem.name);
            listitem.SetActive(false);
        }
    }
}
