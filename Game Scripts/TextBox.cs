using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Animator animator;
    public bool playerInRange;
    private int page = 0;

    [Header("Buttons")]
    public bool hasButtons = false;
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

    public List<DialogueBox> dialogueBoxes = new List<DialogueBox>();

    void Start()
    {
        myRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        animator = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && playerInRange)
        {
            if (GlobalVariables.powerOn)
            {
                textArea.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
                if (page == dialogueBoxes.Count)
                {
                    CloseTextBox();
                    page = 0;
                    if (hasButtons)
                        buttons.SetActive(false);
                    return;
                }
                SetTextBox(dialogueBoxes[page].text, dialogueBoxes[page].x, dialogueBoxes[page].y, dialogueBoxes[page].width, dialogueBoxes[page].height);
                page += 1;
                if (hasButtons)
                {
                    buttons.SetActive(true);
                    StartCoroutine(SelectButton());
                }
            }
            else
            {
                if (page == dialogueBoxes.Count)
                {
                    CloseTextBox();
                    page = 0;
                    return;
                }
                SetTextBox(dialogueBoxes[dialogueBoxes.Count-1].text, dialogueBoxes[dialogueBoxes.Count-1].x, dialogueBoxes[dialogueBoxes.Count-1].y, dialogueBoxes[dialogueBoxes.Count-1].width, dialogueBoxes[dialogueBoxes.Count-1].height);
                page = dialogueBoxes.Count;
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
