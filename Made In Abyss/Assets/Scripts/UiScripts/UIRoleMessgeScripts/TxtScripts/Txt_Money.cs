using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Txt_Money : UIBase
{
    public Text moneyTxt;
	void Start ()
    {
        moneyTxt = GetComponent<Text>();
        PrintMoney();
    }

    public void PrintMoney()
    {
        moneyTxt.text = "金币： " + PlayCharacter.Instance.money;
    }
}
