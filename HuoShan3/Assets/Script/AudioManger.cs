using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioManger : MonoBehaviour {
    #region
    public bool isPlay;//是否播放音乐

    public AudioSource backgroundAudioSource;//背景音乐播放器
    public AudioSource takeAudioSource_Left;//左手拿东西的音乐播放器
    public AudioSource takeAudioSource_Right;//右手拿东西的音乐播放器
    public AudioSource boomAudioSource;//火山爆发的音乐播放器

    public AudioClip backgroundAudio;
    public AudioClip takeAudio;//拿
    public AudioClip loseAudio;//丢
    public AudioClip shedAudio_Suda;
    public AudioClip shedAudio_Cu;
    public AudioClip boomAudio;//各种音效
    #endregion




    void Awake()
    {
        #region
        backgroundAudioSource = gameObject.transform.Find("Audio_Background").GetComponent<AudioSource>();
        takeAudioSource_Left = gameObject.transform.Find("Audio_Take_Left").GetComponent<AudioSource>();
        takeAudioSource_Right = gameObject.transform.Find("Audio_Take_Right").GetComponent<AudioSource>();
        boomAudioSource = gameObject.transform.Find("Audio_Boom").GetComponent<AudioSource>();
        takeAudioSource_Left.playOnAwake = false;
        takeAudioSource_Right.playOnAwake = false;
        boomAudioSource.playOnAwake = false;
        backgroundAudio = (AudioClip)Resources.Load("Audio/背景音乐", typeof(AudioClip));
        takeAudio = (AudioClip)Resources.Load("Audio/拿瓶子", typeof(AudioClip));
        loseAudio = (AudioClip)Resources.Load("Audio/杯子放下", typeof(AudioClip));
        shedAudio_Suda = (AudioClip)Resources.Load("Audio/撒苏打02", typeof(AudioClip));
        shedAudio_Cu = (AudioClip)Resources.Load("Audio/洒醋", typeof(AudioClip));
        boomAudio = (AudioClip)Resources.Load("Audio/火山爆发02", typeof(AudioClip));

        BackgroundPlay();
        #endregion


    }


    void Update ()
    {
		
	}

    #region
    public void BackgroundPlay()//播放背景音乐
    {
        if (isPlay)
        {
            backgroundAudioSource.clip = backgroundAudio;
            backgroundAudioSource.volume = 0.2f;
            backgroundAudioSource.playOnAwake = true;
            backgroundAudioSource.loop = true;
            backgroundAudioSource.Play();
        }

    }

    public void TakePlayLeft()
    {
        if (isPlay)
        {
            takeAudioSource_Left.clip = takeAudio;
            takeAudioSource_Left.Play();
        }
    }

    public void TakePlayRight()
    {
        if (isPlay)
        {
            takeAudioSource_Right.clip = takeAudio;
            takeAudioSource_Right.Play();
        }
    }

    public void LosePlayLeft()
    {
        if (isPlay)
        {
            takeAudioSource_Left.clip = loseAudio;
            takeAudioSource_Left.Play();
        }
    }

    public void LosePlayRight()
    {
        if (isPlay)
        {
            takeAudioSource_Right.clip = loseAudio;
            takeAudioSource_Right.Play();
        }
    }

    public void SuDaPlay()
    {
        if (isPlay)
        {
            takeAudioSource_Left.clip = shedAudio_Suda;
            takeAudioSource_Left.Play();
        }
    }

    public void CuPlay()
    {
        if (isPlay)
        {
            takeAudioSource_Right.clip = shedAudio_Cu;
            takeAudioSource_Right.Play();
        }
    }

    public void BoomPlay()
    {
        if (isPlay)
        {
            boomAudioSource.clip = boomAudio;
            boomAudioSource.playOnAwake = true;
            boomAudioSource.Play();
        }
    }

    public void BoomEnd()
    {
        if (isPlay)
        {
            boomAudioSource.clip = null;
            boomAudioSource.playOnAwake = false;
        }
    }
    #endregion
}
