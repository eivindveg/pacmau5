using UnityEngine;

// ReSharper disable once CheckNamespace
public class CameraScript : MonoBehaviour
{
    private GameObject player;

    private PlayerSpawnScript spawn;

    // Assign the passed player to the camera. Made available for use if the player dies.
    public void AssignPlayer(GameObject player)
    {
        if (player.tag == "Player")
        {
            this.player = player;
        }
    }

    // Get the spawn object for this level so we know who to ask for a new player to follow
    public void AssignSpawn(PlayerSpawnScript spawnScript)
    {
        this.spawn = spawnScript;
    }

    // Use this for initialization
    // ReSharper disable once UnusedMember.Local
    private void Start()
    {
    }

    // Follow the player. Get a new one if our player actor has been killed.
    // ReSharper disable once UnusedMember.Local
    private void Update()
    {
        if (player == null)
        {
            this.AssignPlayer(this.spawn.GetPlayer());
        }
        else
        { 
        Vector3 newPosition = this.player.transform.position;
        newPosition.y += 8;
        this.transform.position = newPosition;
        }
    }
}
