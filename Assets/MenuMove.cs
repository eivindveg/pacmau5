using UnityEngine;
using System.Collections;

public class MenuMove : MonoBehaviour {

	
	private float moveSpeed = 0.05f; 


	
	// Update is called once per frame
	void Update ()
	{
		if (MenuScript.startAnim)
		{

			Vector3 newPosition = this.transform.position;
			newPosition.x = this.transform.position.x - moveSpeed;
			this.transform.position = newPosition;


		}
		if (this.transform.position.x <= -20)
		{
			this.transform.position = new Vector3 (20, 0, 11);
		}
	}
}
