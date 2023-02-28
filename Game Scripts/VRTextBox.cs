using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class VRTextBox : MonoBehaviour
{
    private Animator animator;
    public GameObject vrGoggleFloor;
    public bool playerInRange;

    [Header("Scene Management")]
    public string SceneToLoad;

    [Header("Buttons")]
    public GameObject buttons;
    public GameObject defaultButton;
    public GameObject defaultButtons;

    [Header("Day 2 Buttons")]
    public GameObject powerReturnedButton;
    public GameObject doorActivatedButton;
    public GameObject powerReturnedButtons;
    public GameObject doorActivatedButtons;


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
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (GlobalVariables.powerOn)
            {
                if (animator.GetInteger("suitFoodNum") > 0 || animator.GetInteger("suitNum") == 1)
                {
                    if (!textBox.activeInHierarchy)
                    {
                        if (animator.GetInteger("suitFoodNum") > 0)
                        {
                            SetTextBox("I should probably\neat first.", x, y, 200, 60);
                            return;
                        }
                        else
                        {
                            SetTextBox("I should get out of this suit first.", x, y, 200, 60);
                            return;
                        }
                    }
                    else
                    {
                        CloseTextBox();
                        return;
                    }

                }
                else
                {
                    if (!textBox.activeInHierarchy && !GlobalVariables.powerReturned && !GlobalVariables.doorActivated)
                    {
                        if (GlobalVariables.dayNumber == 2)
                        {
                            powerReturnedButtons.SetActive(false);
                            doorActivatedButtons.SetActive(false);
                        }
                        defaultButtons.SetActive(true);
                        textArea.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
                        SetTextBox(text, x, y, width, height);
                        buttons.SetActive(true);
                        StartCoroutine(SelectButton());
                    }
                    else if (!textBox.activeInHierarchy && GlobalVariables.powerReturned && !GlobalVariables.doorActivated)
                    {
                        defaultButtons.SetActive(false);
                        powerReturnedButtons.SetActive(true);
                        textArea.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
                        SetTextBox("Game is on again...\n" + text, x, y, width, height + 15);
                        buttons.SetActive(true);
                        StartCoroutine(SelectButtonPowerReturned());
                    }
                    else if (!textBox.activeInHierarchy && GlobalVariables.powerReturned && GlobalVariables.doorActivated)
                    {                            
                        powerReturnedButtons.SetActive(false);
                        defaultButtons.SetActive(false);
                        doorActivatedButtons.SetActive(true);
                        textArea.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
                        SetTextBox(text, x, y, width, height);
                        buttons.SetActive(true);
                        StartCoroutine(SelectButtonDoorActivated());
                    }
                    else
                    {
                        CloseTextBox();
                        buttons.SetActive(false);
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
                    SetTextBox("No power. No games.", 160, -30, 210, 45);
                }
            }
            //if (dialogBox.activeInHierarchy && page == 1)
            //{
            //    print("Starting Co Routine");
            //    StartCoroutine(StartGame());
            //    print("Co Rutine Completed");
            //    dialogBox.SetActive(false);
            //    GlobalVariables.canMove = true;
            //    vrGoggleFloor.SetActive(false);
            //    page = 2;
            //    animator.SetBool("vrOn", true);

            //}
            //else if (!dialogBox.activeInHierarchy && page == 2)
            //{
            //    animator.SetBool("vrOn", false);
            //    vrGoggleFloor.SetActive(true);
            //    page = 1;
            //}
            //else if (!dialogBox.activeInHierarchy && page == 1)
            //{
            //    dialogBox.SetActive(true);
            //    dialogText.text = dialog;
            //    GlobalVariables.canMove = false;
            //}
        }
    }

    public void StartVR()
    {
        StartCoroutine(StartGame());
        GlobalVariables.canMove = true;
        vrGoggleFloor.SetActive(false);
        animator.SetBool("vrOn", true);
    }

    private IEnumerator SelectButton()
    {
        yield return new WaitForSeconds(.0001f);
        EventSystem.current.SetSelectedGameObject(defaultButton);
    }
    private IEnumerator SelectButtonPowerReturned()
    {
        yield return new WaitForSeconds(.0001f);
        EventSystem.current.SetSelectedGameObject(powerReturnedButton);
    }
    private IEnumerator SelectButtonDoorActivated()
    {
        yield return new WaitForSeconds(.0001f);
        EventSystem.current.SetSelectedGameObject(doorActivatedButton);
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Day1VRMenu");
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
