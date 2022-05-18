using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LoadScenes : MonoBehaviour
{
    public GameObject ControlScreen;

    public void Update()
    {     
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void StartGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowControls()
    {
        ControlScreen.SetActive(true);
    }
    public void HideControls()
    {
        ControlScreen.SetActive(false);
    }
}
