using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Img_Data_Bg : UIBase {

	
	void Start ()
    {
        InitText();

    }
	
	
	void Update () {
		
	}

    public void InitText()
    {
        var txtCount =  UiManger.Instance.GreatUI<Txt_Conut>(transform);
        txtCount.transform.localPosition = new Vector3(13,250,0);

        var txtName = UiManger.Instance.GreatUI<Txt_Name>(transform);
        txtName.transform.localPosition = new Vector3(-69.8f, 179.7f, 0);

        var txtHeahl =  UiManger.Instance.GreatUI<Txt_Heahl>(transform);
        txtHeahl.transform.localPosition = new Vector3(-70,138.5f,0);

        var txtAtk = UiManger.Instance.GreatUI<Txt_Atk>(transform);
        txtAtk.transform.localPosition = new Vector3(-70.1f,97,0);

        var txtDfs = UiManger.Instance.GreatUI<Txt_Dfs>(transform);
        txtDfs.transform.localPosition = new Vector3(-70f, 55.9f, 0);

        var txtExp = UiManger.Instance.GreatUI<Txt_Exp>(transform);
        txtExp.transform.localPosition = new Vector3(-70.5f, 13.8f,0);

        var txtLevel = UiManger.Instance.GreatUI<Txt_Level>(transform);
        txtLevel.transform.localPosition = new Vector3(-70.4f, -27.1f, 0);

        var txtMoney = UiManger.Instance.GreatUI<Txt_Money>(transform);
        txtMoney.transform.localPosition = new Vector3(-69.9f, -67.7f, 0);
    }
}
