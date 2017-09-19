using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Img_Armor_Bg : UIBase {

    public GameObject armor;
	void Start ()
    {
        armor = transform.Find("Img_Armor").gameObject;
        armor.AddComponent<Img_Armor>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
