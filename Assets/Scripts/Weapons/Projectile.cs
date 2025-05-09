using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    public AudioClip audioClip;

    public int damage = 0;
    private bool isInstantiated;

    private AudioSource audioSource;

    void Awake()
    {
        if (Application.isPlaying && gameObject.scene.isLoaded)
        {
            isInstantiated = true;
        }


        if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 6)
        {
            audioSource = GameObject.Find("GamePlayer").GetComponent<AudioSource>();
        }
        
    }
    public void selfDestruct()
    {
        StartCoroutine(TimedDestroy.DestroyAfterSeconds(2f, gameObject));
    }


    private void OnCollisionEnter(Collision collision)
    {
        audioSource.PlayOneShot(audioClip);

        GameObject other = collision.gameObject;
        //Debug.Log("Collided with " + other.tag + "dealing " + damage + " damage.");
        if (other.tag == "Enemy" && other.TryGetComponent(out HPManager HP))
        {
            HP.ApplyDamage(damage);
        }
        
        if (isInstantiated) { Destroy(gameObject); }
    }

}
