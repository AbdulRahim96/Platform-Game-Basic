using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOVerScreen : MonoBehaviour
{
    public Slider slider;
    public Button continueButton;
    public Text timeText;

    public static bool isUsed;

    private float time = 3;
    // Start is called before the first frame update
    void Start()
    {
        timeText.text = GameSetup.timeFormat(GameSetup.gameTime);
        continueButton.interactable = !isUsed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time -= Time.deltaTime;
        slider.value = time;
        if (time <= 0)
            continueButton.interactable = false;

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public static void getLives()
    {
        GameSetup.lives = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isUsed = true;
    }

}
