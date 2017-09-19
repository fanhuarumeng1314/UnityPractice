using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Txt_Atk : UIBase
{

    public Text atkTxt;
	void Start ()
    {
        atkTxt = GetComponent<Text>();
        PrintAtk();
    }
	
	
	void Update () {
		
	}

    public void PrintAtk()
    {
        atkTxt.text = "攻击力： " + PlayCharacter.Instance.atk;
    }
}
