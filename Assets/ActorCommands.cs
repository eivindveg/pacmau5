using System.Diagnostics.CodeAnalysis;

using UnityEngine;

// ReSharper disable once CheckNamespace
public class ActorCommands : MonoBehaviour
{
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
     public static void PlayerKill(GameObject player)
    {
        if (player.tag == "Player")
        {
            ScoreScript.Lives--;
            Destroy(player);
            if (ScoreScript.Lives <= 0)
            {
                // TODO ADD GAME-OVER SEQUENCE!
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
