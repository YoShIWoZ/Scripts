using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class dialogD3Box : MonoBehaviour
{
    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public GameObject image4;
    public GameObject image5;
    public GameObject image6;
    public GameObject image7;

    public GameObject canvas;
    public bool playerInRange;
    private int page;
    public float delay = 0.1f;
    private bool isWriting;

    [Header("Dialog Textbox Settings")]
    [Tooltip("Textbox Game Object")]
    public GameObject textBox;
    public Text textArea;
    private string currentText;
    private Color color;
    public Font defaultFont;
    public Font aleneaFont;

    void Start()
    {
        page = 1;
        //textBox.SetActive(true);
        //textArea.GetComponent<Text>().font = fontMai;
        //if (ColorUtility.TryParseHtmlString("#D6D6D6", out color))
        //{ textArea.GetComponent<Text>().color = color; }
        //StartCoroutine(SetText("", 0, -167, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (textBox.activeInHierarchy && !isWriting)
            {
                CloseTextBox();
                return;
            }
            if (page == 1 && !isWriting)
            {
                StartCoroutine(SetText("Behold.", -220, 180, 105, 50));
            }
            else if (page == 2 && !isWriting)
            {
                StartCoroutine(SetText("I grow.", -220, 180, 105, 50));
            }
            else if (page == 3 && !isWriting)
            {
                StartCoroutine(SetText("Bold...", -220, 180, 105, 50));
            }
            else if (page == 4 && !isWriting)
            {
                StartCoroutine(SetText("...but slow.", -200, 180, 155, 50));
            }
            else if (page == 5 && !isWriting)
            {
                image1.SetActive(false);
                image2.SetActive(false);
                image3.SetActive(false);
                image4.SetActive(false);
                image5.SetActive(true);
                StartCoroutine(SetText("Birds like you should\nnot be caged.", -150, 180, 260, 70));
            }
            else if (page == 6 && !isWriting)
            {
                StartCoroutine(SetText("Take flight,\nchild of a dying world.", -150, 180, 274, 70));
            }
            else if (page == 7 && !isWriting)
            {
                StartCoroutine(SetText("Breathe free.", -195, 180, 168, 50));
            }
            else if (page == 8 && !isWriting)
            {
                image5.SetActive(false);
                image6.SetActive(true);
                StartCoroutine(SetText("  ...", 205, 180, 150, 50));
            }
            else if (page == 9 && !isWriting)
            {
                StartCoroutine(SetText("This is something\nbeautiful...", 180, 180, 215, 70));
            }
            else if (page == 10 && !isWriting)
            {
                StartCoroutine(SetText(" ...isn’t it?", 200, 180, 170, 50));
            }
            else if (page == 11 && !isWriting)
            {
                image6.SetActive(false);
                image7.SetActive(true);
                textArea.GetComponent<Text>().font = aleneaFont;
                if (ColorUtility.TryParseHtmlString("#E537C1", out color))
                { textArea.GetComponent<Text>().color = color; }
                StartCoroutine(SetText("Yes, little one.", -190, 180, 175, 50));
            }
            else if (page == 12 && !isWriting)
            {
                StartCoroutine(SetText("Awake to your new home", -150, 180, 250, 50));
            }
            else if (page == 13 && !isWriting)
            {
                StartCoroutine(SetText("  and sleep no more.", -175, 180, 210, 50));
            }
            else if (page == 14 && !isWriting)
            {
                StartCoroutine(GoToDay1());
            }
        }
        //if (Input.GetKeyDown(KeyCode.Space) && playerInRange && animator.GetInteger("suitFoodNum") > 0 && !textBox.activeInHierarchy)
        //{
        //    GlobalVariables.canMove = false;
        //    textBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(-125, 40);
        //    textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(181, 54);
        //    textArea.text = "I should probably\neat first.";
        //    textBox.SetActive(true);
        //}
        //else if (Input.GetKeyDown(KeyCode.Space) && playerInRange && animator.GetInteger("suitFoodNum") > 0 && textBox.activeInHierarchy)
        //{
        //    textBox.SetActive(false);
        //    GlobalVariables.canMove = true;
        //}
        //if (Input.GetKeyDown(KeyCode.Space) && playerInRange && !spokenWithRiley && animator.GetInteger("suitFoodNum") == 0)
        //{
        //    if (imageBoxRiley.activeInHierarchy && page == 1)
        //    {
        //        StartCoroutine(SetText("Hello, sleeping beauty.\nHow are you feeling today?", -154, 166, 270, 60));
        //    }
        //    else if (imageBoxRiley.activeInHierarchy && page == 2)
        //    {
        //        StartCoroutine(SetText("Fine. A little restless.", 167, -153, 258, 46));
        //    }
        //    else if (imageBoxRiley.activeInHierarchy && page == 3)
        //    {
        //        StartCoroutine(SetText("Well, that’s to be expected.\nWe are analysing the samples of\nflora you and Ken collected on\nAlenea.Fascinating.", -132, 156, 320, 88));
        //    }
        //    else if (imageBoxRiley.activeInHierarchy && page == 4)
        //    {
        //        StartCoroutine(SetText("Yes?", 247, -157, 83, 46));
        //    }
        //    else if (imageBoxRiley.activeInHierarchy && page == 5)
        //    {
        //        StartCoroutine(SetText("Oh yes. Alenea’s vegetation is\nripe wth potential.Seems her\natmosphere is mostly breathable,\nthanks to her rich plant life.", -122, 154, 328, 88));
        //    }
        //    else if (imageBoxRiley.activeInHierarchy && page == 6)
        //    {
        //        StartCoroutine(SetText("That’s amazing,\nDoctor Reiley!", 197, -150, 180, 60));
        //    }
        //    else if (imageBoxRiley.activeInHierarchy && page == 7)
        //    {
        //        StartCoroutine(SetText("Yes, but be glad you didn’t\nbreathe it, Mai.According to\nthe analysis, spores in the air\nare highly hallucinogetic. And\nlong exposure could be fatal.", -128, 148, 320, 102));
        //    }
        //    else if (imageBoxRiley.activeInHierarchy && page == 8)
        //    {
        //        StartCoroutine(SetText("Well, ...\nguess that’s why I’m in here.", 142, -152, 302, 60));
        //    }
        //    else if (imageBoxRiley.activeInHierarchy && page == 9)
        //    {
        //        StartCoroutine(SetText("Chin up, Mai! We have prepared a\nnice dinner for you. And if you get\nbored play a game or listen to some\nmusic.\nDon’t forget to update your journal.\nIt’s important to keep track of your\nthoughts in isolation.", -110, 132, 363, 131));
        //    }
        //    else if (imageBoxRiley.activeInHierarchy && page == 10)
        //    {
        //        StartCoroutine(SetText("I know.", 232, -159, 110, 46));
        //    }
        //    else if (imageBoxRiley.activeInHierarchy && page == 11)
        //    {
        //        StartCoroutine(SetText("Alright, see you later.\nI will go analyse your\nblood sample for toxins.", -154, 161, 258, 74));
        //    }
        //    else if (imageBoxRiley.activeInHierarchy && page == 12)
        //    {
        //        imageBoxRiley.SetActive(false);
        //        textBox.SetActive(false);
        //        GlobalVariables.canMove = true;
        //        spokenWithRiley = true;
        //    }
        //    else
        //    {
        //        GlobalVariables.doorActivated = true;
        //        GlobalVariables.canMove = false;
        //        StartCoroutine(LoadDoorScreen());
        //    }
        //}
        //else if (Input.GetKeyDown(KeyCode.Space) && playerInRange && spokenWithRiley && animator.GetInteger("suitFoodNum") == 0)
        //{
        //    if (imageBoxEmpty.activeInHierarchy)
        //    {
        //        imageBoxEmpty.SetActive(false);
        //        GlobalVariables.canMove = true;
        //    }
        //    else
        //    {
        //        GlobalVariables.canMove = false;
        //        StartCoroutine(LoadDoorScreenEmpty());
        //    }
        //}
    }
    private IEnumerator delayDay1()
    {
        yield return new WaitForSeconds(0);
        textBox.SetActive(false);
        image3.SetActive(false);
        image4.SetActive(true);
        StartCoroutine(GoToDay1());
    }
    private IEnumerator GoToDay1()
    {
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene("Day3");
    }
    private IEnumerator SetText(string text, float x, float y, float width, float height)
    {
        textBox.SetActive(true);
        textBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        playerInRange = false;
        isWriting = true;
        for (int i = 0; i < text.Length + 1; i++)
        {
            currentText = text.Substring(0, i);
            textArea.text = currentText;
            yield return new WaitForSeconds(delay);
        }
        page = page + 1;
        isWriting = false;
    }

    private void CloseTextBox()
    {
        textBox.SetActive(false);
    }
}
