using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomGameMode : MonoBehaviour {

    public int Checkpoint = 0;//最高层数
    public int nowCheckpoint = 0;//当前层数
    public char[,] maps;
    public List<GameObject> mapObj = new List<GameObject>();
    public bool isDir = true;

    public GameObject loadingUi;
    #region//声明物品名称
    public GameObject floor;//地板
    public GameObject stone;//石头
    public GameObject gate;//门
    public GameObject key;//钥匙
    public GameObject Gold;//金币
    public GameObject monster;//怪物
    public GameObject starisUp;//上的楼梯
    public GameObject starisDown;//下的楼梯
    public GameObject Weapon;//武器
    public GameObject DunPai;//盾牌
    public GameObject Ring;//戒指
    public GameObject Box;//宝箱
    public GameObject Boom;//炸弹
    public GameObject aiDethTx;//怪物死亡特效
    #endregion

    public bool isGreat = true;

    public Vector3 nextDown;//下去的初始位置
    public Vector3 nextUp;//上去的初始位置

    public int addVaule;

    void Awake()
    {
        #region//读取物品
        floor = Resources.Load("MapPrefeb/Floor") as GameObject;
        stone = Resources.Load("MapPrefeb/Stone") as GameObject;
        gate = Resources.Load("MapPrefeb/Gate") as GameObject;
        key = Resources.Load("MapPrefeb/Key") as GameObject;
        Gold = Resources.Load("Gold") as GameObject;
        monster = Resources.Load("Monster") as GameObject;
        starisUp = Resources.Load("StairsUp") as GameObject;
        starisDown = Resources.Load("StairsDown") as GameObject;
        Weapon = Resources.Load("PropsPrefeb/EquipmentPrefeb/sword2h15_green") as GameObject;
        DunPai = Resources.Load("PropsPrefeb/EquipmentPrefeb/shield01_beige") as GameObject;
        Ring = Resources.Load("PropsPrefeb/EquipmentPrefeb/ring6_platinum_amethyst") as GameObject;
        Box = Resources.Load("PropsPrefeb/SpecialPropPrefeb/woodenchest_big4") as GameObject;
        Boom = Resources.Load("PropsPrefeb/SpecialPropPrefeb/Boom") as GameObject;
        aiDethTx = Resources.Load("AiDethTx") as GameObject;
        #endregion

    }

    #region//关卡切换的函数

    public void RoomGreatMode()//房间生成
    {
        if (isGreat)
        {
            addVaule += 2;
            isGreat = false;
            ClearSence();
            maps = RoomCreate.Instance.Maps(19+addVaule,19+addVaule);
            
            StartCoroutine(PrintFloor());

        }
       
    }

    IEnumerator Print()//打印物品
    {
        for (int i = 0; i < (19+addVaule); i++)
        {
            for (int j = 0; j < (19+addVaule); j++)
            {
                #region//如果是门，设置门的方向
                if (maps[i, j] == 'g')
                {
                    #region//备用防止超出数组范围
                    //if (i - 1 < 0)
                    //{
                    //    if (maps[i + 1, j] == ' ')
                    //    {
                    //        var tmp_Gate = Instantiate(gate, new Vector3(i, 0, j), Quaternion.identity) as GameObject;
                    //        mapObj.Add(tmp_Gate);
                    //        tmp_Gate.name = "tmp_Gate" + "x-1";
                    //    }
                    //}
                    //else if (i + 1 > 48)
                    //{
                    //    if (maps[i - 1, j] == ' ')
                    //    {
                    //        var tmp_Gate = Instantiate(gate, new Vector3(i, 0, j), Quaternion.identity) as GameObject;
                    //        mapObj.Add(tmp_Gate);
                    //        tmp_Gate.name = "tmp_Gate" + "X+1";
                    //    }
                    //}
                    //else if (j - 1 < 0)
                    //{
                    //    if (maps[i, j + 1] == ' ')
                    //    {
                    //        var tmp_Gate = Instantiate(gate, new Vector3(i, 0, j), Quaternion.Euler(0, 90, 0)) as GameObject;
                    //        mapObj.Add(tmp_Gate);
                    //        tmp_Gate.name = "tmp_Gate" + "Y-1";
                    //    }
                    //}
                    //else if (j+1>48)
                    //{
                    //    if (maps[i, j - 1] == ' ')
                    //    {
                    //        var tmp_Gate = Instantiate(gate, new Vector3(i, 0, j), Quaternion.Euler(0, 90, 0)) as GameObject;
                    //        mapObj.Add(tmp_Gate);
                    //        tmp_Gate.name = "tmp_Gate" + "Y+1";
                    //    }
                    //}
                    #endregion
                    if (maps[i - 1, j] == ' ' && maps[i + 1, j] == ' ')
                    {
                        var tmp_Gate = Instantiate(gate, new Vector3(i, 0, j), Quaternion.identity) as GameObject;
                        mapObj.Add(tmp_Gate);
                        tmp_Gate.name = "tmp_Gate" + nowCheckpoint + " Now";
                    }
                    else if (maps[i, j - 1] == ' ' && maps[i, j + 1] == ' ')
                    {
                        var tmp_Gate = Instantiate(gate, new Vector3(i, 0, j), Quaternion.Euler(0, 90, 0)) as GameObject;
                        mapObj.Add(tmp_Gate);
                        tmp_Gate.name = "tmp_Gate" + nowCheckpoint + " Now";
                    }
                }
                #endregion

                if (maps[i, j] == 'k')
                {
                    var tmp_Key = Instantiate(key, new Vector3(i, 0.5f, j), Quaternion.Euler(0, 90, 0)) as GameObject;
                    mapObj.Add(tmp_Key);
                    tmp_Key.name = "Key";
                    tmp_Key.AddComponent<KeyTrigger>();
                    mapObj.Add(tmp_Key);
                }

                if (maps[i, j] == '6')
                {
                    nextDown = new Vector3(i, 1, j);
                }

                if (maps[i, j] == '9')
                {
                    nextUp = new Vector3(i, 1, j);
                }

                if (maps[i, j] == 'o')
                {

                }

                if (maps[i, j] == 'j')//金币
                {
                    var tmp_Gold = Instantiate(Gold, new Vector3(i, 0.5f, j), Quaternion.identity);
                    tmp_Gold.AddComponent<GoldTrigger>();
                    mapObj.Add(tmp_Gold);
                }

                if (maps[i, j] == 'm')//怪物
                {
                    GameObject monsterPrefeb = Resources.Load("MonsterPrefeb/skeletonbaseFour") as GameObject;//读取怪物模型
                    var tmp_Monster = Instantiate(monsterPrefeb, new Vector3(i, 0.5f, j), Quaternion.identity);
                    mapObj.Add(tmp_Monster);
                }

                if (maps[i, j] == 'p')//上楼梯
                {
                    var tmp_StarisUp = Instantiate(starisUp, new Vector3(i, 0.14f, j), Quaternion.identity);
                    tmp_StarisUp.AddComponent<StairsUpTrigger>();
                    mapObj.Add(tmp_StarisUp);
                }

                if (maps[i, j] == 'o')//下楼梯
                {
                    var tmp_StarisDown = Instantiate(starisDown, new Vector3(i, 0.14f, j), Quaternion.identity);
                    tmp_StarisDown.AddComponent<StairsDownTrigger>();
                    mapObj.Add(tmp_StarisDown);
                }

                if (maps[i, j] == 'b')//宝箱
                {
                    var tmp_Box = Instantiate(Box, new Vector3(i, 0.35f, j), Quaternion.identity);
                    tmp_Box.AddComponent<BoxTrigger>();
                    mapObj.Add(tmp_Box);
                }
            }

            yield return 0;
        }
        yield return 0;
        if (isDir)
        {
        }
        else
        {
        }
        isGreat = true;
        yield return null;
    }

    IEnumerator PrintFloor()//先打印地板
    {

        for (int i = 0; i < (19 + addVaule); i++)
        {
            for (int j = 0; j < (19+addVaule); j++)
            {
                var tmp_Floor = Instantiate(floor, new Vector3(i, 0, j), Quaternion.identity) as GameObject;
                mapObj.Add(tmp_Floor);
                tmp_Floor.name = "tmp_Floor" + Checkpoint;
                if (maps[i, j] == ' ')
                {
                    var tmp_Stone = Instantiate(stone, new Vector3(i, 0.2f, j), Quaternion.identity) as GameObject;
                    mapObj.Add(tmp_Stone);

                    tmp_Stone.name = "tmp_Stone" + Checkpoint;
                }
            }
            yield return 0;
        }
        StartCoroutine(Print());
        yield return null;
    }

    void ClearSence()//清空场景所有物体
    {
        if (mapObj.Count > 0)
        {
            for (int i = 0; i < mapObj.Count; i++)
            {
                Destroy(mapObj[i]);

            }
            mapObj.Clear();
        }


    }
    #endregion
}
