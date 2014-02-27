using UnityEngine;
using System.Collections;

public class EndScreenScores : MonoBehaviour {

	public GUIText Score;
	public GUIText HighScore;
	public GUIText LivesLeft;
	public GUIText NewHighScore;
	public GUIText WinOrLoose;
	public bool EndState;
	public Transform ChangingLightsContainer;
	public Transform pacmau5_v5;
	public Transform deaddeadmau5;

	
	
	
	
	
	// Use this for initialization
	void Start () {

		// Won or lost
		if (ScoreScript.Lives >= 1) EndState = true;
		if (ScoreScript.Lives == 0)
						EndState = false;

		if (EndState) {
			WinOrLoose.text = "YOU WIN!";
			deaddeadmau5.active = false;
			}

		if (!EndState) {
			WinOrLoose.text = "Game Over";
			ChangingLightsContainer.active = false;
			pacmau5_v5.active = false;
			}

		// Scores
		Score.text = "Score: " + ScoreScript.CurrentScore;
		if (ScoreScript.CurrentScore > ScoreScript.HighScore) {
						ScoreScript.HighScore = ScoreScript.CurrentScore;
						NewHighScore.text = "New High Score!";
				}
		HighScore.text = "Highscore: " + ScoreScript.HighScore;
		LivesLeft.text = "Lives Left: " + ScoreScript.Lives;

		
	}
}
