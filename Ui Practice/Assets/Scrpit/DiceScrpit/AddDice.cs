using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDice : MonoBehaviour {

    public RectTransform rect;
    private void Awake()
    {
        rect = gameObject.transform.Find("Dice_Bg/Scroll View/Viewport/Content").GetComponent<RectTransform>();
        var dice_1 = Resources.Load("DicePrefeb/Com_Dice_1")as GameObject;
    }


    void Update () {
		
	}
}
