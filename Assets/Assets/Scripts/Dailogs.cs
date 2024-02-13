using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dailogs : MonoBehaviour
{
    public GameObject[] dailogs;
    public int index = 0;

    public void nextDailog()
    {
        Destroy(dailogs[index]);
        index++;
        if (index < dailogs.Length)
        {
            dailogs[index].SetActive(true);
        }
        else
            startGame();
    }

    public void startGame()
    {
        Destroy(gameObject);
    }
}
