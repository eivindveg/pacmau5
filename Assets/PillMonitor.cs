using System.Collections.Generic;

using UnityEngine;

// ReSharper disable once CheckNamespace
public class PillMonitor : MonoBehaviour
{
    private static List<GameObject> scorePills;

    public static void EatPill(GameObject scorePill)
    {
        scorePills.Remove(scorePill);
    }

    // Use this for initialization
    // ReSharper disable once UnusedMember.Local
    private void Start()
    {
        scorePills = new List<GameObject>(GameObject.FindGameObjectsWithTag("ScorePill"));
    }

    // Update is called once per frame
    // ReSharper disable once UnusedMember.Local
    private void Update()
    {
        if (scorePills.Count <= 0)
        {
            Application.LoadLevel(Application.loadedLevel + 1);
        }
    }
}
