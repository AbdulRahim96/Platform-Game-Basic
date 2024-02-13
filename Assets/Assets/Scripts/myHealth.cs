using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class myHealth : MonoBehaviour
{
    public int currentDamage = 6, max = 6;
    public Slider slider;
    public bool isPlayer = false;
    public GameObject smoke;
    public GameObject blood;
    public void damage()
    {
        currentDamage--;
        slider.value--;
        if(isPlayer == true)
        {
            GameObject obj = Instantiate(blood, transform.position, transform.rotation);
            Destroy(obj, 1);
        }
        if(currentDamage < 2)
        {
            gameOver();
        }
    }

    public void gameOver()
    {
        StartCoroutine(restart());
        if(isPlayer == false)
        {
            GameObject obj = Instantiate(smoke, transform.position, transform.rotation);
            Destroy(obj, 3);
        }
        Destroy(gameObject);
        
    }

    public IEnumerator restart()
    {
        yield return new WaitForSeconds(2);
        if (isPlayer == true)
            SceneManager.LoadScene(1);
        
        
    }
        
}
