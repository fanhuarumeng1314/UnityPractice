using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L3_Tigger : MonoBehaviour {

    public Text victory_Text3;
    public Button victory_Button3;
	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        victory_Text3.text = "恭喜你通过了尖刺山脉，赶快开始下一关的冒险吧！";
        victory_Button3.gameObject.SetActive(true);
        victory_Text3.gameObject.SetActive(true);
    }
}
