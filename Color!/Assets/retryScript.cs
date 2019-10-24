using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class retryScript : MonoBehaviour {

	public TextMeshProUGUI score;
	public TextMeshProUGUI highScoreDisplay;

	// Use this for initialization
	void Start () {
		int currentScore = PlayerPrefs.GetInt("ballFlipCurrentUserScore", 0);

		int highScore = PlayerPrefs.GetInt("ballFlipCurrentUserHighScore", 0);

		if (currentScore > highScore){
			highScore = currentScore;
			PlayerPrefs.SetInt("ballFlipCurrentUserHighScore", highScore);
		}		

		score.text = currentScore.ToString();
		highScoreDisplay.text = highScore.ToString();

	}

	public void retryToPlay()
	{
		SceneManager.LoadScene("Main");
	}
}
