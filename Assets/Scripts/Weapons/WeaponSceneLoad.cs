using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onEnableCallback : MonoBehaviour
{
    private void OnEnable()
    {
        GameObject.Find("GameManager").GetComponent<WeaponManager>().InitialIze();
    }
}
