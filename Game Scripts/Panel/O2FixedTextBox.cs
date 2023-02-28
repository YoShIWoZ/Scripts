using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class O2FixedTextBox : MonoBehaviour
{
    private bool playerInRange;
    private Animator animator;

    //[Header("Overworld")]
    ////public GameObject panel;
    //public GameObject O2Fixed;

    [Header("Images")]
    public GameObject imageHolder;
    //public GameObject imageLeak;
    //public GameObject imageO2Leak;
    public Sprite image;

    [Header("Dialog Textbox Settings")]
    [Tooltip("Textbox Game Object")]
    public GameObject textBox;
    public Text textArea;

    //[Header("Buttons")]
    //public GameObject buttons;
    //public GameObject defaultButton;

    // Update is called once per frame
    private void Start()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();
        textArea.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
        StartCoroutine(SetTextBox("Whew! That did it!", -150, -130, 200, 45, 0f));
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit") && playerInRange && GlobalVariables.canInteract)
        {
            if (!imageHolder.activeInHierarchy)
            {
                imageHolder.SetActive(true);
                imageHolder.GetComponent<Image>().sprite = image;
            }
            else
            {
                StartCoroutine(RemoveImage());
                if (!GlobalVariables.o2Fixed)
                    GlobalVariables.o2Fixed = true;
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
    //private IEnumerator SelectButton()
    //{
    //    yield return new WaitForSeconds(.0001f);
    //    EventSystem.current.SetSelectedGameObject(defaultButton);
    //}

    //public void Yes()
    //{
    //    buttons.SetActive(false);
    //    textBox.SetActive(false);
    //    imageO2Leak.SetActive(false);
    //    imageHolder.SetActive(true);
    //    imageHolder.GetComponent<Image>().sprite = image;
    //    O2.SetActive(false);
    //    O2Fixed.SetActive(true);
    //}

    //public void No()
    //{
    //    StartCoroutine(RemoveImage());
    //}

    private IEnumerator RemoveImage()
    {
        yield return new WaitForSeconds(0.0001f);
        imageHolder.SetActive(false);
        textBox.SetActive(false);
        //buttons.SetActive(false);
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
