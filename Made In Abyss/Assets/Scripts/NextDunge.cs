using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextDunge : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        var player = other.transform.gameObject.GetComponent<PlayController>();

        if (player)
        {
            SceneManager.LoadScene("Made In Abyss");
        }
    }
}
