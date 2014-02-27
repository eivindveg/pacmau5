using UnityEngine;
using System.Collections;

public class ScoreHUDScript : MonoBehaviour {

	public GUIText CurrentScore;
	public GUIText HighScore;
	public GUIText LivesLeft;

	// Use this for initialization
	void Start () {
		HighScore.text = "HighScore " + ScoreScript.HighScore;


	}
	
	// Update is called once per frame
	void Update () {
		CurrentScore.text = "Current Score: " + ScoreScript.CurrentScore;
		LivesLeft.text = "Lives: " + ScoreScript.Lives;
	}
}
