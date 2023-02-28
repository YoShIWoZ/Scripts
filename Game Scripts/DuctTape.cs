using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DuctTape : MonoBehaviour
{
    public bool playerInRange;

    public GameObject ductTape;

    [Header("Buttons")]
    public GameObject buttons;
    public GameObject defaultButton;

    [Header("Dialog Textbox Settings")]
    [Tooltip("Textbox Game Object")]
    public GameObject textBox;
    public Text textArea;
    [TextArea(3, 10)]
    public string text;
    public float x;
    public float y;
    public float width;
    public float height;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (!textBox.activeInHierarchy)
            {
                textArea.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
                SetTextBox(text, x, y, width, height);
                buttons.SetActive(true);
                StartCoroutine(SelectButton());
            }
            else
            {
                CloseTextBox();
                buttons.SetActive(false);
            }
        }
    }

    private void SetTextBox(string text, float x, float y, float width, float height)
    {
        GlobalVariables.canMove = false;
        textBox.SetActive(true);
        textBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        textArea.text = text;
    }
    private void CloseTextBox()
    {
        textBox.SetActive(false);
        GlobalVariables.canMove = true;
    }

    private IEnumerator SelectButton()
    {
        yield return new WaitForSeconds(.0001f);
        EventSystem.current.SetSelectedGameObject(defaultButton);
    }

    public void PickUpDuctTape()
    {
        ductTape.SetActive(false);
        CloseTextBox();
        buttons.SetActive(false);
        GlobalVariables.ductTapeTaken = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractField") && collision.isTrigger)
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractField") && collision.isTrigger)
        {
            playerInRange = false;
        }
    }
}
