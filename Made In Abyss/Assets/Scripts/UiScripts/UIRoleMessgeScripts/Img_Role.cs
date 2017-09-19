using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Img_Role : UIBase
{
    public Image messgeBg;
    public Sprite bgImg;

    void Start ()
    {
        messgeBg = GetComponent<Image>();
        bgImg = Resources.Load("Photo/Player", typeof(Sprite)) as Sprite;
        messgeBg.sprite = bgImg;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
