using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Img_Bg_Role : UIBase
{

	void Start ()
    {
        Init();

    }
	
	
	void Update () {
		
	}

    public void Init()
    {
        UiManger.Instance.GreatUI<Img_Role>(transform);
        UiManger.Instance.GreatUI<Img_Weapon_Bg>(transform);
        UiManger.Instance.GreatUI<Img_Armor_Bg>(transform);
        UiManger.Instance.GreatUI<Img_Shoes_Bg>(transform);
        var dataBg = UiManger.Instance.GreatUI<Img_Data_Bg>(transform);
        dataBg.transform.localPosition = new Vector3(215,20,0);

        var exit =  UiManger.Instance.GreatUI<Btn_Exit_Messeg>(transform);
        exit.transform.localPosition = new Vector3(384,335,0);

    }
}
