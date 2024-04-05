using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFieldController : MonoBehaviour
{
    [SerializeField] private GameObject PlywoodInputTextFields;
    [SerializeField] private BasinMovement basinMovement;

    void Start()
    {
        basinMovement.OnCounterMoving += BasinMovement_OnCounterMoving;
        basinMovement.OnCounterStopMoving += BasinMovement_OnCounterStopMoving;
    }

    private void BasinMovement_OnCounterStopMoving()
    {
        PlywoodInputTextFields.SetActive(false);
    }

    private void BasinMovement_OnCounterMoving()
    {
        PlywoodInputTextFields.SetActive(true);
    }

    void Update()
    {
        
    }

    private void SetTextFieldToUnIntractable()
    {
        foreach(Transform textField in PlywoodInputTextFields.transform)
        {
            textField.transform.GetChild(0).GetComponent<TMP_InputField>().interactable = false;
        }
    }

    private void SetTextFieldToIntractable()
    {
        foreach (Transform textField in PlywoodInputTextFields.transform)
        {
            textField.transform.GetChild(0).GetComponent<TMP_InputField>().interactable = true;
        }
    }
}
