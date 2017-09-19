using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManger : MonoBehaviour {

    public static UiManger Instance;
    public GameObject UIRoot;
    public GameObject canvasUi;

    private void Awake()
    {
        Instance = this;
        Init();
        Debug.Log("UIRoot生成");
    }
    void Start () {
		
	}
	
	
	void Update () {
		
	}
    public void Init()
    {
        UIRoot = Resources.Load("UIPrefeb/StartGameUi/UIRoot") as GameObject;
        UIRoot = Instantiate(UIRoot);
        DontDestroyOnLoad(UIRoot);
    }

    public GameObject GreatUI<T>(Transform transParent) where T : UIBase
    {
        var uiName = typeof(T).Name;
        GameObject tmp_UiObj = Resources.Load("UIPrefeb/StartGameUi/" + uiName) as GameObject;

        if (tmp_UiObj == null)
        {
            Debug.Log("读取异常");
            return null;
        }
        tmp_UiObj = Instantiate(tmp_UiObj, transParent);
        tmp_UiObj.name = uiName;

        var menuScript = tmp_UiObj.AddComponent<T>();

        return tmp_UiObj;
    }
}
