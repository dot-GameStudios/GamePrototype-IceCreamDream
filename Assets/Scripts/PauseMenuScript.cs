using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    private KeyCode PauseButton = KeyCode.Return;
    
    public static bool Paused = false;
    public GameObject MenuUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(PauseButton))
        {
            if (Paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume() {
        MenuUI.SetActive(false);
        Time.timeScale = 1;
        Paused = false;
    }

    public void Pause() {
        MenuUI.SetActive(true);
        Time.timeScale = 0;
        Paused = true;
    }

    public void Quit() {
        Time.timeScale = 1;
    }

   
}
