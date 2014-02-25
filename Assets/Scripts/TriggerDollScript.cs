using UnityEngine;

// ReSharper disable once CheckNamespace
public class TriggerDollScript : MonoBehaviour
{

    GameObject _left;
    GameObject _right;
    GameObject _front;
    public const float ScanDistance = 1.0f / 15.0f;
    // Use this for initialization
// ReSharper disable once UnusedMember.Local
    void Start()
    {
        _left = transform.Find("LeftTrigger").gameObject;
        _right = transform.Find("RightTrigger").gameObject;
        _front = transform.Find("FrontTrigger").gameObject;
    }

    // Update is called once per frame
// ReSharper disable once UnusedMember.Local
    void Update()
    {

    }

    public bool IsLeftClear()
    {
        Vector3 direction = transform.TransformDirection(Vector3.left);
        if (Physics.Raycast(new Ray(_left.transform.position, direction), (ScanDistance + transform.localScale.x)))
        {
            return false;
        }
        return true;
        
    }
    public bool IsRightClear()
    {
        var direction = transform.TransformDirection(Vector3.right);
        return !Physics.Raycast(new Ray(_right.transform.position, direction), (ScanDistance + transform.localScale.x));
    }

    public bool IsClearOnBothSides()
    {
        return (IsLeftClear() && IsRightClear());
    }
    public bool IsClearForward()
    {
        var forwardDirection = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(new Ray(_front.transform.position, forwardDirection), (ScanDistance)))
        {
            return false;
        }
        if (Physics.Raycast(new Ray(_left.transform.position, forwardDirection), (ScanDistance + transform.localScale.x)))
        {
            return false;
        }
        if (Physics.Raycast(new Ray(_right.transform.position, forwardDirection), ScanDistance + transform.localScale.x))
        {
            return false;
        }
        return true;
    }
}
