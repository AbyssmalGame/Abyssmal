using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalmUpUI : MonoBehaviour
{
    public Transform leftHand;
    public GameObject palmCanvas;
    public float activationAngle = 0.6f; 

    void Update()
    {
        float dot = Vector3.Dot(leftHand.up.normalized, Vector3.up);

        bool isPalmUp = dot > activationAngle;

        palmCanvas.SetActive(isPalmUp);
    }
}

