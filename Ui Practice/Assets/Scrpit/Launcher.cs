using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviour {

	void Awake ()
    {
        GameObject managerObj = new GameObject("UiManager");//开始实例化一个管理器
        managerObj.AddComponent<UiManager>();
        managerObj.AddComponent<Props_Table>();
        Props_Table.Instance.Init();
        DontDestroyOnLoad(managerObj);

        GameObject startUi = UiManager.Instance.GreatUI<Page_Bag2>(UiManager.Instance.UIRoot.transform);
	}
}
