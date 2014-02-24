using UnityEngine;
using System.Collections;

public class PacMau5Movement : MonoBehaviour {

	//string previousMovementDirection;

	string lastActiveInput;

	string[] possibleInputs =
	{
		"North",
		"South",
		"West",
		"East",
	};

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update() {
		
		if(!hasInput()) {
			// TODO IMPLEMENT THE REST OF THIS METHOD

			//if(!attemptPreviousMovement()) {
			if(!false) {
				Move (lastActiveInput);
			} else {
				// DO SOMETHING
			} 
		
		} else {
			Move(lastActiveInput);
		}
		
	}
	// TODO IMPLEMENT THIS METHOD
	/*
	boolean attemptPreviousMovement() {
		if(canMove(previousMovementDirection)) {
			return Move(previousMovementDirection);
		} else return false;
	}
	*/

	// TODO IMPLEMENT THIS METHOD
	bool CanMove(string direction) {
		
		// WHATEVER WE NEED TO DO TO FIND OUT IF WE CAN MOVE, BUT DO NOT MOVE
		return true;
	}


	void Move(string direction) {

		if (CanMove (direction)) {
			Debug.Log ("Attempting to move " + direction);
			float x = this.transform.position.x;
			float z = this.transform.position.z;



			if (direction == "East") {
				x += 1.0f/15.0f;
			} else if (direction == "West") {
				x += -1.0f/15.0f;
			} else if (direction == "North") {
				z += 1.0f/15.0f;
			} else if (direction == "South") {
				z += -1.0f/15.0f;
			}
			Vector3 newPosition = new Vector3 (x, this.transform.position.y, z);
			this.transform.position = newPosition;
			Rotate (direction);
		}
	}

	void Rotate(string direction) {

		float y = 0;;

		if (direction == "East") {
			Debug.Log ("Pointing player East");
			y = 90.0f;
		} else if (direction == "West") {
			Debug.Log ("Pointing player West");
			y = 270.0f;
		} else if (direction == "North") {
			y = 0.0f;
		} else if (direction == "South") {
			y = 180.0f;
		}

		Quaternion newRotation = Quaternion.identity;
		newRotation.eulerAngles = new Vector3(0, y, 0);
		//new Quaternion (this.transform.rotation.x, y, this.transform.rotation.z, this.transform.rotation.w);
		this.transform.rotation = newRotation;
	}

	bool hasInput() {

		foreach(string inputValue in possibleInputs) {
			if(getInput (inputValue)) {
				return true;
			}
		}
		return false;
		
		
	}

	bool getInput(string inputValue) {
		
		if(Input.GetButtonDown (inputValue)) {
			lastActiveInput = inputValue;
			Debug.Log (inputValue);
			return true;
		} else return false;
		
	}

}
