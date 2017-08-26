using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L4_Tigger : MonoBehaviour {

    public Text victory_Text4;
    public Button victory_Button4;


    void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        victory_Text4.text = "恭喜你通过了海边小村，赶快进入下一关吧！";
        victory_Text4.gameObject.SetActive(true);
        victory_Button4.gameObject.SetActive(true);
    }
}
