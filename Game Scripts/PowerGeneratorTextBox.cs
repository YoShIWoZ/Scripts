using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PowerGeneratorTextBox : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Animator animator;
    private Transform transform;
    public bool playerInRange;
    private int page = 0;

    [Header("Dialog Textbox Settings")]
    [Tooltip("Textbox Game Object")]
    public GameObject textBox;
    public Text textArea;

    public List<DialogueBox> dialogueBoxes = new List<DialogueBox>();

    void Start()
    {
        myRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        animator = GameObject.Find("Player").GetComponent<Animator>();
        transform = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && playerInRange)
        {
            if (GlobalVariables.powerOn)
            {
                textArea.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
                if (page == dialogueBoxes.Count)
                {
                    CloseTextBox();
                    page = 0;
                    StartCoroutine(NextScene());
                    return;
                }
                SetTextBox(dialogueBoxes[page].text, dialogueBoxes[page].x, dialogueBoxes[page].y, dialogueBoxes[page].width, dialogueBoxes[page].height);
                page += 1;
            }
            else
            {
                if (page == dialogueBoxes.Count)
                {
                    CloseTextBox();
                    page = 0;
                    return;
                }
                SetTextBox(dialogueBoxes[dialogueBoxes.Count-1].text, dialogueBoxes[dialogueBoxes.Count-1].x, dialogueBoxes[dialogueBoxes.Count-1].y, dialogueBoxes[dialogueBoxes.Count-1].width, dialogueBoxes[dialogueBoxes.Count-1].height);
                page = dialogueBoxes.Count;
            }
        }
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

    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(0.1f);
        GlobalVariables.powerReturned = true;
        GlobalVariables.usedGenerator = true;
        GlobalVariables.savedPosition = transform.position;
        SceneManager.LoadScene("Day2");
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
