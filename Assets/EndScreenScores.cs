using UnityEngine;
using System.Collections;

public class EndScreenScores : MonoBehaviour {

	public GUIText Score;
	public GUIText HighScore;
	public GUIText LivesLeft;
	public GUIText NewHighScore;
	
	
	
	
	
	// Use this for initialization
	void Start () {

		Score.text = "Score: " + ScoreScript.CurrentScore;
		if (ScoreScript.CurrentScore > ScoreScript.HighScore) {
						ScoreScript.HighScore = ScoreScript.CurrentScore;
						NewHighScore.text = "New High Score!";
				}
	

		HighScore.text = "Highscore: " + ScoreScript.HighScore;

		LivesLeft.text = "Lives Left: " + ScoreScript.Lives;

		
	}
}
