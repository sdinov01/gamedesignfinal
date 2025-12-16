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

    //private static levelTracker instance;

    private void Awake()
    {
        Debug.Log("INITIALIZED LEVEL TRACKER");
        if (!initialized) {
            initialized = true;

            wrathComplete = false;
            slothComplete = false;
            envyComplete = false;
            prideComplete = false;
            greedComplete = false;
            gluttonyComplete = false;
            lustComplete = false;

        }
    }

    // private void Awake()
    // {
    //     if (instance != null && instance != this)
    //     {
    //         Destroy(gameObject);
    //         return;
    //     }

    //     instance = this;
    //     DontDestroyOnLoad(gameObject);

    //     if (initialized) return;
    //     initialized = true;

    //     wrathComplete = false;
    //     slothComplete = false;
    //     envyComplete = false;
    //     prideComplete = false;
    //     greedComplete = false;
    //     gluttonyComplete = false;
    //     lustComplete = false;

    //     introPlayed = false;
    // }
    
}
