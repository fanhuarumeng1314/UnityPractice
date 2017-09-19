using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Img_Weapon : UIBase {

    public Image weaponImg;
    public Sprite weaponSprite;
	void Start ()
    {
        weaponImg = GetComponent<Image>();
        if (PlayCharacter.Instance.nowWeapon!=null)
        {
            weaponSprite = Resources.Load("Photo/" + PlayCharacter.Instance.nowWeapon, typeof(Sprite)) as Sprite;
            Debug.Log("Photo/" + PlayCharacter.Instance.nowWeapon + "  文件路径");
            weaponImg.sprite = weaponSprite;
        }
        

    }
	
	
	void Update ()
    {
	    	
	}


}
