using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using UnityEngine;

// ReSharper disable once CheckNamespace
public class ActorCommands : MonoBehaviour
{
    public const int MaxGhosts = 4;

    // Default ghost timer in frames, e.g 600 = 10 seconds
    public const int DefaultGhostTimer = 300;
    private static List<GameObject> ghosts;

    private static int GhostSpawnTimer { get; set; }

    public static void ResetGhostTimer()
    {
        GhostSpawnTimer = DefaultGhostTimer;
    }

    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public static void PlayerKill(GameObject player)
    {
        if (player.tag == "Player")
        {
            ScoreScript.Lives--;
            Destroy(player);
            if (ScoreScript.Lives <= 0)
            {
                Application.LoadLevel(4);
            }
            else
            {
                var spawnPosition = GameObject.FindGameObjectWithTag("PlayerSpawn").transform.position;
                // ReSharper disable once RedundantNameQualifier
                var newPlayer = (GameObject)Instantiate(Resources.Load("Actors/PlayerObject"));
                newPlayer.transform.position = spawnPosition;
            }
        }
    }

    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public static void GhostKill(GameObject ghost)
    {
        if (ghost.tag == "Ghost")
        {
            if (ghosts.Remove(ghost))
            {
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
        ghosts = new List<GameObject>();
        ResetGhostTimer();
    }

    // Update is called once per frame
    // ReSharper disable once UnusedMember.Local
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
