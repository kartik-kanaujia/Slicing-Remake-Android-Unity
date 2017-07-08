using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

	public Text Highscore;
	public Text StartCountdown;
	public GameObject StartCountGo;

	public AudioSource first;
	public AudioSource second;

	// Use this for initialization
	void Start () 
	{
		if (Highscore)
		{
			Highscore.text = PlayerPrefs.GetInt ("score").ToString ();
		}
		StartCountdown.text = "3";
		first.Play ();
		StartCoroutine (WaitToStart (1f));
		StartCoroutine (WaitToStartTwo (2f));
		StartCoroutine (WaitToStartLast (3f));
		StartCoroutine (WaitToStartLastEnd (3.2f));
	}

	IEnumerator WaitToStart(float time)
	{
		yield return new WaitForSeconds (time);

		StartCountdown.text = "2";

		first.Play ();
	}

	IEnumerator WaitToStartTwo(float time)
	{
		yield return new WaitForSeconds (time);

		StartCountdown.text = "1";

		first.Play ();
	}

	IEnumerator WaitToStartLast(float time)
	{
		yield return new WaitForSeconds (time);

		StartCountdown.text = "GO!";

		second.Play ();
	}

	IEnumerator WaitToStartLastEnd(float time)
	{
		yield return new WaitForSeconds (time);

		StartCountGo.SetActive (false);
	}

//	// Update is called once per frame
//	void Update () 
//	{
//		if (Input.GetKeyDown(KeyCode.Escape)) {Application.Quit();}
//	}

	public void LoadScene(string scene)
	{
		SceneManager.LoadScene (scene);
	}
}
