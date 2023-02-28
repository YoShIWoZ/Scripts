using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stool : MonoBehaviour
{
    public GameObject player;
    private Vector2 playerLocOld;
    public GameObject stoolEat;
    public GameObject stoolEatPS;
    public bool playerInRange;
    private Animator animator;

    [Header("Dialog Textbox Settings")]
    [Tooltip("Textbox Game Object")]
    public GameObject textBox;
    public Text textArea;
    private int page = 1;

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
            if (!stoolEat.activeInHierarchy && animator.GetInteger("suitFoodNum") == 1)
            {
                stoolEat.SetActive(true);
                GlobalVariables.canMove = false;
                playerLocOld = player.GetComponent<Transform>().position;
                player.GetComponent<Transform>().position = new Vector2(9999f, 999f);
                StartCoroutine(Eating());
            }
            else if (!stoolEatPS.activeInHierarchy && animator.GetInteger("suitFoodNum") == 2)
            {
                stoolEatPS.SetActive(true);
                GlobalVariables.canMove = false;
                playerLocOld = player.GetComponent<Transform>().position;
                player.GetComponent<Transform>().position = new Vector2(9999f, 999f);
                StartCoroutine(EatingPS());
            }
            if (GlobalVariables.dayNumber == 3)
            {
                if (page == 2)
                {
                    SetTextBox("Good weird?", -77, 166, 132.5f, 45);
                    page += 1;
                }
                else if (page == 3)
                {
                    SetTextBox("Maybe.", -74, 166, 85f, 45);
                    page += 1;
                }
                else if (page == 4)
                {
                    CloseTextBox();
                }
            }
        }
    }

    private IEnumerator Eating()
    {
        yield return new WaitForSeconds(4f);
        stoolEat.SetActive(false);
        GlobalVariables.canMove = true;
        player.GetComponent<Transform>().position = playerLocOld;
        animator.SetInteger("suitFoodNum", 0);
        GlobalVariables.doorActivated = true;
        if (GlobalVariables.dayNumber == 3)
        {
            if (!GlobalVariables.o2Fixed)
            {
                GlobalVariables.canMove = false;
                yield return new WaitForSeconds(1f);
                textArea.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
                if (page == 1)
                {
                    SetTextBox("That tasted weird.", -74, 166, 196.5f, 45);
                    page += 1;
                }
            }
        }
    }
    private IEnumerator EatingPS()
    {
        yield return new WaitForSeconds(4f);
        stoolEatPS.SetActive(false);
        GlobalVariables.canMove = true;
        player.GetComponent<Transform>().position = playerLocOld;
        animator.SetInteger("suitFoodNum", 0);
        GlobalVariables.doorActivated = true;

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
