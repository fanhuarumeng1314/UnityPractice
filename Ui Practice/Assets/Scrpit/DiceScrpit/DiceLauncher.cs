using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceLauncher : MonoBehaviour {

    private void Awake()
    {
        var dice_Bg = Resources.Load("DicePrefeb/Dice_UIRoot") as GameObject;
        var bg = Instantiate(dice_Bg);
        bg.AddComponent<AddDice>();

    }

    void Update () {
		
	}
}
