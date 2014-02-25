using UnityEngine;

// ReSharper disable once CheckNamespace
public class TriggerDollScript : MonoBehaviour
{
    public const float ScanDistance = 1.0f / 15.0f;
    private GameObject left;
    private GameObject right;
    private GameObject front;

    public bool IsLeftClear()
    {
        var direction = transform.TransformDirection(Vector3.left);
        return !Physics.Raycast(new Ray(this.left.transform.position, direction), ScanDistance + this.transform.localScale.x);
    }

    public bool IsRightClear()
    {
        var direction = transform.TransformDirection(Vector3.right);
        return !Physics.Raycast(new Ray(this.right.transform.position, direction), ScanDistance + transform.localScale.x);
    }

    public bool IsClearOnBothSides()
    {
        return this.IsLeftClear() && this.IsRightClear();
    }

    public bool IsClearForward()
    {
        var forwardDirection = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(new Ray(this.front.transform.position, forwardDirection), ScanDistance))
        {
            return false;
        }

        if (Physics.Raycast(new Ray(this.left.transform.position, forwardDirection), ScanDistance + transform.localScale.x))
        {
            return false;
        }

        if (Physics.Raycast(new Ray(this.right.transform.position, forwardDirection), ScanDistance + transform.localScale.x))
        {
            return false;
        }

        return true;
    }

    // Use this for initialization
    // ReSharper disable once UnusedMember.Local
    private void Start()
    {
        this.left = transform.Find("LeftTrigger").gameObject;
        this.right = transform.Find("RightTrigger").gameObject;
        this.front = transform.Find("FrontTrigger").gameObject;
    }
}
