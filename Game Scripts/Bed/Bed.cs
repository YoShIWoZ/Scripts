using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Bed : MonoBehaviour
{
    public GameObject player;
    private Vector2 playerLocOld;
    public GameObject bedSleep;
    public GameObject bedSleepPS;
    public bool playerInRange;
    private Animator animator;
    private bool goToBed = false;

    public string DreamName = "DreamD2";

    [Header("Buttons")]
    public GameObject buttons;
    public GameObject buttonYes;
    public GameObject buttonNo;
    public GameObject defaultButton;

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

    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && playerInRange)
        {
            if (GlobalVariables.powerOn)
            {
                if (!textBox.activeInHierarchy && (animator.GetInteger("suitFoodNum") > 0 || animator.GetInteger("suitNum") == 1))
                {
                    if (animator.GetInteger("suitFoodNum") > 0)
                    {
                        SetTextBox("I should probably\neat first.", x, y, 200, 60);
                        return;
                    }
                    else
                    {
                        SetTextBox("I should get out of this suit first.", x, y, 210, 60);
                        return;
                    }
                }
                if (GlobalVariables.dayNumber != 3)
                {
                    if (!textBox.activeInHierarchy && (animator.GetInteger("suitNum") == 0 || animator.GetInteger("suitNum") == 2) && animator.GetInteger("suitFoodNum") == 0 && GlobalVariables.doorActivated && !goToBed)
                    {
                        GlobalVariables.canMove = false;
                        textBox.SetActive(true);
                        textArea.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
                        if (GlobalVariables.hasEaten)
                        {
                            if (GlobalVariables.dayNumber == 1)
                            {
                                SetTextBox("Should I sleep?", 150, 12, 175, 55);
                                TransformButtons(-1, 65, 10, 40, 28);
                            }
                            else if (GlobalVariables.dayNumber == 2)
                            {
                                SetTextBox("Sleep?", 150, 12, 130, 55);
                                TransformButtons(21, 80, 10, 40, 28);
                            }
                        }
                        else
                        {
                            SetTextBox("I’m pretty hungry.\nShould I still sleep ?", 150, 12, 250, 70);
                            TransformButtons(-39, 28, 3, 40, 21);
                        }
                        buttons.SetActive(true);
                        StartCoroutine(SelectButton());
                    }
                    else if (!textBox.activeInHierarchy && !GlobalVariables.doorActivated && animator.GetInteger("suitFoodNum") == 0 && !GlobalVariables.powerReturned)
                    {
                        textArea.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
                        SetTextBox(text, x, y, width, height);
                    }
                    else if (!textBox.activeInHierarchy && !GlobalVariables.doorActivated && animator.GetInteger("suitFoodNum") == 0 && GlobalVariables.powerReturned)
                    {
                        textArea.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
                        SetTextBox("I’m not going to sleep now!", x, y, 180, 60);
                    }
                    else if (textBox.activeInHierarchy)
                    {
                        CloseTextBox();
                        buttons.SetActive(false);
                        return;
                    }
                }
                else
                {
                    if (textBox.activeInHierarchy)
                    {
                        CloseTextBox();
                        buttons.SetActive(false);
                        return;
                    }
                    else
                    {
                        textArea.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
                        SetTextBox("I’ve slept enough.", 147, 17, 197, 41);
                    }
                }
            }
            else
            {
                if (textBox.activeInHierarchy)
                {
                    CloseTextBox();
                }
                else
                {
                    SetTextBox("I think this is\nthe bed.", 150, 12, 180, 60);
                }
            }
            
        }
    }

    private void TransformButtons(float yesX, float noX, float y, float width, float height)
    {
        buttonYes.GetComponent<RectTransform>().anchoredPosition = new Vector2(yesX, y);
        buttonYes.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        buttonNo.GetComponent<RectTransform>().anchoredPosition = new Vector2(noX, y);
        buttonNo.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
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

    private IEnumerator Sleep()
    {
        yield return new WaitForSeconds(4f);
        bedSleep.SetActive(false);
        GlobalVariables.canMove = true;
        player.GetComponent<Transform>().position = playerLocOld;
    }
    private IEnumerator SleepPS()
    {
        yield return new WaitForSeconds(4f);
        bedSleepPS.SetActive(false);
        GlobalVariables.canMove = true;
        player.GetComponent<Transform>().position = playerLocOld;
    }

    private IEnumerator GoToDream()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(DreamName);
        GlobalVariables.doorActivated = false;
        GlobalVariables.powerReturned = false;
    }

    public void GoToNextPhase()
    {
        GlobalVariables.canMove = false;
        playerLocOld = player.GetComponent<Transform>().position;
        player.GetComponent<Transform>().position = new Vector2(9999f, 999f);
        if (animator.GetInteger("suitNum") == 0)
        {
            bedSleep.SetActive(true);
            StartCoroutine(Sleep());
        }
        else if (animator.GetInteger("suitNum") == 2)
        {
            bedSleepPS.SetActive(true);
            StartCoroutine(SleepPS());
        }
        StartCoroutine(GoToDream());
    }

    private IEnumerator SelectButton()
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
