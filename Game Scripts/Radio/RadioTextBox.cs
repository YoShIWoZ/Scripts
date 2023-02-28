using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RadioTextBox : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Animator animator;
    public GameObject radioImageTest;
    public GameObject radioImages;
    public Sprite image;
    public Sprite imageSecret;
    public Sprite imagePowerReturned;
    public GameObject casette;
    public GameObject casettePowerReturned;
    public GameObject radioImage;
    public GameObject radioOn;
    public GameObject radioOff;
    public GameObject defaultButton;
    public bool playerInRange;

    private int page = 0;

    private bool hasPlayedNew;


    [Header("Buttons")]
    public GameObject buttonsSwap;
    public GameObject buttons;
    public GameObject defaultButtonSwap;

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

    void Start()
    {
        myRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        animator = GameObject.Find("Player").GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.powerReturned && !hasPlayedNew)
        {
            StartCoroutine(PlayRadio());
        }

        if (Input.GetButtonDown("Submit") && playerInRange)
        {
            if (GlobalVariables.powerOn)
            {
                if (GlobalVariables.powerReturned && GlobalVariables.dayNumber == 2)
                {
                    if (!radioImageTest.activeInHierarchy)
                    {
                        radioImageTest.GetComponent<Image>().sprite = imagePowerReturned;
                        radioImageTest.SetActive(true);
                        radioImages.SetActive(true);
                        casette.SetActive(false);
                        casettePowerReturned.SetActive(true);
                        GlobalVariables.canMove = false;
                        StartCoroutine(SelectButton());
                    }
                    else
                    {
                        radioImageTest.SetActive(false);
                        radioImages.SetActive(false);
                        GlobalVariables.canMove = true;
                    }
                }
                else
                {
                    if (!GlobalVariables.secretTapeTaken)
                    {
                        if (!radioImageTest.activeInHierarchy)
                        {
                            radioImageTest.GetComponent<Image>().sprite = image;
                            radioImageTest.SetActive(true);
                            radioImages.SetActive(true);
                            buttons.SetActive(true);
                            if (GlobalVariables.dayNumber == 2 || GlobalVariables.dayNumber == 3)
                                casettePowerReturned.SetActive(false);
                            casette.SetActive(true);
                            GlobalVariables.canMove = false;
                            StartCoroutine(SelectButton());
                        }
                        else
                        {
                            radioImageTest.SetActive(false);
                            radioImages.SetActive(false);
                            GlobalVariables.canMove = true;
                        }
                    }
                    else
                    {
                        if (!radioImageTest.activeInHierarchy && page == 0)
                        {
                            radioImageTest.GetComponent<Image>().sprite = image;
                            radioImageTest.SetActive(true);
                            buttons.SetActive(false);
                            radioImages.SetActive(true);
                            SetTextBox("Insert found tape?", -195, 190, 200, 60);
                            buttonsSwap.SetActive(true);
                            GlobalVariables.canMove = false;
                            StartCoroutine(SelectButtonSwap());
                            page += 1;
                        }
                        else if (page == 1)
                        {
                            radioImageTest.GetComponent<Image>().sprite = imageSecret;
                            radioImageTest.SetActive(true);
                            radioImages.SetActive(true);
                            GlobalVariables.secretTapePlayed = true;
                            CloseTextBox();
                            buttonsSwap.SetActive(false);
                            buttons.SetActive(true);
                            casette.SetActive(false);
                            GlobalVariables.canMove = false;
                            StartCoroutine(SelectButton());
                            page = 3;
                        }
                        else if (radioImageTest.activeInHierarchy && page == 2)
                        {
                            radioImageTest.GetComponent<Image>().sprite = image;
                            radioImageTest.SetActive(true);
                            radioImages.SetActive(true);
                            buttons.SetActive(true);
                            if (GlobalVariables.dayNumber == 2 || GlobalVariables.dayNumber == 3)
                                casettePowerReturned.SetActive(false);
                            casette.SetActive(true);
                            GlobalVariables.canMove = false;
                            StartCoroutine(SelectButton());
                        }
                        else if (radioImageTest.activeInHierarchy && page == 3)
                        {
                            radioImageTest.SetActive(false);
                            radioImages.SetActive(false);
                            GlobalVariables.canMove = true;
                            if (GlobalVariables.secretTapePlayed)
                                page = 1;
                            else
                                page = 2;
                        }
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
                    SetTextBox("No power. No music.", -10, 180, 210, 45);
                }
            }
        }
    }

    public void Play()
    {
        radioOn.SetActive(true);
        radioOff.SetActive(false);
        if (GlobalVariables.secretTapePlayed)
        {
            StartCoroutine(StopRadioAnimation(2.07584f*60));
        }
    }
    public void Stop()
    {
        radioOn.SetActive(false);
        radioOff.SetActive(true);
    }

    public void SwapTape()
    {
        StartCoroutine(SwapTapeAssist());
    }
    public void DontSwapTape()
    {

    }

    private IEnumerator StopRadioAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        Stop();
    }

    private IEnumerator SwapTapeAssist()
    {
        yield return new WaitForSeconds(.0001f);
        page = 3;
    }

    private IEnumerator PlayRadio()
    {
        yield return new WaitForSeconds(.00001f);
        radioOn.SetActive(true);
        radioOff.SetActive(false);
        hasPlayedNew = true;
    }

    private IEnumerator SelectButton()
    {
        yield return new WaitForSeconds(.1f);
        EventSystem.current.SetSelectedGameObject(defaultButton);
    }

    private IEnumerator SelectButtonSwap()
    {
        yield return new WaitForSeconds(.1f);
        EventSystem.current.SetSelectedGameObject(defaultButtonSwap);
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
