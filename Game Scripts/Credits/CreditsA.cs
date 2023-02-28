using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsA : MonoBehaviour
{
    public GameObject image1;
    public GameObject image2;
    public GameObject image3;

    [Header("Dialog Textbox Settings")]
    [Tooltip("Textbox Game Object")]
    public GameObject textBox;
    public Text textArea;
    public float delay = 0.1f;
    private bool isWriting;
    private int page = 1;
    private string currentText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetText("Mother,\nI am coming home.", 0, 0, 190, 60));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (textBox.activeInHierarchy && !isWriting)
            {
                textBox.SetActive(false);
                image1.SetActive(false);
                image2.SetActive(true);
                image3.SetActive(true);
            }
        }

    }

    private IEnumerator SetText(string text, float x, float y, float width, float height)
    {
        textBox.SetActive(true);
        textBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
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
