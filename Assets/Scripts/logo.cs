using UnityEngine;
using System.Collections;

public class logo : MonoBehaviour {
	public float max = 150f;
	public float count = 0f;
	public bool stop= false;
	public bool back= false;
	public  GUITexture texture;
	Color color;
	public float overaltimespeed = 1f;
	public float alphacount = 0f;
	public float alpha=0f;
	public static bool showmenu=false;	

	void OnGUI()
	{
		texture.guiTexture.pixelInset = new Rect (Screen.width / 2 - Screen.width / 2 / 2 - Screen.width / 2 / 2, Screen.height / 2 - Screen.height / 2 / 2, Screen.width / 2 ,Screen.height / 2);
		Color textureColor = texture.color;
		textureColor.a = alpha;
		if(!stop){
		if (!back) {		
			
			textureColor.a = alpha;
				alpha+=alphacount;
				count+=overaltimespeed;


			if (count > max) {
				back = true;
			}
		}
		else if (back) {		
			
			textureColor.a = alpha;
				alpha-=alphacount;
				count-=overaltimespeed;

			
			if (alpha < 0) {
				stop = true;
					showmenu=true;
			}
		}
		}
		texture.color = textureColor;
	}



	void Start () {

		
	}


	
	void Update () {

				}



		
		
		


	
	}

