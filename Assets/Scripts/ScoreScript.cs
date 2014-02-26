using UnityEngine;

// ReSharper disable once CheckNamespace
public class ScoreScript : MonoBehaviour
{
    static ScoreScript()
    {
        HighScore = 0;
        CurrentScore = 0;
    }

    public static int HighScore { get; set; }

    public static int CurrentScore { get; set; }

    // Use this for initialization
    // ReSharper disable once UnusedMember.Local
    private void Start()
    {
    }

    // Update is called once per frame
    // ReSharper disable once UnusedMember.Local
    private void Update()
    {
    }
}
