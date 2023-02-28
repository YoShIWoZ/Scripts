using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DoorTextBox : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Animator animator;
    public GameObject imageBoxRiley;
    public GameObject imageBoxEmpty;
    public GameObject doorOff;
    public GameObject doorOn;
    public GameObject dispenserEmpty;
    public GameObject dispenserFoodClean;

    [Header("Images")]
    public GameObject imageHolder;
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public Sprite image4;
    public Sprite image5;

    [Header("Images Day2")]
    public GameObject doorDay2Off;
    public int blinkTimes = 0;
    public float blinkerTimer = 0.80f;
    public GameObject overlayCanvas;
    public GameObject radioOn;
    public GameObject radioOff;
    public GameObject buttons;
    public GameObject defaultButton;

    public bool playerInRange;
    public float delay = 0.1f;
    private bool spokenWithReiley = false;

    [Header("Dialog Textbox Settings")]
    [Tooltip("Textbox Game Object")]
    public GameObject textBox;
    public Text textArea;
    private string currentText;
    private int page = 0;
    private int pageKen = 0;
    private int pageReileyD3SDT = 0;
    private int pageReileyD3Last = 0;

    public List<DialogueBox> dialogueBoxes = new List<DialogueBox>();
    public List<DialogueBox> dialogueBoxesKen = new List<DialogueBox>();

    void Start()
    {
        textArea.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
        myRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        animator = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange && animator.GetInteger("suitFoodNum") > 0 && !textBox.activeInHierarchy)
        {
            GlobalVariables.canMove = false;
            textBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(-125, 40);
            textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(181, 54);
            textArea.text = "I should probably\neat first.";
            textBox.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && playerInRange && animator.GetInteger("suitFoodNum") > 0 && textBox.activeInHierarchy)
        {
            textBox.SetActive(false);
            GlobalVariables.canMove = true;
        }
        if (animator.GetInteger("suitFoodNum") == 0)
        {
            if (GlobalVariables.dayNumber == 1)
            {
                if (Input.GetKeyDown(KeyCode.Space) && playerInRange && !spokenWithReiley)
                {
                    textArea.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
                    if (imageHolder.activeInHierarchy)
                    {
                        if (page == dialogueBoxes.Count)
                        {
                            imageHolder.SetActive(false);
                            textBox.SetActive(false);
                            GlobalVariables.canMove = true;
                            doorOff.SetActive(true);
                            doorOn.SetActive(false);
                            spokenWithReiley = true;
                            return;
                        }
                        StartCoroutine(SetText(dialogueBoxes[page].text, dialogueBoxes[page].x, dialogueBoxes[page].y, dialogueBoxes[page].width, dialogueBoxes[page].height));
                    }
                    else
                    {
                        GlobalVariables.doorActivated = true;
                        GlobalVariables.canMove = false;
                        doorOn.SetActive(true);
                        doorOff.SetActive(false);
                        StartCoroutine(LoadDoorScreen());
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Space) && playerInRange && spokenWithReiley)
                {
                    if (imageHolder.activeInHierarchy)
                    {
                        imageHolder.SetActive(false);
                        GlobalVariables.canMove = true;
                        doorOff.SetActive(true);
                        doorOn.SetActive(false);
                    }
                    else
                    {
                        GlobalVariables.canMove = false;
                        doorOn.SetActive(true);
                        doorOff.SetActive(false);
                        StartCoroutine(LoadDoorScreenEmpty());
                    }
                }
            }
            else if (GlobalVariables.dayNumber == 2)
            {
                if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
                {
                    if (GlobalVariables.powerReturned)
                    {
                        if (!spokenWithReiley)
                        {
                            textArea.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
                            if (imageHolder.activeInHierarchy)
                            {
                                buttons.SetActive(false);
                                if (pageKen == dialogueBoxesKen.Count)
                                {
                                    imageHolder.SetActive(false);
                                    textBox.SetActive(false);
                                    GlobalVariables.canMove = true;
                                    doorOff.SetActive(true);
                                    doorOn.SetActive(false);
                                    spokenWithReiley = true;
                                    return;
                                }
                                StartCoroutine(SetText(dialogueBoxesKen[pageKen].text, dialogueBoxesKen[pageKen].x, dialogueBoxesKen[pageKen].y, dialogueBoxesKen[pageKen].width, dialogueBoxesKen[pageKen].height));
                            }
                            else
                            {
                                GlobalVariables.doorActivated = true;
                                GlobalVariables.canMove = false;
                                doorOn.SetActive(true);
                                doorOff.SetActive(false);
                                StartCoroutine(LoadDoorScreenKen());
                            }
                        }
                        else
                        {
                            if (imageHolder.activeInHierarchy)
                            {
                                imageHolder.SetActive(false);
                                GlobalVariables.canMove = true;
                                doorOff.SetActive(true);
                                doorOn.SetActive(false);
                            }
                            else
                            {
                                GlobalVariables.canMove = false;
                                doorOn.SetActive(true);
                                doorOff.SetActive(false);
                                StartCoroutine(LoadDoorScreenEmpty());
                            }
                        }
                    }
                    else
                    {
                        textArea.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
                        if (!imageHolder.activeInHierarchy && GlobalVariables.powerOn)
                        {
                            //GlobalVariables.doorActivated = true;
                            GlobalVariables.canMove = false;
                            doorOn.SetActive(true);
                            doorOff.SetActive(false);
                            StartCoroutine(BlinkTimerStart());
                        }
                        else if (!textBox.activeInHierarchy && !GlobalVariables.powerOn && page > 2)
                        {
                            SetTextBox(dialogueBoxes[3].text, dialogueBoxes[3].x, dialogueBoxes[3].y, dialogueBoxes[3].width, dialogueBoxes[3].height);
                            buttons.SetActive(true);
                            StartCoroutine(SelectButton());
                        }
                        else if (!textBox.activeInHierarchy && !GlobalVariables.powerOn)
                        {
                            SetTextBox(dialogueBoxes[page].text, dialogueBoxes[page].x, dialogueBoxes[page].y, dialogueBoxes[page].width, dialogueBoxes[page].height);
                        }
                        else if (textBox.activeInHierarchy && !GlobalVariables.powerOn)
                        {
                            CloseTextBox();
                            buttons.SetActive(false);
                        }
                    }
                }
            }
            else if (GlobalVariables.dayNumber == 3)
            {
                if (Input.GetKeyDown(KeyCode.Space) && playerInRange && GlobalVariables.lastEntryRead && !textBox.activeInHierarchy)
                {
                    SetTextBox("", -200, 45, 190, 45);
                    buttons.SetActive(true);
                    StartCoroutine(SelectButton());
                    return;
                }
                else if (Input.GetKeyDown(KeyCode.Space) && playerInRange && GlobalVariables.lastEntryRead && textBox.activeInHierarchy)
                {
                    CloseTextBox();
                    buttons.SetActive(false);
                    return;
                }
                if (Input.GetKeyDown(KeyCode.Space) && playerInRange && !spokenWithReiley && !GlobalVariables.posterFixed && !GlobalVariables.o2Fixed)
                {
                    textArea.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
                    if (imageHolder.activeInHierarchy)
                    {
                        if (page == dialogueBoxes.Count)
                        {
                            imageHolder.SetActive(false);
                            textBox.SetActive(false);
                            GlobalVariables.canMove = true;
                            doorOff.SetActive(true);
                            doorOn.SetActive(false);
                            spokenWithReiley = true;
                            return;
                        }
                        StartCoroutine(SetText(dialogueBoxes[page].text, dialogueBoxes[page].x, dialogueBoxes[page].y, dialogueBoxes[page].width, dialogueBoxes[page].height));
                    }
                    else
                    {
                        GlobalVariables.doorActivated = true;
                        GlobalVariables.canMove = false;
                        doorOn.SetActive(true);
                        doorOff.SetActive(false);
                        StartCoroutine(LoadDoorScreen());
                    }
                }
                else if (Input.GetButtonDown("Submit") && playerInRange && GlobalVariables.o2Fixed)
                {
                    textArea.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
                    if (imageHolder.activeInHierarchy)
                    {
                        if (pageReileyD3Last == 0)
                        {
                            StartCoroutine(SetText("I did it, dr. Reiley!", 170, -165, 225, 45));
                            pageReileyD3Last += 1;
                        }
                        else if (pageReileyD3Last == 1)
                        {
                            StartCoroutine(SetText("Good job! You fixed the\nroom just in time to\nsay goodbye to it.", -155, 166, 250, 75));
                            pageReileyD3Last += 1;
                        }
                        else if (pageReileyD3Last == 2)
                        {
                            StartCoroutine(SetText("Your blood samples were\ncompletely uninfected.\nEverything seems fine.When\nyou’re ready, you can leave.", -135, 158, 290, 90));
                            pageReileyD3Last += 1;
                        }
                        else if (pageReileyD3Last == 3)
                        {
                            StartCoroutine(SetText("In the meantime, I will find\nKen and have a talk with him\nabout the importance of proper\nmaintenance in deep space...", -127, 159, 310, 90));
                            pageReileyD3Last += 1;
                        }
                        else if (pageReileyD3Last == 4)
                        {
                            imageHolder.SetActive(false);
                            textBox.SetActive(false);
                            GlobalVariables.canMove = true;
                            doorOff.SetActive(true);
                            doorOn.SetActive(false);
                            GlobalVariables.gameCompleted = true;
                            if (!GlobalVariables.doorActivated)
                            {
                                GlobalVariables.doorActivated = true;
                                dispenserEmpty.SetActive(false);
                                dispenserFoodClean.SetActive(true);
                            }
                            StartCoroutine(announceGameComplete());
                        }
                    }
                    else
                    {
                        if (!GlobalVariables.gameCompletedAnnouncement)
                        {
                            GlobalVariables.canMove = false;
                            doorOn.SetActive(true);
                            doorOff.SetActive(false);
                            if (!GlobalVariables.gameCompleted)
                                StartCoroutine(LoadDoorScreenD3ReileyClean());
                            else
                                StartCoroutine(LoadDoorScreenD3CleanEmpty());
                        }
                        else
                        {
                            SetTextBox("", -200, 45, 190, 45);
                            buttons.SetActive(true);
                            StartCoroutine(SelectButton());
                            return;
                        }
                    }
                }

                else if (Input.GetKeyDown(KeyCode.Space) && playerInRange && spokenWithReiley && !GlobalVariables.posterFixed)
                {
                    if (imageHolder.activeInHierarchy)
                    {
                        imageHolder.SetActive(false);
                        GlobalVariables.canMove = true;
                        doorOff.SetActive(true);
                        doorOn.SetActive(false);
                    }
                    else
                    {
                        GlobalVariables.canMove = false;
                        doorOn.SetActive(true);
                        doorOff.SetActive(false);
                        StartCoroutine(LoadDoorScreenEmpty());
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Space) && playerInRange && GlobalVariables.posterFixed && !GlobalVariables.screwdriverTaken)
                {
                    textArea.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
                    if (imageHolder.activeInHierarchy)
                    {
                        if (pageKen == dialogueBoxesKen.Count)
                        {
                            imageHolder.SetActive(false);
                            textBox.SetActive(false);
                            GlobalVariables.canMove = true;
                            doorOff.SetActive(true);
                            doorOn.SetActive(false);
                            spokenWithReiley = true;
                            GlobalVariables.screwdriverAdded = true;
                            return;
                        }
                        StartCoroutine(SetText(dialogueBoxesKen[pageKen].text, dialogueBoxesKen[pageKen].x, dialogueBoxesKen[pageKen].y, dialogueBoxesKen[pageKen].width, dialogueBoxesKen[pageKen].height));
                        if (pageKen == 10)
                            imageHolder.GetComponent<Image>().sprite = image3;
                    }
                    else
                    {
                        //GlobalVariables.doorActivated = false;
                        GlobalVariables.canMove = false;
                        doorOn.SetActive(true);
                        doorOff.SetActive(false);
                        StartCoroutine(LoadDoorScreen());
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Space) && playerInRange && GlobalVariables.screwdriverTaken)
                {
                    textArea.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
                    if (imageHolder.activeInHierarchy)
                    {
                        if (pageReileyD3SDT == 0)
                        {
                            StartCoroutine(SetText("You know, that O2 leak could\ncause you to become lightheaded,\ngiddy or even hallucinate.", -120, 170, 326, 70));
                            pageReileyD3SDT += 1;
                        }
                        else if (pageReileyD3SDT == 1)
                        {
                            StartCoroutine(SetText("Seen any weird\nthings lately?", -200, 175, 160, 60));
                            pageReileyD3SDT += 1;
                        }
                        else if (pageReileyD3SDT == 2)
                        {
                            StartCoroutine(SetText("No comment.", 210, -166, 135, 45));
                            pageReileyD3SDT += 1;
                        }
                        else if (pageReileyD3SDT == 3)
                        {
                            imageHolder.SetActive(false);
                            textBox.SetActive(false);
                            GlobalVariables.canMove = true;
                            doorOff.SetActive(true);
                            doorOn.SetActive(false);
                        }
                    }
                    else
                    {
                        GlobalVariables.canMove = false;
                        doorOn.SetActive(true);
                        doorOff.SetActive(false);
                        StartCoroutine(LoadDoorScreen());
                    }
                }
            }
        }
        
    }
    private IEnumerator SetText(string text, float x, float y, float width, float height)
    {
        textBox.SetActive(true);
        textBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        playerInRange = false;
        for (int i = 0; i < text.Length + 1; i++)
        {
            currentText = text.Substring(0, i);
            textArea.text = currentText;
            if (Input.GetKey(KeyCode.Space) && i > 3)
            {
                textArea.text = text;
                break;
            }
            yield return new WaitForSeconds(delay);
        }
        playerInRange = true;
        if (GlobalVariables.powerReturned || GlobalVariables.posterFixed)
            pageKen = pageKen + 1;
        else
            page = page + 1;
    }
    private void SetTextBox(string text, float x, float y, float width, float height)
    {
        GlobalVariables.canMove = false;
        textBox.SetActive(true);
        textBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        textArea.text = text;
        page = page + 1;
    }
    private void CloseTextBox()
    {
        textBox.SetActive(false);
        GlobalVariables.canMove = true;
    }
    private IEnumerator LoadDoorScreen()
    {
        yield return new WaitForSeconds(.40f);
        imageHolder.GetComponent<Image>().sprite = image2;
        imageHolder.SetActive(true);
    }
    private IEnumerator LoadDoorScreenKen()
    {
        yield return new WaitForSeconds(.40f);
        imageHolder.GetComponent<Image>().sprite = image3;
        imageHolder.SetActive(true);
    }
    private IEnumerator LoadDoorScreenEmpty()
    {
        yield return new WaitForSeconds(.40f);
        imageHolder.GetComponent<Image>().sprite = image1;
        imageHolder.SetActive(true);
    }
    private IEnumerator LoadDoorScreenD3CleanEmpty()
    {
        yield return new WaitForSeconds(.40f);
        imageHolder.GetComponent<Image>().sprite = image5;
        imageHolder.SetActive(true);
    }
    private IEnumerator LoadDoorScreenD3ReileyClean()
    {
        yield return new WaitForSeconds(0.40f);
        imageHolder.GetComponent<Image>().sprite = image4;
        imageHolder.SetActive(true);
    }
    private IEnumerator BlinkTimerStart()
    {
        StartCoroutine(LoadDoorScreenEmpty());
        yield return new WaitForSeconds(blinkerTimer);
        StartCoroutine(LoadDoorScreenEmptyD2());
    }
    private IEnumerator LoadDoorScreenD2()
    {
        yield return new WaitForSeconds(.20f);
        imageHolder.GetComponent<Image>().sprite = image2;
        imageHolder.SetActive(true);
        if (blinkTimes < 3)
            StartCoroutine(LoadDoorScreenEmptyD2());
        else if (blinkTimes == 3)
            StartCoroutine(TurnOffDoor());
        blinkTimes += 1;
    }

    private IEnumerator announceGameComplete()
    {
        yield return new WaitForSeconds(10);
        GlobalVariables.gameCompletedAnnouncement = true;
    }

    private IEnumerator TurnOffDoor()
    {
        yield return new WaitForSeconds(1.20f);
        doorDay2Off.SetActive(true);
        yield return new WaitForSeconds(.80f);
        doorDay2Off.SetActive(false);
        imageHolder.SetActive(false);
        textBox.SetActive(false);
        doorOff.SetActive(true);
        doorOn.SetActive(false);
        overlayCanvas.SetActive(true);
        GlobalVariables.powerOn = false;
        yield return new WaitForSeconds(.80f);
        SetTextBox("What!? A power failure?", -150, 70, 250, 45);  
    }
    private IEnumerator LoadDoorScreenEmptyD2()
    {
        radioOn.SetActive(false);
        radioOff.SetActive(true);
        yield return new WaitForSeconds(.10f);
        imageHolder.GetComponent<Image>().sprite = image1;
        imageHolder.SetActive(true);
        StartCoroutine(LoadDoorScreenD2());
    }
    private IEnumerator SelectButton()
    {
        yield return new WaitForSeconds(.0001f);
        EventSystem.current.SetSelectedGameObject(defaultButton);
    }

    public void Stay()
    {
        CloseTextBox();
        buttons.SetActive(false);
    }
    
    public void Leave()
    {
        SceneManager.LoadScene("CreditsA");
    }

    public void D2Leave()
    {
        GlobalVariables.powerOn = true;
        StartCoroutine(LoadHall());
    }

    private IEnumerator LoadHall()
    {
        yield return new WaitForSeconds(0.0005f);
        GlobalVariables.canMove = true;
        SceneManager.LoadScene("Day2 Hall");
    }

    public void D2Stay()
    {
        StartCoroutine(StartTimer());
    }
    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(15f);
        overlayCanvas.SetActive(false);
        GlobalVariables.powerOn = true;
        GlobalVariables.powerReturned = true;
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
