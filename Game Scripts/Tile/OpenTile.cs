using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenTile : MonoBehaviour
{
    private bool playerInRange;
    private int page = 0;

    public GameObject openTile;
    public GameObject tiltedTile;

    [Header("Buttons")]
    public GameObject buttons;
    public GameObject defaultButton;

    [Header("Dialog Textbox Settings")]
    [Tooltip("Textbox Game Object")]
    public GameObject textBox;
    public Text textArea;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && playerInRange)
        {
            if (textBox.activeInHierarchy && page == 0)
            {
                SetTextBox("The note reads:\n\n“From one guinea pig to another, let me congratulate you.\nI hid this treasure in my cage, once I had proof of certain things. Got to be careful.\nWalls have ears and ceilings have eyes. But this floor has voices.\nGood luck to you!\n\n-Seriously, the previous occupant.", -140, 70, 280, 260);
                page += 1;
            }
            else if (textBox.activeInHierarchy && page == 1)
            {
                SetTextBox("Take tape?", 46, 60, 150, 60);
                buttons.SetActive(true);
                StartCoroutine(SelectButton());
            }
            else if (textBox.activeInHierarchy && page == 2)
            {
                CloseTextBox();
                buttons.SetActive(false);
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
    private IEnumerator SelectButton()
    {
        yield return new WaitForSeconds(.0001f);
        EventSystem.current.SetSelectedGameObject(defaultButton);
    }

    public void TakeTape()
    {
        CloseTextBox();
        buttons.SetActive(false);
        GlobalVariables.secretTapeTaken = true;
        openTile.SetActive(false);
        tiltedTile.SetActive(true);
    }
    public void DontTakeTape()
    {
        CloseTextBox();
        buttons.SetActive(false);
        openTile.SetActive(false);
        tiltedTile.SetActive(true);
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
