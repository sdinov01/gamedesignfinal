using UnityEngine;

public class BallLifetime : MonoBehaviour
{
    private int beatsRemaining = 5;
    [SerializeField] private float scaleIncreasePerBeat = 0.08f;
    
    private Vector3 initialScale;
    private BallInputHandler inputHandler;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));


        initialScale = transform.localScale;
        inputHandler = GetComponent<BallInputHandler>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    void OnEnable()
    {
        BeatSync.OnBeat += OnBeat;
    }

    void OnDisable()
    {
        BeatSync.OnBeat -= OnBeat;
    }

    void OnBeat()
    {
        beatsRemaining--;
        
        // Grow the ball
        transform.localScale += Vector3.one * scaleIncreasePerBeat;
        
        // Check if this is the last beat (biggest size)
        if (beatsRemaining == 1)
        {
            if (inputHandler != null)
            {
                inputHandler.SetAtMaxSize(true);
            }
            
            // Turn green at max size
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.green;
            }
        }
        
        if (beatsRemaining <= 0)
        {
            Destroy(gameObject);
        }
    }


}