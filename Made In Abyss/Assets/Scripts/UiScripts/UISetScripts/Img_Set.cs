using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Img_Set : UIBase {

    public Button saveBtn;
    public Button ContiuneBtn;
    public Button endGameBtn;
    public Button exitBtn;

    public AudioClip btnAudio;

    private void Awake()
    {

        btnAudio = Resources.Load("AudioClicp/按钮12_爱给网_aigei_com", typeof(AudioClip)) as AudioClip;
        ContiuneBtn = transform.Find("But_Contiune").GetComponent<Button>();
        saveBtn = transform.Find("Btn_Save").GetComponent<Button>();
        endGameBtn = transform.Find("Btn_EndGame").GetComponent<Button>();
        exitBtn = transform.Find("But_Exit_Set").GetComponent<Button>();//获取set界面的4个按钮

        ContiuneBtn.onClick.AddListener(ContiueOnClick);
        exitBtn.onClick.AddListener(ContiueOnClick);//继续游戏以及关闭设置菜单

        saveBtn.onClick.AddListener(SaveGame);
        endGameBtn.onClick.AddListener(EndGameOnClick);
    }

    void Start () {
		
	}
	
	
	void Update () {
		
	}

    public void SaveGame()
    {
        AudioPlay();
        Debug.Log("存储功能暂未实现");
        //ContiuneBtn.gameObject.SetActive(false);
        //saveBtn.gameObject.SetActive(false);
        //endGameBtn.gameObject.SetActive(false);

        //var saveOne = UiManger.Instance.GreatUI<But_Save_One>(transform);
        //var saveTwo = UiManger.Instance.GreatUI<But_Save_Two>(transform);//实例化2个存档点

        //saveOne.transform.position = new Vector3(620,591,0);
        //saveTwo.transform.position = new Vector3(620,496,0);

        //saveOne.transform.DOMove(new Vector3(960,591,0),0.5f);
        //saveTwo.transform.DOMove(new Vector3(960,496,0),0.5f);
        
    }

    public void ContiueOnClick()//继续游戏，关闭这个菜单
    {
        AudioPlay();
        Destroy(gameObject);
    }

    public void EndGameOnClick()//结束游戏
    {
        AudioPlay();
        Application.Quit();
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
