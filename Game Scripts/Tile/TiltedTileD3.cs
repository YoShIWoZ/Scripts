using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TiltedTileD3 : MonoBehaviour
{
    private bool playerInRange;

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
            if (!textBox.activeInHierarchy && GlobalVariables.screwdriverTaken && !GlobalVariables.secretTapeTaken)
            {
                textArea.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
                SetTextBox("Use screwdriver to open?", 46, 60, 255, 60);
                buttons.SetActive(true);
                StartCoroutine(SelectButton());
            }
        }
    }
    private IEnumerator SelectButton()
    {
        yield return new WaitForSeconds(.0001f);
        EventSystem.current.SetSelectedGameObject(defaultButton);
    }

    public void OpenTile()
    {
        openTile.SetActive(true);
        tiltedTile.SetActive(false);
        buttons.SetActive(false);
        SetTextBox("A note. And a recording.", 46, 60, 255, 45);
    }
    public void No()
    {
        StartCoroutine(CloseAll());
    }

    private IEnumerator CloseAll()
    {
        yield return new WaitForSeconds(.0001f);
        CloseTextBox();
        buttons.SetActive(false);
    }

    private void CloseTextBox()
    {
        textBox.SetActive(false);
        GlobalVariables.canMove = true;
    }

    private void SetTextBox(string text, float x, float y, float width, float height)
    {
        GlobalVariables.canMove = false;
        textBox.SetActive(true);
        textBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        textArea.text = text;
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
