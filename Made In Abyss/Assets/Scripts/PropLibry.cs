using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class Prop
{
    public readonly string ID;
    public readonly string Name;
    public readonly string Route;
    public readonly string Atk;
    public readonly string Dfs;
    public readonly string Type;
    public readonly string About;
    public readonly string RoutePrefeb;
    public readonly string ScriptName;

    public Prop()
    {
        
    }

    public Prop(Prop tmp_Prop)
    {
        ID = tmp_Prop.ID;
        Name = tmp_Prop.Name;
        Route = tmp_Prop.Route;
        Atk = tmp_Prop.Atk;
        Dfs = tmp_Prop.Dfs;
        Type = tmp_Prop.Type;
        About = tmp_Prop.About;
        RoutePrefeb = tmp_Prop.RoutePrefeb;
        ScriptName = tmp_Prop.ScriptName;
    }
}


public class PropLibry : MonoBehaviour {

    public static PropLibry Instance;
    public Dictionary<string, Prop> propTable = new Dictionary<string, Prop>();

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    void Start () {
		
	}
	
	
	void Update () {
		
	}

    public void Init()
    {
        propTable = Load<Prop>();

        //foreach (var prop in propTable)
        //{

        //    Debug.Log("Key:" + prop.Key + "Id:" + prop.Value.ID + "Name" + prop.Value.Name + "Atk" + prop.Value.Atk);
        //}
    }

    public Dictionary<string, T> Load<T>() where T : class//数据的读取
    {
        string fileName = typeof(T).Name;
        string fileRoute = Application.persistentDataPath + "/" + fileName + ".txt";
        string fileData;

        if (File.Exists(fileRoute))
        {
            StreamReader textData = File.OpenText(fileRoute);//读取文件
            fileData = textData.ReadToEnd();//将读取文件的流转化为字符串
            Debug.Log("读取成功");
            textData.Close();
            textData.Dispose();
        }
        else
        {
            TextAsset propData = Resources.Load("物品列表") as TextAsset;//读取文档并转换
            if (propData == null)
            {
                Debug.Log("读取异常");
            }

            fileData = propData.text;
        }

        Dictionary<string, T> propDict = JsonMapper.ToObject<Dictionary<string, T>>(fileData);//将文档数据转存到字典当中

        return propDict;
    }
}
