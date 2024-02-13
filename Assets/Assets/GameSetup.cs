using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSetup : MonoBehaviour
{
    public static int lives = 3;
    public static float gameTime = 0;
    public Text livesText, time;
    public GameObject gameOvermenu, blackScreen, completeScreen;
    public static bool hasStarted;
    private void Awake()
    {
        GameObject obj = Instantiate(blackScreen);
        livesText.text = "x" + lives.ToString("0");
        obj.GetComponentInChildren<Text>().text = livesText.text;
        Destroy(obj, 2);
        StartCoroutine(timer());
        
    }
    public void restartLevel()
    {
        StartCoroutine(reset());
    }

    private void FixedUpdate()
    {
        if (hasStarted)
            gameTime += Time.deltaTime;
        time.text = timeFormat(gameTime);
    }

    public static string timeFormat(float time)
    {
        float s = time % 60;
        int min = (int)(time / 60);
        return min.ToString("00") + ":" + s.ToString("00.00");
    }

    IEnumerator reset()
    {
        
        yield return new WaitForSeconds(3);
        if (lives > 0)
        {
            lives--;
            int current = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(current);
        }
        else
            Instantiate(gameOvermenu);
    }

    IEnumerator timer()
    {
        hasStarted = false;
        yield return new WaitForSeconds(2);
        hasStarted = true;
    }

    public static void levelComplete()
    {
        Instantiate(FindObjectOfType<GameSetup>().completeScreen);
    }
}
