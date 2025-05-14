using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAmbienceAudio : MonoBehaviour
{
    
    [SerializeField] private AudioClip[] ambientSounds;
    [SerializeField] private int minimumTimeBetween = 5;
    [SerializeField] private int maximumTimeBetween = 20;

    private AudioSource audioSource;

    private int timeBetween;
    private float timer = 0f;

    private AudioClip nextSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timeBetween = Random.Range(minimumTimeBetween, maximumTimeBetween);
        nextSound = ambientSounds[0];
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetween)
        {
            audioSource.PlayOneShot(nextSound);
            nextSound = ambientSounds[Random.Range(0, ambientSounds.Length)];
            timer = 0f;
        }
    }
}
