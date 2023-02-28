using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PosterTextBox : MonoBehaviour
{
    private bool playerInRange;
    private bool d2HuhRead;
    private int page = 0;
    private bool yesPressed = false;
    private bool textLoaded = true;

    [Header("Objects")]
    public GameObject posterWall;
    public GameObject posterFolded;
    public GameObject panel;
    
    [Header("Images")]
    public GameObject imageHolder;
    public Sprite image;
    public GameObject imageLeak;

    [Header("Dialog Textbox Settings")]
    [Tooltip("Textbox Game Object")]
    public GameObject textBox;
    public Text textArea;

    [Header("Buttons")]
    public GameObject buttons;
    public GameObject defaultButton;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && playerInRange && textLoaded)
        {
            if (GlobalVariables.powerOn)
            {
                if (GlobalVariables.dayNumber != 3)
                {
                    if (imageHolder.activeInHierarchy && GlobalVariables.dayNumber != 2)
                    {
                        imageHolder.SetActive(false);
                        GlobalVariables.canMove = true;
                    }
                    else if (imageHolder.activeInHierarchy && textBox.activeInHierarchy && GlobalVariables.dayNumber == 2)
                    {
                        CloseTextBox();
                        GlobalVariables.canMove = false;
                        d2HuhRead = true;
                    }
                    else if (imageHolder.activeInHierarchy && !textBox.activeInHierarchy && GlobalVariables.dayNumber == 2 && d2HuhRead)
                    {
                        imageHolder.SetActive(false);
                        GlobalVariables.canMove = true;
                    }
                    else if (!imageHolder.activeInHierarchy)
                    {
                        imageHolder.GetComponent<Image>().sprite = image;
                        imageHolder.SetActive(true);
                        if (GlobalVariables.dayNumber == 2)
                        {
                            textArea.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
                            StartCoroutine(SetTextBox("Huh.", -200, -130, 100, 45, 0.5f));
                            d2HuhRead = false;
                        }
                        GlobalVariables.canMove = false;
                    }
                }
                else
                {
                    if (imageHolder.activeInHierarchy)
                    {
                        if (!GlobalVariables.ductTapeTaken)
                        {
                            imageHolder.SetActive(false);
                            GlobalVariables.canMove = true;
                        }
                        else
                        {
                            if (page == 1 && !yesPressed)
                            {
                                SetTextBox("Fix poster with duct tape?", -150, -130, 271, 60);
                                buttons.SetActive(true);
                                StartCoroutine(SelectButton());
                                yesPressed = true;
                            }
                            else if (page == 5)
                            {
                                ExitLeakImage();
                            }
                        }
                    }
                    else if (!yesPressed)
                    {
                        imageHolder.GetComponent<Image>().sprite = image;
                        imageHolder.SetActive(true);
                        GlobalVariables.canMove = false;
                        if (GlobalVariables.ductTapeTaken)
                        {
                            textArea.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
                            if (page == 0)
                            {
                                StartCoroutine(SetTextBox("The tear in the\nposter has grown....", -150, -130, 220, 60, 0.5f));
                                page += 1;
                                textLoaded = false;
                            }
                        }
                        GlobalVariables.canMove = false;
                    }
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
                    SetTextBox("Well, at least I can’t\nlook at that stupid\ncat poster anymore.", -10, 120, 240, 75);
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
        textLoaded = true;
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

    private void ExitLeakImage()
    {
        posterWall.SetActive(false);
        panel.SetActive(true);
        posterFolded.SetActive(true);

        imageHolder.SetActive(false);
        imageLeak.SetActive(false);
        CloseTextBox();
    }

    public void Yes()
    {
        buttons.SetActive(false);
        imageLeak.SetActive(true);
        imageHolder.SetActive(true);
        textBox.SetActive(false);
        StartCoroutine(ResetPoster());
        textArea.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
        StartCoroutine(SetTextBox("What’s this?!", -150, -130, 150, 45, 0.5f));
        GlobalVariables.posterFixed = true;
    }
    public void No()
    {
        imageHolder.SetActive(false);
        textBox.SetActive(false);
        buttons.SetActive(false);
        GlobalVariables.canMove = true;
        page = 1;
        StartCoroutine(RemoveImage());
        yesPressed = false;

    }

    private IEnumerator ResetPoster()
    {
        yield return new WaitForSeconds(0.0001f);
        yesPressed = false;
        page = 5;
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
