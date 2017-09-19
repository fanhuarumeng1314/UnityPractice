using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Img_About : UIBase{

    public Button closeBtn;
    public Text propName;
    public Text about;
    public Text atk;
    public Text dfs;
    public AudioClip btnAudio;

    private void Awake()
    {
        btnAudio = Resources.Load("AudioClicp/按钮12_爱给网_aigei_com", typeof(AudioClip)) as AudioClip;
        closeBtn = transform.Find("Btn_CloseAbout").GetComponent<Button>();
        closeBtn.onClick.AddListener(CloseAbout);
        propName = transform.Find("Txt_ProPName").GetComponent<Text>();
        about = transform.Find("Txt_About").GetComponent<Text>();
        atk = transform.Find("Txt_Atk").GetComponent<Text>();
        dfs = transform.Find("Txt_Dfs").GetComponent<Text>();
    }



    public void PrintShuXing(Prop nowProp)
    {

        if (nowProp.Type != "")
        {
            propName.text = nowProp.Name;
            about.text = nowProp.About;
            atk.text = "攻击力： "+nowProp.Atk;
            dfs.text = "防御力： "+nowProp.Dfs;

        }
        else
        {
            if (nowProp.Atk == "")
            {
                propName.text = nowProp.Name;
                about.text = nowProp.About;
                atk.text = "";
                dfs.text = "";
            }
            else
            {
                if (nowProp.Name == "生命药剂")
                {
                    propName.text = nowProp.Name;
                    about.text = nowProp.About;
                    atk.text = "增加血量： " + nowProp.Dfs;
                    dfs.text = "";
                }
                else
                {
                    propName.text = nowProp.Name;
                    about.text = nowProp.About;
                    atk.text = "攻击力增加： " + nowProp.Atk;
                    dfs.text = "防御力增加： " + nowProp.Dfs;
                }
                
            }

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


    public void CloseAbout()
    {
        AudioPlay();
        Destroy(gameObject);
    }

}
