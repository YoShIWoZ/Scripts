using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WakeUp : MonoBehaviour
{
    public GameObject player;
    private Vector2 playerLocOld;
    public GameObject bedWake;
    private bool hasRun = false;

    [Header("Dialog Textbox Settings")]
    [Tooltip("Textbox Game Object")]
    public GameObject textBox;
    public Text textArea;
    private int page = 1;

    // Start is called before the first frame update
    void Start()
    {
        GlobalVariables.canMove = false;
        playerLocOld = player.GetComponent<Transform>().position;
        if (GlobalVariables.dayNumber == 2)
        {
            if (!GlobalVariables.usedGenerator)
            {
                player.GetComponent<Transform>().position = new Vector2(999f, 999f);
                bedWake.SetActive(true);
            }
            else
            {
                ExitHall();
            }

        }
        else
        {
            player.GetComponent<Transform>().position = new Vector2(999f, 999f);
            bedWake.SetActive(true);
        }

    }

    private void WakeUpMai()
    {
        hasRun = true;
        bedWake.SetActive(false);
        GlobalVariables.canMove = true;
        player.GetComponent<Transform>().position = playerLocOld;
    }
    private void ExitHall()
    {
        GlobalVariables.canMove = true;
        player.GetComponent<Transform>().position = GlobalVariables.savedPosition;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && !hasRun)
        {
            if (GlobalVariables.dayNumber != 3)
            {
                if (!GlobalVariables.usedGenerator)
                    WakeUpMai();
                else
                    ExitHall();
            }
            else
            {
                if (page == 1)
                    SetTextBox("Weird dream.", 160, 190, 145, 45);
                else if (page == 2)
                {
                    CloseTextBox();
                    page += 1;
                }
                else if (page == 3)
                    WakeUpMai();
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
        page = page + 1;
    }

    private void CloseTextBox()
    {
        textBox.SetActive(false);
        GlobalVariables.canMove = true;
    }
}
