using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTwo : MonoBehaviour {

    public Image skillMask;
    public Button skillMaskBtn;

    void Start()
    {
        skillMask = transform.Find("Btn_SkliiTwo_Mask").GetComponent<Image>();
        skillMaskBtn = transform.Find("Btn_SkliiTwo_Mask").GetComponent<Button>();
        skillMaskBtn.onClick.AddListener(BtnSkillOnclik);//添加点击图片释放技能的监听事件
    }

    void Update()
    {

    }

    public void ChangeSkillTwoMask(float value)
    {
        skillMask.fillAmount = value;
    }

    public void BtnSkillOnclik()
    {
        if (skillMask.fillAmount == 0)
        {
            PlayCharacter.Instance.BattleSkillTwo();
            PlayCharacter.Instance.anmitorTimeTwo = 45;
        }

    }
}
