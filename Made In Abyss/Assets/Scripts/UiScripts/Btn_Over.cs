using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Btn_Over : UIBase {

    public Button overBtn;
    public PlayCamera nowCamera;


    void Awake()
    {
        nowCamera = GameObject.FindObjectOfType<PlayCamera>();
    }

    void Start ()
    {
        overBtn = GetComponent<Button>();
        overBtn.onClick.AddListener(GameOver);

    }
	
	
	void Update () {
		
	}

    public void GameOver()
    {
        Destroy(nowCamera.gameObject);
        Destroy(InitGameMode.instance.gameObject);
        Destroy(UiManger.Instance.UIRoot.gameObject);
        Destroy(UiManger.Instance.gameObject);
        Destroy(NewPlayerGameMode.Instance.gameObject);
        Destroy(Launcher.Instance.gameObject);
        Destroy(PlayGameMode.Instance.gameObject);
        Destroy(PlayCharacter.Instance.gameObject);
        



        SceneManager.LoadScene("StartGameUi");
    }

}
