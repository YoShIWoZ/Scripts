using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DiaryTextBox : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Animator animator;
    private bool triggerRight;
    private bool triggerLeft;


    [Header("Buttons")]
    public Text pageNumbers;
    public bool playerInRange;
    private int page = 1;
    private int pageTotal = 1;


    [Header("Images")]
    public GameObject imageHolder;
    public Sprite image;
    public GameObject textBoxPages;
    public Text textAreaPage1;
    public Text textAreaPage2;
    public GameObject scribble;
    //public GameObject defaultButton;

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
    private string oldText1;
    private string oldText2;

    void Start()
    {
        myRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        animator = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        pageNumbers.text = page + "/" + pageTotal;

        if (Input.GetButtonDown("Submit") && playerInRange)
        {
            if (GlobalVariables.powerOn)
            {
                if (!textBox.activeInHierarchy && (animator.GetInteger("suitFoodNum") > 0 || animator.GetInteger("suitNum") == 1))
                {
                    if (animator.GetInteger("suitFoodNum") > 0)
                    {
                        SetTextBox("I should probably\neat first.", x, y, 210, 60);
                        return;
                    }
                    else
                    {
                        SetTextBox("I should get out of this suit first.", x, y, 210, 60);
                        return;
                    }
                }
                else if (textBox.activeInHierarchy)
                {
                    CloseTextBox();
                    return;
                }
                if (!imageHolder.activeInHierarchy)
                {
                    if (page == 1)
                    {
                        ShowPage1();
                    }
                    else if (page == 2)
                    {
                        ShowPage2();
                    }
                }
                else
                {
                    CloseTextBoxPages();
                }
            }
            else
            {
                textArea.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
                if (textBox.activeInHierarchy)
                {
                    CloseTextBox();
                }
                else
                {
                    SetTextBox("Can’t see. Can’t write.", 150, 130, 140, 60);
                }
            }
        }

        if (GlobalVariables.doorActivated && playerInRange && imageHolder.activeInHierarchy)
        {
            pageTotal = 2;
            if (TriggerRight() && playerInRange && imageHolder.activeInHierarchy)
            {
                ShowPage2();
                openDiary(oldText1, oldText2);
                page = 2;
                imageHolder.GetComponent<Image>().sprite = image;
                imageHolder.SetActive(true);
                textBoxPages.SetActive(true);
            }
            if (TriggerLeft() && playerInRange && imageHolder.activeInHierarchy)
            {
                ShowPage1();
                page = 1;
                imageHolder.GetComponent<Image>().sprite = image;
                imageHolder.SetActive(true);
                textBoxPages.SetActive(true);
            }
        }
    }

    public void ButtonExit()
    {
        StartCoroutine(CloseDiary());
    }
    public void ButtonNext()
    {
        ShowPage2();
        openDiary(oldText1, oldText2);
        page = 2;
        imageHolder.GetComponent<Image>().sprite = image;
        imageHolder.SetActive(true);
        textBoxPages.SetActive(true);
    }
    public void ButtonPrev()
    {
        ShowPage1();
        page = 1;
        imageHolder.GetComponent<Image>().sprite = image;
        imageHolder.SetActive(true);
        textBoxPages.SetActive(true);
    }

    private bool TriggerRight()
    {
        if (Input.GetAxis("Horizontal") > 0 && !triggerRight)
            triggerRight = true;
        else if (Input.GetAxis("Horizontal") == 0 && triggerRight)
        {
            triggerRight = false;
            return true;
        }
        return false;
    }
    private bool TriggerLeft()
    {
        if (Input.GetAxis("Horizontal") < 0 && !triggerLeft)
            triggerLeft = true;
        else if (Input.GetAxis("Horizontal") == 0 && triggerLeft)
        {
            triggerLeft = false;
            return true;
        }
        return false;
    }
    private void SetTextBox(string text, float x, float y, float width, float height)
    {
        GlobalVariables.canMove = false;
        textBox.SetActive(true);
        textBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        textArea.text = text;
    }
    private IEnumerator CloseDiary()
    {
        yield return new WaitForSeconds(.0001f);
        CloseTextBoxPages();
    }

    private void CloseTextBox()
    {
        textBox.SetActive(false);
        GlobalVariables.canMove = true;
    }

    private void ShowPage1()
    {
        openDiary("Day one of my quarantine. Standard procedure. They say it’s good to keep track of your thoughts in isolation.\nMy thoughts drift back to Alenea.\nHer beautiful surface floating in the dark just below us.", "I can’t wait to get out. But the rules are in place for a reason, after all.\n\nPatience, Mai. Patience.");
        scribble.SetActive(true);
    }

    private void ShowPage2()
    {
        scribble.SetActive(false);
        if (!GlobalVariables.diaryWritten && GlobalVariables.hasEaten)
        {
            scribble.SetActive(false);
            oldText1 = "Doctor Reiley is checking my blood sample. Hope it’s good news, although standard procedure is still 72 hours of quarantine.\n\nSigh... I’m not getting out any time soon.";
            oldText2 = "But dinner was alright. I feel pretty good.";
            openDiary(oldText1, oldText2);
        }
        else if (!GlobalVariables.diaryWritten && !GlobalVariables.hasEaten && GlobalVariables.doorActivated)
        {
            scribble.SetActive(false);
            oldText1 = "Doctor Reiley is checking my blood sample. Hope it’s good news, although standard procedure is still 72 hours of quarantine.\n\nSigh... I’m not getting out any time soon.";
            oldText2 = "I didn’t feel like eating dinner even though I AM hungry. Odd.";
            openDiary(oldText1, oldText2);
            GlobalVariables.diaryWritten = true;
        }
        else if (GlobalVariables.diaryWritten && GlobalVariables.hasEaten)
        {
            scribble.SetActive(false);
            oldText1 = "Doctor Reiley is checking my blood sample. Hope it’s good news, although standard procedure is still 72 hours of quarantine.\n\nSigh... I’m not getting out any time soon.";
            oldText2 = "I didn’t feel like eating dinner even though I AM hungry. Odd.\n\nI ate my dinner anyway. No need to let it go to waste. It was pretty good I guess.";
            openDiary(oldText1, oldText2);
        }
        else
        {
            imageHolder.GetComponent<Image>().sprite = image;
            imageHolder.SetActive(true);
            textBoxPages.SetActive(true);
        }
    }

    private void openDiary(string text1, string text2)
    {
        imageHolder.GetComponent<Image>().sprite = image;
        imageHolder.SetActive(true);
        textArea.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        SetTextPage1(text1);
        SetTextPage2(text2);
    }

    private void SetTextPage1(string text)
    {
        GlobalVariables.canMove = false;
        textBoxPages.SetActive(true);
        textAreaPage1.text = text;
    }

    private void SetTextPage2(string text)
    {
        GlobalVariables.canMove = false;
        textBoxPages.SetActive(true);
        textAreaPage2.text = text;
    }

    private void CloseTextBoxPages()
    {
        textBoxPages.SetActive(false);
        imageHolder.SetActive(false);
        GlobalVariables.canMove = true;
    }

    private IEnumerator SelectButton(GameObject defaultButton)
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
