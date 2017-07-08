using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class DestroyOnTrigger : MonoBehaviour {

	public GameObject panel;
	public Text tips;
	public GameObject spawner;
	public ChangeColour change;
	public int tempScore = 0;

	public Text score;
	public GameObject scoreGo;

	public GameObject scoreIncrementGo;
	public GameObject scoreIncrementGo2;

	public AudioManager am;
	public PlayGamesScript pg;

	public bool isLookedAt = false;
	public GameObject saveMe;

	public Slider slider;

	public float lookTimer = 0f;
	public float timerDuration = 2f;
	public float startingAmount = 1f;
	public float currentAmount;

	public AudioSource power;
	public AudioSource perfect;
	public AudioSource miss;
	public AudioSource emptyTap;

	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;

	// How long the object should shake for.
	public float shakeDuration = 0f;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.3f;
	public float decreaseFactor = 1.0f;

	Vector3 originalPos;

	public Text firstX;
	public Text secondX;
	public Text thirdX;
	public int xMark = 0;

	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = Camera.main.transform;
		}
	}

	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void Start()
	{
		StartCoroutine (WaitToStart (3f));

		am = GameObject.FindGameObjectWithTag ("AM").GetComponent<AudioManager> ();
		pg = GameObject.FindGameObjectWithTag ("PG").GetComponent<PlayGamesScript> ();

		if (am.reloadScore == true)
		{
			tempScore = am.tempScore;
			am.reloadScore = false;
		}
		currentAmount = startingAmount;
	}

	IEnumerator WaitToStart(float time)
	{
		yield return new WaitForSeconds (time);

		GameStart ();
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "over")
		{
			Destroy (col.gameObject);
		}
		else if (col.gameObject.tag == "kill")
		{
			Destroy (col.gameObject);
//			FindObjectOfType<Spawner> ().SpawnPrefabs ();
		}
		else
		{

			miss.Play ();
			IsGameOver ();
			GetRandomTip ();
			Destroy (col.gameObject);
		}
	}

	void Update()
	{
		score.text = tempScore.ToString ();

		am.tempScore = tempScore;

		if(isLookedAt)
		{
			lookTimer += Time.deltaTime;
			currentAmount -= 0.5f * Time.deltaTime;
			SetUI ();
		}

		if (slider.value <= 0)
		{
			lookTimer = 0;
			currentAmount = 0;
			saveMe.SetActive (false);
			GameOver ();
		}

		//shake the cam code
		if (shakeDuration > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
			camTransform.localPosition = originalPos;
		}


		if (am.emptyTaps <= 0)
		{
			am.emptyTaps = 0;

			firstX.color = new Color (0.2f, 0.2f, 0.2f);
			secondX.color = new Color (0.2f, 0.2f, 0.2f);
			thirdX.color = new Color (0.2f, 0.2f, 0.2f);

			Color color;
			color = firstX.color;
			color.a = 0.5f;

			firstX.color = color;
			secondX.color = color;
			thirdX.color = color;
		}
		if (am.emptyTaps == 1)
		{
			firstX.color = Color.red;
			secondX.color = new Color (0.2f, 0.2f, 0.2f);
			thirdX.color = new Color (0.2f, 0.2f, 0.2f);

			Color color;
			color = secondX.color;
			color.a = 0.5f;

			secondX.color = color;
			thirdX.color = color;
		}
		if (am.emptyTaps == 2)
		{
			secondX.color = Color.red;
			thirdX.color = new Color (0.2f, 0.2f, 0.2f);

			Color color;
			color = thirdX.color;
			color.a = 0.5f;

			thirdX.color = color;
		}
		if (am.emptyTaps == 3)
		{
			thirdX.color = Color.red;
		}
	}

	public void ShakeCamOnEmptyTaps()
	{
		shakeDuration = 0.1f;
		emptyTap.Play ();
	}

	public void IsGameOver()
	{
//		FindObjectOfType<AudioManager> ().emptyTaps = 0;
		am.gameStarted = false;
		change.enabled = false;
		spawner.SetActive (false);
		GetComponent<Collider> ().enabled = false;

		if (am.allowChance == true)
		{
			if (Advertisement.IsReady ())
			{
				saveMe.SetActive (true);
				spawner.SetActive (false);
				change.enabled = false;
				isLookedAt = true;
			}
			else
			{
				GameOver ();
			}
		}
		else
		{
			GameOver ();
		}
	}

	public void GameOver()
	{
		panel.SetActive (true);
		//GetRandomTip ();
//		spawner.SetActive (false);
//		change.enabled = false;
		if (PlayerPrefs.GetInt ("score") < tempScore)
		{
			PlayerPrefs.SetInt("score", tempScore);
			PlayGamesScript.AddScoreToLeaderboard (GPGSIds.leaderboard_highscores, tempScore);
		}

		if (PlayerPrefs.GetInt ("score") >= 10)
			UnlockScoreRankAchievementOne ();

		if (PlayerPrefs.GetInt ("score") >= 30)
			UnlockScoreRankAchievementTwo ();

		if (PlayerPrefs.GetInt ("score") >= 70)
			UnlockScoreRankAchievementThree ();

		if (PlayerPrefs.GetInt ("score") >= 200)
			UnlockScoreRankAchievementFour ();

		if (PlayerPrefs.GetInt ("score") >= 500)
			UnlockScoreRankAchievementFive ();

		am.allowChance = true;
	}

	public void GameStart()
	{
		FindObjectOfType<AudioManager> ().emptyTaps = 0;
		spawner.SetActive (true);
		change.enabled = true;
		scoreGo.SetActive (true);
		am.gameStarted = true;
	}

	public void GetRandomTip()
	{
		int randomInt = Random.Range (1,7);

		switch (randomInt)
		{
			case 1:
				tips.text = "3 consecutive empty taps will end the game";
				return;
			case 2:
				tips.text = "Dont slice the dark squares";
				return;
			case 3:
				tips.text = "Slice before the white square passes over the slicing line to get a POWER SLICE +2";
				return;
			case 4:
				tips.text = "Slice the white square in exactly two halfs to get a PERFECT SLICE +4";
				return;
			case 5:
				tips.text = "Dont miss any white squares";
				return;
			case 6:
				tips.text = "A perfect slice reduces the strikes by 1";
				return;
		}
	}

	public void IncrementScore(int value)
	{
		tempScore += value;
	}

	public void IncrementAchievementOne()
	{
		PlayGamesScript.IncrementAchievement (GPGSIds.achievement_power_master, 1);
		scoreIncrementGo.SetActive (true);
		power.Play ();
		StartCoroutine (DisableBonusOne (1f));
	}

	public void UnlockAchievementOne()
	{
		PlayGamesScript.UnlockAchievement (GPGSIds.achievement_power_slice);
	}

	public void IncrementAchievementTwo()
	{
		PlayGamesScript.IncrementAchievement (GPGSIds.achievement_perfect_master, 1);
		scoreIncrementGo2.SetActive (true);
		Time.timeScale = 0.5f;
		Time.fixedDeltaTime = 0.02f * Time.timeScale;
		perfect.Play ();
		xMark--;
		StartCoroutine (DisableBonusTwo (0.5f));
	}

	public void UnlockAchievementTwo()
	{
		PlayGamesScript.UnlockAchievement (GPGSIds.achievement_perfect_slice);
	}

	public void UnlockScoreRankAchievementOne()
	{
		PlayGamesScript.UnlockAchievement (GPGSIds.achievement_youngling);
	}

	public void UnlockScoreRankAchievementTwo()
	{
		PlayGamesScript.UnlockAchievement (GPGSIds.achievement_padawan);
	}

	public void UnlockScoreRankAchievementThree()
	{
		PlayGamesScript.UnlockAchievement (GPGSIds.achievement_knight);
	}

	public void UnlockScoreRankAchievementFour()
	{
		PlayGamesScript.UnlockAchievement (GPGSIds.achievement_master);
	}

	public void UnlockScoreRankAchievementFive()
	{
		PlayGamesScript.UnlockAchievement (GPGSIds.achievement_grand_master);
	}

	IEnumerator DisableBonusOne(float time)
	{
		yield return new WaitForSeconds (time);

		scoreIncrementGo.SetActive (false);
	}

	IEnumerator DisableBonusTwo(float time)
	{
		yield return new WaitForSeconds (time);

		scoreIncrementGo2.SetActive (false);

		Time.timeScale = 1f;

		Time.fixedDeltaTime = 0.02f;
	}

	public void OnSaveMe()
	{
		isLookedAt = false;
		saveMe.SetActive (false);

		if (Advertisement.IsReady ())
		{
			Advertisement.Show ("rewardedVideo", new ShowOptions (){ resultCallback = HandleAdResult });
		}
		else
		{
			//no add or internet connection
			GameOver ();
		}
		//		am.reloadScore = true;
		//		SceneManager.LoadScene ("Game");
	}

	private void HandleAdResult(ShowResult result)
	{
		switch (result)
		{
			case ShowResult.Finished:
				am.reloadScore = true;
				am.allowChance = false;
				SceneManager.LoadScene ("Game");
				break;

			case ShowResult.Skipped:
				GameOver ();
				break;

			case ShowResult.Failed:
				GameOver ();
				Debug.Log ("Failed to launch ad");
				break;
		}
	}

	public void SetUI()
	{
		slider.value = currentAmount;
	}
}
