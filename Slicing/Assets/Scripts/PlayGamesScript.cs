using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class PlayGamesScript : MonoBehaviour {

	public AudioManager am;

	void Awake () {
		DontDestroyOnLoad (this);

		if (FindObjectsOfType (GetType ()).Length > 1)
		{
			Destroy (gameObject);
		}
	}

	void Start()
	{
		am = GameObject.FindGameObjectWithTag ("AM").GetComponent<AudioManager> ();
	}

	void Update()
	{
//		if (am.isSignedIn == false)
//		{
//			ToActivate ();
//		}
	}

	public void ToActivate () 
	{
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder ().Build ();
		PlayGamesPlatform.InitializeInstance (config);
		PlayGamesPlatform.Activate();

		SignIn ();
	}

	void SignIn()
	{
		Social.localUser.Authenticate (success => {
		
			am.isSignedIn = success;
			am.askedOnce = true;
		});
	}

	#region Achievements

	public static void UnlockAchievement(string id)
	{
		Social.ReportProgress(id, 100, success => { }); 
	}

	public static void IncrementAchievement(string id, int stepsToIncrement)
	{
		PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
	}

	public static void ShowAchievementsUI()
	{
		Social.ShowAchievementsUI ();
	}

	#endregion /Achievements

	#region Leaderboards

	public static void AddScoreToLeaderboard(string leaderboardId, long score)
	{
		Social.ReportScore(score, leaderboardId, success => { });
	}

	public static void ShowLeaderboardsUI()
	{
		Social.ShowLeaderboardUI ();
	}

	#endregion /Leaderboards


}
