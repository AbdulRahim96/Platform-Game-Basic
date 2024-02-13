using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public Animator animator;
    public Slider healthBar;
    public bool isShield = true, isDead;
    public float shieldTime, damage;
    public GameObject minions, shield;
    public Transform[] spawnPos;
    public float spawnTime, resetTime;
    public int minionAmount;
    public GameObject hitParticle, hitPos, sheildParticle;


    public GameObject traps;
    private float trapTime = 5;

    private bool timeup = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        spawnTime -= Time.deltaTime;
        if(spawnTime <= 0)
        {
            StartCoroutine(animate("victory", 3));
            StartCoroutine(spawn(minionAmount));
            spawnTime = Random.Range(10, 30);
            timeup = true;
        }
        if(timeup)
        {
            shieldTime -= Time.deltaTime;
            isShield = false;
            if (shieldTime <= 0)
            {
                isShield = true;
                timeup = false;
                shieldTime = 5;
            }
            shield.SetActive(isShield);
        }

        trapTime -= Time.deltaTime;
        if(trapTime <= 0)
        {
            Instantiate(traps);
            trapTime = Random.Range(3, 10);
        }

        if (FindObjectOfType<PlayerController>().controlEnabled == false)
        {
            StartCoroutine(animate("victory", 30));
        }

    }

    

    IEnumerator animate(string name, float time)
    {
        animator.SetBool(name, true);
        yield return new WaitForSeconds(time);
        animator.SetBool(name, false);
    }
    IEnumerator spawn(int num)
    {
        int randomPos = Random.Range(0, spawnPos.Length);
        yield return new WaitForSeconds(1);
        GameObject obj = Instantiate(minions, spawnPos[randomPos].position, spawnPos[randomPos].rotation);
        obj.GetComponent<Enemies>().fromBoss = true;
        if (num > 0)
        {
            StartCoroutine(spawn(num - 1));
        }
        
    }

    public void attackPlayer()
    {
        animator.SetBool("punch", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            attackPlayer();
        if (collision.tag == "playerhit")
        {
            if (!isDead && !isShield)
            {
                healthBar.value += 2;
                minionAmount += 2;
                if (healthBar.value < 10)
                {

                    StartCoroutine(animate("hit", 1));
                    isShield = true;
                    timeup = false;
                    shieldTime = 30;
                    spawnTime = 30;
                    shield.SetActive(isShield);
                }
                else
                {
                    isDead = true;
                    animator.SetBool("die", true);
                    Destroy(gameObject, 2);
                }
                GameObject obj = Instantiate(hitParticle, hitPos.transform.position, hitPos.transform.rotation);
                Destroy(obj, 1);
            }
            else if(!isDead && isShield)
            {
                GameObject obj = Instantiate(sheildParticle, hitPos.transform.position, hitPos.transform.rotation);
                Destroy(obj, 1);
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            animator.SetBool("punch", false);
    }

    private void OnDestroy()
    {
        GameSetup.levelComplete();
    }
}
