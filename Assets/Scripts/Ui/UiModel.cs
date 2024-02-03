using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiModel : MonoBehaviour
{
  
    public List<GameObject> UiButtonsAndElementsList = new List<GameObject>();
    public Dictionary<string, GameObject> UiButtonsAndElementsDict = new Dictionary<string, GameObject>();
    public string lastSliderActiveName = "Width";
    public string lastModelShapeActiveName = "Round";
    public string PreviousUiState;
    public string TopActiveButton = "SurfaceSize";

    public void AddUiElementsToDictonary()
    {
        foreach (GameObject listitem in UiButtonsAndElementsList)
        {
            UiButtonsAndElementsDict.Add(listitem.name, listitem);
            listitem.SetActive(false);
        }
    }

    public void SetAllUiElementsToFalse(string TopButtonName)
    {
        foreach (GameObject listitem in UiButtonsAndElementsList)
        {
            if (listitem.name == TopButtonName) continue;
            Debug.Log("SetAllUiElementsToFalse : " + listitem.name);
            listitem.SetActive(false);
        }
    }

    public void SetTopActiveButtons(string activeButtonName)
    {
        TopActiveButton = activeButtonName;
    }
}
