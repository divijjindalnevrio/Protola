using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiButtonsGenerator : MonoBehaviour
{
    private UiModel uiModel;
    [SerializeField] private CounterSurfaceChanger counterSurfaceChanger;
    private Dictionary<int, GameObject> palaceButtons = new Dictionary<int, GameObject>();
    [SerializeField] List<GameObject> palaceColorButtons = new List<GameObject>();
    [SerializeField] List<GameObject> palaceTextureButtons = new List<GameObject>();
    [SerializeField] List<GameObject> GranulatesTextureButtons = new List<GameObject>();

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



    public void HighlightPalaceButton(int button)
    {
        DisableAllHighLightButton(palaceColorButtons);
        palaceColorButtons[button].transform.Find("SelectedState").gameObject.SetActive(true);


    }

    public void HighlightPalaceTextureButton(int button)
    {
        DisableAllHighLightButton(palaceTextureButtons);
        palaceTextureButtons[button].transform.Find("SelectedState").gameObject.SetActive(true);

    }

    public void GranulatesTextureButton(int button)
    {
        DisableAllHighLightButton(GranulatesTextureButtons);
        GranulatesTextureButtons[button].transform.Find("SelectedState").gameObject.SetActive(true);

    }

    private void DisableAllHighLightButton(List<GameObject> buttons)
    {
        foreach(GameObject button in buttons)
        {
            button.transform.Find("SelectedState").gameObject.SetActive(false);
        }
    }

}
