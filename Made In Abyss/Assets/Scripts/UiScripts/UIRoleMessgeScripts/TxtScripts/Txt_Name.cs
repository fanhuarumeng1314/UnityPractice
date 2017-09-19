using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Txt_Name : UIBase
{
    public Text nameTxt;
	void Start ()
    {
        nameTxt = GetComponent<Text>();
        PrintName();
    }

    public void PrintName()
    {
        nameTxt.text = "钥匙数量： " + PlayCharacter.Instance.keyNumber;
    }
	
}
