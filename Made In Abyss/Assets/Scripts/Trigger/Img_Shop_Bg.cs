using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Img_Shop_Bg : UIBase {

    public Button shopBtn;
    public AudioClip btnAudio;
    void Start ()
    {
        shopBtn = transform.Find("Img_Shop/Btn_Shop").GetComponent<Button>();
        btnAudio = Resources.Load("AudioClicp/按钮12_爱给网_aigei_com", typeof(AudioClip)) as AudioClip;
        shopBtn.onClick.AddListener(GreatChoics);
    }
	
	
	void Update () {
		
	}

    public void GreatChoics()//生成选择Ui
    {
        AudioPlay();
        UiManger.Instance.GreatUI<Img_Choice_Shop>(transform);
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
