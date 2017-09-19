using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayGameMode : MonoBehaviour {

    public int Checkpoint = 0;//最高层数
    public int nowCheckpoint = 0;//当前层数

    public int widthMin = 19;
    public int lenghtMin =19;

    public static PlayGameMode Instance;
    public PlayCharacter character;
    public char[,] maps;
    public List<GameObject> mapObj = new List<GameObject>();
    public List<char[,]> CheckpointMaps = new List<char[,]>();//封装所有到达关卡的地图
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
    public GameObject roomEdge;//房间边缘物体
    public GameObject floorTwo;
    public GameObject floorThree;
    public GameObject stoneTwo;
    public GameObject stoneThree;
    public GameObject gateTwo;
    public GameObject gateThree;
#endregion

    public Vector3 nextDown;//下去的初始位置
    public Vector3 nextUp;//上去的初始位置

    public AudioClip coldClip;
   

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
#region//读取物品
            character = GameObject.FindObjectOfType<PlayCharacter>();
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
            roomEdge = Resources.Load("MapPrefeb/StoneNull") as GameObject;
            floor = Resources.Load("MapPrefeb/Floor") as GameObject;//地板
            floorTwo = Resources.Load("MapPrefeb/Floor2") as GameObject;
            floorThree = Resources.Load("MapPrefeb/Floor3") as GameObject;
            stone = Resources.Load("MapPrefeb/Stone") as GameObject;//石头
            stoneTwo = Resources.Load("MapPrefeb/Stone2") as GameObject;
            stoneThree = Resources.Load("MapPrefeb/Stone3") as GameObject;
            gate = Resources.Load("MapPrefeb/Gate") as GameObject;//门
            gateTwo = Resources.Load("MapPrefeb/Gate2") as GameObject;
            gateThree = Resources.Load("MapPrefeb/Gate3") as GameObject;
            #endregion
        }

    }

    void Start()
    {
        coldClip = Resources.Load("AudioClicp/酷游戏音3_爱给网_aigei_com", typeof(AudioClip)) as AudioClip;
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            character.MoveStart(nextDown);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            character.MoveStart(nextUp);
        }
    }

#region//关卡切换的函数
    public void AddChenkPoint()//去下一关
    {
        loadingUi = Resources.Load("MapPrefeb/CsLoading") as GameObject;
        var tmp_Ui = Instantiate(loadingUi);
        tmp_Ui.AddComponent<LoadingUi>();

        character.isMove = false;
        isDir = true;
        character.MoveStart(new Vector3(30,30,30));
        if (nowCheckpoint == Checkpoint)//如果现在的最高层数与现在的层数相当，说明进入了新的高度，
        {
                   
            CheckpointMaps[nowCheckpoint - 1] = CopyMaps(maps);
            nowCheckpoint++;
            Checkpoint++;
            ClearSence();

            widthMin = 19 + 2 * nowCheckpoint;
            lenghtMin = 19 + 2 * nowCheckpoint;//更改下一关的关卡大小

            maps = RoomCreate.Instance.Maps(widthMin,lenghtMin);
            CheckpointMaps.Add(CopyMaps(maps));//保存下一关的地图
            StartCoroutine(PrintFloor());
            Debug.Log("去下一关地图数量：  " + CheckpointMaps.Count + "   当前关卡：  " + nowCheckpoint + "最高关卡：  " + Checkpoint);
        }
        else
        {
            widthMin = 19 + 2 * nowCheckpoint;
            lenghtMin = 19 + 2 * nowCheckpoint;//改回本关的大小，方便复制数组
            CheckpointMaps[nowCheckpoint-1] = CopyMaps(maps);//保存本关场景

            nowCheckpoint++;

            widthMin = 19 + 2 * nowCheckpoint;
            lenghtMin = 19 + 2 * nowCheckpoint;//改回本关的大小

            ClearSence();
            maps = CheckpointMaps[nowCheckpoint-1];
            StartCoroutine(PrintFloor());
        }  
    }

    public void StartScene()//初始加载场景
    {
        character.MoveStart(new Vector3(30, 30, 30));
        isDir = true;
        nowCheckpoint++;
        Checkpoint++;
        widthMin = 19 + 2 * Checkpoint;
        lenghtMin = 19 + 2 * Checkpoint;
        maps = RoomCreate.Instance.Maps(widthMin,lenghtMin );
        CheckpointMaps.Add(maps);//保存本层的地图
        StartCoroutine(PrintFloor());

    }

    public void ReduceCheckPoint()//回到上一关
    {
        loadingUi = Resources.Load("MapPrefeb/CsLoading") as GameObject;
        var tmp_Ui = Instantiate(loadingUi);
        tmp_Ui.AddComponent<LoadingUi>();

        character.MoveStart(new Vector3(3, 3, 3));
        widthMin = 19 + 2 * nowCheckpoint;
        lenghtMin = 19 + 2 * nowCheckpoint;//改回本关的大小，方便复制数组
        CheckpointMaps[nowCheckpoint-1] = CopyMaps(maps);//更新场景
        nowCheckpoint--;
        if (nowCheckpoint >=1)
        {
            isDir = false;
            ClearSence();

            widthMin = 19 + 2 * nowCheckpoint;
            lenghtMin = 19 + 2 * nowCheckpoint;//改回本关的大小，方便复制数组

            maps = CheckpointMaps[nowCheckpoint-1];

            StartCoroutine(PrintFloor());
        }
        else
        {
            Debug.Log(CheckpointMaps.Count + "地图数量"+"   回初始场景");
            ClearSence();
            SceneManager.LoadScene("StartGame");
        }
        Debug.Log("回到上一关");
    }

    public void SetCheckPoint(int Vaule)//改变关卡
    {
        nowCheckpoint = Vaule;
        ClearSence();
        maps = CheckpointMaps[nowCheckpoint - 1];
        StartCoroutine(PrintFloor());
    }

    public void LoadLevel()//重新进入地下城场景加载初始关卡
    {
        if (CheckpointMaps.Count > 0)//如果有地图被保存，直接加载第第一层
        {
            nowCheckpoint++;
            maps =CheckpointMaps[0];
            StartCoroutine(PrintFloor());
        }
        else
        {
            StartScene();
        }
    }

    public char[,] CopyMaps(char[,] maps)//复制地图
    {
        char[,] tmp_Maps = new char[widthMin,lenghtMin];
        for (int i = 0; i < widthMin; i++)
        {
            for (int j = 0; j < lenghtMin; j++)
            {
                tmp_Maps[i, j] = maps[i, j];
            }
        }

        return tmp_Maps;
    }

    IEnumerator Print()//打印物品
    {
        for (int i=0;i<widthMin;i++)
        {
            for (int j=0;j<lenghtMin;j++)
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
                        GameObject tmp_Gate = null;
                        if (nowCheckpoint <= 3)
                        {
                            tmp_Gate = Instantiate(gate, new Vector3(i, 0.2f, j), Quaternion.identity) as GameObject;
                        }
                        else if (nowCheckpoint > 3 && nowCheckpoint <= 6)
                        {
                            tmp_Gate = Instantiate(gateTwo, new Vector3(i, 0.2f, j), Quaternion.identity) as GameObject;
                        }
                        else
                        {
                            tmp_Gate = Instantiate(gateThree, new Vector3(i, 0.2f, j), Quaternion.identity) as GameObject;
                        }
                        mapObj.Add(tmp_Gate);
                        tmp_Gate.name = "tmp_Gate" + nowCheckpoint+" Now";
                    }
                    else if (maps[i, j - 1] == ' ' && maps[i, j + 1] == ' ')
                    {
                        GameObject tmp_Gate = null;
                        if (nowCheckpoint <= 3)
                        {
                            tmp_Gate = Instantiate(gate, new Vector3(i, 0.2f, j), Quaternion.Euler(0, 90, 0)) as GameObject;
                        }
                        else if (nowCheckpoint > 3 && nowCheckpoint <= 6)
                        {
                            tmp_Gate = Instantiate(gateTwo, new Vector3(i, 0.2f, j), Quaternion.Euler(0, 90, 0)) as GameObject;
                        }
                        else
                        {
                            tmp_Gate = Instantiate(gateThree, new Vector3(i, 0.2f, j), Quaternion.Euler(0, 90, 0)) as GameObject;
                        }
                        mapObj.Add(tmp_Gate);
                        tmp_Gate.name = "tmp_Gate" + nowCheckpoint + " Now";
                    }
                }
                #endregion

                if (maps[i,j]=='k')
                {
                    var tmp_Key = Instantiate(key, new Vector3(i, 0.5f, j), Quaternion.Euler(0, 90, 0)) as GameObject;
                    mapObj.Add(tmp_Key);
                    tmp_Key.name = "Key";
                    tmp_Key.AddComponent<KeyTrigger>();
                    mapObj.Add(tmp_Key);
                }

                if (maps[i,j]=='6')
                {
                    nextDown = new Vector3(i,1,j);
                }

                if (maps[i,j]=='9')
                {
                    nextUp = new Vector3(i,1,j);
                }

                if (maps[i,j]=='o')
                {

                }

                if (maps[i,j]=='j')//金币
                {
                    var tmp_Gold = Instantiate(Gold,new Vector3(i,0.5f,j),Quaternion.identity);
                    tmp_Gold.AddComponent<GoldTrigger>();
                    mapObj.Add(tmp_Gold);
                }

                if (maps[i,j]=='m')//怪物
                {
                    var tmp_moster = Init();
                    if (tmp_moster == null)
                    {
                        Debug.Log("读取怪物模型失败，重新测试");
                    }
                    GameObject monsterPrefeb = Resources.Load(tmp_moster.Route) as GameObject;//读取怪物模型
                    var tmp_Monster = Instantiate(monsterPrefeb, new Vector3(i, 0.5f, j), Quaternion.identity);
                    var aiCharact =  tmp_Monster.AddComponent<AiCharacter>();
                    aiCharact.Init(tmp_moster);//初始化怪物属性
                    mapObj.Add(tmp_Monster);
                }

                if (maps[i,j] == 'p')//上楼梯
                {
                    var tmp_StarisUp = Instantiate(starisUp, new Vector3(i, 0.14f, j),Quaternion.identity);
                    tmp_StarisUp.AddComponent<StairsUpTrigger>();
                    mapObj.Add(tmp_StarisUp);
                }

                if (maps[i,j]=='o')//下楼梯
                {
                    var tmp_StarisDown = Instantiate(starisDown, new Vector3(i, 0.14f, j),Quaternion.identity);
                    tmp_StarisDown.AddComponent<StairsDownTrigger>();
                    mapObj.Add(tmp_StarisDown);
                }

                if (maps[i,j]=='b')//宝箱
                {
                    var tmp_Box = Instantiate(Box, new Vector3(i, 0.35f, j),Quaternion.identity);
                    tmp_Box.AddComponent<BoxTrigger>();
                    mapObj.Add(tmp_Box);
                }
            }

            yield return 0;
        }
        yield return 0;
        if (isDir)
        {
            character.MoveStart(nextUp);//地图加载完毕，玩家移动  下
            character.isMove = true;
        }
        else
        {
            character.MoveStart(nextDown);//地图加载完毕，玩家移动  上
            character.isMove = true;
        }
        yield return null;
    }

    IEnumerator PrintFloor()//先打印地板
    {

        for (int i = widthMin;i>-1;i--)
        {
            var tmp_Edge1 = Instantiate(roomEdge, new Vector3(i, 0, -1), Quaternion.identity) as GameObject;
            var tmp_Edge2 = Instantiate(roomEdge, new Vector3(-1, 0, i), Quaternion.identity) as GameObject;
            var tmp_Edge3 = Instantiate(roomEdge, new Vector3(i, 0, widthMin), Quaternion.identity) as GameObject;
            var tmp_Edge4 = Instantiate(roomEdge, new Vector3(widthMin, 0, i), Quaternion.identity) as GameObject;
            mapObj.Add(tmp_Edge1);
            mapObj.Add(tmp_Edge2);
            mapObj.Add(tmp_Edge3);
            mapObj.Add(tmp_Edge4);
        }

        for (int i=0;i<widthMin;i++)
        {
            for (int j=0;j<lenghtMin;j++)
            {
                GameObject tmp_Floor = null;
                if (nowCheckpoint <= 3)
                {
                    tmp_Floor = Instantiate(floor, new Vector3(i, 0, j), Quaternion.identity) as GameObject;
                }
                else if (nowCheckpoint > 3 && nowCheckpoint <= 6)
                {
                    tmp_Floor = Instantiate(floorTwo, new Vector3(i, 0, j), Quaternion.identity) as GameObject;
                }
                else
                {
                    tmp_Floor = Instantiate(floorThree, new Vector3(i, 0, j), Quaternion.identity) as GameObject;
                }
                mapObj.Add(tmp_Floor);
                tmp_Floor.name = "tmp_Floor" + Checkpoint;

                if (maps[i, j] == ' ')
                {
                    GameObject tmp_Stone = null;
                    if (nowCheckpoint <= 3)
                    {
                        tmp_Stone = Instantiate(stone, new Vector3(i, 0.3f, j), Quaternion.identity) as GameObject;
                    }
                    else if (nowCheckpoint > 3 && nowCheckpoint <= 6)
                    {
                        tmp_Stone = Instantiate(stoneTwo, new Vector3(i, 0.3f, j), Quaternion.identity) as GameObject;
                    }
                    else
                    {
                        tmp_Stone = Instantiate(stoneThree, new Vector3(i, 0.3f, j), Quaternion.identity) as GameObject;
                    }

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
        if (mapObj.Count>0)
        {
            for (int i = 0; i < mapObj.Count; i++)
            {
                Destroy(mapObj[i]);

            }
            mapObj.Clear();
        }

        
    }
    #endregion

#region//道具添加，地图改变的函数
    public void ReduceHeathly(int damage)//血量减少
    {
        
        PlayCharacter.Instance.heathly -= damage;
    }

    public void Addlevel()//等级增加
    {
        PlayCharacter.Instance.exps -= 100;
        PlayCharacter.Instance.level++;
        PlayCharacter.Instance.atk++;
        PlayCharacter.Instance.dfs++;
        PlayCharacter.Instance.heathly += 50;
    }

    public void AddExps(int exp)//经验增加
    {
        PlayCharacter.Instance.exps = PlayCharacter.Instance.exps + exp;
    }

    public void AddMoney(int tmp_money,Transform goldPos)//金币增加
    {
        AudioPlay(coldClip);
        PlayCharacter.Instance.money += tmp_money;
        maps[(int)goldPos.position.x, (int)goldPos.position.z] = '=';

        Debug.Log(maps[(int)goldPos.position.x, (int)goldPos.position.z]);
    }
    public void AddKey(Transform keyPos)//增加钥匙数量
    {
        AudioPlay(coldClip);
        PlayCharacter.Instance.keyNumber++;
        maps[(int)keyPos.position.x, (int)keyPos.position.z] = '=';
    }

    public void ChangeMaps(Transform changePos)//改变地图
    {
        maps[(int)changePos.position.x, (int)changePos.position.z] = '=';
    }

    public void DeletProp(GameObject prop)//删除道具
    {
        Sprite tmp_PropImage = prop.GetComponent<Sprite>();
    }
    #endregion

    public Sprite ResouseSprite(string name)//读取文件图片
    {
        var tmp_Img = Resources.Load("PropImg/"+name,typeof(Sprite)) as Sprite;
        
        return tmp_Img;
    }

    public void AddProp(string PropId)//添加道具
    {
        AudioPlay(coldClip);
        var tmp_Prop = PropLibry.Instance.propTable[PropId];
        if (PlayCharacter.Instance.bag.ContainsKey(tmp_Prop))
        {
            PlayCharacter.Instance.bag[tmp_Prop]++;
        }
        else
        {
            var bag_Prop = new Prop(tmp_Prop);
            PlayCharacter.Instance.bag.Add(bag_Prop, 1);
        }
    }

    public void DeletProp(string PropId)//删除道具
    {
        var tmp_Prop = PropLibry.Instance.propTable[PropId];
        if (PlayCharacter.Instance.bag.ContainsKey(tmp_Prop))
        {
            PlayCharacter.Instance.bag[tmp_Prop]--;
            if (PlayCharacter.Instance.bag[tmp_Prop] <= 0)
            {
                PlayCharacter.Instance.bag.Remove(tmp_Prop);
            }
        }
        else
        {
            Debug.Log("删除错误,背包不存在道具");
        }
    }

    public Moster Init()//随机怪物
    {
        Moster tmp_Monster = null;
        int tmp_MonsterId = Random.Range(0, MonsterLibry.Instance.mosterLibry.Count);

        int number = 0;
        foreach (var moster in MonsterLibry.Instance.mosterLibry)
        {
            if (number == tmp_MonsterId)
            {
                tmp_Monster = moster.Value;
                break;
               
            }
            number++;
        }
        return tmp_Monster;

    }

    public void MosterDeth(NowPos AiPos)//怪物死亡,改变地图
    {
        maps[AiPos.nowX, AiPos.nowZ] = '=';
    }

    public void AddPlayShuXin()
    {

    }

    IEnumerator DeletClip(GameObject audio)
    {
        yield return new WaitForSeconds(1f);
        Destroy(audio);
        yield return null;
    }

    public void AudioPlay(AudioClip nowClip)
    {
        GameObject audio = new GameObject();
        var source = audio.AddComponent<AudioSource>();
        source.clip = nowClip;
        source.volume = 0.5f;
        source.Play();
        StartCoroutine(DeletClip(audio));
    }

}
