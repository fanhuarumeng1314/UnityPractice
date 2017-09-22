using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour {

    public static UiManager Instance;
    public GameObject UIRoot;

    public Dictionary<string, UIBase> uiScrpits = new Dictionary<string, UIBase>();//保存Ui的脚本

    public void Awake()
    {
        Instance = this;
        Init();
    }

    public void Init()
    {

        var  res = Resources.Load("Interface/UIRoot") as GameObject;
        if (res == null)
        {
            Debug.Log("异常");
            return;
        }

        UIRoot = Instantiate(res) as GameObject;
        UIRoot.name = "UIRoot";
        DontDestroyOnLoad(UIRoot);
    }

    public T GetUi<T>() where T: UIBase
    {
        var uiName = typeof(T).Name;

        return uiScrpits[uiName] as T;

    }

    public GameObject GreatUI<T>(Transform transParent) where T : UIBase
    {
        var uiName = typeof(T).Name;  
        GameObject tmp_UiObj = Resources.Load("Interface/" + uiName) as GameObject;

        if (tmp_UiObj==null)
        {
            Debug.Log("读取异常");
            return null;
        }

        tmp_UiObj = Instantiate(tmp_UiObj, transParent);
        tmp_UiObj.name = uiName;
        tmp_UiObj.transform.localPosition = Vector3.zero;

        var menuScript = tmp_UiObj.AddComponent<T>();
        if (!uiScrpits.ContainsKey(uiName))
        {
            uiScrpits.Add(uiName, menuScript);
        }
        
        return tmp_UiObj;
    }

}
