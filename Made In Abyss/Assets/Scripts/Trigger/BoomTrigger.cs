using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomTrigger : MonoBehaviour {

	
	void Start ()
    {
		
	}
	
	
	void Update () {

		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Stone")
        {
            other.gameObject.SetActive(false);
            PlayGameMode.Instance.ChangeMaps(other.transform);
        }

        if (other.gameObject.tag == "Gate")
        {
            other.gameObject.SetActive(false);
            PlayGameMode.Instance.ChangeMaps(other.transform);
        }
    }
}
