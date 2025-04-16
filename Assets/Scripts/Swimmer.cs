using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Swimmer : MonoBehaviour
{
	[Header("Values")]
	[SerializeField] float swimForce = 1f;
	[SerializeField] float dragForce = 1f;
	[SerializeField] float minForce;
	[SerializeField] float minTimeBetweenStrokes;
	[Header("References")]
	[SerializeField] SteamVR_Behaviour_Pose leftHandPose;
	[SerializeField] SteamVR_Behaviour_Pose rightHandPose;
	[SerializeField] SteamVR_Action_Boolean swimAction;
	[SerializeField] Transform trackingReference;
	[SerializeField] AudioClip swimSound;

	Rigidbody _rigidbody;
	float _cooldownTimer;
	AudioSource _audioSource;

	void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_rigidbody.useGravity = false;
		_rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
		UpdateSwimSpeed();	

		_audioSource= GetComponent<AudioSource>();
		_audioSource.loop = false;
		_audioSource.playOnAwake = false;
	}



	void FixedUpdate()
	{
		_cooldownTimer += Time.fixedDeltaTime;

		bool leftTriggerPressed = swimAction.GetState(leftHandPose.inputSource);
		bool rightTriggerPressed = swimAction.GetState(rightHandPose.inputSource);
		bool isSwimming = leftTriggerPressed || rightTriggerPressed;

		if (isSwimming)
		{
			Vector3 leftHandVelocity = leftHandPose.GetVelocity();
			Vector3 rightHandVelocity = rightHandPose.GetVelocity();
			Vector3 localVelocity = leftHandVelocity + rightHandVelocity;
			localVelocity *= -1;

			if (_cooldownTimer > minTimeBetweenStrokes && localVelocity.sqrMagnitude > minForce * minForce)
			{
				Vector3 worldVelocity = trackingReference.TransformDirection(localVelocity);
				_rigidbody.AddForce(worldVelocity * swimForce, ForceMode.Acceleration);
				_cooldownTimer = 0f;

				if (!_audioSource.isPlaying) 
				{
					_audioSource.PlayOneShot(swimSound);
				}
			}
		}
		if (_rigidbody.velocity.sqrMagnitude > 0.01f)
		{
			_rigidbody.AddForce(-_rigidbody.velocity * dragForce, ForceMode.Acceleration);
		}
	}

	public void UpdateSwimSpeed()
    {
		swimForce = GameManager.GetSpeed();

	}
}
