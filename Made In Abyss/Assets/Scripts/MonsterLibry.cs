using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;
using System.IO;

public class Moster
{
    public readonly string ID;
    public readonly string Name;
    public readonly string Route;
    public readonly string Hp;
    public readonly string Atk;
    public readonly string Dfs;
    public readonly string Money;
    public readonly string Exp;
    public readonly string Level;
}


public class MonsterLibry : MonoBehaviour {

    public static MonsterLibry Instance;
    public Dictionary<string, Moster> mosterLibry = new Dictionary<string, Moster>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Init()
    {
        mosterLibry = Load<Moster>();
    }


    public Dictionary<string, T> Load<T>() where T : class//数据的读取
    {
        string fileName = typeof(T).Name;
        //string fileRoute = Application.persistentDataPath + "/" + fileName + ".txt";
        string fileData;

        TextAsset propData = Resources.Load("怪物列表") as TextAsset;//读取文档并转换
        if (propData == null)
        {
            Debug.Log("读取异常");
        }

        fileData = propData.text;

        Dictionary<string, T> propDict = JsonMapper.ToObject<Dictionary<string, T>>(fileData);//将文档数据转存到字典当中

        return propDict;
    }
}
