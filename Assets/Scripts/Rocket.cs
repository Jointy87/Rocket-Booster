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
		ProcessInput();
	}

	private void ProcessInput()
	{
		if(Input.GetKey(KeyCode.Space))
		{
			rb.AddRelativeForce(Vector3.up * thrustForce);
			if(!ac.isPlaying)
			{
				ac.Play();
			}
		}
		else
		{
			ac.Stop();
		}

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
