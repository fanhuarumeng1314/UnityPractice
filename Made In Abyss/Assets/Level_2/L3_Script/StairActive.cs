using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairActive : MonoBehaviour
{
    public GameObject stair;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stair.SetActive(true); 
        }
    }
}
