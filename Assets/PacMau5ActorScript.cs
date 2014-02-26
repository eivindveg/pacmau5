using System.Linq;

using UnityEngine;

// ReSharper disable once CheckNamespace
public class PacMau5ActorScript : MonoBehaviour
{
    public const float MoveDistancePerFrame = 1.0f / 15.0f;

    private static readonly Color NormalColor = new Color(255.0f, 0.0f, 0.0f);
    private static readonly Color SuperColor = new Color(0.0f, 0.0f, 255.0f);

    private readonly string[] possibleDirections =
    {
        "North",
        "South",
        "West",
        "East"
    };

    private bool isPlayer;
    private bool ghostKiller;

    private int killTimer;
    private string direction;
    private float[] wallDistance;
    private int framesSinceLeftBlocked;
    private int framesSinceRightBlocked;
    private int ammunition;
    private int godModeFrames;
    private GameObject playerModel;

    private GameObject mau5Model;

    private TriggerDollScript triggerDoll;

    public void AddAmmo(int amnt)
    {
        this.ammunition++;
    }

    public void TriggerGodMode(int duration)
    {
        this.godModeFrames = duration;
    }

    // Use this for initialization
    // ReSharper disable once UnusedMember.Local
    private void Start()
    {
        this.ammunition = 0;
        this.killTimer = 180;
        this.playerModel = transform.Find("Body").gameObject;
        if (this.tag == "Player")
        {
            this.mau5Model = this.playerModel.transform.Find("pacmau5_v3").gameObject;
        }

        this.triggerDoll = transform.Find("Body/TriggerDoll").gameObject.GetComponent<TriggerDollScript>();

        this.isPlayer = this.tag == "Player";
    }

    // Update is called once per frame
    // ReSharper disable once UnusedMember.Local
    private void Update()
    {
        if (this.killTimer >= 1)
        {
            this.killTimer--;
        }

        if (this.isPlayer)
        {
            if (!Input.GetButtonDown("Shoot"))
            {
                this.Shoot();
            }

            if (this.godModeFrames >= 1)
            {
                this.godModeFrames--;
                if (!this.ghostKiller)
                {
                    this.ghostKiller = true;
                    this.mau5Model.renderer.material.color = SuperColor;
                }
            }
            else
            {
                if (this.ghostKiller)
                {
                    this.ghostKiller = false;
                    this.mau5Model.renderer.material.color = NormalColor;
                }
            }
        }

        var localDirection = this.RegisterDirection() ?? this.direction;
        this.Move(localDirection);
    }

    private bool CanMove()
    {
        if (this.triggerDoll.IsClearForward())
        {
            return true;
        }

        this.direction = null;
        return false;
    }

    private void TurnLeft()
    {
        switch (this.direction)
        {
            case "North":
                this.direction = "West";
                break;
            case "East":
                this.direction = "North";
                break;
            case "South":
                this.direction = "East";
                break;
            case "West":
                this.direction = "South";
                break;
        }
    }

    private void TurnRight()
    {
        switch (this.direction)
        {
            case "North":
                this.direction = "East";
                break;
            case "East":
                this.direction = "South";
                break;
            case "South":
                this.direction = "West";
                break;
            case "West":
                this.direction = "North";
                break;
        }
    }

    private void Move(string localDirection)
    {
        if (!this.CanMove())
        {
            return;
        }

        var x = transform.position.x;
        var z = transform.position.z;

        if (!this.isPlayer)
        {
            if (Random.Range(0, 100) < 1)
            {
                if (Random.Range(0, 2) < 1)
                {
                    if (this.framesSinceLeftBlocked > 10)
                    {
                        this.TurnLeft();
                    }
                }
                else
                {
                    if (this.framesSinceRightBlocked > 10)
                    {
                        this.TurnRight();
                    }
                }
            }
            else
            {
                if (this.triggerDoll.IsLeftClear())
                {
                    this.framesSinceLeftBlocked++;
                }
                else
                {
                    this.framesSinceLeftBlocked = 0;
                }

                if (this.triggerDoll.IsRightClear())
                {
                    this.framesSinceRightBlocked++;
                }
                else
                {
                    this.framesSinceRightBlocked = 0;
                }
            }
        }

        switch (localDirection)
        {
            case "East":
                x += MoveDistancePerFrame;
                break;
            case "West":
                x -= MoveDistancePerFrame;
                break;
            case "North":
                z += MoveDistancePerFrame;
                break;
            case "South":
                z -= MoveDistancePerFrame;
                break;
        }

        var newPosition = new Vector3(x, transform.position.y, z);
        transform.position = newPosition;
    }

    private void Rotate(string localDirection)
    {
        float y = 0;

        switch (localDirection)
        {
            case "East":
                y = 90.0f;
                break;
            case "West":
                y = 270.0f;
                break;
            case "North":
                y = 0.0f;
                break;
            case "South":
                y = 180.0f;
                break;
        }

        var newRotation = Quaternion.identity;
        newRotation.eulerAngles = new Vector3(0, y, 0);
        this.playerModel.transform.rotation = newRotation;
    }

    private string RegisterDirection()
    {
        if (this.isPlayer)
        {
            return this.possibleDirections.FirstOrDefault(this.GetInput);
        }

        if (this.direction != null)
        {
            this.Rotate(this.direction);
            return this.direction;
        }

        var dirAsInt = (int)Mathf.Round(Random.Range(0, 4));
        string localDirection;
        switch (dirAsInt)
        {
            case 0:
                localDirection = "North";
                break;
            case 1:
                localDirection = "South";
                break;
            case 2:
                localDirection = "East";
                break;
            case 3:
                localDirection = "West";
                break;
            default:
                return null;
        }

        this.direction = localDirection;
        this.Rotate(this.direction);
        return localDirection;
    }

    private bool GetInput(string inputValue)
    {
        if (!Input.GetButtonDown(inputValue))
        {
            return false;
        }

        this.Rotate(inputValue);
        this.direction = inputValue;
        return true;
    }

    private void Shoot()
    {
        if (this.ammunition >= 1)
        {
            this.ammunition--;

            // INSTANTIATE BULLET OBJECT
        }
    }

    // ReSharper disable once UnusedMember.Local
    private void OnTriggerEnter(Component other)
    {
        if (!(this.killTimer >= 1))
        {
            if (other.tag == "Ghost" && this.tag == "Player")
            {
                if (this.ghostKiller)
                {
                    // TODO Implement ActorCommands.GhostKill
                    // ActorCommands.GhostKill(other);
                }
                else
                {
                    Debug.Log("Attempting to kill player!");

                    // TODO Implement ActorCommands.PlayerKill
                    ActorCommands.PlayerKill(this.gameObject);
                }
            }
        }
    }
}
