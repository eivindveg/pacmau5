using UnityEngine;

// ReSharper disable once CheckNamespace
// ReSharper disable once InconsistentNaming
public class ScoreHUDScript : MonoBehaviour
{
    public GUIText currentScore;
    public GUIText highScore;
    public GUIText livesLeft;

    // Use this for initialization
    // ReSharper disable once UnusedMember.Local
    private void Start() 
    {
        this.highScore.text = "HighScore: " + ScoreScript.HighScore;
    }
    
    // Update is called once per frame
    // ReSharper disable once UnusedMember.Local
    private void Update()
    {
        this.currentScore.text = "Current Score: " + ScoreScript.CurrentScore;
        this.livesLeft.text = "Lives: " + ScoreScript.Lives;
    }
}
