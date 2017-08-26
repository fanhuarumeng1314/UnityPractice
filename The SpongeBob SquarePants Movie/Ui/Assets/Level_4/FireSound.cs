using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSound : MonoBehaviour
{
    AudioSource audioS;
    public GameObject treeFire;

    int a = 1; 
	void Start ()
    {
        audioS = GetComponent<AudioSource>();
	}
	
	void Update ()
    {
        if (treeFire.activeSelf == true)
        {
            if (a == 1)
            {
                audioS.Play();
                a++;
            }
        }
	}


}
