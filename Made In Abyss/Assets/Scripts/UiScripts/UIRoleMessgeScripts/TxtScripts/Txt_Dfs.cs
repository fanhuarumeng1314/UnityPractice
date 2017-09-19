using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Txt_Dfs : UIBase
{

    public Text dfsTxt;
	void Start ()
    {
        dfsTxt = GetComponent<Text>();
        PrintDfs();
    }

    public void PrintDfs()
    {
        dfsTxt.text = "当前防御: " + PlayCharacter.Instance.dfs;
    }
}
