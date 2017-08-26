using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiButton : MonoBehaviour {


	void Start () {
		
	}
	
	void Update () {
		
	}

    public void OnClick()
    {
        SceneManager.LoadScene("L1_Sence");
    }

}
