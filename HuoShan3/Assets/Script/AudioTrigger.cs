using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour {

    public AudioManger audioManger;
    bool isPlay = false;
	void Start ()
    {
        audioManger = FindObjectOfType<AudioManger>();

    }
	
    //void OnTriggerEnter(Collider other)
    //{
    //    if (isPlay)
    //    {
    //        audioManger.LosePlayLeft();

    //    }
    //    isPlay = true;
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    audioManger.TakePlayLeft();
    //}
}
