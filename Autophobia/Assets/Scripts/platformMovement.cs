using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/* Important Plaform class */
//using Platform;


/* Handles movement between platforms */
public class platformMovement : MonoBehaviour
{
    /* Array containing the platforms the player can go onto */
    [SerializeField] GameObject[] platformObjects;
    private Platform[] platforms;
    /* Player object */
    [SerializeField] GameObject player;
    /* Platform the player is currently on */
    private int currentPos = 0;
    private float cooldown = 1f;

    /* Initialize platform list */
    void Start()
    {
        platforms = new Platform[platformObjects.Length];
        for (int curr = 0; curr < platforms.Length; curr++)
        {
            Platform p = platformObjects[curr].GetComponent<Platform>();
            if (p == null)
            {
                p = platformObjects[curr].AddComponent<Platform>();
            }
            p.Initialize(true, platformObjects[curr]);
            platforms[curr] = p;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            platforms[1].setAvailability(false);
            platforms[1].changeVisibility();
        }

        /* If a player presses left (A) or right (D) key, update the platform the player will be situated on */
        if (Input.GetKeyDown(KeyCode.A))
        {
            /*          Calculates the next platform to the left         */
            currentPos--;
            if (currentPos < 0)
            {
                currentPos = platforms.Length - 1;
            }

            /* If the platform is not available, move left until the next one is available. If a full loop is performed, do nothing */
            int initialPos = currentPos;
            while (!(platforms[currentPos].getAvailability()))
            {
                /* Calculate new platform */
                currentPos--;
                if (currentPos < 0)
                {
                    currentPos = platforms.Length - 1;
                }
                /* If this is true, we have done a full loop and no platforms are available. */
                if (initialPos == currentPos)
                {
                    return;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            /*          Calculates the next platform to the right         */
            currentPos++;
            if (currentPos > platforms.Length - 1)
            {
                currentPos = 0;
            }

            /* If the platform is not available, move right until the next one is available. If a full loop is performed, do nothing */
            int initialPos = currentPos;
            while (!(platforms[currentPos].getAvailability()))
            {
                /* Calculate new platform */
                currentPos++;
                if (currentPos > platforms.Length - 1)
                {
                    currentPos = 0;
                }
                /* If this is true, we have done a full loop and no platforms are available. */
                if (initialPos == currentPos)
                {
                    return;
                }
            }
        }
        /* Update position */
        StartCoroutine(UpdatePosition(platforms[currentPos]));
    }

    /* Takes in a platform and updates the player's position */
    private IEnumerator UpdatePosition(Platform other)
    {
        /* Retrieve platform object */
        GameObject platform = other.getPlatform();

        /* Platforms y position */
        Vector3 new_position = new Vector3(
            platform.transform.position.x,
            platform.transform.position.y,
            player.transform.position.z
        );

        player.transform.position = new_position;
        yield return null;
    }
}
