using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_Bag : UIBase {

    public Button bagBtn;
    public Transform bagParnt;
    public GameObject bag;

    public AudioClip btnAudio;
    void Start ()
    {

        btnAudio = Resources.Load("AudioClicp/按钮12_爱给网_aigei_com", typeof(AudioClip)) as AudioClip;
        bagBtn = GetComponent<Button>();
        bagParnt = UiManger.Instance.UIRoot.transform.Find("Canvas_Menu").GetComponent<Transform>();
        bagBtn.onClick.AddListener(OpenBag);

    }

    public void OpenBag()
    {
        AudioPlay();
        bag = UiManger.Instance.GreatUI<Img_Bj_BackGround>(bagParnt);
        //bag.transform.position = new Vector3(960,540,0);
        bag.transform.localPosition = Vector3.zero;
        bagBtn.onClick.RemoveAllListeners();
        bagBtn.onClick.AddListener(CloseBag);
    }

    public void CloseBag()
    {
        AudioPlay();
        if (bag != null)
        {
            Destroy(bag);
            bagBtn.onClick.RemoveAllListeners();
            bagBtn.onClick.AddListener(OpenBag);
        }
        else
        {
            OpenBag();
        }

    }

    public void UpdateBag()
    {
        Destroy(bag);
        bag = UiManger.Instance.GreatUI<Img_Bj_BackGround>(bagParnt);
        //bag.transform.position = new Vector3(960,540,0);
        bag.transform.localPosition = Vector3.zero;
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
