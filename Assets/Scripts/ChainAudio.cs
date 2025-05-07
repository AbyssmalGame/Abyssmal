using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainAudio : MonoBehaviour
{
	public AudioSource audioSource;
	public AudioClip secondClip;

	void Start()
	{
		StartCoroutine(PlayAfterAwakeClip());
	}

	IEnumerator PlayAfterAwakeClip()
	{
		yield return new WaitForSeconds(audioSource.clip.length); 
		audioSource.clip = secondClip;
		audioSource.Play();
	}
}
