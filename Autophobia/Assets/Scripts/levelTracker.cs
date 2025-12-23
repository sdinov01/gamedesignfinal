using UnityEngine;

public class levelTracker : MonoBehaviour
{
    public static bool wrathComplete;
    public static bool slothComplete;
    public static bool envyComplete;
    public static bool prideComplete;
    public static bool greedComplete;
    public static bool gluttonyComplete;
    public static bool lustComplete;
    private static bool introPlayed = false;
    private static bool initialized = false;
    private static bool enabled = false;

    public static bool wrathSaved;
    public static bool slothSaved;
    public static bool envySaved;
    public static bool prideSaved;
    public static bool greedSaved;
    public static bool gluttonySaved;
    public static bool lustSaved;

    private void Awake()
    {
        if (!initialized) {
            wrathComplete = false;
            slothComplete = false;
            envyComplete = false;
            prideComplete = false;
            greedComplete = false;
            gluttonyComplete = false;
            lustComplete = false;
        }
        initialized = true;
    }
    
    public static void enableAllLevels() {
        enabled = !enabled;
        if (enabled)
        {
            /* save progress before swapping all to true */
            wrathSaved = wrathComplete;
            slothSaved = slothComplete;
            lustSaved = lustComplete;
            envySaved = envyComplete;
            prideSaved = prideComplete;
            greedSaved = greedComplete;
            gluttonySaved = gluttonyComplete;

            wrathComplete = true;
            slothComplete = true;
            envyComplete = true;
            prideComplete = true;
            greedComplete = true;
            gluttonyComplete = true;
            lustComplete = true;
        } else
        {
            /* go to previous progress */
            wrathComplete = wrathSaved;
            slothComplete = slothSaved;
            envyComplete = envySaved;
            prideComplete = prideSaved;
            greedComplete = greedSaved;
            gluttonyComplete = gluttonySaved;
            lustComplete = lustSaved;
        }
    }

    public static bool IsEnabled()
    {
        return enabled;
    }
}
