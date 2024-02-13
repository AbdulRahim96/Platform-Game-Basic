using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    public GameObject FadeScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Win();
        }
    }

    public void Win()
    {
        Instantiate(FadeScreen);
        StartCoroutine(delay());
    }

    private IEnumerator delay()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(next);
    }
}
