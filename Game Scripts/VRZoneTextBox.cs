using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRZoneTextBox : MonoBehaviour
{
    public GameObject cam1On;
    public GameObject cam1Off;
    public GameObject cam2On;
    public GameObject cam2Off;
    public bool playerInRange;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        //{
        //    if (dialogBox.activeInHierarchy)
        //    {
        //        dialogBox.SetActive(false);
        //        myRigidbody.constraints = ~RigidbodyConstraints2D.FreezePosition;
        //        animator.enabled = true;
        //        doorOff.SetActive(true);
        //        doorOn.SetActive(false);
        //    }
        //    else
        //    {
        //        dialogBox.SetActive(true);
        //        dialogText.text = dialog;
        //        myRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
        //        animator.enabled = false;
        //        doorOn.SetActive(true);
        //        doorOff.SetActive(false);
        //    }
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            playerInRange = true;
            cam1On.SetActive(true);
            cam1Off.SetActive(false);
            cam2On.SetActive(true);
            cam2Off.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            playerInRange = false;
            cam1On.SetActive(false);
            cam1Off.SetActive(true);
            cam2On.SetActive(false);
            cam2Off.SetActive(true);
        }
    }

}
