using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class Watchforangel : MonoBehaviour {
	/*
	// Use this for initialization
	void Start () {


		if (PlayerPrefs.GetInt ("AngelAmount") == 0) {
			this.gameObject.SetActive (true);
	
		} else {

			this.gameObject.SetActive (false);
		}
	}
	
	public void ShowRewardedAd()
	{
		if (Advertisement.IsReady("rewardedVideo"))
		{
			Debug.Log ("Activate The object!");
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
		}
	}

	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log ("The ad was successfully shown.");
			PlayerPrefs.SetInt ("AngelAmount", 1);
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}

	}
	*/
}