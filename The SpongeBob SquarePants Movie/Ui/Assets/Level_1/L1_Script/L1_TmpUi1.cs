using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L1_TmpUi1 : MonoBehaviour {

    public Text tips;
	void Start ()
    {
        tips.text = "嗨，海绵宝宝，欢迎来到异世界，你可以使用<color=#FF0000FF>AD</color>键进行移动，<color=#FF0000FF>空格键</color>进行跳跃，赶紧探索世界吧" ;
        Destroy(tips.gameObject,3);
        Destroy(gameObject,3);
    }
	
	// Update is called once per frame
	void Update ()
    {
        
            
        	
	}
}
