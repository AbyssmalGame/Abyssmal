using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class MagazineBehavior : MonoBehaviour
{
    /// <summary>
    /// All ammo count reloading functionality for weapons are done inside of the weapon script. This script contains functionality for the magazine model.
    /// </summary>
    public Rigidbody body;

    public void Eject()
    {
        GameObject newMagazine = Instantiate(gameObject, transform.position, transform.rotation, gameObject.transform).gameObject;
        newMagazine.transform.localScale = Vector3.one;
        newMagazine.transform.parent = null;
        StartCoroutine(destroyAfterSeconds(newMagazine, 3f));
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Rigidbody magazineRB = newMagazine.GetComponent<Rigidbody>();

        magazineRB.constraints = RigidbodyConstraints.None; // By default the magazine is frozen to the gun
        magazineRB.AddForce(Vector3.down * 10);
    }

    private IEnumerator destroyAfterSeconds(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(obj);
    }

    public void Reload()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

}
