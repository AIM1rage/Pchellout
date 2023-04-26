using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectSelector : MonoBehaviour
{
    private GameObject SelectedImage;
    private GameObject UnselectedImage;
    public GameObject ButtonsCanvas;
    private Button ShowInformationButton;
    private Button HideInformationButton;
    private Button SellObjectButton;

    private GameObject Information;
    private bool _selected = false;

    private void Start()
    {
        SelectedImage = transform.GetChild(0).gameObject;
        UnselectedImage = transform.GetChild(1).gameObject;
        var sideBar = ButtonsCanvas.transform.GetChild(0);
        SellObjectButton = sideBar.GetChild(1).GetComponent<Button>();
        ShowInformationButton = sideBar.GetChild(0).GetComponent<Button>();
        ShowInformationButton.onClick.AddListener(() => Information.SetActive(true));
        Information = ButtonsCanvas.transform.GetChild(1).gameObject;
        HideInformationButton = Information.transform.GetChild(0).GetComponent<Button>();
        Information.SetActive(false);
        ButtonsCanvas.SetActive(false); 
        
        HideInformationButton.onClick.AddListener(()=> Information.SetActive(false));
        SellObjectButton.onClick.AddListener(() =>
        {
            ButtonsCanvas.SetActive(false);
            Destroy(gameObject);
        });
    }

    private void OnMouseDown()
    {
        if (!_selected)
        {
            SelectedImage.gameObject.SetActive(true);
            UnselectedImage.gameObject.SetActive(false);
            ButtonsCanvas.SetActive(true);
            _selected = true;
        }
        else
        {
            SelectedImage.gameObject.SetActive(false);
            UnselectedImage.gameObject.SetActive(true);
            ButtonsCanvas.SetActive(false);
            _selected = false;
        }
    }
}