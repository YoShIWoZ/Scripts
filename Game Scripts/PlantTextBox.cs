using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlantTextBox : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Animator animator;
    public bool playerInRange;
    private int page = 0;
    private bool givePlantName = false;
    private bool plantHasName = false;

    [Header("Buttons")]
    public bool hasButtons = false;
    public GameObject buttons;
    public GameObject defaultButton;

    [Header("Dialog Textbox Settings")]
    [Tooltip("Textbox Game Object")]
    public GameObject textBox;
    public Text textArea;
    public List<DialogueBox> dialogueBoxes = new List<DialogueBox>();

    [Header("Inputbox")]
    public GameObject inputbox;
    public Text nameField;
    public GameObject UpperCaseBox;
    public GameObject LowerCaseBox;
    public GameObject NumbersBox;
    public GameObject defaultLowerCase;
    public GameObject defaultUpperCase;
    public GameObject defaultNumber;


    void Start()
    {
        myRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        animator = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (textBox.activeInHierarchy && plantHasName)
            {
                StartCoroutine(CloseTextBox());
                buttons.SetActive(false);
                return;
            }
            if (textBox.activeInHierarchy && GlobalVariables.dayNumber == 3 && page == 2)
            {
                StartCoroutine(CloseTextBox());
                buttons.SetActive(false);
                page = 0;
                return;
            }
            if (!GlobalVariables.powerOn && !plantHasName)
            {
                if (page == dialogueBoxes.Count)
                {
                    StartCoroutine(CloseTextBox());
                    page = 0;
                    return;
                }
                SetTextBox(dialogueBoxes[dialogueBoxes.Count - 1].text, dialogueBoxes[dialogueBoxes.Count - 1].x, dialogueBoxes[dialogueBoxes.Count - 1].y, dialogueBoxes[dialogueBoxes.Count - 1].width, dialogueBoxes[dialogueBoxes.Count - 1].height);
                page = dialogueBoxes.Count;
            }
            if (!GlobalVariables.powerOn && plantHasName)
            {
                if (page == dialogueBoxes.Count)
                {
                    StartCoroutine(CloseTextBox());
                    page = 0;
                    return;
                }
                SetTextBox("Is that you " + GlobalVariables.plantName + "?", 46, 60, 150 + (GlobalVariables.plantName.Length * 10), 45);
                page = dialogueBoxes.Count;
            }
            else if (GlobalVariables.dayNumber == 3 && GlobalVariables.plantName.Length > 0)
            {
                textArea.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
                if (!GlobalVariables.doorActivated)
                {
                    if (page == 0)
                    {
                        SetTextBox("Hey, " + GlobalVariables.plantName + ".\n" + "You have been a solid friend\nthrough these times.", 46, 60, 290, 80);
                        page += 1;
                    }
                    else if (page == 1)
                    {
                        SetTextBox("Thanks, little guy.", 46, 60, 205, 45);
                        page += 1;
                    }
                }
                else
                {
                    SetTextBox("Oh, dear " + GlobalVariables.plantName + "!\n" + "You are the only one I trust.", 46, 60, 300, 60);
                    page = 2;
                }
            }
            else if (GlobalVariables.dayNumber == 3 && GlobalVariables.plantName == "")
            {
                textArea.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
                SetTextBox("I hate you.", 46, 60, 131, 45);
                page = 2;
            }
            else if (GlobalVariables.powerOn && !givePlantName)
            {
                textArea.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
                if (page == dialogueBoxes.Count - 2)
                {
                    StartCoroutine(CloseTextBox());
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
            else if (GlobalVariables.powerOn && GlobalVariables.powerReturned && GlobalVariables.doorActivated && GlobalVariables.plantName.Length > 0)
            {
                textArea.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
                SetTextBox("How are you doing,\n" + GlobalVariables.plantName + "?", 46, 60, 205, 60);
            }
            else if (GlobalVariables.powerOn && GlobalVariables.powerReturned && GlobalVariables.plantName.Length > 0)
            {
                textArea.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
                SetTextBox("At least you seem\nfine, " + GlobalVariables.plantName + ".", 46, 60, 205, 60);
            }
            else if (GlobalVariables.powerOn && givePlantName && GlobalVariables.plantName == "")
            {
                print("Name Plant");
                SetTextBox(dialogueBoxes[dialogueBoxes.Count - 2].text, dialogueBoxes[dialogueBoxes.Count - 2].x, dialogueBoxes[dialogueBoxes.Count - 2].y, dialogueBoxes[dialogueBoxes.Count - 2].width, dialogueBoxes[dialogueBoxes.Count - 2].height);
                buttons.SetActive(false);
            }
            else if (GlobalVariables.powerOn && givePlantName && GlobalVariables.plantName.Length > 0 && !textBox.activeInHierarchy)
            {
                textArea.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
                SetTextBox("Hello, " + GlobalVariables.plantName + ".", 46, 60, 96 + (GlobalVariables.plantName.Length * 10f), 45);
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

    private IEnumerator CloseTextBox()
    {
        yield return new WaitForSeconds(.0000000001f);
        textBox.SetActive(false);
        GlobalVariables.canMove = true;
    }

    private IEnumerator SelectButton()
    {
        yield return new WaitForSeconds(.0001f);
        EventSystem.current.SetSelectedGameObject(defaultButton);
    }
    private IEnumerator SelectLowerCaseLetter(float seconds = .0001f)
    {
        yield return new WaitForSeconds(seconds);
        EventSystem.current.SetSelectedGameObject(defaultLowerCase);
    }
    private IEnumerator SelectUpperCaseLetter()
    {
        yield return new WaitForSeconds(.0001f);
        EventSystem.current.SetSelectedGameObject(defaultUpperCase);
    }
    private IEnumerator SelectNumber()
    {
        yield return new WaitForSeconds(.0001f);
        EventSystem.current.SetSelectedGameObject(defaultNumber);
    }

    public void NamePlant()
    {
        textArea.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
        givePlantName = true;
        inputbox.SetActive(true);
        ToLower();
    }
    public void NoNamePlant()
    {

    }

    public void InputLetter(string letter)
    {
        if(nameField.text.Length < 12)
            nameField.text = nameField.text + letter;
    }

    public void RemoveLetter()
    {
        nameField.text = nameField.text.Remove(nameField.text.Length - 1);
    }

    public void ToUpper()
    {
        LowerCaseBox.SetActive(false);
        UpperCaseBox.SetActive(true);
        StartCoroutine(SelectUpperCaseLetter());

    }

    public void ToLower()
    {
        NumbersBox.SetActive(false);
        LowerCaseBox.SetActive(true);
        StartCoroutine(SelectLowerCaseLetter());
    }

    public void Numbers()
    {
        UpperCaseBox.SetActive(false);
        NumbersBox.SetActive(true);
        StartCoroutine(SelectNumber());
    }

    public void Done()
    {
        if (nameField.text.Length ==  0)
            givePlantName = false;
        else
            plantHasName = true;
        GlobalVariables.plantName = nameField.text;
        inputbox.SetActive(false);
        StartCoroutine(CloseTextBox());
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
