using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WardrobeTextBox : MonoBehaviour
{
    private Animator animator;
    public GameObject door;
    public bool playerInRange;
    public bool ductOnFloor = false;
    private int suitNum = 0;
    public float closeTime = 0.20f;
    public GameObject leftActivateField;

    [Header("Wardrobe Settings")]
    public int alternateSuitNumber;
    public string alternateSuitString;
    public GameObject ductTape;
    public GameObject ductTapeRolling;
    public GameObject ductTapeFloor;

    [Header("Images")]
    public GameObject imageHolder;
    public Sprite image1;
    public Sprite image2;
    public GameObject buttons;
    public GameObject defaultButton;
    public Sprite image1Blackout;
    public Sprite image2Blackout;

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
        animator = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        suitNum = animator.GetInteger("suitNum");

        if (Input.GetButtonDown("Submit") && playerInRange)
        {
            if (animator.GetInteger("suitFoodNum") > 0)
            {
                if (!textBox.activeInHierarchy)
                {

                    SetTextBox("I should probably\neat first.", x, y, 200, 60);
                    buttons.SetActive(false);
                    return;
                }
                else
                {
                    CloseTextBox();
                    return;
                }

            }
            else
            {
                if (suitNum == 0)
                {
                    if (!imageHolder.activeInHierarchy)
                    {
                        door.SetActive(true);
                        StartCoroutine(SuitSSArtActivate());
                    }
                    else
                    {
                        imageHolder.SetActive(false);
                        CloseTextBox();
                        suitNum = animator.GetInteger("suitNum");
                        StartCoroutine(CloseDoor());
                    }
                }
                else if (suitNum == alternateSuitNumber)
                {
                    if (GlobalVariables.points == 100)
                    {
                        if (!imageHolder.activeInHierarchy)
                        {
                            door.SetActive(true);
                            StartCoroutine(SuitMaiArtActivate());
                        }
                        else
                        {
                            imageHolder.SetActive(false);
                            CloseTextBox();
                            suitNum = animator.GetInteger("suitNum");
                            StartCoroutine(CloseDoor());
                            if (ductTape && !GlobalVariables.hasRolled)
                                StartCoroutine(DuctTapeRoll());
                        }
                    }
                }
            }
        }
    }

    public void SwapGear()
    {
        if (suitNum == alternateSuitNumber)
        {
            animator.SetInteger("suitNum", 0);
            GlobalVariables.spaceSuitOn = false;
        }
        else if (suitNum == 0)
        {
            animator.SetInteger("suitNum", alternateSuitNumber);
            if (alternateSuitNumber == 1)
                GlobalVariables.spaceSuitOn = true;
        }
    }

    private void SetTextBox(string text, float x, float y, float width, float height)
    {
        GlobalVariables.canMove = false;
        textArea.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
        textBox.SetActive(true);
        textBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        textArea.text = text;
        buttons.SetActive(true);
    }
    private void CloseTextBox()
    {
        textBox.SetActive(false);
        buttons.SetActive(false);
        GlobalVariables.canMove = true;
    }

    private IEnumerator SuitSSArtActivate()
    {
        yield return new WaitForSeconds(.30f);
        if (GlobalVariables.powerOn)
            imageHolder.GetComponent<Image>().sprite = image1;
        else
            imageHolder.GetComponent<Image>().sprite = image1Blackout;
        imageHolder.SetActive(true);
        SetTextBox(alternateSuitString, -200, 197, 200, 55);
        EventSystem.current.SetSelectedGameObject(defaultButton);
    }
    private IEnumerator SuitMaiArtActivate()
    {
        yield return new WaitForSeconds(.30f);
        if (GlobalVariables.powerOn)
            imageHolder.GetComponent<Image>().sprite = image2;
        else
            imageHolder.GetComponent<Image>().sprite = image2Blackout;
        imageHolder.SetActive(true);
        SetTextBox("Put on jump suit?", -200, 197, 200, 55);
        EventSystem.current.SetSelectedGameObject(defaultButton);
    }

    private IEnumerator DuctTapeRoll()
    {
        GlobalVariables.canMove = false;
        yield return new WaitForSeconds(.70f);
        GlobalVariables.hasRolled = true;
        ductTape.SetActive(false);
        ductTapeRolling.SetActive(true);
        yield return new WaitForSeconds(.80f);
        ductTapeRolling.SetActive(false);
        ductTapeFloor.SetActive(true);
        GlobalVariables.canMove = true;
    }

    private IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(closeTime);
        door.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractField") && collision.isTrigger && !leftActivateField.activeInHierarchy)
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
