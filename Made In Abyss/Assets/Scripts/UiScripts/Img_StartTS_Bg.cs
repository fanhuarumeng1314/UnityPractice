using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Img_StartTS_Bg : UIBase {

    public Button closeTs;
    public AudioClip btnAudio;

    void Start ()
    {
        btnAudio = Resources.Load("AudioClicp/按钮12_爱给网_aigei_com", typeof(AudioClip)) as AudioClip;
        closeTs = transform.Find("Btn_CloseTS").GetComponent<Button>();
        closeTs.onClick.AddListener(Close);

    }
	
	
	void Update () {
		
	}

    public void Close()
    {
        AudioPlay();
        Destroy(gameObject);
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
