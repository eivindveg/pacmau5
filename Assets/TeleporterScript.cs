using System.Collections.Generic;

using UnityEngine;

// ReSharper disable once CheckNamespace
public class TeleporterScript : MonoBehaviour
{
    private GameObject other;

    // Use this for initialization
    // ReSharper disable once UnusedMember.Local
    private void Start()
    {
        List<GameObject> teleporters = new List<GameObject>(GameObject.FindGameObjectsWithTag("Teleporter"));

        foreach (GameObject teleporter in teleporters)
        {
            if (!teleporter.Equals(this.gameObject))
            {
                this.other = teleporter;
            }
        }
    }

    // Update is called once per frame
    // ReSharper disable once UnusedMember.Local
    private void Update()
    {
    }

    // ReSharper disable once UnusedMember.Local
    private void OnTriggerEnter(Component comp)
    {
        if (comp.tag == "Player" || comp.tag == "Ghost")
        {
            this.TeleportToOther(comp.gameObject);
        }
    }

    private void TeleportToOther(GameObject actor)
    {
        var actorScript = actor.GetComponent<PacMau5ActorScript>();
        if (actorScript.TeleportCooldown <= 0)
        {
            actor.transform.position = this.other.transform.position;
            actorScript.TeleportCooldown = 8;
        }
    }
}
