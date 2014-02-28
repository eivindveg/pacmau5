using UnityEngine;

// ReSharper disable once CheckNamespace
public class TriggerDollScript : MonoBehaviour
{
    public const float ScanDistance = 1.0f / 12.0f;
    private GameObject left;
    private GameObject right;
    private GameObject front;

    // Casts a ray from both the forward and left trigger objects, then returns true if neither hit a target.
    public bool IsLeftClear()
    {
        var direction = transform.TransformDirection(Vector3.left);
        if (Physics.Raycast(new Ray(this.left.transform.position, direction), ScanDistance + (this.transform.localScale.x / 2)))
        {
            return false;
        }

        var position = this.transform.position;
        position.z -= 0.3f;
        if (Physics.Raycast(new Ray(position, direction), ScanDistance + transform.localScale.x))
        {
            return false;
        }

        return true;
    }

    // As above, but for right.
    public bool IsRightClear()
    {
        var direction = transform.TransformDirection(Vector3.right);
        if (Physics.Raycast(new Ray(this.right.transform.position, direction), ScanDistance + (this.transform.localScale.x / 2)))
        {
            return false;
        }

        var position = this.transform.position;
        position.z -= 0.3f;
        if (Physics.Raycast(new Ray(position, direction), ScanDistance + transform.localScale.x))
        {
            return false;
        }

        return true;
    }

    public bool IsClearOnBothSides()
    {
        return this.IsLeftClear() && this.IsRightClear();
    }

    // Sends a ray forwards from all three trigger objects, then returns false if any of them hit a wall
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
        // Assign trigger objects.
        this.left = transform.Find("LeftTrigger").gameObject;
        this.right = transform.Find("RightTrigger").gameObject;
        this.front = transform.Find("FrontTrigger").gameObject;
    }
}
