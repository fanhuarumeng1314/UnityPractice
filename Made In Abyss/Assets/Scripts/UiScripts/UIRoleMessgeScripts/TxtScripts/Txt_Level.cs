using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Txt_Level : UIBase
{
    public Text levelTxt;
	void Start ()
    {
        levelTxt = GetComponent<Text>();
        PrintLevel();
    }

    public void PrintLevel()
    {
        levelTxt.text = "等级： " + PlayCharacter.Instance.level;
    }
}
