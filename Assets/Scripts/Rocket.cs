using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
	//Parameters
	[Header("Movement")]
	[SerializeField] float thrustForce;
	[SerializeField] float rotateForce;
	[Header("SFX")]
	[SerializeField] AudioClip thrustSFX;
	[SerializeField] AudioClip deathSFX;
	[SerializeField] AudioClip landingSFX;
	[Header("VFX")]
	[SerializeField] ParticleSystem thrustVFX;
	[SerializeField] ParticleSystem deathVFX;
	[SerializeField] ParticleSystem landingVFX;

	//States
	enum State { Alive, Dying, Trenscending};
	State currentState;
	bool invincible;
	
	//Cache
	Rigidbody rb;
	AudioSource ac;

	void Start()
	{
		currentState = State.Alive;
		rb = GetComponent<Rigidbody>();
		ac = GetComponent<AudioSource>();
		invincible = false;
	}

	void Update()
	{
		if(currentState == State.Alive)
		{
			RespondToThrustInput();
			RespondToRotationInput();

			if (Debug.isDebugBuild)
			{
				RespondToInvincibleInput();
			}
		}
	}
	private void RespondToThrustInput()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			Thrust();

			if (!ac.isPlaying)
			{
				ac.PlayOneShot(thrustSFX);
			}

			thrustVFX.Play();
		}
		else
		{
			ac.Stop();
			thrustVFX.Stop();
		}
	}

	private void Thrust()
	{
		rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
	}

	private void RespondToRotationInput()
	{
		rb.angularVelocity = Vector3.zero;	//remove rotation due to physics

		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.forward * rotateForce * Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(Vector3.forward * -rotateForce * Time.deltaTime);
		}
	}

	private void RespondToInvincibleInput()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			invincible = !invincible;
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (currentState != State.Alive) { return; } //Rest in oncollision method isn't called if not alive

		if (other.gameObject.tag == "Hostile")
		{
			if(invincible == false)
			{
				HandleDeathSequence();
			}
		}
		else if (other.gameObject.tag == "LandingPad")
		{
			HandleLandingSequence();
		}
	}

	private void HandleDeathSequence()
	{
		Invoke("RestartScene", 2f);

		ac.Stop();
		ac.PlayOneShot(deathSFX);

		thrustVFX.Stop();
		deathVFX.Play();

		currentState = State.Dying;
	}

	private void HandleLandingSequence()
	{
		Invoke("LoadNextScene", 2f);

		ac.Stop();
		ac.PlayOneShot(landingSFX);

		thrustVFX.Stop();
		landingVFX.Play();

		currentState = State.Trenscending;
	}

	private void LoadNextScene()
	{
		FindObjectOfType<LevelLoader>().LoadNextScene();
	}
	private void RestartScene()
	{
		FindObjectOfType<LevelLoader>().RestartScene();
	}
}
