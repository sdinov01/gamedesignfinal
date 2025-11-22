using UnityEngine;
using System.Collections.Generic;

public class Platform : MonoBehaviour
{
    /* Whether the platform can be teleported to */
    private bool isAvailable = true;
    /* Current platform */
    [SerializeField] GameObject platform;
    /* The platforms that the player can go to from the left/right */
    [SerializeField] public Platform[] left;
    [SerializeField] public Platform[] right;
    [SerializeField] public Platform[] up;
    [SerializeField] public Platform[] down;

    public void setAvailability(bool availability)
    {
        isAvailable = availability;
    }

    public bool getAvailability()
    {
        return isAvailable;
    }

    public GameObject getPlatform()
    {
        return platform;
    }

    /* Change visibility of platform based on availability */
    public void changeVisibility()
    {
        platform.SetActive(isAvailable);
    }
}


