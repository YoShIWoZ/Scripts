using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeManager : MonoBehaviour
{
    [Header("Buttons")]
    public GameObject ductTape;
    public GameObject ductTapeIcon;
    public GameObject screwdriverIcon;
    public GameObject secretTapeIcon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.ductTapeTaken && !ductTapeIcon.activeInHierarchy)
        {
            ductTape.SetActive(false);
            ductTapeIcon.SetActive(true);
        }
        if (GlobalVariables.screwdriverTaken && !screwdriverIcon.activeInHierarchy)
        {
            screwdriverIcon.SetActive(true);
        }
        if (GlobalVariables.secretTapeTaken && !secretTapeIcon.activeInHierarchy)
        {
            secretTapeIcon.SetActive(true);
        }
    }
}
