using UnityEngine;
using System.Collections;

public class gameOverButtons : MonoBehaviour {


	void OnGUI () {

			
			
			
			if (GUI.Button (new Rect (Screen.width / 2 - 80, Screen.height / 2 + 200, 160, 40), "Main Menu")) {
				Application.LoadLevel ("Start_Menu");
				
			}
			
	}

}
