using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class First : MonoBehaviour {

	//public GameObject loadingImage;
	public Slider loadingBar;
	public GameObject loadingImage;

	private AsyncOperation async;

	void Awake () {
		DontDestroyOnLoad (this);

		if (FindObjectsOfType (GetType ()).Length > 1)
		{
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (ExecuteAfterTime (2f));
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (SceneManager.GetActiveScene ().name == "Menu" || SceneManager.GetActiveScene ().name == "First")
		{
			if (Input.GetKeyDown (KeyCode.Escape))
			{
				Application.Quit ();
			}
		}
		else if (SceneManager.GetActiveScene ().name == "Game")
		{
			if (Input.GetKeyDown (KeyCode.Escape))
			{
				SceneManager.LoadScene ("Menu");
			}
		}
	}

	IEnumerator ExecuteAfterTime (float time)
	{
		yield return new WaitForSeconds (time);
			
		loadingImage.SetActive (true);
		StartCoroutine (LoadLevelWithBar (1));
		//SceneManager.LoadScene ("Game");
	}
	IEnumerator LoadLevelWithBar (int level)
	{
		async = SceneManager.LoadSceneAsync (level);
		while (!async.isDone)
		{
			loadingBar.value = async.progress;
			yield return null;
		}
	}
}
