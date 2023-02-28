using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class buttonSelector : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public GameObject button;
    public Text dialogText;
    public string dialog;
    private string dialogOld;
    public bool defaultSelection;
    // Start is called before the first frame update
    void Start()
    {
        dialogOld = dialogText.text;
        if (defaultSelection)
        {
            EventSystem.current.SetSelectedGameObject(button);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnSelect(BaseEventData eventData)
    {
        // Do something.
        dialogText.text = dialogText.text + dialog;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        dialogText.text = dialogOld;
    }
}
