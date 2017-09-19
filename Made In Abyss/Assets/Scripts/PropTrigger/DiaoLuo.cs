using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaoLuo : MonoBehaviour {


    public static DiaoLuo Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start ()
    {

    }
	
	
	void Update () {
		
	}



    public int Probability()//概率计算
    {
        int seed = Random.Range(0, 100);

        if (seed >= 0 && seed < 10)//道具库随机道具添加
        {
            return 1;
        }
        else if (seed >= 10 && seed < 20)//药水
        {
            return 2;
        }
        else
        {
            return 4;//金币
        }
    }

    public Prop GetProp(int propType)//获取道具物体的引用
    {
        Prop tmp_Prop = null;
        if (propType == 1 || propType == 2)
        {
            int propNumber = Random.Range(0, PropLibry.Instance.propTable.Count);
            foreach (var prop in PropLibry.Instance.propTable)
            {
                propNumber--;
                tmp_Prop = prop.Value;
                if (propNumber == 0)
                {
                    break;
                }
            }
        }
        else
        {
            tmp_Prop = null;
        }

        return tmp_Prop;
    }


    public GameObject GetScprits(Prop tmp_Prop)
    {
        GameObject newProp = null;
        GameObject tmp_NowProp = null;
        if (tmp_Prop != null)
        {
            System.Type t = System.Type.GetType(tmp_Prop.ScriptName);
            Debug.Log("=====" + t.Name);
            newProp = Resources.Load(tmp_Prop.RoutePrefeb) as GameObject;
            tmp_NowProp = Instantiate(newProp);
            tmp_NowProp.transform.parent = null;
            tmp_NowProp.AddComponent(t);
        }
        else
        {
            newProp = Resources.Load("Gold") as GameObject;
            tmp_NowProp = Instantiate(newProp);
            tmp_NowProp.transform.parent = null;
            tmp_NowProp.AddComponent<GoldTrigger>();
        }


        return tmp_NowProp;
    }

    public GameObject GreatProp()
    {
        int number = 0;
        Prop numberProp = null;
        GameObject nowProp = null;
        number = Probability();
        numberProp = GetProp(number);
        nowProp = GetScprits(numberProp);

        

        return nowProp;
    }


}
