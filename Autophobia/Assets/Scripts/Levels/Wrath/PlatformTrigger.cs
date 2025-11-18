
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    public TutorialManager manager;
    public int stepIndex = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        Debug.Log("Player entered platform: " + stepIndex);
        manager.PlayerReachedPlatform(stepIndex);
        gameObject.SetActive(false);
    }
}
