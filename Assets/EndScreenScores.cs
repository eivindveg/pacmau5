using UnityEngine;

// ReSharper disable once CheckNamespace
public class EndScreenScores : MonoBehaviour
{
    public GUIText Score;
    public GUIText HighScore;
    public GUIText LivesLeft;
    public GUIText NewHighScore;
    public GUIText WinOrLoose;
    public bool EndState;
    public Transform ChangingLightsContainer;
    public Transform Pacmau5V5;
    public Transform Deaddeadmau5;
    public new AudioClip audio;





    // Use this for initialization
    void Start()
    {

        // Won or lost
        if (ScoreScript.Lives >= 1) EndState = true;
        if (ScoreScript.Lives == 0)
        {
            EndState = false;
        }

        if (EndState)
        {
            WinOrLoose.text = "YOU WIN!";
            this.Deaddeadmau5.gameObject.SetActive(false);
            audio = (AudioClip)Resources.Load("Sounds/PM_P_WinSound");
            LivesLeft.text = "Lives Left: " + ScoreScript.Lives;
        }

        if (!EndState)
        {
            WinOrLoose.text = "Game Over";
            ChangingLightsContainer.gameObject.SetActive(false);
            this.Pacmau5V5.gameObject.SetActive(false);
            audio = (AudioClip)Resources.Load("Sounds/PM_P_Death_Game_Over");

        }

        AudioSource audioSource = GameObject.FindObjectOfType<Camera>().gameObject.GetComponent<AudioSource>();
        audioSource.clip = this.audio;
        audioSource.Play();

        // Scores
        Score.text = "Score: " + ScoreScript.CurrentScore;
        if (ScoreScript.CurrentScore > ScoreScript.HighScore)
        {
            ScoreScript.HighScore = ScoreScript.CurrentScore;
            NewHighScore.text = "New High Score!";
        }
        HighScore.text = "Highscore: " + ScoreScript.HighScore;



    }
}
