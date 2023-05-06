using System.Linq;
using Hive;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    public HiveBuilding SelectedItem;
    public GameObject InformationWindow;
    public GameObject TextMeshItem;
    public Image Picture;

    public GameObject HoneyTextMeshObject;

    private TextMeshProUGUI _honeyCount;
    private TextMeshProUGUI _textMesh;
    
    private void Start()
    {
        SelectedItem = null;
        _textMesh = TextMeshItem.GetComponent<TextMeshProUGUI>();
        _honeyCount = HoneyTextMeshObject.GetComponent<TextMeshProUGUI>();
        // InformationWindow = Menu.transform.GetChild(1).GetComponent<GameObject>();
        InformationWindow.SetActive(false);
        HoneyTextMeshObject.SetActive(false);
    }

    private void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var current = transform.GetChild(i).transform.GetComponent<HiveBuilding>();
            if (current.Selected)
            {
                if (SelectedItem == null)
                {
                    current.Select();
                    SelectedItem = current;
                }
                else if (SelectedItem.name != current.name)
                {
                    SelectedItem.Unselect();
                    SelectedItem = current;
                }
            }
        }

        if (!Enumerable.Range(0, transform.childCount)
                .Select(x => transform.GetChild(x))
                .Any(x => x.GetComponent<HiveBuilding>().Selected))
        {
            SelectedItem = null;
            HideMenu();
        }
        else if (SelectedItem != null)
            ShowMenu();
    }

    public void HideMenu()
    {
        InformationWindow.SetActive(false);
        HoneyTextMeshObject.SetActive(false);
    }
    public void ShowMenu()
    {
        InformationWindow.SetActive(true);
        var name = SelectedItem.transform.name;
        var description = SelectedItem.GetDescription();
        Picture.sprite = SelectedItem.GetImage();
        _textMesh.text = $"{name}\n\n{description}";

        if (SelectedItem is IHoneyContainer)
        {
            HoneyTextMeshObject.SetActive(true);
            var asHoney = ((IHoneyContainer)SelectedItem);
            _honeyCount.text = asHoney.Honey.ToString();
        }
        else
        {
            HoneyTextMeshObject.SetActive(false);
        }
    }
}