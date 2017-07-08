﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPlane : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "kill")
		{
			if (col.gameObject.GetComponent<Rigidbody> ().velocity.y < 0)
			{
				FindObjectOfType<Spawner> ().SpawnPrefabs ();
			}
		}
	}
}
