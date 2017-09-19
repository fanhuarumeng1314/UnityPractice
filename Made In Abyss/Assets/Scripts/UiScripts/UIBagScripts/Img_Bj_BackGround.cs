using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Img_Bj_BackGround : UIBase {

    public Transform propParnt;
    public Button closeBag;
    public AudioClip btnAudio;

    void Start ()
    {
        btnAudio = Resources.Load("AudioClicp/按钮12_爱给网_aigei_com", typeof(AudioClip)) as AudioClip;
        closeBag = transform.Find("Btn_ColseBag").GetComponent<Button>();//关闭背包
        closeBag.onClick.AddListener(CloseBag);

        propParnt = transform.Find("Scroll View/Viewport/Content");
        AddProp();
    }
	
	
	void Update () {
		
	}

    public void AddProp()
    {

        foreach (var prop in PlayCharacter.Instance.bag)
        {
            var tmp_Prop = UiManger.Instance.GreatUI<Img_Bj_Prop>(propParnt);

            Image img_Prop = tmp_Prop.transform.Find("Btn_Prop/Img_Prop").GetComponent<Image>();
            Text prop_Number = tmp_Prop.transform.Find("Btn_Prop/Img_Prop/Txt_Number").GetComponent<Text>();

            Sprite tmp_ImgProp = Resources.Load("Photo/"+prop.Key.Route,typeof(Sprite)) as Sprite;
            img_Prop.sprite = tmp_ImgProp;
            prop_Number.text = prop.Value.ToString();
        }
    }

    public void CloseBag()
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
