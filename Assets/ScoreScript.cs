﻿using UnityEngine;

// ReSharper disable once CheckNamespace
public class ScoreScript : MonoBehaviour
{
    static ScoreScript()
    {
        HighScore = 0;
        CurrentScore = 0;
        Lives = 3;
    }

    public static int Lives { get; set; }

    public static int HighScore { get; set; }

    public static int CurrentScore { get; set; }

	public static int checkHighScore() {
		if (CurrentScore > HighScore) HighScore = CurrentScore;
		return HighScore;
	}
	public static int checkLives() {
				return Lives;
		}

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
