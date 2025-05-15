using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glowstick : MonoBehaviour
{

    [SerializeField] private int index;
    
    public GlowstickPath glowstickPath;

    private Renderer[] renderers;

    // Start is called before the first frame update
    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        glowstickPath = GameObject.Find("Path")?.GetComponent<GlowstickPath>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            glowstickPath.activatedGlowstick = index;
        }
    }
}
