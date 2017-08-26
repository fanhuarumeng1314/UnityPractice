using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangerJumpPower : MonoBehaviour
{
    public AudioClip waterSound;

    AudioSource audioS;

    Player_Character p_c;

    void Awake()
    {
        p_c = FindObjectOfType<Player_Character>();

        audioS = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioS.clip = waterSound;
            audioS.Play();

            p_c.jumpPower = 45f;
        }
    }
}
