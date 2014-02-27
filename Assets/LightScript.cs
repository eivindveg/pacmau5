using System;
using System.Collections;
using UnityEngine;

using Random = UnityEngine.Random;

// ReSharper disable once CheckNamespace
public class LightScript : MonoBehaviour
{
    private bool interval = true;

    // Random rnd = new Random();
    private Transform changingLights;

    // Use this for initialization
    // ReSharper disable once UnusedMember.Local
    private void Start()
    {
        this.StartCoroutine(this.Interval());
        this.changingLights = this.transform;
    }

    // Update is called once per frame
    // ReSharper disable once UnusedMember.Local
    private void Update()
    {
        if (!this.interval)
        {
            return;
        }
        float rndNumber = Random.Range(0, 5);
        if (Math.Abs(rndNumber) < 1)
        {
            this.changingLights.light.color = Color.red;
        }
        else if (Math.Abs(rndNumber - 1) < 1)
        {
            this.changingLights.light.color = Color.blue;
        }
        else if (Math.Abs(rndNumber - 2) < 1)
        {
            this.changingLights.light.color = Color.white;
        }
        else if (Math.Abs(rndNumber - 3) < 1)
        {
            this.changingLights.light.color = Color.yellow;
        }
        else if (Math.Abs(rndNumber - 4) < 1)
        {
            this.changingLights.light.color = Color.green;
        }

        this.StartCoroutine(this.Interval());
    }

    private IEnumerator Interval()
    {
        this.interval = false;
        yield return new WaitForSeconds((float)0.3);
        this.interval = true;
    }
}
