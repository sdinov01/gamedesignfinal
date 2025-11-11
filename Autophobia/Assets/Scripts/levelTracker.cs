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

    void Start()
    {
        wrathComplete = false;
        slothComplete = false;
        envyComplete = false;
        prideComplete = false;
        greedComplete = false;
        gluttonyComplete = false;
        lustComplete = false;
    }
}
