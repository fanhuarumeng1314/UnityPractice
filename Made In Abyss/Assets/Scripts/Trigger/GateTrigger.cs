using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour {

	
	void Start () {
	}
	
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<PlayCharacter>();
        if (player)
        {
            if (player.keyNumber >= 1)
            {
                PlayCharacter.Instance.keyNumber--;
                PlayGameMode.Instance.ChangeMaps(transform);
                transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
