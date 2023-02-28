using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public class DialogueBoxes
{


    public enum Position
    {
        TopLeft, Top, TopRight,
        Left, Center, Right,
        ButtomLeft, Buttom, ButtomRight
    }
    public enum Font
    {
        Alenea,
        Mai
    }
    public Position position;
    public Font font;
    public float offsetX;
    public float offsetY;
    public float width;
    public float height;
    [TextArea(3, 10)]
    public string text;
    public DialogueBoxes(Position position, Font font, float offsetX, float offsetY ,string text, float width, float height)
    {
        this.position = position;
        this.font = font;
        this.offsetX = offsetX;
        this.offsetY = offsetY;
        this.width = width;
        this.height = height;
        this.text = text;
    }
}

public class dialogBox : MonoBehaviour
{

    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public GameObject image4;
    public GameObject canvas;
    public AudioClip beepAudio;
    public float delayAudio;
    public bool playerInRange;
    private int page = 1;
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

    public List<DialogueBoxes> dialogueBoxes = new List<DialogueBoxes>();

    void Start()
    {
        image1.SetActive(true);
        //StartCoroutine(SetText("Welcome.", 0, 0, 95, 50));
        StartCoroutine(SetText(dialogueBoxes[0].position, dialogueBoxes[0].font, dialogueBoxes[0].offsetX, dialogueBoxes[0].offsetY, dialogueBoxes[0].text, dialogueBoxes[0].width, dialogueBoxes[0].height));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (page == 1 && !image2.activeInHierarchy && !isWriting)
            {
                image2.SetActive(true);
                textArea.GetComponent<Text>().font = fontAlenea;
                //StartCoroutine(SetText("We are well met,\nchild of a distant world.", 0, 0, 230, 60));
            }
            else if (page == 2 && !isWriting)
            {
                textArea.GetComponent<Text>().font = fontMai;
                if (ColorUtility.TryParseHtmlString("#D6D6D6", out color))
                { textArea.GetComponent<Text>().color = color; }
                //StartCoroutine(SetText("She calls to me...\nAlenea.", 0, -100, 230, 60));
            }
            else if (page == 3 && !isWriting)
            {
                //StartCoroutine(SetText("She tells me I am\nwelcome here.", 0, -100, 230, 60));
            }
            else if (page == 4 && !isWriting)
            {
                //StartCoroutine(SetText("Her forests...", 0, -100, 170, 50));
            }
            else if (page == 5 && !isWriting)
            {
                //StartCoroutine(SetText("Her air...", 0, -100, 170, 50));
            }
            else if (page == 6 && !isWriting)
            {
                //StartCoroutine(SetText("Her life...", 0, -100, 170, 50));
            }
            else if (page == 7 && !isWriting)
            {
                image1.SetActive(false);
                image2.SetActive(false);
                image3.SetActive(true);
                //StartCoroutine(SetText("She could be our new home.", 0, 150, 280, 50));
            }
            else if (page == 8 && !isWriting)
            {
                //StartCoroutine(SetText("Our new mother.", 0, 150, 190, 50));
            }
            else if (page == 9 && !isWriting)
            {
                //StartCoroutine(SetText("If only we...", 0, 150, 160, 50));
            }
            else if (page == 10 && !isWriting)
            {
                canvas.GetComponent<AudioSource>().Stop();
                canvas.GetComponent<AudioSource>().clip = beepAudio;
                canvas.GetComponent<AudioSource>().Play();
                StartCoroutine(delayDay1());
            }

        }
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
        SceneManager.LoadScene("Day1");
    }
    private IEnumerator SetText(DialogueBoxes.Position position, DialogueBoxes.Font font, float offsetX, float offsetY, string text, float width, float height)
    {
        textBox.SetActive(true);
        textBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(0 + offsetX, 0 + offsetY);
        textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        if (font == DialogueBoxes.Font.Alenea)
        {
            textArea.GetComponent<Text>().font = fontAlenea;
            if (ColorUtility.TryParseHtmlString("#E537C1", out color))
            { textArea.GetComponent<Text>().color = color; }
        }
        else if (font == DialogueBoxes.Font.Mai)
        {
            textArea.GetComponent<Text>().font = fontMai;
            if (ColorUtility.TryParseHtmlString("#D6D6D6", out color))
            { textArea.GetComponent<Text>().color = color; }
        }
        if (position == DialogueBoxes.Position.TopLeft)
        {
            textBox.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 1f);
            textBox.GetComponent<RectTransform>().anchorMax = new Vector2(0f, 1f);
            textBox.GetComponent<RectTransform>().pivot = new Vector2(0f, 1f);
        }
        else if (position == DialogueBoxes.Position.Top)
        {
            textBox.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1f);
            textBox.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1f);
            textBox.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1f);
        }
        else if (position == DialogueBoxes.Position.TopRight)
        {
            textBox.GetComponent<RectTransform>().anchorMin = new Vector2(1f, 1f);
            textBox.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1f);
            textBox.GetComponent<RectTransform>().pivot = new Vector2(1f + width/(width*(width/100)), 1f);
        }
        else if (position == DialogueBoxes.Position.Left)
        {
            textBox.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 0.5f);
            textBox.GetComponent<RectTransform>().anchorMax = new Vector2(0f, 0.5f);
            textBox.GetComponent<RectTransform>().pivot = new Vector2(-0.5f, 0.5f);
        }
        else if (position == DialogueBoxes.Position.Center)
        {
            textBox.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            textBox.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
            textBox.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        }
        else if (position == DialogueBoxes.Position.Right)
        {
            textBox.GetComponent<RectTransform>().anchorMin = new Vector2(1f, 0.5f);
            textBox.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 0.5f);
            textBox.GetComponent<RectTransform>().pivot = new Vector2(1f + width / (width * (width / 100)), 1f);
        }
        else if (position == DialogueBoxes.Position.ButtomLeft)
        {
            textBox.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 0f);
            textBox.GetComponent<RectTransform>().anchorMax = new Vector2(0f, 0f);
            textBox.GetComponent<RectTransform>().pivot = new Vector2(-0.5f, 0f);
        }
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
