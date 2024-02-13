using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    
    private bool hasFallen = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!hasFallen)
        {
            if(collision.gameObject.tag == "Player")
            {
                FindObjectOfType<PlayerController>().dying();
            }
            hasFallen = true;
            GetComponent<AudioSource>().Play();
            Destroy(gameObject, 1);
        }
    }
}
