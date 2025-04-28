using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderPushback : MonoBehaviour
{
	public float pushForce = 5f;
	public Vector3 center = Vector3.zero;

	private void OnTriggerStay(Collider other)
	{
		Swimmer swimmer = other.GetComponentInParent<Swimmer>();
		if (swimmer != null)
		{
			Vector3 direction = (center - other.transform.position).normalized;
			swimmer.ApplyExternalForce(direction * pushForce);
		}
	}
}
