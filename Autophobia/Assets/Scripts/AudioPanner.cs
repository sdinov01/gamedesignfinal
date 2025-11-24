using UnityEngine;

public class AudioPanner : MonoBehaviour
{
    private AudioSource audioSource;
    public float panSpeed = 0.2f; // Speed of left-right panning

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on this GameObject!");
        }
    }

    void Update()
    {
        if (audioSource == null) return;

        // Ping-pong from -1 (left) to +1 (right)
        audioSource.panStereo = Mathf.Sin(Time.time * panSpeed);
    }
}
