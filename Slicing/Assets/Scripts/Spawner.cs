using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject[] prefabs;


	void Start () 
	{
		SpawnPrefabs (0);
	}
	
	public void SpawnPrefabs()
	{
			GameObject go = Instantiate (prefabs [Random.Range (0, prefabs.Length)]);
			Rigidbody temp = go.GetComponent<Rigidbody> ();

			float x = Random.Range (-2f,2f);
			go.GetComponent<Rigidbody> ().AddForce (transform.up * 750);
			go.GetComponent<Rigidbody> ().AddForce (transform.right * Random.Range(150,-150));
			//go.GetComponent<Rigidbody> ().AddForce( new Vector3(Random.Range(-300,300),Random.Range(700,750),0));
			//temp.velocity = new Vector3 (x, 14f, 0f);
			temp.useGravity = true;
			Vector3 pos = transform.position;

			pos.x += Random.Range (-2f, 2f);
			go.transform.position = pos;


			//go.transform.rotation = Random.rotation;

	}

	public void SpawnPrefabs(int prefabNumber)
	{
		GameObject go = Instantiate (prefabs [0]);
		Rigidbody temp = go.GetComponent<Rigidbody> ();

		float x = Random.Range (-2f,2f);
		go.GetComponent<Rigidbody> ().AddForce (transform.up * 750);
		go.GetComponent<Rigidbody> ().AddForce (transform.right * Random.Range(150,-150));
		//go.GetComponent<Rigidbody> ().AddForce( new Vector3(Random.Range(-300,300),Random.Range(700,750),0));
		//temp.velocity = new Vector3 (x, 14f, 0f);
		temp.useGravity = true;
		Vector3 pos = transform.position;

		pos.x += Random.Range (-2f, 2f);
		go.transform.position = pos;


		//go.transform.rotation = Random.rotation;

	}
}
