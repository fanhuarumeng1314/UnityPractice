using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
