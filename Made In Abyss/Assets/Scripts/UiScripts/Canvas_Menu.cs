using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Canvas_Menu : UIBase {

    public Button startGameBtn;
    public Transform startPoint;
    public GameObject backpack;
    public GameObject seting;
    public GameObject roleMessage;

    public GameObject skillOne;
    public GameObject skillTwo;
    public GameObject skillBoom;

    public GameObject btnParnt;//获取3个按钮的父物体
    public Transform anmitorStartPoint;//获取动画的开始与结束的点
    public Transform anmitorEndPoint;

    public AudioClip btnAudio;

    void Start()
    {
        btnAudio = Resources.Load("AudioClicp/按钮12_爱给网_aigei_com", typeof(AudioClip)) as AudioClip;

        skillOne = transform.Find("Img_Skill_Bg/SkillOne").gameObject;
        skillTwo = transform.Find("Img_Skill_Bg/SkillTwo").gameObject;
        skillBoom = transform.Find("Img_Skill_Bg/Boom").gameObject;//获取3个技能的物体
        skillOne.AddComponent<SkillOne>();
        skillTwo.AddComponent<SkillTwo>();
        skillBoom.AddComponent<SkillBoom>();//添加脚本

        startGameBtn = transform.Find("Btn_Menu").GetComponent<Button>();
        anmitorStartPoint = transform.Find("MaskBtn/AnimtorStartPoint").GetComponent<Transform>();
        anmitorEndPoint = transform.Find("MaskBtn/AnimtorEndPoint").GetComponent<Transform>();

        backpack = transform.Find("MaskBtn/AnimtorUi/Btn_Set").gameObject;
        backpack.AddComponent<Btn_Set>();

        seting = transform.Find("MaskBtn/AnimtorUi/Btn_Bag").gameObject;
        seting.AddComponent<Btn_Bag>();

        roleMessage = transform.Find("MaskBtn/AnimtorUi/Btn_Message").gameObject;
        roleMessage.AddComponent<Btn_Message>();

        btnParnt = transform.Find("MaskBtn/AnimtorUi").gameObject;
        btnParnt.transform.position = anmitorEndPoint.position;//获取隐藏按钮

        startGameBtn.onClick.AddListener(StartGameOnClick);
        startPoint = startGameBtn.transform;
    }


    void Update()
    {
        
    }

    public void StartGameOnClick()
    {
        AudioPlay();
        btnParnt.transform.DOMove(anmitorStartPoint.position,0.5f);
        startGameBtn.onClick.RemoveAllListeners();
        startGameBtn.onClick.AddListener(EndUiOnClick);
    }

    public void EndUiOnClick()
    {
        AudioPlay();
        btnParnt.transform.DOMove(anmitorEndPoint.position, 0.5f);
        startGameBtn.onClick.RemoveAllListeners();
        startGameBtn.onClick.AddListener(StartGameOnClick);
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
