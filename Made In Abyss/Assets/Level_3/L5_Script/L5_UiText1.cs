using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L5_UiText1 : MonoBehaviour {

    public Text victoyr_Text5;
    public Button victory_Button5;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        victoyr_Text5.text = "<color=#FF0000FF>恭喜你已经完成了异世界的冒险之旅！</color>";
        victoyr_Text5.gameObject.SetActive(true);
        victory_Button5.gameObject.SetActive(true);
    }
}