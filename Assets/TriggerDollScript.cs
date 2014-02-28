using UnityEngine;

// ReSharper disable once CheckNamespace
public class TriggerDollScript : MonoBehaviour
{
    public const float ScanDistance = 1.0f / 12.0f;
    private GameObject backLeft;
    private GameObject frontLeft;
    private GameObject backRight;
    private GameObject frontRight;
    private GameObject center;

    // Casts a ray from both the forward and left trigger objects, then returns true if neither hit a target.
    public bool IsLeftClear()
    {
        var direction = transform.TransformDirection(Vector3.left);
        if (Physics.Raycast(new Ray(this.frontLeft.transform.position, direction), ScanDistance + (this.transform.localScale.x / 2)))
        {
            return false;
        }
        if (Physics.Raycast(new Ray(this.backLeft.transform.position, direction), ScanDistance + (this.transform.localScale.x / 2)))
        {
            return false;
        }

        if (Physics.Raycast(new Ray(this.center.transform.position, direction), (2 * ScanDistance) + this.transform.localScale.x))
        {
            return false;
        }

        return true;
    }

    // As above, but for right.
    public bool IsRightClear()
    {
        var direction = transform.TransformDirection(Vector3.right);
        if (Physics.Raycast(new Ray(this.frontRight.transform.position, direction), ScanDistance + (this.transform.localScale.x / 2)))
        {
            return false;
        }

        if (Physics.Raycast(new Ray(this.backRight.transform.position, direction), ScanDistance + (this.transform.localScale.x / 2)))
        {
            return false;
        }

        if (Physics.Raycast(new Ray(this.center.transform.position, direction), (2 * ScanDistance) + this.transform.localScale.x))
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

        if (Physics.Raycast(new Ray(this.center.transform.position, forwardDirection), ScanDistance + this.transform.localScale.x))
        {
            return false;
        }

        if (Physics.Raycast(new Ray(this.frontLeft.transform.position, forwardDirection), ScanDistance))
        {
            return false;
        }

        if (Physics.Raycast(new Ray(this.frontRight.transform.position, forwardDirection), ScanDistance))
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
        this.frontLeft = transform.Find("FrontLeftTrigger").gameObject;
        this.frontRight = transform.Find("FrontRightTrigger").gameObject;
        this.center = transform.Find("CenterTrigger").gameObject;
        this.backLeft = transform.Find("BackLeftTrigger").gameObject;
        this.backRight = transform.Find("BackRightTrigger").gameObject;
    }
}
