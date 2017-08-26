using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L1_TmpUi2 : MonoBehaviour {

    public Text tips;
    bool isTriger = false;
	void Start () {
		
	}
	

	void Update ()
    {
        if (isTriger == true)
        {
            Destroy(tips.gameObject,3);
            Destroy(gameObject,3);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        tips.text = "前方有一堵高墙，靠近箱子按下<color=#FF0000FF>K</color>键，找寻过去的方法吧";
        tips.gameObject.SetActive(true);
        isTriger = true;
    }
}
