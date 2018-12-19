using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseScript : MonoBehaviour {

    GameObject PauseMenu;

    public bool paused;

	// Use this for initialization
	void Start ()
    {
        paused = false;

        PauseMenu = GameObject.Find("PauseMenu");
        PauseMenu.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            if (paused)
            {
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                PauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void Pause()
    {
        paused = !paused;

        if (paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    
    public void Resume()
    {
        paused = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }    

    public void MainMenu()
    {
        //Application.LoadLevel(0);
        SceneManager.LoadScene("MainMenu");
    }

    public void Options()
    {
        throw new System.Exception("not implemented yet");
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
