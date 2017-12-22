using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float speed = 2f;
    public float maxSpeed = 5f;
    public bool grounded;
    public float jumpPower = 6.5f;
    public AudioSource aud;
    public AudioClip audClp;
    public AudioClip audSword;

    private Rigidbody2D rb2d;
    private Animator anim;
    private bool jump;
    private bool doubleJump;
    private bool movement = true;
    private SpriteRenderer spr;
    private bool transparent=false;
    private Color color = new Color(255 / 255f, 106 / 255f, 0f, 200 / 255f);
    private GameObject healtBar;
    private CircleCollider2D cc2d;
    private bool rollAtack = false;
    private bool atack=false;
    public ParticleSystem dust;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        aud = GetComponent<AudioSource>();
        cc2d = gameObject.GetComponent<CircleCollider2D>();
        healtBar = GameObject.Find("HealtBar");// no muy recomendable cuando se tienen muchos objetos, mejor hacerlo desde el editor
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("Grounded", grounded);
        anim.SetBool("rollAtack",rollAtack);
        anim.SetBool("Atack", atack);
        /*if (!grounded && Input.GetKeyDown(KeyCode.X))
        {
            //rollAtack = true;
            rollAtack=false;
           // atack = true;
            Debug.Log("Roll Time");


        }
        else {
            rollAtack=false;
        }*/

        if (grounded) {
            doubleJump = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            dustStop();
            if (grounded) {
                jump = true;
                doubleJump = true;
            } else if (doubleJump) {
                jump = true;
                doubleJump = false;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.X) /*&& grounded*/)
        {
            //anim.SetBool("Atack", false);
            anim.speed = 20;
            aud.clip = audSword;
            aud.Play();
            atack = true;
        }
        else {
            //anim.speed = 60;
            anim.speed = 1;
            atack=false;

        }

        if ((jump || doubleJump) && atack)
        {
            rollAtack = true;
        }
        else
        {
            rollAtack = false;
        }

    }

    private void FixedUpdate()
    {
        if (healtBar.GetComponent<HealtBar>().getHp() > 0)
        {
            Vector3 fixedVelocity = rb2d.velocity;
            fixedVelocity.x *= 0.75f;
            if (grounded)
            {
                rb2d.velocity = fixedVelocity;
            }

            float h = Input.GetAxis("Horizontal");
            if (!movement)
            {
                h = 0f;
            }

            rb2d.AddForce(Vector2.right * speed * h);
            float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);//funcion clamp limite el valor de una variable entre un rango de valores
            rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

            if (h > 0.1f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }else if (h < -0.1f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            if (jump)
            {
                aud.clip = audClp;
                aud.Play();
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                jump = false;
            }
            if (transparent)
            {
                //Invoke("TransparentTime",0.8f);
                TransparentTime();
            }
        }
        else {
            anim.SetBool("Die", true);
            Invoke("GameOver", 1);
        }


        
    }

    private void OnBecameInvisible()
    {
        if (healtBar.GetComponent<HealtBar>().getHp() > 0)
        {
            transform.position = new Vector3(-1, 0, 0);
        }
        //else {
          //  Destroy(gameObject);
       // }
        
    }

    public void EnemyJump() {
        jump = true;
    }

    public void EnemyKnockBack(float enemyPosX) {
        healtBar.SendMessage("TakeDamage",20);
        jump = true;
        float side = Mathf.Sign(enemyPosX - transform.position.x);
        rb2d.AddForce(Vector2.left*side*4,ForceMode2D.Impulse);
        movement = false;
        Invoke("EnableMovement", 0.8f);
        transparent = true;
        //spr.color = color;

    }

    void EnableMovement() {
        movement = true;
        transparent = false;
        spr.color = Color.white;
        
    }

    void TransparentTime() {
        
        if (spr.color != Color.white)
        {
            spr.color = Color.white;
        }
        else {
            spr.color = color;
        }
        
    }

    void gameOver() {
        Destroy(gameObject, 0f);

        //rb2d.isKinematic = false;
        //cc2d.isTrigger = true;
        //rb2d.velocity = Vector2.zero;
        //rb2d.AddForce(Vector2.down * 05f, ForceMode2D.Force);
        //

        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        gameOver();

        if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
            
            SceneManager.LoadScene("Principal");
        }
    }

    void prueba() {
        anim.StopPlayback();
    }

    public void dustPlay() {
        dust.Play();
    }
    public void dustStop() {
        dust.Stop();
    }
}
