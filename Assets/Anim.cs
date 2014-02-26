using UnityEngine;

[RequireComponent(typeof(Animator))]


public class Anim : MonoBehaviour {
    public Animator anim;
    public GameObject obj;
    static int runState = Animator.StringToHash("isMoving");
    
    void Update ()
    {
        if(obj.rigidbody.IsSleeping()) {
            this.anim.SetBool(runState,true);
        }else{
            this.anim.SetBool(runState,false);
        }
    }
}
