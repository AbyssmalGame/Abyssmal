using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ReloadItem : MonoBehaviour
{
    private Interactable interactable;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
    }
    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes grabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(gameObject);

        if (isGrabEnding)
        {
            hand.DetachObject(gameObject);
            hand.HoverUnlock(gameObject.GetComponent<Interactable>());
            StartCoroutine(DestroyAfterSeconds(gameObject, 1f));
        }
    }

    private IEnumerator DestroyAfterSeconds(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}
