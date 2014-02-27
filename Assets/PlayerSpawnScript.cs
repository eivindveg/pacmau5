using System.Diagnostics.CodeAnalysis;

using UnityEngine;

// ReSharper disable once CheckNamespace
public class PlayerSpawnScript : MonoBehaviour
{
    private GameObject player;

    public void SpawnPlayer()
    {
        var spawnPosition = this.transform.position;
        var newPlayer = (GameObject)Instantiate(Resources.Load("Actors/PlayerObject"));
        this.player = newPlayer;
        this.player.transform.position = spawnPosition;
    }

    // Use this for initialization
    // ReSharper disable once UnusedMember.Local
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    private void Start()
    {
        this.SpawnPlayer();
        Instantiate(Resources.Load("HUD/ScoreGUI"));
    }
}
