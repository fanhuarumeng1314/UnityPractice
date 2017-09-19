using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsUpTrigger : MonoBehaviour {


	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayCharacter>();
        if (player)
        {
            gameObject.SetActive(false);
            PlayGameMode.Instance.ReduceCheckPoint();
            
        }
    }

}
