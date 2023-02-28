using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Points : MonoBehaviour
{
    public GameObject pointsText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        pointsText.GetComponent<TextMeshProUGUI>().text = GlobalVariables.points.ToString();
    }
}
