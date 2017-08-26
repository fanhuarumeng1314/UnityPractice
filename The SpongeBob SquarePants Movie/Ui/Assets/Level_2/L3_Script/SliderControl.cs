using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderControl : MonoBehaviour
{
    L3_Player pc;

    public GameObject player;
    public GameObject slider;
    public GameObject cube;
    public GameObject sliderDraw;

    bool active = false;

    void Awake()
    {
        pc = FindObjectOfType<L3_Player>();
    }

    void Update()
    {
        if (active)
        {
            slider.transform.position += new Vector3(0, 0, -1) * Time.deltaTime * 0.1f;
        }
    }

    void OnTriggerStay(Collider other)
    {
        Transform sliderTrans = slider.GetComponent<Transform>();

        if (other.CompareTag("RaiseBox"))
        {
            player.transform.SetParent(sliderTrans);
            player.transform.localRotation = Quaternion.identity;
            active = true;
            cube.SetActive(true);
            pc.IsControl = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RaiseBox"))
        {
            player.transform.SetParent(null);
            active = false;
            cube.SetActive(false);
            pc.IsControl = true;
            sliderDraw.SetActive(false);
            pc.grabObjectCarry = null;
        }
    }
}   
