using UnityEngine;

public class circle : MonoBehaviour
{
    public Animator animator;
    public float growTime = 2f;

    public float lifespan = 2.1f;
    private float timer = 0f;
    private bool ready_click = true;
    private bool player_touch = false;
    public AudioSource musicSource;
    public float spawnTime; //time to appear
    public float targetTime; //time to hit

    public SpriteRenderer spriteRenderer;
    public Sprite[] eyeSprites; //fully open, close, half open

    void Start(){
    }

    void Update(){
        float songTime = musicSource.time; // current time in music
        float delta = songTime - spawnTime;
        lifespan -= Time.deltaTime;

        // control eye animation
        float t = Mathf.Clamp01(delta / growTime);
        int frameCount = eyeSprites.Length;
        int frameIndex = Mathf.Clamp(Mathf.FloorToInt(t * frameCount), 0, frameCount - 1);
        spriteRenderer.sprite = eyeSprites[frameIndex];

        if (Input.GetKeyDown(KeyCode.Space) && ready_click && player_touch){
            OnClick(delta);
        }
        if (lifespan <= 0f){
            ready_click = false;
            animator.SetBool("miss", true);
            FindObjectOfType<GameHandler>().ShowResult("Miss!"); //function in gamehandler.cs
            Debug.Log("Miss!");
            Destroy(gameObject);
        }

    }

    void OnClick(float delta) {
        if (Mathf.Abs(delta - growTime) <= 0.5f) {
            //GameHandler.Instance.AddScore(1f);
            animator.SetBool("hit", true);
            FindObjectOfType<GameHandler>().ShowResult("Perfect");
            Debug.Log("Perfect");
        }
        else if (Mathf.Abs(delta - growTime) <= 1f) {
            //GameHandler.Instance.AddScore(0.5f);
            animator.SetBool("hit", true);
            FindObjectOfType<GameHandler>().ShowResult("Good");
            Debug.Log("Good");
        }
        else {
            FindObjectOfType<GameHandler>().ShowResult("Too Late");
            Debug.Log("Too Late");
        }

        ready_click = false;

        Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) 
        {
            player_touch = true;
            Debug.Log("Player entered range!");
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            player_touch = false;
            Debug.Log("Player left range!");
        }
    }
}