using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance=null;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null) {
            instance = this;
        } else if (instance!=this) {
            Destroy(gameObject);
        }
    }

   	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("1") ) {
            LoadScene("Principal");
        } else if (Input.GetKeyDown("0") ) {
            LoadScene("Inicio");
        }
	}

    void LoadScene(string name) {
        SceneManager.LoadScene(name);
    }
}
