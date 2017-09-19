using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Img_Shoes : UIBase
{

    public Image shoesImg;
    public Sprite shoesSprite;
    void Start()
    {
        shoesImg = GetComponent<Image>();
        if (PlayCharacter.Instance.nowRing != null)
        {
            shoesSprite = Resources.Load("Photo/" + PlayCharacter.Instance.nowRing, typeof(Sprite)) as Sprite;
            shoesImg.sprite = shoesSprite;
        }


    }
    void Update () {
		
	}
}
