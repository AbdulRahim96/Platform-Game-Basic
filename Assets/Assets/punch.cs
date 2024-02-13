using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class punch : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void punching()
    {
        StartCoroutine(timer());
    }


    public IEnumerator timer()
    {
        anim.SetBool("punch", true);
        yield return new WaitForSeconds(0.3f);
        GetComponent<BoxCollider2D>().enabled = true;
        anim.SetBool("punch", false);
        yield return new WaitForSeconds(0.3f);
        GetComponent<BoxCollider2D>().enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "pillow")
        {
            collision.gameObject.GetComponent<enemyAttack>().effect();
            Destroy(collision.gameObject);
        }
        if (collision.tag == "bed")
        {
            collision.gameObject.GetComponent<myHealth>().damage();
        }
    }
}
