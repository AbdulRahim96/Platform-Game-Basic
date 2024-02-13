using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerHit : MonoBehaviour
{
    public GameObject door;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<myHealth>().damage();
        }
    }
    private void OnDestroy()
    {
        door.SetActive(true);
    }
}
