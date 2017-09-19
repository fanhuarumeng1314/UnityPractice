using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiTxDelete : MonoBehaviour {

    public float timeSpeed = 0;
    bool isDelet = false;
	void Start ()
    {
        isDelet = true;

    }
	
	
	void Update ()
    {
        if (isDelet)
        {
            timeSpeed += Time.deltaTime;
            if (timeSpeed > 1.8f)
            {
                Debug.Log("删除特效");
                Destroy(gameObject);
            }
        }
        
    }
}
