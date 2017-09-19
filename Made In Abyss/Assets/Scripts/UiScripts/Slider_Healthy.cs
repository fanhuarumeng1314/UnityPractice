using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider_Healthy : UIBase {

    public Text heathly;
    public Image heathlyColor;
    public Color32 greenColor = new Color32(59,251,124,255);
    public Color32 redColor = new Color32(251, 59, 59, 255);


    void Start ()
    {
        heathly = transform.Find("Img_Healty_Bg/Txt_Healthy").GetComponent<Text>();
        heathlyColor = transform.Find("Img_Healty_Bg/Img_Heathy").GetComponent<Image>();

    }
	
	
	void Update ()
    {

        ChangeHeathly();
    }

    public void ChangeHeathly()
    {

        heathly.text = PlayCharacter.Instance.heathly.ToString();
        heathlyColor.fillAmount = (float)PlayCharacter.Instance.heathly / PlayCharacter.Instance.heathlyMax;
        if (PlayCharacter.Instance.heathly < 0)
        {
            heathly.text = "0";
            return;
        }
    }
}
