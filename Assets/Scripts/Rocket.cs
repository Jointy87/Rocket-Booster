using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
	//Parameters
	[SerializeField] float thrustForce;
	[SerializeField] float rotateForce;
	
	//Cache
	Rigidbody rb;
	AudioSource ac;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		ac = GetComponent<AudioSource>();
	}

	void Update()
	{
		Thrust();
		Rotate();
	}

	private void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Hostile")
		{
			Destroy(gameObject);
		}
	}

	private void Thrust()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			rb.AddRelativeForce(Vector3.up * thrustForce);

			if (!ac.isPlaying)
			{
				ac.Play();
			}
		}
		else
		{
			ac.Stop();
		}
	}
	private void Rotate()
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
}
