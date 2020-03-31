using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] 
public class Oscillator : MonoBehaviour
{
	[SerializeField] Vector3 movementVector;
	[SerializeField] float period;

	Vector3 startingPos;
	void Start()
	{
		startingPos = transform.position;
	}

	void Update()
	{
		if(period <= Mathf.Epsilon)
		{
			print("Period = 0. Oscillation not active.");
			return;
		}
		
		float cycles = Time.time / period; //grows continually from 0

		const float tau = Mathf.PI * 2;
		float rawSinWave = Mathf.Sin(cycles * tau);

		float movementFactor = rawSinWave / 2f + 0.5f;
		
		Vector3 offset = movementVector * movementFactor;
		transform.position = startingPos + offset;
	}
}
