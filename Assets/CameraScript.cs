using UnityEngine;

// ReSharper disable once CheckNamespace
public class CameraScript : MonoBehaviour
{
    private GameObject player;

    private PlayerSpawnScript spawn;

    public void AssignPlayer(GameObject player)
    {
        if (player.tag == "Player")
        {
            this.player = player;
        }
    }

    public void AssignSpawn(PlayerSpawnScript spawn)
    {
        this.spawn = spawn;
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
        if (player == null)
        {
            this.AssignPlayer(this.spawn.GetPlayer());
        }

        Vector3 newPosition = player.transform.position;
        newPosition.y += 8;
        this.transform.position = newPosition;
    }
}
