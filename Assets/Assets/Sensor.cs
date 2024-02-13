using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sensor : MonoBehaviour
{
    public UnityEvent enter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            enter.Invoke();
    }
}
