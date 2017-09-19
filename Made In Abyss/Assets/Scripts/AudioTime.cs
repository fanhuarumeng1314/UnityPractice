using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTime : MonoBehaviour {

    public AudioSource gameAudio;
    public AudioClip bgmClip;
    public float timeSpeed;
	void Start ()
    {
        gameAudio = GetComponent<AudioSource>();
        bgmClip = Resources.Load("AudioClicp/犯罪现场没有锣鼓点没有低音_爱给网_aigei_com",typeof(AudioClip)) as AudioClip;

    }
	
	
	void Update ()
    {
        timeSpeed += Time.deltaTime;
        if (timeSpeed>=60.5f)
        {
            timeSpeed = 0;
            gameAudio.clip = bgmClip;
            gameAudio.Play();
        }
	}
}
