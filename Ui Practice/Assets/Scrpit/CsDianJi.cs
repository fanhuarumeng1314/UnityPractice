using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CsDianJi : MonoBehaviour {

    public Button btn;
    Event e = Event.current;
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Print()
    {
       

        if (e.button==1)
        {
            Debug.Log("右键点击");
        }
       
    }
}
