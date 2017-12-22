using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour {
    private PlayerController player;
    private Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
        player = GetComponentInParent<PlayerController>();
        rb2d = GetComponentInParent<Rigidbody2D>();
        
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            rb2d.velocity = new Vector3(0f,0f,0f);
            player.transform.parent = collision.transform;
            player.grounded = true;

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Ground") {
            player.grounded = true;
        }
        /*if (collision.gameObject.tag == "Platform")
        {
            player.transform.parent = collision.transform;
            player.grounded = true;
        }*/


    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Ground") {
            player.grounded = false;
        }
        if (collision.gameObject.tag == "Platform") {
            player.transform.parent = null;
            player.grounded = false;
        }
        
    }
}
