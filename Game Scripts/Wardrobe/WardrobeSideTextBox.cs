using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WardrobeSideTextBox : MonoBehaviour
{
    public bool playerInRange;
    public GameObject leftActivateField;


    [Header("Images")]
    public GameObject imageHolder;
    public Sprite image;
    public Sprite imageBlackout;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && playerInRange)
        {
            if (GlobalVariables.powerOn)
            {
                if (!imageHolder.activeInHierarchy)
                {
                    GlobalVariables.canMove = false;
                    WardrobeSideActivate();
                }
                else
                {
                    imageHolder.SetActive(false);
                    GlobalVariables.canMove = true;
                }
            }
            else
            {
                return;
            }

        }
    }
    private void WardrobeSideActivate()
    {
        if (GlobalVariables.powerOn)
            imageHolder.GetComponent<Image>().sprite = image;
        else
            imageHolder.GetComponent<Image>().sprite = imageBlackout;
        imageHolder.SetActive(true);
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractField") && collision.isTrigger && leftActivateField.activeInHierarchy)
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
