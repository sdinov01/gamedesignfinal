using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class endscene : MonoBehaviour
{
    private GameObject cameraObj;
    private GameObject self;
    private AudioSource audio;
    private PublicFuncs funcs;
    private float time = 3f;
    private float minscale = 0.01f;
    private float maxscale = 1.2f;
    private SpriteRenderer sr;
    public bool didwin;
    private float currZ = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio       = null;
        funcs       = GameObject.FindWithTag ("GameController").GetComponent<PublicFuncs>();
        transform.localScale = funcs.vEqual (minscale);
        // transform.gameObject.SetActive (false);
        sr          = transform.gameObject.GetComponent<SpriteRenderer>();
        sr.enabled  = false;
    }
    public void ScreenFill()
    {
        audio       = GameObject.FindWithTag ("MusicPlayer").GetComponent<AudioSource>();
        sr.enabled  = true;
        sr.color    = Color.gray;
        StartCoroutine(EndingCoro());
    }

    private IEnumerator EndingCoro()
    {
        float elapsedtime = 0f;
        while (elapsedtime <= time)
        {
            // Scale
            elapsedtime             += Time.deltaTime;
            float t                 = elapsedtime / time;
            float newscale          = Mathf.Lerp(minscale, maxscale, t);
            transform.localScale    = funcs.vEqual (newscale);

            // Rotation
            currZ = (currZ + Time.deltaTime) % 360;
            transform.Rotate (new Vector3(
                transform.localEulerAngles.x,
                transform.localEulerAngles.y,
                currZ), Space.Self);

            if (!didwin)
            {
                sr.color = Color.Lerp (sr.color, Color.black, Time.deltaTime);
            }
            else
            {
                sr.color = Color.Lerp (sr.color, Color.white, Time.deltaTime);
            }

            yield return null;
        }
        if (audio.isPlaying) { audio.Stop(); }
        transform.localScale = funcs.vEqual(maxscale);
        if (didwin) { SceneManager.LoadScene("Pride_end_dialogue"); }
        else { SceneManager.LoadScene("GameOver_Scene"); }
    }
    
}
