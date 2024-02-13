using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform player;
    public Animator animation;
    public GameObject hitParticle, hitPos;

    public float speed;
    public bool goRight;
    public float changingRouteTime = 5;

    private float time;
    public bool playerDetection = false;
    public bool fromBoss = false;
    //Attacking
    public AudioSource swordSound;
    public Collider2D collider;
    public TrailRenderer trail;
    public bool isDead = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        time = changingRouteTime;
    }

    private void Update()
    {
        if (fromBoss)
            playerDetection = true;
        if(player.gameObject.GetComponent<PlayerController>().controlEnabled == false)
        {
            animation.SetBool("victory", true);
            rb.bodyType = RigidbodyType2D.Static;
        }
        else if (!isDead)
        {
            if (playerDetection)
            {
                if (Vector2.Distance(transform.position, player.transform.position) < 3f)
                    attackPlayer();
                else
                    chasePlayer();
            }
            else
                NormalRoute();
        }
        else
            rb.bodyType = RigidbodyType2D.Static;
    }

    void NormalRoute()
    {
        time -= Time.deltaTime;
        if(time <= 0)
        {
            time = changingRouteTime;
            goRight = goRight == true ? false : true;
        }
        animation.SetBool("move", true);
        animation.SetBool("punch", false);
        if (goRight)
        {
            direction(-1f, 1);
        }
        else
        {
            direction(1f, -1);
        }
        
        collider.enabled = false;
        swordSound.enabled = false;
        trail.enabled = false;
    }

    void direction(float dir, float spd)
    {
        Vector3 scale;
        rb.velocity = new Vector2(spd * speed, rb.velocity.y);
        scale = new Vector3(dir, transform.localScale.y, transform.localScale.z);
        transform.localScale = scale;
    }

    public void attackPlayer()
    {
        collider.enabled = true;
        swordSound.enabled = true;
        trail.enabled = true;
        animation.SetBool("punch", true);
        animation.SetBool("move", false);
    }

    public void chasePlayer()
    {
        collider.enabled = false;
        swordSound.enabled = false;
        trail.enabled = false;
        animation.SetBool("move", true);
        animation.SetBool("punch", false);
        if (player.transform.position.x > transform.position.x) // player at right
            {
                print("Player at right");
                direction(-1f, 1);
            }
            else
            {
                print("Player at left");
                direction(1f, -1);
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            playerDetection = true;
        if(collision.tag == "playerhit")
        {
            if(!isDead)
            {
                isDead = true;
                animation.SetBool("die", true);
                Destroy(collider);
                Destroy(swordSound);
                Destroy(gameObject, 2);
            }
            GameObject obj = Instantiate(hitParticle, hitPos.transform.position, hitPos.transform.rotation);
            Destroy(obj, 1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            playerDetection = false;
    }


}
