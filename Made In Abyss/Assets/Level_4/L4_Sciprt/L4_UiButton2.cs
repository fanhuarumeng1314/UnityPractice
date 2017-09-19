using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class L4_UiButton2 : MonoBehaviour
{

	void Start () {
		
	}
	
	void Update ()
    {
        
	}

    public void OnChilk()
    {
        SceneManager.LoadScene("DemoScene");
    }
}
