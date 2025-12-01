using UnityEngine;
using System.Collections;

public class StartLifetime : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int beatsRemaining = 5;
    private float scaleIncreasePerBeat = 0.08f;
    private Vector3 initialScale;
    private BallInputHandler inputHandler;
    private BallDmgHandler damageHandler;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Color affected;
    private ModularAudioHandler MAH;
    private RotateOnBeat ROB;

    void Start()
    {
        transform.rotation  = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        MAH                 = GameObject.FindWithTag("AudioParser").GetComponent<ModularAudioHandler>();
        initialScale        = transform.localScale;
        inputHandler        = GetComponent<BallInputHandler>();
        spriteRenderer      = GetComponent<SpriteRenderer>();
        ROB                 = GetComponent<RotateOnBeat>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        affected    = GameObject.FindWithTag("ColorSource").GetComponent<SpriteRenderer>().color;
        StartCoroutine (ManualCountIn());
    }



    // void OnEnable()
    // {
    //     // BeatSync.OnHalfBeat += OnHalfBeat;
    //     StartCoroutine (ManualCountIn());
    // }

    private IEnumerator ManualCountIn()
    {
        for (int i = 0; i < 5; i++)
        {
            OnBeat();
            ROB.RotateStep();
            if (i == 4) { spriteRenderer.color = affected; Debug.Log ("color changed!"); }
            yield return new WaitForSeconds((float)MAH.beatint);
        }
        EndLife();
    }


    void OnBeat()
    {
        beatsRemaining--;
        
        // Grow the ball
        transform.localScale += Vector3.one * scaleIncreasePerBeat;
        
        
        if (beatsRemaining <= 0)
        {
            EndLife();
        }
    }

    public void EndLife()
    {
        Destroy(gameObject);
    }

    // void OnHalfBeat ()
    // {
    //     transform.localScale += Vector3.one * scaleIncreasePerBeat;
    // }
}
