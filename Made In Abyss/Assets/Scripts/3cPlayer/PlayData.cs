using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayState
{
    None,
    AddAtk,//增加攻击
    AddDfs,//增加防御
    AddHeal,//加血

}


public class PlayData : MonoBehaviour {

    public string Name = "DaLin";
    public int heathly = 100;
    public int heathlyMax = 100;
    public int level;
    public int exps;
    public List<PlayState> states = new List<PlayState>();
    public int money =0;
    public int atk = 30;
    public int dfs = 15;
    public int keyNumber;
    public Dictionary<Prop, int> bag = new Dictionary<Prop, int>();

    public string nowWeapon;//武器
    public string nowRing;//戒指
    public string nowArmor;//玩家当前身上的装备

    public void ReduceHeathly(int damage)//血量减少
    {
        heathly -= damage;
    }

    public void Addlevel()//等级增加
    {
        if (exps>=100)
        {
            exps -= 100;
            level++;
            atk++;
            dfs++;
            heathly += 50;
            if (heathly>heathlyMax)
            {
                heathly = heathlyMax;
            }
        }
        
    }

    public void AddExps(int exp)//经验增加
    {
        exps = exps + exp;
        Addlevel();
    }

    public void AddMoney(int tmp_money)//金币增加
    {
        money += tmp_money;
    }
    public void AddKey()//增加钥匙数量
    {
        keyNumber++;
    }

   


}
