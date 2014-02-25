using UnityEngine;
using System.Collections;

public class TriggerDollScript : MonoBehaviour
{

    GameObject left;
    GameObject right;
    GameObject front;
    public const float SCAN_DISTANCE = 1.0f / 15.0f;
    // Use this for initialization
    void Start()
    {

        left = this.transform.Find("LeftTrigger").gameObject;
        right = this.transform.Find("RightTrigger").gameObject;
        front = this.transform.Find("FrontTrigger").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool isLeftClear()
    {
        return true;
    }
    bool isRightClear()
    {
        return true;
    }
    bool isClearOnBothSides()
    {
        return (isLeftClear() && isRightClear());
    }
    public bool isClearForward()
    {
        Vector3 forwardDirection = this.transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(new Ray(front.transform.position, forwardDirection), (SCAN_DISTANCE)))
        {
            return false;
        }
        else if (Physics.Raycast(new Ray(left.transform.position, forwardDirection), (SCAN_DISTANCE + this.transform.localScale.x)))
        {
            return false;
        }
        else if (Physics.Raycast(new Ray(right.transform.position, forwardDirection), (SCAN_DISTANCE + this.transform.localScale.x)))
        {
            return false;
        }
        else
            return true;
    }
}
