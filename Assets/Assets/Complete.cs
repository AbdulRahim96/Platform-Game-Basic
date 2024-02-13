using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Complete : MonoBehaviour
{
    public Text timeText, str;
    // Start is called before the first frame update
    void Start()
    {
        timeText.text = GameSetup.timeFormat(GameSetup.gameTime);
        float currentBest = PlayerPrefs.GetFloat("highscore", 9999999999);
        if (GameSetup.gameTime < currentBest)
        {
            str.text = "New high score";
            str.color = Color.green;
            PlayerPrefs.SetFloat("highscore", GameSetup.gameTime);
        }
        else
        {
            str.text = "Current score - " + GameSetup.timeFormat(currentBest);
        }
    }

    public void quitLevel()
    {
        SceneManager.LoadScene(0);
    }
}
