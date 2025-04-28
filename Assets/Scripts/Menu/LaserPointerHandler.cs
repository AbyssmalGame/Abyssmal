/* SceneHandler.cs*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;

public class LaserPointerHandler : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;

    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.gameObject.GetComponent<UIElement>() != null)
        {
            e.target.gameObject.GetComponent<UIElement>().onHandClick.Invoke(null);
        }
    }

    /* public void PointerInside(object sender, PointerEventArgs e)
    {
        if (e.target.gameObject.GetComponent<EventTrigger>() != null)
        {
            e.target.gameObject.GetComponent<EventTrigger>().OnPointerEnter.Invoke();
        }
    } */

    public void PointerInside(object sender, PointerEventArgs e)
    {
        EventTrigger trigger = e.target.gameObject.GetComponent<EventTrigger>();

        if (trigger != null)
        {
            // Find the OnPointerEnter event in the EventTrigger
            EventTrigger.Entry entry = trigger.triggers.Find(x => x.eventID == EventTriggerType.PointerEnter);

            if (entry != null)
            {
                // Manually invoke all callbacks in the event
                for ( int i = 0; i < entry.callback.GetPersistentEventCount(); i++)
                {
                    entry.callback.Invoke(new PointerEventData(EventSystem.current));
                }
            }
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {

    }
}