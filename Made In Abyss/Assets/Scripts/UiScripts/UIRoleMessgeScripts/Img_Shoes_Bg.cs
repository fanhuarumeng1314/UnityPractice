using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Img_Shoes_Bg : UIBase {

    public GameObject shoes;
	void Start ()
    {
        shoes = transform.Find("Img_Shoes").gameObject;
        shoes.AddComponent<Img_Shoes>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
