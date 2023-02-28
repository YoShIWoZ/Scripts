using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    private bool isPaused;
    public GameObject PausePanel;
    public string mainMenu;

    [Header("Buttons")]
    public GameObject defaultButton;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            ChangePause();
        }
    }

    public void ChangePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            PausePanel.SetActive(true);
            //Time.timeScale = 0f;
            StartCoroutine(SelectButton());
            GlobalVariables.vRGamePaused = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            PausePanel.SetActive(false);
            //Time.timeScale = 1f;
            GlobalVariables.vRGamePaused = false;
        }
    }
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        //Time.timeScale = 1f;

    }

    private IEnumerator SelectButton()
    {
        yield return new WaitForSeconds(.0001f);
        EventSystem.current.SetSelectedGameObject(defaultButton);
    }
}
