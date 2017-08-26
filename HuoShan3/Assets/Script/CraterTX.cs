using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraterTX : MonoBehaviour {

    public GameObject huoShan;//传入火山特效
    public AudioManger audioManger;

    public float timeSpeed_SuDa;//苏打在火上口上撒的时间
    public float timeSpeed_Cu;//醋在火上口上撒的时间
    public float timeSpeed_HuoShan;//火山特效的持续时间
    Animator hsAnimator;
    Animator hsLAnimator;//火山流动的动画
    float timeSpeed_SuDaPlay;
    float timeSpeed_CuPlay;//动画的播放时间

    bool isPlayHS = true;

    void Start ()
    {
        audioManger = FindObjectOfType<AudioManger>();
        hsAnimator = huoShan.GetComponent<Animator>();
        hsLAnimator = GetComponent<Animator>();
        hsLAnimator.StartPlayback();
    }
	
	
	void Update ()
    {

        if (timeSpeed_SuDa > 3 && timeSpeed_Cu > 3)
        {
            timeSpeed_HuoShan += Time.deltaTime;
            huoShan.SetActive(true);
            if (isPlayHS)
            {
                //audioManger.BoomPlay();
                isPlayHS = false;
            }

            hsAnimator.SetBool("isStart", true);
            hsLAnimator.StopPlayback();
            if (timeSpeed_HuoShan > 6)
            {
                isPlayHS = true;
                timeSpeed_HuoShan = 0;
                timeSpeed_SuDa = 0;
                timeSpeed_Cu = 0;
                //audioManger.BoomEnd();
                hsAnimator.SetBool("isStart",false);
                hsLAnimator.StartPlayback();
            }
        }



    }
}
