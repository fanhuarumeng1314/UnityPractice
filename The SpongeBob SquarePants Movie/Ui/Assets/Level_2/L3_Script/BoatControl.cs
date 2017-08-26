using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatControl : MonoBehaviour
{
    public AudioClip vectiorySound;

    AudioSource audioS;

    L3_Player pc;
    public Light lightSpecially; // 光源
    public GameObject boatSpecially;
    public GameObject boatDownSpecially;
    bool active = false;

    public Material sky01;

    int a = 1;

    void Awake()
    {
        pc = FindObjectOfType<L3_Player>();
        audioS = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (a==2)
        {
            PlaySound();
        }

        if (active)
        {
            transform.position += new Vector3(0, 0, -1) * Time.deltaTime * 2.5f;

            lightSpecially.intensity -= 0.005f;
            if (lightSpecially.intensity == 0)
            {
                lightSpecially.gameObject.SetActive(false);
                boatSpecially.SetActive(true);
                boatDownSpecially.SetActive(true);
                RenderSettings.skybox = sky01;

            }
        }


    }

    void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
            other.transform.localRotation = Quaternion.identity;
            pc.IsControl = false;
            active = true;
            ++a;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RaiseBox"))
        {
            other.transform.SetParent(null);
            pc.IsControl = true;
            active = false;
        }
    }

    void PlaySound()
    {
        audioS.clip = vectiorySound;
        audioS.Play();
    }
}
