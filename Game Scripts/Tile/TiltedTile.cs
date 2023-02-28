using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TiltedTile : MonoBehaviour
{
    public GameObject player;
    public Vector3 playerLocOld;

    private Animator animator;
    public GameObject trip;

    private bool playerInRange;
    public GameObject tiltedTile;
    public GameObject openTile;
    private Vector3 mO;

    public Collider2D TripActivator;
    public Collider2D InteractActivator;

    public bool tileActivated;

    [Header("Buttons")]
    public GameObject buttons;
    public GameObject defaultButton;

    [Header("Dialog Textbox Settings")]
    [Tooltip("Textbox Game Object")]
    public GameObject textBox;
    public Text textArea;

    private int page = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Submit") && )

        if (GlobalVariables.powerReturned)
        {
            if (GlobalVariables.hasTripped)
            {
                TripActivator.enabled = false;
                InteractActivator.enabled = true;
            }
        }

        mO.x = Input.GetAxisRaw("Horizontal");;
        if (GlobalVariables.powerOn)
        {
            if (GlobalVariables.hasTripped)
            {
                if (tileActivated)
                {
                    openTile.SetActive(true);
                    tiltedTile.SetActive(false);
                }
            }   
        }
        if (GlobalVariables.hasTripped && !tileActivated)
        {
            tiltedTile.SetActive(true);
        }
    }

    private IEnumerator SelectButton()
    {
        yield return new WaitForSeconds(.0001f);
        EventSystem.current.SetSelectedGameObject(defaultButton);
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

    public void OpenTile()
    {
        tileActivated = true;
        page += 1;
    }

    private IEnumerator Tripping()
    {
        animator.SetBool("tripping", true);
        yield return new WaitForSeconds(0.1f);
        GlobalVariables.canMove = false;
        playerLocOld = new Vector3(0.1f, 0, 0);
        player.GetComponent<Transform>().position += playerLocOld;
        yield return new WaitForSeconds(0.03f);
        player.GetComponent<Transform>().position += playerLocOld;
        yield return new WaitForSeconds(0.03f);
        player.GetComponent<Transform>().position += playerLocOld;
        yield return new WaitForSeconds(0.03f);
        player.GetComponent<Transform>().position += playerLocOld;
        yield return new WaitForSeconds(0.03f);
        player.GetComponent<Transform>().position += playerLocOld;
        yield return new WaitForSeconds(0.03f);
        player.GetComponent<Transform>().position += playerLocOld;
        yield return new WaitForSeconds(0.03f);
        player.GetComponent<Transform>().position += playerLocOld;
        yield return new WaitForSeconds(0.03f);
        player.GetComponent<Transform>().position += playerLocOld;
        yield return new WaitForSeconds(0.03f);
        player.GetComponent<Transform>().position += playerLocOld;
        yield return new WaitForSeconds(0.03f);
        player.GetComponent<Transform>().position += playerLocOld;
        yield return new WaitForSeconds(0.03f);
        player.GetComponent<Transform>().position += playerLocOld;
        yield return new WaitForSeconds(0.03f);
        player.GetComponent<Transform>().position += playerLocOld;
        yield return new WaitForSeconds(0.03f);
        player.GetComponent<Transform>().position += playerLocOld;
        yield return new WaitForSeconds(0.03f);
        player.GetComponent<Transform>().position += playerLocOld;
        yield return new WaitForSeconds(0.03f);
        player.GetComponent<Transform>().position += playerLocOld;
        yield return new WaitForSeconds(0.03f);
        player.GetComponent<Transform>().position += playerLocOld;
        yield return new WaitForSeconds(0.03f);
        player.GetComponent<Transform>().position += playerLocOld;
        yield return new WaitForSeconds(0.03f);
        player.GetComponent<Transform>().position += playerLocOld;
        yield return new WaitForSeconds(0.03f);
        player.GetComponent<Transform>().position += playerLocOld;
        yield return new WaitForSeconds(0.03f);
        player.GetComponent<Transform>().position += playerLocOld;
        yield return new WaitForSeconds(0.03f);
        player.GetComponent<Transform>().position += playerLocOld;
        yield return new WaitForSeconds(1.2f);
        animator.SetBool("tripping", false);
        textArea.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
        SetTextBox("Ouch!!", 0, 0, 90, 45);
        yield return new WaitForSeconds(1.8f);
        CloseTextBox();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger && mO.x == 1)
        {
            if (!GlobalVariables.powerOn)
            {
                GlobalVariables.hasTripped = true;
                
                StartCoroutine(Tripping());
            }
        }
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
