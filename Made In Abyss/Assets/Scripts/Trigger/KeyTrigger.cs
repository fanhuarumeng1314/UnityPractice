using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTrigger : MonoBehaviour {

	
	void Start () {
		
	}
	
	 
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayCharacter>();
        if (player)
        {
            PlayGameMode.Instance.AddKey(transform);
            PlayGameMode.Instance.ChangeMaps(transform);
            gameObject.SetActive(false);
        }
    }
}
