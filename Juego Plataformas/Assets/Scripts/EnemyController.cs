using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public float speed = 1f;
    public float maxSpeed = 1f;
    public bool grounded;
    public float jumpPower = 6.5f;
    public float floor = -2.5f;
    public AudioClip boingAudio;
    public AudioClip clinkAudio;
    private Rigidbody2D rb2d;
    private Animator anim;
    private bool jump;
    private bool doubleJump;
    private bool died=false;
    private AudioSource audSrc;
    private bool knockBack=false;
    private float orgVelX = 0f;
    

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audSrc = GetComponent<AudioSource>();
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (knockBack==false) {
            if (died == false)
            {

                
                rb2d.AddForce(Vector2.right * speed);
                float  limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
                rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

                if (rb2d.velocity.x > -0.01f && rb2d.velocity.x < 0.01f)
                {
                    speed = -speed;
                    rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
                }

                if (speed < 0f)
                {
                    transform.localScale = new Vector3(1f, 1f, 1f);
                }
                else if (speed > 0f)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
            }
            else
            {
                rb2d.isKinematic = false;
                rb2d.velocity = Vector3.zero;
                anim.SetBool("died", died);
            }
        }
       
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Sword")
        {
            audSrc.clip = clinkAudio;
            audSrc.Play();
            knockBack = true;
            float side = Mathf.Sign(collision.transform.position.x - transform.position.x);
            rb2d.AddForce(Vector2.left * side * 4, ForceMode2D.Impulse);
            Invoke("RestoreForce", 0.15f);
            anim.SetBool("died", died);
            died = true;
            rb2d.transform.position = new Vector3(rb2d.transform.position.x, rb2d.transform.position.y - 0.4f, rb2d.transform.position.z);
            rb2d.AddForce(new Vector2(0, 10));
            Invoke("DestroyMe", 1f);
        }
        else {
            
            knockBack = false;
        }


       
        if (collision.gameObject.tag=="Player") {
            float yOffset = 0.18f;/*no funciona los objetos no estan base zero(el player mas que todo)*/
            collision.SendMessage("EnemyKnockBack", transform.position.x);
            /*if (Mathf.Abs(transform.position.y)+ yOffset > Mathf.Abs(collision.transform.position.y) )
            {
                audSrc.clip = boingAudio;
                audSrc.Play();
                anim.SetBool("died", died);
                rb2d.transform.position=new Vector3(rb2d.transform.position.x, rb2d.transform.position.y +10, rb2d.transform.position.z);
                collision.SendMessage("EnemyJump");
                died = true;
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, floor, gameObject.transform.position.z);
                rb2d.velocity = new Vector2(0f, rb2d.velocity.y);

                Invoke("DestroyMe", 1f);


            } 
            else {
                collision.SendMessage("EnemyKnockBack", transform.position.x);
            }*/
        }
    }
    private void Update()
    {
        //if (died) {
            //anim.SetBool("died", died);
            //audioBoing.Play();
        //}

    }

    private void DestroyMe() {
        //-2.351
        

        Destroy(gameObject);
    }

    private void RestoreForce() {
        
        knockBack = false;
        
    }

    
}