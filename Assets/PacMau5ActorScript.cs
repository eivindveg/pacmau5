using System.Linq;

using UnityEngine;

// ReSharper disable once CheckNamespace
public class PacMau5ActorScript : MonoBehaviour
{
    public const float MoveDistancePerFrame = 1.0f / 8.0f;

    private static readonly Color NormalColor = new Color(0.89f, 0.0f, 0.13f);
    private static readonly Color SuperColor = new Color(0.03f, 0.56f, 0.84f);

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
    private int blinkTimer;
    private int godModeFrames;
    private GameObject actorModel;
    private GameObject mau5Model;
    private GameObject ghostModel;
    private TriggerDollScript triggerDoll;

    public int TeleportCooldown { get; set; }

    public void TriggerGodMode(int duration)
    {
        this.godModeFrames = duration;
    }

    // Use this for initialization
    // ReSharper disable once UnusedMember.Local
    private void Start()
    {
        // Make sure we know if this is a player without checking its tag all the time
        this.isPlayer = this.tag == "Player";

        // Preinit variable
        this.blinkTimer = 0;

        // Make sure player gets god mode right after spawning. Prevents ghost cheesing.
        this.killTimer = this.isPlayer ? 180 : 0;

        // Assign a reference to the actor's body
        this.actorModel = transform.Find("Body").gameObject;

        // If this is a player, make sure its PacMau5 model is assigned(ghost actors do not have this model)
        if (this.isPlayer)
        {
            this.mau5Model = this.actorModel.transform.Find("pacmau5_v5/Pacmau5.1").gameObject;
        }

        // Assign the trigger doll's script
        this.triggerDoll = transform.Find("Body/TriggerDoll").gameObject.GetComponent<TriggerDollScript>();
    }

    private void DecrementTimers()
    {
        // If the actor currently has a teleport cooldown, decrement it.
        if (this.TeleportCooldown >= 1)
        {
            this.TeleportCooldown--;
        }

        // If the actor has recently been killed, decrement the kill timer.
        if (this.killTimer >= 1)
        {
            this.killTimer--;
            if (this.killTimer == 0)
            {
                this.mau5Model.SetActive(true);
            }
        }


        // This only applies to player actors
        if (this.isPlayer)
        {
            // If the actor has recently eaten a super pill, if not, make sure it's no longer a ghostkiller.
            if (this.godModeFrames >= 1)
            {
                // Decrement the timer
                this.godModeFrames--;

                // Make sure the state becomes ghostKiller
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

    }

    // Update is called once per frame
    // ReSharper disable once UnusedMember.Local
    private void Update()
    {
        // Perform timing functions, blink or otherwise change the visuals of the
        // character in case of god mode because of pills or recent death.
        this.DecrementTimers();

        // Get a travel direction if we don't have one.
        string localDirection = this.RegisterDirection() ?? this.direction;

        // Rotate towards the direction selected by input
        this.Rotate(this.direction);

        // Move the character in the locally selected direction. May or may not be the same as the input-selected direction.
        this.Move(localDirection);
        if (this.isPlayer)
        {
            this.direction = localDirection;
        }
    }

    // Returns true if the triggerDoll reports that there are no walls directly ahead, otherwise it sets the direction of travel
    // to null so a new one has to be registered.
    private bool CanMove()
    {
        if (this.triggerDoll.IsClearForward())
        {
            return true;
        }

        this.direction = null;
        return false;
    }

    // Returns true if the passed direction is left of the currently faced direction.
    private bool IsLeft(string localDirection)
    {
        switch (this.direction)
        {
            case "North":
                return localDirection == "West";
            case "East":
                return localDirection == "North";
            case "South":
                return localDirection == "East";
            case "West":
                return localDirection == "South";
            default:
                return false;
        }
    }

    // Returns true if the passed direction is right of the currently faced direction.
    private bool IsRight(string localDirection)
    {
        switch (this.direction)
        {
            case "North":
                return localDirection == "East";
            case "East":
                return localDirection == "South";
            case "South":
                return localDirection == "West";
            case "West":
                return localDirection == "North";
            default:
                return false;
        }
    }

    // Changes the actor's direction to be left of the currently faced direction.
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

    // Changes the actor's direction to be right of the currently faced direction.
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

    // If, and only if, the character can move forward, moves the character forward.
    private void Move(string localDirection)
    {
        if (!this.CanMove())
        {
            return;
        }

        float x = transform.position.x;
        float z = transform.position.z;

        // Logic for randomly turning ghosts left or right if the sides have been clear for a while.
        if (!this.isPlayer)
        {
            // 2% chance to trigger.
            // If it didn't trigger, increment counters if sides are clear. Else reset counters.
            if (Random.Range(0, 50) < 1)
            {
                // 50% chance per side.
                if (Random.Range(0, 2) < 1)
                {
                    // If left has been clear for ten frames, turn left and reset both frames.
                    if (this.framesSinceLeftBlocked > 10)
                    {
                        this.TurnLeft();
                        this.framesSinceLeftBlocked = 0;
                        this.framesSinceRightBlocked = 0;
                    }
                }
                else
                {
                    // If left has been clear for ten frames, turn left and reset both frames.
                    if (this.framesSinceRightBlocked > 10)
                    {
                        this.TurnRight();
                        this.framesSinceRightBlocked = 0;
                        this.framesSinceLeftBlocked = 0;
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

        // Regardless of the actor's player status, stage the movement towards the selected direction.
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

        // Move the actor.
        Vector3 newPosition = new Vector3(x, transform.position.y, z);
        transform.position = newPosition;
    }

    // Rotates the character to face the selected direction.
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

        Quaternion newRotation = Quaternion.identity;
        newRotation.eulerAngles = new Vector3(0, y, 0);
        this.actorModel.transform.rotation = newRotation;
    }

    // Sets a direction for the player.
    private string RegisterDirection()
    {
        string localDirection;

        // Code specific to player based movement.
        if (this.isPlayer)
        {
            // Get direction from input.
            localDirection = this.possibleDirections.FirstOrDefault(this.GetInput);

            // If the new direction is left of the current direction, make sure it is clear.
            // This (largely) prevents being able to turn towards a wall.
            if (this.IsLeft(localDirection))
            {
                if (this.triggerDoll.IsLeftClear())
                {
                    return localDirection;
                }

                // Return null if the player is attempting to move left and it's blocked.
                return null;
            }

            // As above, but for right.
            if (this.IsRight(localDirection))
            {
                if (this.triggerDoll.IsRightClear())
                {
                    return localDirection;
                }
                
                return null;
            }

            // Return direction, even if it is null.
            return localDirection;
        }

        /*
         * The following code pertains to pseudorandom limited movement for non-player actors.
         */

        // If we can't move in the currently selected direction, reset the direction so we get a new one.
        if (!this.triggerDoll.IsClearForward())
        {
            this.direction = null;
        }

        // If the direction was reset, set a new direction.
        if (this.direction == null)
        {
            int dirAsInt = (int)Mathf.Round(Random.Range(0, 4));
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

            // Turn this direction.
            this.direction = localDirection;
            this.Rotate(this.direction);
        }

        return this.direction;
    }

    // Check if an input is selected.
    private bool GetInput(string inputValue)
    {
        if (!Input.GetButton(inputValue))
        {
            return false;
        }

        return true;
    }

    // If the player runs into a ghost, send one of them to the ActorCommands script for destruction, unless the player is immune due to recent death.
    // ReSharper disable once UnusedMember.Local
    private void OnTriggerEnter(Component other)
    {
        if (!(this.killTimer >= 1))
        {
            if (other.tag == "Ghost" && this.isPlayer)
            {
                if (this.ghostKiller)
                {
                    ActorCommands.GhostKill(other.gameObject);
                }
                else
                {
                    ActorCommands.PlayerKill(this.gameObject);
                }
            }
        }
    }
}
