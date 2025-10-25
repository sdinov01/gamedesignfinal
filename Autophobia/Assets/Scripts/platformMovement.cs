using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class platformMovement : MonoBehaviour
{
    /* Array containing the platforms the player can go onto */
    [SerializeField] GameObject[] platforms;
    /* Player object */
    [SerializeField] GameObject player;
    /* Platform the player is currently on */
    private int currentPos = 0;
    private float cooldown = 1f;

    void Update()
    {
        /* If a player presses left (A) or right (D) key, update the platform the player will be situated on */

        if (Input.GetKeyDown(KeyCode.A))
        {
            // Handles transition to left platform
            currentPos--;
            if (currentPos < 0)
            {
                currentPos = platforms.Length - 1;
            }
            StartCoroutine(UpdatePosition(platforms[currentPos]));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            // Handles transition to right platform
            currentPos++;
            if (currentPos > platforms.Length - 1)
            {
                currentPos = 0;
            }
            StartCoroutine(UpdatePosition(platforms[currentPos]));
        }

        /* Update player's position to be on top of the platform */
    }

    /* Takes in a platform and updates the player's position */
    private IEnumerator UpdatePosition(GameObject other)
    {
        /* Box colliders to calculate the position of the player on the platforms */
        BoxCollider playerCollider = player.GetComponent<BoxCollider>();
        BoxCollider platformCollider = other.GetComponent<BoxCollider>();

        /* (x,y) position of platform */
        float new_pos_x = other.transform.position.x;
        float new_pos_y = other.transform.position.y;
        /* Offset of player position when on platform */
        float offSet = platformCollider.bounds.extents.y + playerCollider.bounds.extents.y;
        /* Make vector to new position */
        Vector3 new_position = new Vector3(new_pos_x, new_pos_y + offSet, 0f);
        /* Relocate to new position */
        player.transform.position = new_position;
        yield return new WaitForSeconds(cooldown);
    }
}
