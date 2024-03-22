using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiPanelOpenAndClose : MonoBehaviour
{
    [SerializeField] private GameObject buttonDashbord;
    [SerializeField] private GameObject menuButton;
    [SerializeField] private GameObject menuCloseButton;
    [SerializeField] private BasinMovement basinMovement;

    void Start()
    {
        basinMovement.OnGameobjectSelected += BasinMovement_OnGameobjectSelected;
       // basinMovement.OnGameobjectMoving += BasinMovement_OnGameobjectMoving;
    }

    //private void BasinMovement_OnGameobjectMoving()
    //{
    //    StartCoroutine("CloseUiMenu");
        
    //}

    //IEnumerator CloseUiMenu()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    CloseMainUiPanel();
    //}

    private void BasinMovement_OnGameobjectSelected(object sender, SelectedObject e)
    {
        if(e == SelectedObject.none)
        {
            CloseMainUiPanel();
        }
        else
        {
            OpenMainUiPanel();
        }
    }


    public void OpenMainUiPanel()
    {
        buttonDashbord.gameObject.SetActive(true);
        menuCloseButton.gameObject.SetActive(true);
        menuButton.SetActive(false);

    }

    public void CloseMainUiPanel()
    {
        buttonDashbord.gameObject.SetActive(false);
        menuCloseButton.gameObject.SetActive(false);
        menuButton.SetActive(true);
    }
}
