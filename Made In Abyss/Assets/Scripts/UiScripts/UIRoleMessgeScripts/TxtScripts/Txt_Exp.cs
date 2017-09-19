using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Txt_Exp : UIBase {

    public Text expTxt;
	void Start ()
    {
        expTxt = GetComponent<Text>();
        PrintExp();
    }

    public void PrintExp()
    {
        expTxt.text = "经验： " + PlayCharacter.Instance.exps;
    }
}
