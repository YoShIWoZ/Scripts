using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelTextBox : MonoBehaviour
{
    private bool playerInRange;
    private Animator animator;

    [Header("Overworld")]
    public GameObject panel;
    public GameObject O2;

    [Header("Images")]
    public GameObject imageHolder;
    public GameObject imageLeak;
    public GameObject imageO2Leak;

    [Header("Dialog Textbox Settings")]
    [Tooltip("Textbox Game Object")]
    public GameObject textBox;
    public Text textArea;

    [Header("Buttons")]
    public GameObject buttons;
    public GameObject defaultButton;

    private void Start()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && playerInRange)
        {
            if (!GlobalVariables.screwdriverTaken)
            {
                if (!imageHolder.activeInHierarchy)
                {
                    imageLeak.SetActive(true);
                    imageHolder.SetActive(true);
                    textBox.SetActive(false);
                    textArea.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
                    StartCoroutine(SetTextBox("What’s this?!", -150, -130, 150, 45, 0.5f));
                }
                else
                {
                    imageLeak.SetActive(false);
                    imageHolder.SetActive(false);
                    textBox.SetActive(false);
                    GlobalVariables.canMove = true;
                }
            }
            else
            {
                if (!imageHolder.activeInHierarchy)
                {
                    imageLeak.SetActive(true);
                    imageHolder.SetActive(true);
                    buttons.SetActive(true);
                    StartCoroutine(SelectButton());
                    textArea.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
                    SetTextBox("Unscrew leaky panel?", 0, 150, 220, 60);
                }
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
        imageLeak.SetActive(false);
        imageO2Leak.SetActive(true);
        imageHolder.SetActive(true);
        O2.SetActive(true);
        panel.SetActive(false);
    }

    public void No()
    {
        StartCoroutine(RemoveImage());
        imageLeak.SetActive(false);
        buttons.SetActive(false);
    }

    private IEnumerator RemoveImage()
    {
        yield return new WaitForSeconds(0.0001f);
        imageHolder.SetActive(false);
        textBox.SetActive(false);
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
