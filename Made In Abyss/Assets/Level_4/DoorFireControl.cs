using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFireControl : MonoBehaviour
{
    public GameObject tree;
    public GameObject treeFire;
    Player_Character p_c;

    public GameObject fire;

    bool IsFire = false;
    float timeSpeed = 0;

    void Awake()
    {
        p_c = FindObjectOfType<Player_Character>();  
    }

    void Update()
    {
        if (IsFire)
        {
            OpenDoor();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GrabBox") && fire.activeSelf == true)
        {
            treeFire.SetActive(true);
            IsFire = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GrabBox") && fire.activeSelf == true)
        {
            if (p_c.grabObjectGrab != null)
            {
                tree.SetActive(false);
                p_c.grabObjectGrab = null;
            }
        }
    }

    void OpenDoor()
    {
        timeSpeed += Time.deltaTime;
        if (timeSpeed > 3.0f)
        {
            if (p_c.grabObjectGrab != null)
            {
                tree.SetActive(false);
                p_c.grabObjectGrab = null;
            }
            else
            {
                tree.SetActive(false);
            }

            gameObject.SetActive(false);
            treeFire.SetActive(false);
            IsFire = false;
        }
    }

}
