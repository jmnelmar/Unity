using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformFalling : MonoBehaviour {

    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;
    private bool on;
    private Vector3 originalPosition;
    private float reaPear = 3f;
    public float floatingTime = 1f;


    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        originalPosition = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }
	
	// Update is called once per frame
	void Update () {
        
        
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            Invoke("Fall", floatingTime);
            Invoke("Reset", floatingTime+reaPear);

        }
    }
    void Fall() {
        rb2d.isKinematic = false;
        bc2d.isTrigger = true;
    }

    private void Reset() {
        transform.position = originalPosition;
        rb2d.isKinematic = true;
        rb2d.velocity = Vector3.zero;
        bc2d.isTrigger = false;
       
    }
}
