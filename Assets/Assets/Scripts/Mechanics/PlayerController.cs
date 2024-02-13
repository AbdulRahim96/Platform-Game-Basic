using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerController : MonoBehaviour
    {
        public float maxSpeed = 7;
        public float jumpForce = 10;
        private AudioSource walkAudio;
        public AudioClip jumpSound, hitSound;
        public AudioSource hitAudio;
        public GameObject ScreenEffects;

        private Rigidbody2D rb;
        
        public bool controlEnabled = true;

        public bool jump, canPunch = true;
        Vector2 move;
        public SpriteRenderer spriteRenderer;
        public Animator animator;

        public Transform groundCheckPoint;
        public float groundRadius;
        public LayerMask groundLayer;
        public BoxCollider2D collider;
        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            walkAudio = GetComponent<AudioSource>();
        }

        
        void FixedUpdate()
        {
            if (controlEnabled)
            {
                move.x = CrossPlatformInputManager.GetAxis("Horizontal");
                move.y = CrossPlatformInputManager.GetAxis("Vertical");

                rb.velocity = new Vector2(move.x * maxSpeed, rb.velocity.y);

                if (move.x > 0.01f)
                {
                    Vector3 scale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
                    transform.localScale = scale;
                }
                else if (move.x < -0.01f)
                {
                    Vector3 scale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
                    transform.localScale = scale;
                }

                animator.SetBool("move", (move.x != 0));

                jump = Physics2D.OverlapCircle(groundCheckPoint.position, groundRadius, groundLayer);
                
            if(jump == true)
            {
                animator.SetBool("jump", false);
            }

                if(jump && Mathf.Abs(move.x) > 0f)
                {
                    
                    walkAudio.enabled = true;
                }
                else
                    walkAudio.enabled = false;


            }
        }

        public void punch()
        {
            if(canPunch)
            {
                hitAudio.clip = hitSound;
                hitAudio.Play();
                StartCoroutine(anim(0.2f));
            }
            
        }

        IEnumerator anim(float time)
        {
            collider.enabled = true;
            canPunch = false;
            animator.SetBool("punch", true);
            yield return new WaitForSeconds(time);
            animator.SetBool("punch", false);
            collider.enabled = false;
            canPunch = true;
        }

        public void jumpButton()
        {
            if(jump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                animator.SetBool("jump", true);
                hitAudio.clip = jumpSound;
                hitAudio.Play();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "hit")
            {
                if(controlEnabled)
                {
                    dying();
                }
                
            }
        }

        public void dying()
        {
            controlEnabled = false;
            animator.SetBool("die", true);
            Instantiate(ScreenEffects);
            FindObjectOfType<GameSetup>().restartLevel();
        }
    }