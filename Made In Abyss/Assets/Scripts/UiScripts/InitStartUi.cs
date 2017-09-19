using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class InitStartUi : MonoBehaviour {

    int nowUI = 1;
    public Button start_Button;
    public Button left_Button;
    public Button right_Button;
    public Text game_Text;
    public Color32 nowColor;
    public AudioClip btnAudio;

	void Start ()
    {
        btnAudio = Resources.Load("AudioClicp/按钮12_爱给网_aigei_com", typeof(AudioClip)) as AudioClip;
        start_Button = transform.Find("Menu/Image/But_Start").GetComponent<Button>();
        left_Button = transform.Find("Menu/Image/But_Arrow_Left").GetComponent<Button>();
        right_Button = transform.Find("Menu/Image/But_Arrow_Right").GetComponent<Button>();
        game_Text = transform.Find("Menu/Image/But_Start/Txt_Start").GetComponent<Text>();
        nowColor = game_Text.color;
        start_Button.onClick.AddListener(StartOnclick);
        left_Button.onClick.AddListener(LeftOnClick);
        right_Button.onClick.AddListener(RightOnClick);
    }
	
	
	void Update ()
    {
		
	}

    public void LeftOnClick()
    {
        AudioPlay();
        if (nowUI == 1)
        {
            nowUI = 3;
        }
        else
        {
            --nowUI;
        }

        switch (nowUI)
        {
            case 1:
                start_Button.onClick.RemoveAllListeners();
                game_Text.DOColor(new Color32(0, 0, 0, 0), 0.5f);
                game_Text.DOText("Start Game", 0.1f);
                game_Text.DOColor(nowColor, 0.5f);
                start_Button.onClick.AddListener(StartOnclick);

                break;
            case 2:
                start_Button.onClick.RemoveAllListeners();
                game_Text.DOColor(new Color32(0, 0, 0, 0), 0.5f);
                game_Text.DOText("RoomGreat", 0.1f);
                game_Text.DOColor(nowColor, 0.5f);
                start_Button.onClick.AddListener(ContinueOnClick);
                break;
            case 3:
                start_Button.onClick.RemoveAllListeners();
                game_Text.DOColor(new Color32(0, 0, 0, 0),0.5f);
                game_Text.DOText("End Game", 0.1f);
                game_Text.DOColor(nowColor,0.5f);
                start_Button.onClick.AddListener(EndGameOnClick);
                break;
        }
    }

    public void RightOnClick()
    {
        AudioPlay();
        if (nowUI == 3)
        {
            nowUI = 1;
        }
        else
        {
            ++nowUI;
        }

        switch (nowUI)
        {
            case 1:
                start_Button.onClick.RemoveAllListeners();
                game_Text.DOColor(new Color32(0, 0, 0, 0), 0.5f);
                game_Text.DOText("Start Game", 0.1f);
                game_Text.DOColor(nowColor, 0.5f);
                start_Button.onClick.AddListener(StartOnclick);

                break;
            case 2:
                start_Button.onClick.RemoveAllListeners();
                game_Text.DOColor(new Color32(0, 0, 0, 0), 0.5f);
                game_Text.DOText("RoomGreat", 0.1f);
                game_Text.DOColor(nowColor, 0.5f);
                start_Button.onClick.AddListener(ContinueOnClick);
                break;
            case 3:
                start_Button.onClick.RemoveAllListeners();
                game_Text.DOColor(new Color32(0, 0, 0, 0), 0.5f);
                game_Text.DOText("End Game", 0.1f);
                game_Text.DOColor(nowColor, 0.5f);
                start_Button.onClick.AddListener(EndGameOnClick);
                break;
        }
        Debug.Log("RightOnClick");
    }

    public void StartOnclick()
    {
        SceneManager.LoadScene("StartGame");
        Debug.Log("执行Start");
    }

    public void ContinueOnClick()
    {
        SceneManager.LoadScene("RoomModel");
        Debug.Log("ContinueOnClick");
    }

    public void EndGameOnClick()
    {
        Application.Quit();
        Debug.Log("EndGameOnClick");
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
