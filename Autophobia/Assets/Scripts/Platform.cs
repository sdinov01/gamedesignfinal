using UnityEngine;

public class Platform : MonoBehaviour
{
    private bool isAvailable;
    private GameObject platform;
    /* Constructor for Platform class */
    public void Initialize(bool isAvailable, GameObject platform)
    {
        this.isAvailable = isAvailable;
        this.platform = platform;
    }

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
        if (isAvailable)
        {
            makeVisible();
        }
        else
        {
            makeInvisible();
        }
    }

    /* Make the platform disappear and the player can no longer teleport to it */
    private void makeInvisible()
    {
        platform.SetActive(false);
    }

    /* Make the platform reappear and the player can teleport to it */
    private void makeVisible()
    {
        platform.SetActive(true);
    }
}
