using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvader : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;

    private Vector3 pointALocation;
    private Vector3 pointBLocation;

    // Start is called before the first frame update
    void Start()
    {
        pointALocation = pointA.transform.position;
        pointBLocation = pointB.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(pointALocation, pointBLocation, time); 
    }
}
