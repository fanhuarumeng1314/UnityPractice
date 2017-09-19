﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillOne : MonoBehaviour {

    public Image skillMask;
    public Button skillMaskBtn;

    void Start ()
    {
        skillMask = transform.Find("Btn_SkliiOne_Mask").GetComponent<Image>();
        skillMaskBtn = transform.Find("Btn_SkliiOne_Mask").GetComponent<Button>();
        skillMaskBtn.onClick.AddListener(BtnSkillOnclik);//添加点击图片释放技能的监听事件
    }
	
	void Update ()
    {
		
	}

    public void ChangeMask(float value)
    {
        skillMask.fillAmount = value;
    }

    public void BtnSkillOnclik()
    {
        if (skillMask.fillAmount==0)
        {
            PlayCharacter.Instance.BattleSkillOne();
            PlayCharacter.Instance.anmitorTimeOne = 45;
        }
        
    }
}
