using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSentivity = 100f;

    public Transform playerBody;

    private float xRotation = 0f;

    public float camMin = -85f;
    public float camMax = 85f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSentivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSentivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, camMin, camMax);

        
        if (!GlobalVariables.vRGamePaused)
        {
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
            
    }
}
