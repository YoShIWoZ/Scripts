using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonsBed : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public GameObject button;
    public Text dialogText;
    public string dialog;
    private string dialogOld;

    // Start is called before the first frame update
    void Start()
    {
        dialogOld = dialogText.text;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnSelect(BaseEventData eventData)
    {
        StartCoroutine(UpdateText());
    }

    public void OnDeselect(BaseEventData eventData)
    {
        dialogText.text = dialogOld;
    }

    private IEnumerator UpdateText()
    {
        yield return new WaitForSeconds(.0001f);
        dialogText.text = dialogOld;
        dialogText.text = dialog + dialogText.text;
    }
}
