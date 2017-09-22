using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Page_Bag2 : UIBase
{

	void Awake()
    {
        Button startBut = gameObject.transform.Find("Img_HuaJi/Btn_Start").GetComponent<Button>();
        startBut.onClick.AddListener(OpenMuen);

	}

    public void OpenMuen()
    {
        var packPage = UiManager.Instance.GreatUI<Page_Bag>(UiManager.Instance.UIRoot.transform);//实例化菜单
    }
}
