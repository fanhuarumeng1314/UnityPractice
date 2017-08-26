using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderControl : MonoBehaviour
{
    public GameObject gameO;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("GrabBox"))
        {
            gameO.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("GrabBox"))
        {
            gameO.SetActive(false);
        }
    }

}
