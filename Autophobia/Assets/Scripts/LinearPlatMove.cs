using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Array of platform objects to be made into Platform class objects */

public class linearPlatMove : MonoBehaviour
{
    [SerializeField] GameObject[] platformObjects;
    [SerializeField] GameObject player;
    
    private Platform[] platforms;
    private int currPosition;
    private float offset = 0.2f;

    public AnimationCurve curveMove = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    private Vector3 tweenOrigin;
    private Vector3 tweenTarget;
    private float tweenElapsed = 0;
    private float tweenSpeed = 10f;
    private bool canMove = false;

    void Start()
{
    platforms = new Platform[platformObjects.Length];
    currPosition = 0;

    bool valid = true;

    for (int i = 0; i < platformObjects.Length; i++)
    {
        if (platformObjects[i] == null)
        {
            Debug.LogError("ERROR: platformObjects[" + i + "] is NULL. Fix array in Inspector.");
            valid = false;
            continue;
        }

        Platform p = platformObjects[i].GetComponent<Platform>();
        if (p == null)
        {
            Debug.LogError("ERROR: platformObjects[" + i + "] has NO Platform component. Fix in Inspector.", platformObjects[i]);
            valid = false;
            continue;
        }

        platforms[i] = p;
    }

    if (!valid)
    {
        Debug.LogError("Platform setup invalid. Stopping movement script.");
        enabled = false;   // ← prevents Update() from running and prevents all crashes
        return;
    }

    // Position player on first platform
    GameObject platform = platformObjects[0];
    player.transform.position = new Vector3(
        platform.transform.position.x,
        platform.transform.position.y,
        player.transform.position.z
    );

    tweenOrigin = player.transform.position;
    tweenTarget = player.transform.position;
}


    /* Finds the first available platform in a list */
    Platform GetTargetPlatform(Platform[] platformList)
    {
        if (platformList == null || platformList.Length == 0)
            return null;

        for (int i = 0; i < platformList.Length; i++)
        {
            Platform currPlatform = platformList[i];
            if (currPlatform != null && currPlatform.getAvailability())
                return currPlatform;
        }

        return null;
    }


    // void Update()
    // {
    //     Platform target = null;

    //     // =======================
    //     // MOVEMENTS
    //     // =======================

    //     if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
    //     {
    //         target = GetTargetPlatform(platforms[currPosition].right);
    //     }
    //     else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
    //     {
    //         target = GetTargetPlatform(platforms[currPosition].left);
    //     }
    //     else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
    //     {
    //         target = GetTargetPlatform(platforms[currPosition].up);
    //     }
    //     else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
    //     {
    //         target = GetTargetPlatform(platforms[currPosition].down);
    //     }

    //     // =======================
    //     // PROCESS MOVEMENT
    //     // =======================

    //     if (target != null)
    //     {
    //         // Move to new platform index
    //         currPosition = System.Array.IndexOf(platforms, target);
    //         canMove = true;
    //     }

    //     // Always update tween origin AFTER checking movement
    //     tweenOrigin = player.transform.position;

    //     // =======================
    //     // NO MOVEMENT: Snap player to current platform
    //     // =======================
    //     if (!canMove)
    //     {
    //         GameObject currPlatform = platformObjects[currPosition];
    //         Vector3 targetPos = new Vector3(
    //             currPlatform.transform.position.x,
    //             currPlatform.transform.position.y + offset,
    //             player.transform.position.z
    //         );

    //         player.transform.position = targetPos;
    //     }
    // }
    void Update()
{
    if (currPosition < 0 || currPosition >= platforms.Length)
        return; // safety

    Platform target = null;
    Platform current = platforms[currPosition];

    if (current == null)
        return; // safety

    if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        target = GetTargetPlatform(current.right);

    else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        target = GetTargetPlatform(current.left);

    else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        target = GetTargetPlatform(current.up);

    else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        target = GetTargetPlatform(current.down);

    if (target != null)
    {
        currPosition = System.Array.IndexOf(platforms, target);
        canMove = true;
    }

    tweenOrigin = player.transform.position;

    if (!canMove)
    {
        GameObject currPlatform = platformObjects[currPosition];
        Vector3 targetPos = new Vector3(
            currPlatform.transform.position.x,
            currPlatform.transform.position.y + offset,
            player.transform.position.z
        );
        player.transform.position = targetPos;
    }
}


    void FixedUpdate()
    {
        if (!canMove)
            return;

        // Tween Move
        tweenTarget = platformObjects[currPosition].transform.position;
        tweenElapsed += Time.fixedDeltaTime * tweenSpeed;

        Vector3 destination = platformObjects[currPosition].transform.position;
        tweenTarget = new Vector3(
            destination.x,
            destination.y + offset,
            player.transform.position.z
        );

        Vector3 origin = tweenOrigin;
        Vector3 target = tweenTarget;

        player.transform.position = Vector3.Lerp(origin, target, tweenElapsed);

        // End tween
        if (Vector3.Distance(player.transform.position, tweenTarget) <= 0.025f)
        {
            canMove = false;
            tweenElapsed = 0f;
        }
    }

    public int getCurrPosition() {
        return currPosition;
    }
}
