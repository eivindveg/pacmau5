using UnityEngine;

// ReSharper disable once CheckNamespace
public class Anim : MonoBehaviour {
	public Animator anim;
	
	void Update () {
		//Fix this.

		if(this.transform.rigidbody.velocity.magnitude > 0.2){
			anim.SetBool("isMoving",true);
		}
        else
        {
			anim.SetBool("isMoving",false);
		}
	
	}
}
