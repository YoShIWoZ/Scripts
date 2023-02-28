using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class dialogD2Box : MonoBehaviour
{
    public GameObject background;
    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public GameObject image4;
    public GameObject canvas;
    public AudioClip beepAudio;
    public float delayAudio;
    public float delayBeepAudio;
    public bool playerInRange;
    private int page;
    public float delay = 0.1f;
    private bool isWriting;

    [Header("Dialog Textbox Settings")]
    [Tooltip("Textbox Game Object")]
    public GameObject textBox;
    public Text textArea;
    public Font fontAlenea;
    public Font fontMai;
    private string currentText;
    private Color color;

    void Start()
    {
        image1.SetActive(true);
        textBox.SetActive(true);
        textArea.GetComponent<Text>().font = fontMai;
        if (ColorUtility.TryParseHtmlString("#D6D6D6", out color))
        { textArea.GetComponent<Text>().color = color; }
        StartCoroutine(SetText("Breathe it in, Mai.", 0, -167, 230, 50));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (page == 1 && !isWriting)
            {
                StartCoroutine(SetText("Isn't it beautiful?", 0, -167, 230, 50));
            }
            else if (page == 2 && !isWriting)
            {
                background.SetActive(false);
                image1.SetActive(false);
                image2.SetActive(true);
                StartCoroutine(SetText("I can hear her in my head.", -134, 146, 305, 50));
            }
            else if (page == 3 && !isWriting)
            {
                StartCoroutine(SetText("She whispers sweetly.", -134, 146, 251, 50));
            }
            else if (page == 4 && !isWriting)
            {
                image2.SetActive(false);
                image3.SetActive(true);
                StartCoroutine(SetText("Ken! You shouldn’t do that!\nIt’s... against regulation!", 136, -171, 315, 70));
            }
            else if (page == 5 && !isWriting)
            {
                StartCoroutine(SetText("Hah ha! Oh, Mai! You’re such\na stickler for the rules!", -142, 184, 325, 70));
            }
            else if (page == 6 && !isWriting)
            {
                StartCoroutine(SetText("Live a little!", -187, 184, 177, 50));
            }
            else if (page == 7 && !isWriting)
            {
                StartCoroutine(SetText("Can’t you hear our\nmother calling?", -172, 184, 220, 65));
            }
            else if (page == 8 && !isWriting)
            {
                StartCoroutine(delayBeep());
                StartCoroutine(SetText("Beep beep be", -199, 184, 195, 50));
                StartCoroutine(delayDay1());
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
    private IEnumerator delayBeep()
    {
        yield return new WaitForSeconds(delayBeepAudio);
        canvas.GetComponent<AudioSource>().Stop();
        canvas.GetComponent<AudioSource>().clip = beepAudio;
        canvas.GetComponent<AudioSource>().Play();
    }

    private IEnumerator delayDay1()
    {
        yield return new WaitForSeconds(delayAudio);
        textBox.SetActive(false);
        image3.SetActive(false);
        image4.SetActive(true);
        StartCoroutine(GoToDay1());
    }
    private IEnumerator GoToDay1()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Day2");
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
}
