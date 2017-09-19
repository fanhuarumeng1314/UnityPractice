using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Txt_Heahl : UIBase
{
    public Text heahlTxt;
	void Start ()
    {
        heahlTxt = GetComponent<Text>();
        PrintHeahl();
    }

    public void PrintHeahl()
    {
        heahlTxt.text = "血量： " + PlayCharacter.Instance.heathly;
    }
}
