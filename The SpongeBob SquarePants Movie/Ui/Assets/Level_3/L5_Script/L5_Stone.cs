using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L5_Stone : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<L5_PlayerCharacter>().Die();
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

}
