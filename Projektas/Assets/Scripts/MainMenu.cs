using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    PlayerController playerController;
    public Text hiScoreText;
    int hiScoreDay;
    int hiScoreHour;

    // Use this for initialization
    void Start () {
        if (PlayerPrefs.HasKey("HighScoreDay"))
            hiScoreDay = PlayerPrefs.GetInt("HighScoreDay");
        if (PlayerPrefs.HasKey("HighScoreHour"))
            hiScoreHour = PlayerPrefs.GetInt("HighScoreHour");
    }
	
	public void Count (int day, int hour) {
		if ((hiScoreDay * 24 + hiScoreHour) < (day * 24 + hour))
        {
            hiScoreDay = day;
            PlayerPrefs.SetInt("HighScoreDay", hiScoreDay);
            hiScoreHour = hour;
            PlayerPrefs.SetInt("HighScoreHour", hiScoreHour);
        }
        hiScoreText.text = hiScoreDay + " days and " + hiScoreHour + " hours.";
	}

    public void StartGame()
    {
        //Application.LoadLevel(1);
        SceneManager.LoadSceneAsync("Martynas");
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
