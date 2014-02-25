using UnityEngine;
using System.Collections;

public class PacMau5ActorScript : MonoBehaviour
{

    //string previousMovementDirection;
    bool isPlayer;
    string lastDirection;
    string blockedDirection;
    float[] wallDistance;
    GameObject playerModel;
    public const float MOVE_DISTANCE_PER_FRAME = 1.0f / 15.0f;
    TriggerDollScript triggerDoll;
    string[] possibleDirections =
	{
		"North",
		"South",
		"West",
		"East",
	};

    // Use this for initialization
    void Start()
    {

        playerModel = this.transform.Find("Body").gameObject;
        triggerDoll = this.transform.Find("Body/TriggerDoll").gameObject.GetComponent<TriggerDollScript>();

        if (this.tag == "Player")
        {
            this.isPlayer = true;
        }
        else
        {
            this.isPlayer = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        string direction = "";
        //		if (isPlayer) {
        direction = RegisterDirection(isPlayer);
        if (direction == null)
        {
            direction = lastDirection;
        }
        Move(direction);
        //		} else {


        //		}
    }


    bool CanMove(string moveDirection)
    {
        Vector3 oldPosition = this.transform.position;
        if (!triggerDoll.isClearForward())
        {
            lastDirection = null;
            return false;
        }
        else return true;
    }

    // TODO Make sure collider is a wall, other logic for eatable pills
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Wall"))
        {
            Debug.Log("Collision!");
            blockedDirection = lastDirection;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag.Contains("Wall"))
        {
            blockedDirection = null;
            Debug.Log("Exit");
        }
    }
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision!");
        blockedDirection = lastDirection;
    }

    void Move(string direction)
    {
        Rotate(direction);
        if (CanMove(direction))
        {
            //Debug.Log ("Attempting to move " + direction);
            float x = this.transform.position.x;
            float z = this.transform.position.z;


            if (direction == "East")
            {
                x += MOVE_DISTANCE_PER_FRAME;
            }
            else if (direction == "West")
            {
                x -= MOVE_DISTANCE_PER_FRAME;
            }
            else if (direction == "North")
            {
                z += MOVE_DISTANCE_PER_FRAME;
            }
            else if (direction == "South")
            {
                z -= MOVE_DISTANCE_PER_FRAME;
            }
            Vector3 newPosition = new Vector3(x, this.transform.position.y, z);
            this.transform.position = newPosition;
        }
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

        Quaternion newRotation = Quaternion.identity;
        newRotation.eulerAngles = new Vector3(0, y, 0);
        //new Quaternion (this.transform.rotation.x, y, this.transform.rotation.z, this.transform.rotation.w);
        playerModel.transform.rotation = newRotation;
    }

    string RegisterDirection(bool isPlayer)
    {

        if (isPlayer)
        {
            foreach (string inputValue in possibleDirections)
            {
                if (getInput(inputValue))
                {
                    return inputValue;
                }
            }
            return null;
        }
        else
        {
            if (lastDirection == null)
            {
                int dirAsInt = (int)Mathf.Round(Random.Range(0, 4));
                string direction;
                if (dirAsInt == 0)
                {
                    direction = "North";
                }
                else if (dirAsInt == 1)
                {
                    direction = "South";
                }
                else if (dirAsInt == 2)
                {
                    direction = "East";
                }
                else if (dirAsInt == 3)
                {
                    direction = "West";
                }
                else return null;
                lastDirection = direction;
                return direction;
            }
            else return lastDirection;

        }
    }

    bool getInput(string inputValue)
    {

        if (Input.GetButtonDown(inputValue))
        {
            lastDirection = inputValue;
            //			Debug.Log (inputValue);
            return true;
        }
        else
            return false;
    }
}
