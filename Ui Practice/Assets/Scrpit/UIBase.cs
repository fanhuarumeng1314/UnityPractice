using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBase : MonoBehaviour {

    public EventTriggerLisenter SetEventTrigger(GameObject rObj)
    {
        var eventListener = rObj.GetComponent<EventTriggerLisenter>();

        if (eventListener == null)
        {
            eventListener = rObj.AddComponent<EventTriggerLisenter>();
        }

        return eventListener;
    }

    public void ChangeImage(Image image,string Name)
    {
        image.sprite = Resources.Load("bag/" + Name, typeof(Sprite)) as Sprite;
    }
}
