using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateIt : MonoBehaviour {

	int angle;
	float z;
	int a;

	void Start ()
	{
		angle = Random.Range (1,5);

		switch (angle)
		{
			case 1:
				a = 100;
				break;
			case 2:
				a = -100;
				break;
			case 3:
				a = 50;
				break;
			case 4:
				a = -50;
				break;
				
		}
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		z += Time.smoothDeltaTime * a;
		transform.rotation = Quaternion.Euler (0, 0, z);
	}
}
