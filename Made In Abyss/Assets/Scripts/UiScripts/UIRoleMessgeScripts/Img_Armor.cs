using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Img_Armor : UIBase {

    public Image armorImg;
    public Sprite armorSprite;
    void Start()
    {
        armorImg = GetComponent<Image>();
        if (PlayCharacter.Instance.nowArmor != null)
        {
            armorSprite = Resources.Load("Photo/" + PlayCharacter.Instance.nowArmor, typeof(Sprite)) as Sprite;
            armorImg.sprite = armorSprite;
        }


    }
    void Update () {
		
	}
}
