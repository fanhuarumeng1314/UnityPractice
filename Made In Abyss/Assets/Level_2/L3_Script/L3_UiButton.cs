using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class L3_UiButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void Update () {
		
	}

    public void OnChilk()
    {
        SceneManager.LoadScene("DemoScene");
    }
}
