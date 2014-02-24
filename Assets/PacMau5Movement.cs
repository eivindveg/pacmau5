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
	/*
	boolean canMove(string direction) {
		
		// WHATEVER WE NEED TO DO TO FIND OUT IF WE CAN MOVE, BUT DO NOT MOVE
		return false;
	}
	*/


	void Move(string direction) {

		Debug.Log ("Attempting to move " + direction);
		float x = this.transform.position.x;
		float z = this.transform.position.z;


		if (direction == "East") {
			x += 1;
		} else if (direction == "West") {
			x += -1;
		} else if (direction == "North") {
			z += 1;
		} else if (direction == "South") {
			z += -1;
		}
		Vector3 newPosition = new Vector3 (x,this.transform.position.y,z);

		this.transform.position = newPosition;
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
