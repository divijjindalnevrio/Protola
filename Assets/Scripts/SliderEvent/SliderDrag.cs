using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderDrag : MonoBehaviour, IPointerUpHandler
{
    public WorldUiScaler worldUiScaler;

    public void OnPointerUp(PointerEventData eventData)
    {

        Debug.Log("Sliding finished");
    }
}
