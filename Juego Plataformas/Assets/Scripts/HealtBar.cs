using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealtBar : MonoBehaviour {
    public float hp;
    private float maxHp = 100;
    
    public Image healt;
    public Text gameOverText;
	// Use this for initialization
	void Start () {
        hp = maxHp;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(float amoount) {
        hp = Mathf.Clamp(hp-amoount,0f,maxHp);
        healt.transform.localScale = new Vector2(hp/maxHp,healt.transform.localScale.y);
        Debug.Log("enable " + gameOverText.enabled);
        
        //Debug.Log("Visible " + gameOverText.text="Game Over");
        if (hp==0) {

            gameOverText.text = "Game Over";
        }
    }

    public float getHp() {
        return hp;
    }

}
