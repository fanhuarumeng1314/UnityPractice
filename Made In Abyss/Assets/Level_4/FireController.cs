using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public GameObject treeFire;

        

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GrabBox"))
        {
            treeFire.SetActive(true);
        }
    }

}
