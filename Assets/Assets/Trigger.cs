using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public GameObject obstacle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            obstacle.AddComponent<Rigidbody2D>();
            Destroy(obstacle, 2);
            Destroy(gameObject, 2);
        }
    }
}
