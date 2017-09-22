using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Com_Item : UIBase {

    public GameObject prop;
    public PropsTableData propName;
    public Image propImage;
    private void Awake()
    {
        prop = gameObject.transform.Find("Btn_Prop").gameObject;//获取添加监听的物体
        propImage = gameObject.transform.Find("Btn_Prop/Img_Prop").GetComponent<Image>();
        SetEventTrigger(prop).onBeginDrag = OnBeginDrag;
        SetEventTrigger(prop).onDrag = OnDrag;
        SetEventTrigger(prop).onEndDrag = OnEndDrag;//添加监听函数
       
    }
    private void Start()
    {
        ChangeImage(propImage, propName.Image);
    }


    public void OnBeginDrag(PointerEventData eventData)//开始拖动
    {
        var page_Bag = UiManager.Instance.GetUi<Page_Bag>();//获取这个脚本类
        prop.transform.SetParent(page_Bag.trs_UiPoint.transform);//改变物体的父子关系
        TuozhuaiMove(eventData);
    }

    public void OnDrag(PointerEventData eventData)//拖动时
    {
        TuozhuaiMove(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)//结束拖动
    {
        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.name == "Img_DeletProp")
        {
            prop.transform.SetParent(gameObject.transform);//改回自己的父子关系，方便删除这个道具的UI
            BagData.Instance.currentData[propName.ID.ToString()].Count--;

            Destroy(gameObject);
        }
        else
        {
            prop.transform.SetParent(gameObject.transform);//改回自己的父子关系
            eventData.selectedObject.gameObject.transform.localPosition = Vector3.zero;//相对坐标归0
            eventData.selectedObject.gameObject.transform.Find("Img_Prop").localPosition = Vector3.zero;
        }

    }

    public void TuozhuaiMove(PointerEventData eventData)
    {
        RectTransform rt = prop.transform.Find("Img_Prop").GetComponent<RectTransform>();//获取自己的字物体的组件进行移动

        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out globalMousePos))//返回世界空间坐标进行移动
        {
            rt.position = globalMousePos;
        }
    }
}
