using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        var ping = other.gameObject.GetComponent<SuDaTrigger>();//获取瓶子上的脚本
        Debug.Log(other.name);
        if (ping)
        {
            ping.isVolcanoUpper = true;
            Debug.Log("进入触发区域");
        }
        

    }

    private void OnTriggerExit(Collider other)
    {
        var ping = other.gameObject.GetComponent<SuDaTrigger>();//获取瓶子上的脚本
        if (ping)
        {
            ping.isVolcanoUpper = false;
            Debug.Log("退出触发区域");
        }
       
    }
}
