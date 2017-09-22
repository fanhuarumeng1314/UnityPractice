using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CSScrpit : MonoBehaviour,IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public UnityAction<PointerEventData> onBeginDrag;//开始拖动的监听函数
    public UnityAction<PointerEventData> onDrag;//拖动时的监听函数
    public UnityAction<PointerEventData> onEndDrag;//结束拖动的监听函数

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (onBeginDrag != null)
        {
            onBeginDrag(eventData);
        }
        Debug.Log("开始拖动");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (onDrag != null)
        {
            onDrag(eventData);
        }
        Debug.Log("进行时");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (onEndDrag != null)
        {
            onEndDrag(eventData);
        }
        Debug.Log("退出");
    }
}
