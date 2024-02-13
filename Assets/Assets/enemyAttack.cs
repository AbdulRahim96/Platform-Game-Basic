using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttack : MonoBehaviour
{
    public float distance = 1, speed = 2;
    Transform player;
    public GameObject canvas;
    public GameObject smoke;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float dis = Vector3.Distance(transform.position, player.position);
        if(dis < distance)
        {
            transform.position = Vector3.Lerp(transform.position, player.position, speed);
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<myHealth>().damage();
            Destroy(gameObject);
        }
    }

    public void effect()
    {
        
        GameObject obj =  Instantiate(smoke, transform.position, transform.rotation);
        Destroy(obj, 3);
    }
}
