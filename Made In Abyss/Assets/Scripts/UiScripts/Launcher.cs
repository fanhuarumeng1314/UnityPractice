using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {

    public static Launcher Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            GameObject managerObj = new GameObject("UiManager");//开始实例化一个管理器
            managerObj.AddComponent<UiManger>();
            managerObj.AddComponent <PropLibry>();//添加道具库
            managerObj.AddComponent<MonsterLibry>();//添加怪物库
            managerObj.AddComponent<DiaoLuo>();//添加掉落规则

            GameObject tmp_Men = UiManger.Instance.GreatUI<Canvas_Menu>(UiManger.Instance.UIRoot.transform);
            GameObject tmp_Heathly = UiManger.Instance.GreatUI<Slider_Healthy>(tmp_Men.transform);
            UiManger.Instance.GreatUI<Img_StartTS_Bg>(tmp_Men.transform);

            DontDestroyOnLoad(tmp_Men);
            DontDestroyOnLoad(managerObj);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

}
