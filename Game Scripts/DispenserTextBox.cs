using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DispenserTextBox : MonoBehaviour
{
    private Animator animator;
    public GameObject dispenserEmpty;
    public GameObject dispenserFood;
    public GameObject dispenserFoodClean;
    public GameObject dispenserFoodScrewdriver;
    public GameObject dispenserScrewdriver;

    public bool playerInRange;
    public bool doorVisited;
    private bool dispenserChanged;
    private bool dispenserChangedSD;
    public GameObject diary;

    [Header("Images")]
    public GameObject imageHolder;
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public GameObject buttons;
    public GameObject buttonsScredriver;
    public GameObject defaultButton;
    public GameObject defaultButtonScrewdriver;


    [Header("Dialog Textbox Settings")]
    [Tooltip("Textbox Game Object")]
    public GameObject textBox;
    public Text textArea;
    [TextArea(3, 10)]
    public string text;
    public float x;
    public float y;
    public float width;
    public float height;

    void Start()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        doorVisited = GlobalVariables.doorActivated;
        if (GlobalVariables.powerOn)
        {
            if (!doorVisited)
            {
                if (!GlobalVariables.screwdriverAdded)
                {
                    dispenserEmpty.SetActive(true);
                    dispenserFood.SetActive(false);
                }
                else
                {
                    StartCoroutine(changeDispenserEmpty());
                }
                if (GlobalVariables.canInteract && Input.GetButtonDown("Submit") && playerInRange && !GlobalVariables.screwdriverAdded)
                {
                    if (imageHolder.activeInHierarchy)
                    {
                        CloseTextBox();
                        imageHolder.SetActive(false);
                    }
                    else
                    {
                        Empty();
                    }
                }
                else if (GlobalVariables.canInteract && Input.GetButtonDown("Submit") && playerInRange && GlobalVariables.screwdriverAdded && !GlobalVariables.screwdriverTaken)
                {
                    if (textBox.activeInHierarchy)
                    {
                        CloseTextBox();
                    }
                    else
                    {
                        textArea.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
                        SetTextBox("Pick up screwdriver?", -170, 170, 215, 60);
                        buttonsScredriver.SetActive(true);
                        GlobalVariables.canInteract = false;
                        StartCoroutine(SelectButtonScrewdriver());
                    }
                }
            }
            else
            {
                if (!textBox.activeInHierarchy)
                {
                    if (GlobalVariables.canInteract && Input.GetButtonDown("Submit") && playerInRange && animator.GetInteger("suitNum") == 1 && !GlobalVariables.screwdriverAdded)
                    {
                        SetTextBox("I should get out of\nthis suit first.", -180, 25, 210, 60);
                        return;
                    }
                }
                else
                {
                    if (GlobalVariables.canInteract && Input.GetButtonDown("Submit") && playerInRange && (animator.GetInteger("suitNum") == 1) && !GlobalVariables.screwdriverAdded)
                    {
                        CloseTextBox();
                        return;
                    }
                }
                StartCoroutine(changeDispenser());
                if (GlobalVariables.canInteract && Input.GetButtonDown("Submit") && playerInRange && (animator.GetInteger("suitNum") == 0 || animator.GetInteger("suitNum") == 2) && !GlobalVariables.screwdriverAdded)
                {
                    if (imageHolder.activeInHierarchy)
                    {
                        CloseTextBox();
                        imageHolder.SetActive(false);
                    }
                    else
                    {
                        if (GlobalVariables.hasEaten)
                            Empty();
                        else
                            HasFood();
                    }
                }
                else if (GlobalVariables.canInteract && Input.GetButtonDown("Submit") && playerInRange && GlobalVariables.screwdriverAdded)
                    if (textBox.activeInHierarchy)
                    {
                        CloseTextBox();
                    }
                    else if (!textBox.activeInHierarchy && !GlobalVariables.screwdriverTaken)
                    {
                        textArea.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
                        SetTextBox("Pick up screwdriver?", -170, 170, 215, 60);
                        buttonsScredriver.SetActive(true);
                        GlobalVariables.canInteract = false;
                        StartCoroutine(SelectButtonScrewdriver());
                    }
                if (GlobalVariables.dayNumber == 3)
                {
                    if (GlobalVariables.screwdriverAdded)
                        StartCoroutine(changeDispenser());
                }
            }
        }
        else
        {
            if (Input.GetButtonDown("Submit") && playerInRange)
            {
                textArea.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
                if (textBox.activeInHierarchy)
                {
                    CloseTextBox();
                }
                else
                {
                    SetTextBox("Seems empty.Not really\nthe time to eat anyway.", -40, 90, 250, 65);
                }
            }
        }
    }

    private IEnumerator changeDispenser()
    {
        if (!dispenserChanged)
        {
            yield return new WaitForSeconds(0.45f);
            dispenserEmpty.SetActive(false);
            if (!GlobalVariables.hasEaten)
            {
                if (!GlobalVariables.screwdriverAdded)
                    dispenserFood.SetActive(true);
                else
                    dispenserFoodScrewdriver.SetActive(true);
            }
            else
            {
                if (!GlobalVariables.screwdriverAdded)
                    dispenserEmpty.SetActive(true);
                else
                    dispenserScrewdriver.SetActive(true);
            }

            dispenserChanged = true;
        }
        if (GlobalVariables.screwdriverAdded && !dispenserChangedSD)
        {
            dispenserChanged = false;
            dispenserChangedSD = true;
        }
    }

    private IEnumerator changeDispenserEmpty()
    {
        if (!dispenserChanged)
        {
            yield return new WaitForSeconds(0.45f);
            dispenserEmpty.SetActive(false);
            dispenserScrewdriver.SetActive(true);
            dispenserChanged = true;
        }
    }

    private IEnumerator Interactable()
    {
        yield return new WaitForSeconds(0.0001f);
        GlobalVariables.canInteract = true;
    }

    public void TakeScrewdriver()
    {
        GlobalVariables.screwdriverTaken = true;
        GlobalVariables.screwdriverAdded = false;
        if (!doorVisited || GlobalVariables.hasEaten)
            dispenserEmpty.SetActive(true);
        else
            dispenserFood.SetActive(true);
        dispenserFoodScrewdriver.SetActive(false);
        dispenserScrewdriver.SetActive(false);
        buttonsScredriver.SetActive(false);
        CloseTextBox();
        StartCoroutine(Interactable());
    }

    public void DontTakeScredriver()
    {
        CloseTextBox();
        buttonsScredriver.SetActive(false);
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
        buttons.SetActive(false);
        GlobalVariables.canMove = true;
    }

    public void EatFood()
    {
        dispenserEmpty.SetActive(true);
        dispenserFood.SetActive(false);
        if (GlobalVariables.dayNumber == 3)
            dispenserFoodClean.SetActive(false);
        if (animator.GetInteger("suitNum") == 0)
            animator.SetInteger("suitFoodNum", 1);
        else if (animator.GetInteger("suitNum") == 2)
            animator.SetInteger("suitFoodNum", 2);
        GlobalVariables.hasEaten = true;
        diary.GetComponent<Diary>().addEntry(Diary.EntryType.FoodDispenser);
    }

    public void Empty()
    {
        imageHolder.GetComponent<Image>().sprite = image1;
        imageHolder.SetActive(true);
        textArea.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        SetTextBox("Empty.", -14, 60, 100, 50);
    }
    public void HasFood()
    {
        imageHolder.GetComponent<Image>().sprite = image2;
        imageHolder.SetActive(true);
        textArea.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
        if (GlobalVariables.dayNumber == 1)
            SetTextBox("Mmm.\nSynthetic chicken.\nCarrot goop.\nGreen peas.\nImitation Jello.\nAnd a vitamin booster\npill.\n\nShould I eat this?", -14, 95, 230, 180);
        else if (GlobalVariables.dayNumber == 2)
            SetTextBox("Artificial pork.\nBaked beans.\nCardboard toast.\nI-can’t-believe-it’s-\nnot-chocolate-cake\nand vitamin booster.\nYum.\n\nShould I eat this?", -14, 95, 230, 180);
        else if (GlobalVariables.dayNumber == 3)
            if (!GlobalVariables.o2Fixed)
                SetTextBox("\n\n\nLooks ... \n\nvegetated.\n\n\nShould I eat this?", -14, 95, 230, 180);
            else
            {
                imageHolder.GetComponent<Image>().sprite = image3;
                SetTextBox("Pasta with sorta-meat balls.\n\"Fun guy\"-mushrooms.\nBread loaf.\nVanilla sundae with cherry on top.\nAaaaand, of course, the old\nvitamin booster pill!\nIt will do for a last supper.\n\nShould I eat this?", -14, 95, 345, 180);
            }
        buttons.SetActive(true);
        StartCoroutine(SelectButton());
    }

    private IEnumerator SelectButton()
    {
        yield return new WaitForSeconds(.0001f);
        EventSystem.current.SetSelectedGameObject(defaultButton);
    }
    private IEnumerator SelectButtonScrewdriver()
    {
        yield return new WaitForSeconds(.0001f);
        EventSystem.current.SetSelectedGameObject(defaultButtonScrewdriver);
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
