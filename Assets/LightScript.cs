using UnityEngine;
using System.Collections;

public class LightScript : MonoBehaviour {

	public bool interval = true;
	//Random rnd = new Random();
	public Transform ChangingLights;

	// Use this for initialization
	void Start () {
		StartCoroutine(Interval());
	}
	
	// Update is called once per frame
	void Update () {
		if (interval) {
			float rndNumber = (Random.Range(0,5));
			if (rndNumber == 0) ChangingLights.light.color = Color.red;
			if (rndNumber == 1) ChangingLights.light.color = Color.blue;
			if (rndNumber == 2) ChangingLights.light.color = Color.white;
			if (rndNumber == 3) ChangingLights.light.color = Color.yellow;
			if (rndNumber == 4) ChangingLights.light.color = Color.green;

			StartCoroutine(Interval());
		}
	}

	IEnumerator Interval () {
		interval = false;
		yield return new WaitForSeconds ((float)0.3);
		interval = true;
		
	}
}
