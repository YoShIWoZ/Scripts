using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    private Animator animator;
    public float SwingTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            StartCoroutine(SwingNet());            
        }
    }

    private IEnumerator SwingNet()
    {
        animator.SetBool("UseNet", true);
        yield return new WaitForSeconds(SwingTime);
        animator.SetBool("UseNet", false);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (CompareTag("InRange") && collision.isTrigger)
    //    {
    //        netInRange = true;
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (CompareTag("InRange") && collision.isTrigger)
    //    {
    //        netInRange = false;
    //    }
    //}
}
