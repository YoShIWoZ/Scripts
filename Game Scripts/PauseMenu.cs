using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    private bool isPaused;
    public GameObject PausePanel;
    public string mainMenu;
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
            PausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            PausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;

    }
}
