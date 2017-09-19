using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBoom : MonoBehaviour {

    public Text boomNuber;
    public Button boomBtn;
	void Start ()
    {
        boomNuber = transform.Find("Txt_BoomNumber").GetComponent<Text>();
        boomBtn = transform.Find("Img_Boom").GetComponent<Button>();
        boomNuber.text = PlayCharacter.Instance.bag[PropLibry.Instance.propTable["1022"]].ToString();
        boomBtn.onClick.AddListener(BoomOnclick);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BoomOnclick()
    {
        if (PlayCharacter.Instance.bag[PropLibry.Instance.propTable["1022"]]>0)//获取炸弹的数量
        {
            PlayCharacter.Instance.Boom();
        }
    }
}
