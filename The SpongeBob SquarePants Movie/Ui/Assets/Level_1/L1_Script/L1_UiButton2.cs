﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class L1_UiButton2 : MonoBehaviour {
	void Start () {
		
	}
	
	void Update () {
		
	}

    public void OnClick()
    {
        SceneManager.LoadScene("L3_Scene");
    }
}
