using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Img_Weapon_Bg : UIBase {

    public GameObject weapon;

	void Start () {

        weapon = transform.Find("Img_Weapon").gameObject;
        weapon.AddComponent<Img_Weapon>();
    }
	
	
	void Update () {
		
	}
}
