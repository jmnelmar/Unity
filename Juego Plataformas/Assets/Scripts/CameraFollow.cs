using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject follow;
    public Vector2 mincamPos, maxCamPos;
    public float smoothTime;

    private Vector2 smoothVelocity;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {//se llama tantas veces un procesador pueda ejecutar en una pc por frame
		
	}
    private void FixedUpdate()//se llama una vez por frame
    {
        float posX =Mathf.SmoothDamp(transform.position.x,follow.transform.position.x,ref smoothVelocity.x,smoothTime) ;
        float posY = Mathf.SmoothDamp(transform.position.y, follow.transform.position.y, ref smoothVelocity.y, smoothTime); 

        transform.position = new Vector3(
            Mathf.Clamp(posX, mincamPos.x, maxCamPos.x)
            , Mathf.Clamp(posY, mincamPos.y, maxCamPos.y), transform.position.z);

       
    }


   
}
