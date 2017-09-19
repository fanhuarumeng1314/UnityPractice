using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_Set : UIBase {

    public Button setButton;
    public Transform setImgPoint;
    public GameObject imgSet;

    public AudioClip btnAudio;
    private void Awake()
    {

        setButton = GetComponent<Button>();
        setButton.onClick.AddListener(SetUIGreate);

        setImgPoint = UiManger.Instance.UIRoot.transform.Find("Canvas_Menu").GetComponent<Transform>();
    }
    void Start ()
    {
        btnAudio = Resources.Load("AudioClicp/按钮12_爱给网_aigei_com", typeof(AudioClip)) as AudioClip;
    }
	
	
	void Update () {
		
	}

    public void SetUIGreate()
    {
        AudioPlay();
        imgSet = UiManger.Instance.GreatUI<Img_Set>(setImgPoint);
        //imgSet.transform.position = new Vector3(960,540,0);
        imgSet.transform.localPosition = Vector3.zero;
        
        setButton.onClick.RemoveAllListeners();
        setButton.onClick.AddListener(DeletImgSet);
    }

    public void DeletImgSet()
    {
        AudioPlay();
        if (imgSet != null)
        {
            Destroy(imgSet);
            setButton.onClick.RemoveAllListeners();
            setButton.onClick.AddListener(SetUIGreate);
        }
        else
        {
            Debug.Log("imgSet等于NULL");
            SetUIGreate();
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
