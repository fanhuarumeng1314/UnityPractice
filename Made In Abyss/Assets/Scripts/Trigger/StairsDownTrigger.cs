using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsDownTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayCharacter>();
        if (player)
        {
            gameObject.SetActive(false);
            PlayGameMode.Instance.AddChenkPoint();
        }
    }
}
