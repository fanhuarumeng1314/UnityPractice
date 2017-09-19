using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_Message : UIBase {

    public Button messgaeBtn;
    public Transform messageBg;
    public GameObject btnMessgae;

    public AudioClip btnAudio;
    void Awake ()
    {
        btnAudio = Resources.Load("AudioClicp/按钮12_爱给网_aigei_com", typeof(AudioClip)) as AudioClip;

        messgaeBtn = GetComponent<Button>();
        messageBg = UiManger.Instance.UIRoot.transform.Find("Canvas_Menu");
        messgaeBtn.onClick.AddListener(CreateMessgeBackground);
    }
	
	
	void Update ()
    {
		
	}

    public void CreateMessgeBackground()
    {
        AudioPlay();
        btnMessgae = UiManger.Instance.GreatUI<Img_Bg_Role>(messageBg);
       // btnMessgae.transform.position = new Vector3(960,540,0);
        btnMessgae.transform.localPosition = Vector3.zero;

        messgaeBtn.onClick.RemoveAllListeners();
        messgaeBtn.onClick.AddListener(DeletBtnMessage);
    }

    public void DeletBtnMessage()
    {
        AudioPlay();
        if (btnMessgae != null)
        {
            Destroy(btnMessgae);
            messgaeBtn.onClick.RemoveAllListeners();
            messgaeBtn.onClick.AddListener(CreateMessgeBackground);
        }
        else
        {
            CreateMessgeBackground();
        }

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
