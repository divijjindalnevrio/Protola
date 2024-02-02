using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiModel : MonoBehaviour
{
  
    public List<GameObject> UiBuoonsAndElementsList = new List<GameObject>();
    public Dictionary<string, GameObject> UiBuoonsAndElementsDict = new Dictionary<string, GameObject>();

    public void AddUiElementsToDictonary()
    {
        foreach (GameObject listitem in UiBuoonsAndElementsList)
        {
            UiBuoonsAndElementsDict.Add(listitem.name, listitem);
            listitem.SetActive(false);
        }
    }
}
