using UnityEngine;
using System.Collections.Generic;


/* Array of platform objects to be made into Platform class objects */

public class platformMovement: MonoBehaviour
{
    [SerializeField] GameObject[] platformObjects;
    [SerializeField] GameObject player;
    private Platform[] platforms;
    private int currPosition;
    private float offset = 1f;

    /* Implement tweening */
    public AnimationCurve curveMove = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    private Vector3 tweenOrigin;
    private Vector3 tweenTarget;
    private float tweenElapsed = 0;
    private float tweenSpeed = 10f;
    private bool canMove;

    Platform target = null;

    void Start()
    {
        platforms = new Platform[platformObjects.Length];
        currPosition = 0;
        GameObject platform = platformObjects[0];
        player.transform.position = new Vector3(platform.transform.position.x, platform.transform.position.y, player.transform.position.z);


        tweenOrigin = player.transform.position;
        tweenTarget = player.transform.position;
        for (int i = 0; i < platformObjects.Length; i++)
        {
            /* Retrieve Platform objects and put into platforms array */
            platforms[i] = platformObjects[i].GetComponent<Platform>();
        }
    }

    Platform UpdatePosition(Platform[] platformList)
    {
        for (int i = 0; i < platformList.Length; i++)
        {
            Platform currPlatform = platformList[i];
            if (currPlatform.getAvailability())
            {
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
            /* The origin Tween position should be the current platform the player is situated on */
            Platform target = UpdatePosition(platforms[currPosition].right);
            if (target != null)
            {
                currPosition = System.Array.IndexOf(platforms, target);
                canMove = true;
            }
            tweenOrigin = player.transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            /* The origin Tween position should be the current platform the player is situated on */
            Platform target = UpdatePosition(platforms[currPosition].left);
            if (target != null)
            {
                currPosition = System.Array.IndexOf(platforms, target);
                canMove = true;
            }
            tweenOrigin = player.transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            /* Retrieve array of platforms above */
            Platform target = UpdatePosition(platforms[currPosition].up);
            if (target != null)
            {
                currPosition = System.Array.IndexOf(platforms, target);
                canMove = true;
            }
            tweenOrigin = player.transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            /* Retrieve array of platforms below */
            Platform target = UpdatePosition(platforms[currPosition].down);
            if (target != null)
            {
                currPosition = System.Array.IndexOf(platforms, target);
                canMove = true;
            }
            tweenOrigin = player.transform.position;
        }
        else if (!canMove)
        {
            /* Maintain current position on platform */
            GameObject currPlatform = platformObjects[currPosition];
            Vector3 newPos = new Vector3(currPlatform.transform.position.x, currPlatform.transform.position.y + offset, player.transform.position.z);
            player.transform.position = newPos;
        }
    }

    void FixedUpdate()
    {

        // Tween Move:
        if (canMove)
        {
            tweenTarget = platformObjects[currPosition].transform.position;
            tweenElapsed = Mathf.Clamp01(tweenElapsed);
            tweenElapsed += Time.fixedDeltaTime * tweenSpeed;
            Vector3 destination = platformObjects[currPosition].transform.position;
            tweenTarget = new Vector3(destination.x, destination.y + offset, player.transform.position.z);

            Vector3 originWithHeight = new Vector3(tweenOrigin.x, tweenOrigin.y, tweenOrigin.z);
            Vector3 targetWithHeight = new Vector3(tweenTarget.x, tweenTarget.y, tweenTarget.z);
            player.transform.position = Vector3.Lerp(originWithHeight, targetWithHeight, tweenElapsed);

            //restart the tween at the end of the move:
            if (Vector3.Distance(transform.position, tweenTarget) <= 0.05f)
            {
                canMove = false;
                tweenElapsed = 0f;
            }
        }
    }
}