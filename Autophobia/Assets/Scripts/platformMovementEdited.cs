using UnityEngine;
using System.Collections.Generic;


/* Array of platform objects to be made into Platform class objects */

public class platformMovementEdited : MonoBehaviour
{
    [SerializeField] GameObject[] platformObjects;
    [SerializeField] GameObject player;
    private NewPlatform[] platforms;
    private int currPosition;
    private float offset = 1f;
    /* Make the platforms into NewPlatforms */
    void Start()
    {


        platforms = new NewPlatform[platformObjects.Length];
        currPosition = 0;
        GameObject platform = platformObjects[0];
        player.transform.position = new Vector3(platform.transform.position.x, platform.transform.position.y, player.transform.position.z);
        for (int i = 0; i < platformObjects.Length; i++)
        {
            /* Retrieve NewPlatform objects and put into platforms array */
            platforms[i] = platformObjects[i].GetComponent<NewPlatform>();
        }
    }

    NewPlatform UpdatePosition(NewPlatform[] platformList)
    {
        for (int i = 0; i < platformList.Length; i++)
        {
            NewPlatform currPlatform = platformList[i];
            if (currPlatform.getAvailability())
            {
                Debug.Log("Can reposition to platform " + i);
                Vector3 newPosition = new Vector3(currPlatform.transform.position.x, currPlatform.transform.position.y, player.transform.position.z);
                player.transform.position = newPosition;
                return currPlatform;
            }
        }
        /* Could not teleport */
        return null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            /* Retrieve array of platforms to the right */
            NewPlatform[] right = platforms[currPosition].right;
            NewPlatform returned = UpdatePosition(right);
            if (returned == null)
            {
                Debug.Log("Could not update position\n");
            } else
            {
                currPosition = System.Array.IndexOf(platforms, returned);
                Debug.Log("currPosition" + currPosition);
            }

        } else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            /* Retrieve array of platforms to the left */
            NewPlatform[] left = platforms[currPosition].left;
            NewPlatform returned = UpdatePosition(left);
            if (returned == null)
            {
                Debug.Log("Could not update position\n");
            }
            else
            {
                currPosition = System.Array.IndexOf(platforms, returned);
                Debug.Log("currPosition" + currPosition);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            /* Retrieve array of platforms to the left */
            NewPlatform[] up = platforms[currPosition].up;
            NewPlatform returned = UpdatePosition(up);
            if (returned == null)
            {
                Debug.Log("Could not update position\n");
            }
            else
            {
                currPosition = System.Array.IndexOf(platforms, returned);
                Debug.Log("currPosition" + currPosition);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            /* Retrieve array of platforms to the left */
            NewPlatform[] down = platforms[currPosition].down;
            NewPlatform returned = UpdatePosition(down);
            if (returned == null)
            {
                Debug.Log("Could not update position\n");
            }
            else
            {
                currPosition = System.Array.IndexOf(platforms, returned);
                Debug.Log("currPosition" + currPosition);
            }
        } else
        {
            /* Maintain current position on platform */
            GameObject currPlatform = platforms[currPosition].getPlatform();
            Vector3 newPos = new Vector3(currPlatform.transform.position.x, currPlatform.transform.position.y + offset, player.transform.position.z);
            player.transform.position = newPos;
        }
    }
}
