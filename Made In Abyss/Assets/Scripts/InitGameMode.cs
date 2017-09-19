using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGameMode : MonoBehaviour {

    public static InitGameMode instance;
    public GameObject playerPrefeb;
    public GameObject nextPrefeb;
    public GameObject playCamera;
    public Transform playerSport;
    public GameObject nowPlayCamera;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            playerPrefeb = Resources.Load("RolePreFeb/knight") as GameObject;//读取玩家模型物体
            playCamera = Resources.Load("CameaPrefeb/PlayerCamera") as GameObject;//读取相机

            GameObject Player = Instantiate(playerPrefeb,new Vector3(0.6f,0.1f,-8.5f),Quaternion.identity);
            Player.AddComponent<PlayController>();
            Player.AddComponent<PlayCharacter>();
            playerSport = Player.GetComponent<Transform>();

            nowPlayCamera = Instantiate(playCamera, new Vector3(0, 1, 0), Quaternion.identity);//实例化玩家的相机
            nowPlayCamera.AddComponent<PlayCamera>();
            DontDestroyOnLoad(nowPlayCamera);
            DontDestroyOnLoad(Player);
            DontDestroyOnLoad(gameObject);
            FoundNext();
            Debug.Log("Awake1次执行");


            PropLibry.Instance.Init();//读取道具列表
            MonsterLibry.Instance.Init();//读取怪物列表
        }
        else
        {
            InitGameMode.instance.playerSport.position = new Vector3(0,0.12f,0);
            FoundNext();
            Debug.Log("Awake2次执行");
            Destroy(transform.gameObject);
        }

    }

    void Start ()
    {
	    	
	}
	
	void Update () {
		
	}

    public void FoundNext()
    {
        nextPrefeb = Resources.Load("MapPrefeb/Next") as GameObject;
        GameObject next = Instantiate(nextPrefeb,new Vector3(1.2f,-0.52f,5.22f),Quaternion.identity);
        next.AddComponent<NextDunge>();
    }
}
