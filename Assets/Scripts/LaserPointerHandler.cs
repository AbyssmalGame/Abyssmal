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

    public void PointerInside(object sender, PointerEventArgs e)
    {

    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {

    }
}