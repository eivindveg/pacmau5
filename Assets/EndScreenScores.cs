using UnityEngine;
using System.Collections;

public class EndScreenScores : MonoBehaviour {

	public GUIText Score;
	public GUIText HighScore;
	
	
	
	
	
	// Use this for initialization
	void Start () {
		int HighScoreReturn = ScoreScript.checkHighScore();
		Score.text = "Score: " + ScoreScript.CurrentScore;
		HighScore.text = "Highscore: " + HighScoreReturn;
		
	}
}
