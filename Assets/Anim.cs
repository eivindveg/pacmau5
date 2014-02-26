using UnityEngine;

[RequireComponent(typeof(Animator))]


public class Anim : MonoBehaviour {
	public Animator anim;
	public GameObject obj;
	static int runState = Animator.StringToHash("isMoving");
	
	void Update () {
		if(obj.rigidbody.IsSleeping()) {
			anim.SetBool(runState,true);
		}else{
			anim.SetBool(runState,false);
		}
	}
}
