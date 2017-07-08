using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0))
		{
			GetComponent<Image> ().color = Color.red;
			StartCoroutine (Wait ());
		}

	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds (0.1f);
		GetComponent<Image> ().color = Color.black;

	}
}
