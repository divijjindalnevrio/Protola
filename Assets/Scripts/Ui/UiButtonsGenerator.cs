using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiButtonsGenerator : MonoBehaviour
{
    private UiModel uiModel;
    [SerializeField] private CounterSurfaceChanger counterSurfaceChanger;
    private Dictionary<int, GameObject> palaceButtons = new Dictionary<int, GameObject>();
    void Start()
    {
        uiModel = GetComponent<UiModel>();

    }

    public void ButtonGenerator(int length, GameObject instancaiateObj, Transform parent)
    {
        for(int i = 0; i < length; i++)
        {
            GameObject instanciateButton = Instantiate(instancaiateObj, Vector3.zero, Quaternion.identity);
            instanciateButton.transform.SetParent(parent.transform, false);
            palaceButtons.Add(i, instanciateButton);
        }
    }

    public void AddColorToPalaceTextureButtons()
    {
        for(int i = 0; i<palaceButtons.Count; i++)
        {
             Button button = palaceButtons[i].GetComponent<Button>();
             ColorBlock cb = button.colors;
             cb.normalColor = counterSurfaceChanger.colors[i];
             cb.selectedColor = counterSurfaceChanger.colors[i];
             button.colors = cb;
        }
    }

}
