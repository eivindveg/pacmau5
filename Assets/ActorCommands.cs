﻿using System.Collections.Generic;

using UnityEngine;

// ReSharper disable once CheckNamespace
public class ActorCommands : MonoBehaviour
{
    public const int MaxGhosts = 4;

    // Default ghost timer in frames, e.g 600 = 10 seconds
    public const int DefaultGhostTimer = 300;
    private static List<GameObject> ghosts;

    private static int GhostSpawnTimer { get; set; }

    // Called when killing or spawning ghosts.
    public static void ResetGhostTimer()
    {
        GhostSpawnTimer = DefaultGhostTimer;
    }

    // Static method for killing the player.
    public static void PlayerKill(GameObject player)
    {
        if (player.tag == "Player")
        {
            ScoreScript.Lives--;
            Destroy(player);

            // If we've run out of lives, skip straight to the end game screen.
            if (ScoreScript.Lives <= 0)
            {
                Application.LoadLevel(3);
            }
            else
            {
                // var spawnPosition = GameObject.FindGameObjectWithTag("PlayerSpawn").transform.position;
                var spawnerScript = GameObject.FindGameObjectWithTag("PlayerSpawn").GetComponent<PlayerSpawnScript>();
                // ReSharper disable once RedundantNameQualifier
                spawnerScript.SpawnPlayer();
            }
        }
    }

    // Static method for killing ghosts.
    public static void GhostKill(GameObject ghost)
    {
        if (ghost.tag == "Ghost")
        {
            if (ghosts.Remove(ghost))
            {
                ScoreScript.CurrentScore += 10;
                if (GhostSpawnTimer >= 1)
                {
                    ScoreScript.CurrentScore += 10 * (MaxGhosts - ghosts.Count);
                }

                Destroy(ghost);
                ResetGhostTimer();
            }
        }
    }

    public static GameObject SpawnGhost()
    {
        var spawnPosition = GameObject.FindGameObjectWithTag("GhostSpawn").transform.position;
        // ReSharper disable once RedundantNameQualifier
        var newGhost = (GameObject)Instantiate(Resources.Load("Actors/GhostObject"));
        newGhost.transform.position = spawnPosition;
        ghosts.Add(newGhost);
        return newGhost;
    }

    // Use this for initialization
    // ReSharper disable once UnusedMember.Local
    private void Start()
    {
        // When the scene starts, create a blank list and start the ghost timer.
        ghosts = new List<GameObject>();
        ResetGhostTimer();
    }

    // ReSharper disable once UnusedMember.Local
    // Check if there is a timer running on ghost spawning. Spawn a ghost if the timer is out and we have less than four ghosts.
    private void Update()
    {
        if (GhostSpawnTimer >= 1)
        {
            GhostSpawnTimer--;
        }

        if (ghosts.Count < MaxGhosts)
        {
                if (GhostSpawnTimer < 1)
                {
                    ResetGhostTimer();
                    SpawnGhost();
                }
        }
    }
}
