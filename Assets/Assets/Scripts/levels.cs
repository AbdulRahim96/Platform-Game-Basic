using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levels : MonoBehaviour
{
    public Text text;

    private void Start()
    {
        float currentBest = PlayerPrefs.GetFloat("highscore", 9999999999);
        if (currentBest >= 9999999999)
            text.text = "No Current best.";
        else
            text.text = "Current Best: " + GameSetup.timeFormat(currentBest);

        GameOVerScreen.isUsed = false;
    }
    public void changeLevel(int num)
    {
        SceneManager.LoadScene(num);
    }

    public void exit()
    {
        Application.Quit();
    }

}
