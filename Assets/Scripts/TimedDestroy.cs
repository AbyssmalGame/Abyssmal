using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
    public static IEnumerator DestroyAfterSeconds(float seconds, GameObject obj)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(obj);
    }
}
