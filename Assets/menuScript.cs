using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {
	float a = 1f;
	bool last = false;
	float speed=0.01f;
	bool stop = false;
	void OnGUI () {
		if(logo.showmenu == true && !stop){
			a-=speed;
			Camera.main.backgroundColor = new Color(a,a,a);
			RenderSettings.ambientLight = new Color(a,a,a);

			if(a<0){
				last=true;
			}
		}
		if (last&&!stop) {
			GameObject go = (GameObject) Instantiate(Resources.Load ("menubg"));
			go.transform.position= new Vector3(0,0,0);
			go.guiTexture.pixelInset = new Rect (Screen.width / 2 - Screen.width / 2 / 2, Screen.height / 2 - Screen.height / 2 / 2, Screen.width / 2 ,Screen.height / 2);


			stop = true;
		}
		if (stop) {
			
						if (GUI.Button (new Rect (Screen.width / 2 - 180, Screen.height / 2 + 120, 120, 30), "Start")) {
								Application.LoadLevel (1);
				
						}
						if (GUI.Button (new Rect (Screen.width / 2 + 50, Screen.height / 2 + 120, 120, 30), "Quit")) {
								Application.Quit ();
						}
				}
			
		}
		// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
