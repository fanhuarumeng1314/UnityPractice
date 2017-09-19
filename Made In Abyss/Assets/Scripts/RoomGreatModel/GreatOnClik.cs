using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GreatOnClik : MonoBehaviour {

    public RoomGameMode greatRoom;
    public Button greatBtn;
    public Button exitButton;
	
	void Start ()
    {
        greatRoom = FindObjectOfType<RoomGameMode>();
        greatBtn = GameObject.Find("Canvas/Button").GetComponent<Button>();
        exitButton = GameObject.Find("Canvas/Btn_Exit").GetComponent<Button>();
        greatBtn.onClick.AddListener(greatRoom.RoomGreatMode);
        exitButton.onClick.AddListener(Exit);
    }
	
	
	void Update () {
		
	}

    public void Exit()
    {
        SceneManager.LoadScene("StartGameUi");
    }
}
