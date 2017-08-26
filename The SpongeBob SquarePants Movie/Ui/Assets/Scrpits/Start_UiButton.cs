using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_UiButton : MonoBehaviour {

    public Transform guKa;
    bool isMove = false;
	void Start () {
		
	}
	
	
	void Update ()
    {
        if (isMove)
        {
            if (guKa.localPosition.x < -70)
            {
                guKa.localPosition += new Vector3(3, 0, 0);
            }

            if (guKa.localPosition.x>=-70)
            {
                isMove = false;
            }
        }	
	}
    public void OnClick()//-42截至
    {
        isMove = true;
    }
}
