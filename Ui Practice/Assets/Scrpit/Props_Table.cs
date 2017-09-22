using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using LitJson;

public class PropsTableData//道具表格数据
{
    public readonly string ID;
    public readonly string Name;
    public readonly string Image;
    public int Count;
}



public class Props_Table : MonoBehaviour {

    public static Props_Table Instance;
    public Dictionary<string, PropsTableData> tableData;



    private void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        tableData = Load<PropsTableData>();
        foreach (var dict in tableData)
        {
            Debug.Log(dict.Key + "  "+dict.Value.ID+"   " + dict.Value.Name + "  " + dict.Value.Image);
        }

    }

    public Dictionary<string,T> Load<T>() where T: class//数据的读取
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
            TextAsset propData = Resources.Load("Props_Table") as TextAsset;//读取文档并转换
            if (propData == null)
            {
                Debug.Log("读取异常");
            }

            fileData = propData.text;
        }

        Dictionary<string, T> propDict = JsonMapper.ToObject<Dictionary<string, T>>(fileData);//将文档数据转存到字典当中

        return propDict;
    }



    public void Save<T> (Dictionary<string, T> rData) where T : class//数据存储
    {
        string fileName = typeof(T).Name;
        string flieRoute = Application.persistentDataPath + "/" + fileName + ".txt";//文件存储路径
        string propText = JsonMapper.ToJson(rData);//将字典转化为字符串

        Debug.Log(flieRoute);//打印出文件路径

        FileStream fs = new FileStream(flieRoute, FileMode.Create);//实例化一个FS类，并选择文件模式为创建  参数为文件路径

        byte[] data = System.Text.Encoding.UTF8.GetBytes(propText);//将字符串按照.UTF8编码格式转换成byte存储

        fs.Write(data,0, data.Length);//文件写入，参数为byte数组，偏移量-offset，以及长度
        //清空缓冲区、关闭流
        fs.Flush();
        fs.Close();
   

    }

}
