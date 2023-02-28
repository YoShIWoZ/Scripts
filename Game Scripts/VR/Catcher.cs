using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour
{
    public GameObject enemy;

    public bool netInRange = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    if (hit.gameObject.CompareTag("InRange"))
    //    {
    //        Debug.Log("Net In Range!");
    //        netInRange = true;
    //    }
    //    else
    //    {
    //        if (!hit.gameObject.CompareTag("Trap"))
    //        {
    //            Debug.Log("Net Not In Range!");
    //            netInRange = false;
    //        }
    //    }
    //}
}
