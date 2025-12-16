using UnityEngine;
using System.Collections;
public class audioplay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource audioSource;

    void Start()
    {
        StartCoroutine(PlayAfterDelay(2.8f));
    }

    IEnumerator PlayAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        audioSource.Play();
    }
}
