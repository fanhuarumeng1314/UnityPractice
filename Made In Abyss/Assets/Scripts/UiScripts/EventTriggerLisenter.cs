using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class EventTriggerLisenter : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    public UnityAction<PointerEventData> onBeginDrag;//开始拖动的监听函数
    public UnityAction<PointerEventData> onDrag;//拖动时的监听函数
    public UnityAction<PointerEventData> onEndDrag;//结束拖动的监听函数
    public UnityAction<PointerEventData> onPointEnter;//停留在Ui界面
    public UnityAction<PointerEventData> onPointExit;//退出Ui界面


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (onBeginDrag != null)
        {
            onBeginDrag(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (onDrag != null)
        {
            onDrag(eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (onEndDrag != null)
        {
            onEndDrag(eventData);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onPointEnter != null)
        {
            onPointEnter(eventData);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (onPointExit != null)
        {
            onPointExit(eventData);
        }
    }
}
