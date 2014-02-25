using System.Linq;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class PacMau5ActorScript : MonoBehaviour
{

    //string previousMovementDirection;
    bool _isPlayer;
    string _lastDirection;
    float[] _wallDistance;
    private int _framesSinceLeftBlocked;
    private int _framesSinceRightBlocked;
    GameObject _playerModel;
    public const float MoveDistancePerFrame = 1.0f / 15.0f;
    TriggerDollScript _triggerDoll;

    readonly string[] _possibleDirections =
	{
		"North",
		"South",
		"West",
		"East"
	};

    // Use this for initialization
// ReSharper disable once UnusedMember.Local
    void Start()
    {

        _playerModel = transform.Find("Body").gameObject;
        _triggerDoll = transform.Find("Body/TriggerDoll").gameObject.GetComponent<TriggerDollScript>();

        _isPlayer = tag == "Player";
    }

    // Update is called once per frame
// ReSharper disable once UnusedMember.Local
    void Update()
    {
        var direction = RegisterDirection(_isPlayer) ?? _lastDirection;
        Move(direction);
    }


    bool CanMove()
    {
        if (_triggerDoll.IsClearForward()) return true;
        _lastDirection = null;
        return false;
    }

    void TurnLeft()
    {
        switch (_lastDirection)
        {
            case "North":
                _lastDirection = "West";
                break;
            case "East":
                _lastDirection = "North";
                break;
            case "South":
                _lastDirection = "East";
                break;
            case "West":
                _lastDirection = "South";
                break;
        }
    }

    void TurnRight()
    {
        switch (_lastDirection)
        {
            case "North":
                _lastDirection = "East";
                break;
            case "East":
                _lastDirection = "South";
                break;
            case "South":
                _lastDirection = "West";
                break;
            case "West":
                _lastDirection = "North";
                break;
        }
    }

    void Move(string direction)
    {
        Rotate(direction);
        if (!CanMove()) return;
        //Debug.Log ("Attempting to move " + direction);
        var x = transform.position.x;
        var z = transform.position.z;

        if (!_isPlayer)
        {
            if (Random.Range(0, 100) < 1)
            {
                if (Random.Range(0, 2) < 1)
                {
                    if (_framesSinceLeftBlocked > 10)
                    {
                        TurnLeft();
                    }
                }
                else
                {
                    if (_framesSinceRightBlocked > 10)
                    {
                        TurnRight();
                    }
                }
            }
            else
            {
                if (_triggerDoll.IsLeftClear())
                {
                    _framesSinceLeftBlocked++;
                }
                else
                {
                    _framesSinceLeftBlocked = 0;
                }
                if (_triggerDoll.IsRightClear())
                {
                    _framesSinceRightBlocked++;
                }
                else
                {
                    _framesSinceRightBlocked = 0;
                } 
            }

        }

        if (direction == "East")
        {
            x += MoveDistancePerFrame;
        }
        else if (direction == "West")
        {
            x -= MoveDistancePerFrame;
        }
        else if (direction == "North")
        {
            z += MoveDistancePerFrame;
        }
        else if (direction == "South")
        {
            z -= MoveDistancePerFrame;
        }
        var newPosition = new Vector3(x, transform.position.y, z);
        transform.position = newPosition;
    }


    void Rotate(string direction)
    {

        //GameObject rotatableObject = GameObject.Find (this.name + "/Sphere");

        float y = 0;

        if (direction == "East")
        {
            //	Debug.Log ("Pointing player East");
            y = 90.0f;
        }
        else if (direction == "West")
        {
            //	Debug.Log ("Pointing player West");
            y = 270.0f;
        }
        else if (direction == "North")
        {
            y = 0.0f;
        }
        else if (direction == "South")
        {
            y = 180.0f;
        }

        var newRotation = Quaternion.identity;
        newRotation.eulerAngles = new Vector3(0, y, 0);
        _playerModel.transform.rotation = newRotation;
    }

    string RegisterDirection(bool isPlayer)
    {

        if (isPlayer)
        {
            return _possibleDirections.FirstOrDefault(GetInput);
        }
        if (_lastDirection != null) return _lastDirection;
        var dirAsInt = (int)Mathf.Round(Random.Range(0, 4));
        string direction;
        switch (dirAsInt)
        {
            case 0:
                direction = "North";
                break;
            case 1:
                direction = "South";
                break;
            case 2:
                direction = "East";
                break;
            case 3:
                direction = "West";
                break;
            default:
                return null;
        }
        _lastDirection = direction;
        return direction;
    }

    bool GetInput(string inputValue)
    {
        if (!Input.GetButtonDown(inputValue)) return false;
        _lastDirection = inputValue;
        return true;
    }
}
