using UnityEngine;
using System.Collections.Generic;

public class NewPlatform : MonoBehaviour
{
    /* Whether the platform can be teleported to */
    private bool isAvailable = true;
    /* Current platform */
    [SerializeField] GameObject platform;
    /* The platforms that the player can go to from the left/right */
    [SerializeField] public NewPlatform[] left;
    [SerializeField] public NewPlatform[] right;
    [SerializeField] public NewPlatform[] up;
    [SerializeField] public NewPlatform[] down;

    /* Constructor */
    //public void Initialize(bool isAvailable, GameObject platform)
    //{
    //    this.isAvailable = isAvailable;
    //    this.platform = platform;
    //    platform.SetActive(true);
    //}

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


