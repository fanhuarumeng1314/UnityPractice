using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class die : MonoBehaviour
{
    L5_PlayerCharacter l_P;

    private void Awake()
    {
        l_P = FindObjectOfType<L5_PlayerCharacter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            l_P.Die();
        }
    }
}
