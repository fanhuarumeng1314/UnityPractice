using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangerJumpDown : MonoBehaviour {

    Player_Character p_c;

    void Awake()
    {
        p_c = FindObjectOfType<Player_Character>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            p_c.jumpPower = 15f;
        }
    }
}
