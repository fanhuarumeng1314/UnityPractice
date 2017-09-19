using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Img_Choice_Shop : UIBase {


    public Text tishiTxt;
    public Button yesBtn;
    public Button noBtn;
    public AudioClip shopClip;
    public AudioClip btnAudio;

    void Start ()
    {
        shopClip = Resources.Load("AudioClicp/10_爱给网_aigei_com", typeof(AudioClip)) as AudioClip;
        btnAudio = Resources.Load("AudioClicp/按钮12_爱给网_aigei_com", typeof(AudioClip)) as AudioClip;
        tishiTxt = transform.Find("Txt_TiShi").GetComponent<Text>();
        yesBtn = transform.Find("Btn_Yes").GetComponent<Button>();
        noBtn = transform.Find("Btn_No").GetComponent<Button>();
        yesBtn.onClick.AddListener(AddBoom);
        noBtn.onClick.AddListener(CloseTishi);
    }
	
	
	void Update () {
		
	}

    public void AddBoom()
    {
        GreatAduio(btnAudio);
        if (PlayCharacter.Instance.money >= 100)
        {
            GreatAduio(shopClip);

            PlayCharacter.Instance.money = PlayCharacter.Instance.money - 100;
            PlayCharacter.Instance.bag[PropLibry.Instance.propTable["1022"]]++;
            tishiTxt.text = "购买成功，是否继续购买？";
        }
        else
        {
            tishiTxt.text = "金币不足，购买失败！";
        }
    }

    public void CloseTishi()
    {
        GreatAduio(btnAudio);
        Destroy(gameObject);
    }

    public void GreatAduio(AudioClip tmp_Clip)
    {
        GameObject tmp_Audio = new GameObject();
        var tmp_AudioPlay = tmp_Audio.AddComponent<AudioSource>();
        tmp_AudioPlay.clip = tmp_Clip;
        tmp_AudioPlay.Play();
        StartCoroutine(DeletAudio(tmp_Audio));
    }

    IEnumerator DeletAudio(GameObject audioObj)
    {
        yield return new WaitForSeconds(1f);

        Destroy(audioObj);
        yield return null;
    }
}
