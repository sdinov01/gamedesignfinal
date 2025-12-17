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
        wrathComplete = !wrathComplete;
        slothComplete = !slothComplete;
        envyComplete = !envyComplete;
        prideComplete = !prideComplete;
        greedComplete = !greedComplete;
        gluttonyComplete = !gluttonyComplete;
        lustComplete = !lustComplete;
    }
}
