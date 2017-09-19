using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Txt_Conut : UIBase
{
    public Text countTxt;
	
	void Start ()
    {
        countTxt = GetComponent<Text>();
        PrintCount();
    }
	
	void Update () {
		
	}

    public void PrintCount()
    {
        if (PlayGameMode.Instance == null)
        {
            countTxt.text = "当前地图： 城镇";
        }
        else if (PlayGameMode.Instance.nowCheckpoint > 0)
        {
            countTxt.text = "当前地图： " + PlayGameMode.Instance.nowCheckpoint;
        }
        else
        {
            countTxt.text = "当前地图： 城镇";
        }
    }
}
