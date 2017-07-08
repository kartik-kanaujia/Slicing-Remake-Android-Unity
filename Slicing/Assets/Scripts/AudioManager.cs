using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

	public bool allowChance = true;
	public bool reloadScore = false;
	public int tempScore;

	public bool isSignedIn = false;
	public bool askedOnce = false;

	public bool gameStarted = false;
	public int emptyTaps = 0;

	public AudioSource aud;

	void Awake () {
		DontDestroyOnLoad (this);

		if (FindObjectsOfType (GetType ()).Length > 1)
		{
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	void Update () 
	{
		if (SceneManager.GetActiveScene().name == "Menu")
		{
			if (aud.enabled == false)
			{
				aud.enabled = true;
			}
		}
	}
}
