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
    private void Start()
    {
        this.SpawnPlayer();
    }
}
