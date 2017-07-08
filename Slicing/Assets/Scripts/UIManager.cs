using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	public AudioManager am;
	public PlayGamesScript pg;

	// Use this for initialization
	void Start () {
		am = GameObject.FindGameObjectWithTag ("AM").GetComponent<AudioManager> ();
		pg = GameObject.FindGameObjectWithTag ("PG").GetComponent<PlayGamesScript> ();

		if (am.askedOnce == false)
		{
			pg.ToActivate ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void ShowAchievements()
	{
		if (am.isSignedIn == true)
		{
			PlayGamesScript.ShowAchievementsUI ();
		}
		else
		{
			pg.ToActivate ();
		}
	}

	public void ShowLeaderboards()
	{
		if (am.isSignedIn == true)
		{
			PlayGamesScript.ShowLeaderboardsUI ();
		}
		else
		{
			pg.ToActivate ();
		}
	}

	public void SignInIfNot()
	{
		if (am.isSignedIn == true)
		{
			return;
		}
		else
		{
			pg.ToActivate ();
		}
	}
}
