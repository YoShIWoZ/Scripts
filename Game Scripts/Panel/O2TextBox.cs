using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class O2TextBox : MonoBehaviour
{
    private bool playerInRange;
    private Animator animator;

    [Header("Overworld")]
    //public GameObject panel;
    public GameObject O2;
    public GameObject O2Fixed;

    [Header("Images")]
    public GameObject imageHolder;
    //public GameObject imageLeak;
    public GameObject imageO2Leak;
    public Sprite image;

    [Header("Dialog Textbox Settings")]
    [Tooltip("Textbox Game Object")]
    public GameObject textBox;
    public Text textArea;

    [Header("Buttons")]
    public GameObject buttons;
    public GameObject defaultButton;

    // Update is called once per frame
    private void Start()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();
        if (animator.GetInteger("suitNum") == 0 || animator.GetInteger("suitNum") == 2)
        {

            StartCoroutine(SetTextBox("Woah.\nGetting dizzy...", -150, -130, 180, 60, 0.5f));
        }
        else if (animator.GetInteger("suitNum") == 1)
        {
            imageHolder.SetActive(true);
            buttons.SetActive(true);
            StartCoroutine(SelectButton());
            textArea.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
            SetTextBox("Fix leak with duct tape?", 0, 150, 270, 60);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit") && playerInRange)
        {
            if (!imageHolder.activeInHierarchy)
            {
                imageHolder.SetActive(true);
                imageO2Leak.SetActive(true);
                textBox.SetActive(false);
                textArea.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
                if (animator.GetInteger("suitNum") == 0 || animator.GetInteger("suitNum") == 2)
                {

                    StartCoroutine(SetTextBox("Woah.\nGetting dizzy...", -150, -130, 180, 60, 0.5f));
                }
                else if (animator.GetInteger("suitNum") == 1)
                {
                    imageHolder.SetActive(true);
                    buttons.SetActive(true);
                    StartCoroutine(SelectButton());
                    textArea.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
                    SetTextBox("Fix leak with duct tape?", 0, 150, 270, 60);
                }
            }
            else if (imageHolder.activeInHierarchy && animator.GetInteger("suitNum") == 0 || animator.GetInteger("suitNum") == 2)
            {
                imageHolder.SetActive(false);
                imageO2Leak.SetActive(false);
                textBox.SetActive(false);
                GlobalVariables.canMove = true;
            }
        }
    }

    private IEnumerator SetTextBox(string text, float x, float y, float width, float height, float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        GlobalVariables.canMove = false;
        textBox.SetActive(true);
        textBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        textArea.text = text;
    }

    private void SetTextBox(string text, float x, float y, float width, float height)
    {
        GlobalVariables.canMove = false;
        textBox.SetActive(true);
        textBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        textArea.text = text;
    }
    private IEnumerator SelectButton()
    {
        yield return new WaitForSeconds(.0001f);
        EventSystem.current.SetSelectedGameObject(defaultButton);
    }

    public void Yes()
    {
        buttons.SetActive(false);
        textBox.SetActive(false);
        imageO2Leak.SetActive(false);
        imageHolder.SetActive(true);
        imageHolder.GetComponent<Image>().sprite = image;
        O2Fixed.SetActive(true);
        O2.SetActive(false);
    }

    public void No()
    {
        StartCoroutine(RemoveImage());
    }

    private IEnumerator RemoveImage()
    {
        yield return new WaitForSeconds(0.0001f);
        imageHolder.SetActive(false);
        textBox.SetActive(false);
        imageO2Leak.SetActive(false);
        buttons.SetActive(false);
        GlobalVariables.canMove = true;
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
