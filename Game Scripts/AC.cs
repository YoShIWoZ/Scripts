using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC : MonoBehaviour
{
    public GameObject acOn;
    public GameObject dispenserFood;
    public GameObject dispenserFoodClean;

    private bool hasRun = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.o2Fixed && !hasRun)
        {
            hasRun = true;
            acOn.SetActive(true);
            StartCoroutine(StartAC(8f));
        }
    }

    private IEnumerator StartAC(float clearTime)
    {
        yield return new WaitForSeconds(0.001f);
        GlobalVariables.canMove = false;
        GlobalVariables.canInteract = false;
        yield return new WaitForSeconds(clearTime);
        acOn.SetActive(false);
        GlobalVariables.canMove = true;
        if (GlobalVariables.doorActivated && !GlobalVariables.hasEaten)
        {
            dispenserFood.SetActive(false);
            dispenserFoodClean.SetActive(true);
        }
        GlobalVariables.canInteract = true;
    }



}
