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

    public bool isWrathComplete () {
        return wrathComplete;
    }

    public bool isLustComplete () {
        return lustComplete;
    }

    public bool isGreedComplete () {
        return greedComplete;
    }

    public bool isGluttonyComplete () {
        return gluttonyComplete;
    }

    public bool isPrideComplete () {
        return prideComplete;
    }

    public bool isEnvyComplete () {
        return envyComplete;
    }

    public bool isSlothComplete () {
        return slothComplete;
    }
}
