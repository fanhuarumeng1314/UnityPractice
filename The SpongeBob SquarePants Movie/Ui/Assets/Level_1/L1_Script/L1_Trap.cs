using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1_Trap : MonoBehaviour {


	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            var deth = other.GetComponent<L1_PlayerCharacter>();
            deth.Deth();
        }
    }
}
