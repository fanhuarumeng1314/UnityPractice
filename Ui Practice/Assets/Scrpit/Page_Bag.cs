using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Page_Bag : UIBase
{

    public RectTransform rect;//道具添加的地址
    public GridLayoutGroup grid;
    public int uiNumber = 1;//UI道具数量
    public GameObject trs_UiPoint;
    List<string> propId = new List<string>();//保存道具的ID即道具在字典中对应的KEY

    public void Awake()
    {
        Button addButn = gameObject.transform.Find("Package/Btn_AddProp").GetComponent<Button>();//获取添加物体的button并添加事件
        addButn.onClick.AddListener(GreatProp);

        trs_UiPoint = gameObject.transform.Find("Package/Trs_UiPoint").gameObject;

        Button CloseButn = gameObject.transform.Find("Package/Btn_CloseMuen").GetComponent<Button>();
        CloseButn.onClick.AddListener(CloseMeun);

        grid = gameObject.transform.Find("Package/Scroll View/Viewport/Content").GetComponent<GridLayoutGroup>();//获取子物体上的GridLayoutGroup组件
        rect = gameObject.transform.Find("Package/Scroll View/Viewport/Content").GetComponent<RectTransform>();

        DictGreatProp();
    }

    private void Start()
    {
        var tmp_Data = BagData.Instance.currentData;
        foreach (var prop in tmp_Data)
        {
            propId.Add(prop.Key);
        }

    }

    public void GreatProp()//添加道具
    {
        int Id = Random.Range(0, propId.Count);//随机key选出在字典中的实例化的道具
        var tmp_UIProp = UiManager.Instance.GreatUI<Com_Item>(rect);//实例化道具
        var tmp_UIScrpit = tmp_UIProp.GetComponent<Com_Item>();
        tmp_UIScrpit.propName = BagData.Instance.currentData[propId[Id]];
        BagData.Instance.currentData[propId[Id]].Count++;
    }
    public void DictGreatProp()//从字典中加载道具，即从读取的文件数据进行道具加载
    {
        var tmp_Data = BagData.Instance.currentData;
        if (tmp_Data!=null && tmp_Data.Count > 0)
        {
            foreach (var prop in tmp_Data)
            {
                for (int i = 0; i <= prop.Value.Count;i++)
                {
                    var tmp_UIProp = UiManager.Instance.GreatUI<Com_Item>(rect);//实例化道具
                    var tmp_UIScrpit = tmp_UIProp.GetComponent<Com_Item>();
                    tmp_UIScrpit.propName = prop.Value;
                    uiNumber++;//道具数量加1
                    ChangeHeght();
                }
            }
        }

    }

    public void ChangeHeght()//改变高度
    {
        float tmp_Heght = grid.cellSize.y + grid.spacing.y;
        rect.sizeDelta = new Vector2(0, tmp_Heght * (Mathf.Ceil((float)uiNumber / grid.constraintCount)));
    }

    public void CloseMeun()//关闭菜单
    {

        Props_Table.Instance.Save<PropsTableData>(BagData.Instance.currentData);
        Debug.Log("存储成功");

        UiManager.Instance.uiScrpits.Clear();
        Destroy(gameObject);
    }
}
