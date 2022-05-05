using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text highscoreText;
    public Text achievementText;

    void Start(){
        int highscore = PlayerPrefs.GetInt("highscore");
        int progress = PlayerPrefs.GetInt("achievement-progress-barrels");

        highscoreText.text = highscore.ToString();
        achievementText.text = progress + "/10";

    }

    public void PlayGame(){
        Manager.instance.NewGame();
    }

    public void QuitGame(){
        Application.Quit();
    }
}
