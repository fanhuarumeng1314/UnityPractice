using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Btn_Prop : UIBase, IDragHandler,IEndDragHandler, IBeginDragHandler
{
    Transform tmp_TransParent;
    Page_Bag page_Bag;
    public void OnBeginDrag(PointerEventData eventData)//开始拖动
    {
        page_Bag = UiManager.Instance.GetUi<Page_Bag>();//获取这个脚本类
        tmp_TransParent = gameObject.transform.parent.transform;
        gameObject.transform.SetParent(page_Bag.trs_UiPoint.transform);//改变物体的父子关系
        
        TuozhuaiMove(eventData);
    }
    public void OnDrag(PointerEventData eventData)//正在拖动
    {
        TuozhuaiMove(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)//停止拖动
    {
        if (eventData.pointerCurrentRaycast.gameObject !=null && eventData.pointerCurrentRaycast.gameObject.name == "Img_DeletProp")
        {
            gameObject.transform.SetParent(tmp_TransParent);//改回自己的父子关系，方便删除这个道具的UI
            Destroy(gameObject.transform.parent.gameObject);
            page_Bag.uiNumber--;
            page_Bag.ChangeHeght();
        }
        else
        {
            //gameObject.transform.parent.gameObject.transform.SetParent(page_Bag.gameObject.transform.Find("Package/Scroll View/Viewport/Content"));
            //TuozhuaiMove(eventData);
            gameObject.transform.SetParent(tmp_TransParent);//改回自己的父子关系
            eventData.selectedObject.gameObject.transform.localPosition = Vector3.zero;//相对坐标归0
            eventData.selectedObject.gameObject.transform.Find("Img_Prop").localPosition = Vector3.zero;   
        }
    }

    public void TuozhuaiMove(PointerEventData eventData)
    {
        var rt = gameObject.transform.Find("Img_Prop").GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
        }
    }

}
