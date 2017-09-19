using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_Exit_Messeg : UIBase {

    public Button exitBtn;
    public GameObject parntMesseg;
    public AudioClip btnAudio;
    void Start ()
    {
        btnAudio = Resources.Load("AudioClicp/按钮12_爱给网_aigei_com", typeof(AudioClip)) as AudioClip;
        exitBtn = GetComponent<Button>();
        parntMesseg = transform.parent.gameObject;
        exitBtn.onClick.AddListener(Exit);
    }
	
	
	void Update () {
		
	}

    public void Exit()
    {
        AudioPlay();
        Destroy(parntMesseg);
    }

    IEnumerator DeletAudio(GameObject audioObj)
    {
        yield return new WaitForSeconds(1f);
        Destroy(audioObj);
        yield return null;
    }

    public void AudioPlay()
    {
        GameObject tmp_Audio = new GameObject();
        var tmp_AudioPlay = tmp_Audio.AddComponent<AudioSource>();
        tmp_AudioPlay.clip = btnAudio;
        tmp_AudioPlay.Play();
        StartCoroutine(DeletAudio(tmp_Audio));
    }
}
