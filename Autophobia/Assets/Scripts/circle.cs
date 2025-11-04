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

    private bool hasBeatStarted = false;
    private bool hasResult = false;  // avoid repeating hit/miss

  

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            //clone animation controller
            animator.runtimeAnimatorController = Instantiate(animator.runtimeAnimatorController);
        }
        animator.SetBool("beat", false);
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
        else
        {
            animator.SetTrigger("miss");
            FindObjectOfType<GameHandler>().ShowResult("Miss");
            Debug.Log("Late");

        }

        

        // go back go idle state
        StartCoroutine(ResetToIdle());
    }

    System.Collections.IEnumerator ResetToIdle()
    {
        yield return new WaitForSeconds(1.0f); // wait until the animation has finished
        animator.SetBool("beat", false);
        hasBeatStarted = false;
        hasResult = false;
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
    public void TriggerBeat()
    {
        if (!hasBeatStarted)
        {
            hasBeatStarted = true;
            ready_click = true;
            animator.SetBool("beat", true);
            spawnTime = musicSource.time;
            Debug.Log("BEAT!");
            
            
            StartCoroutine(WaitForMiss());
        }
    }

    private System.Collections.IEnumerator WaitForMiss()
    {
        yield return new WaitForSeconds(growTime + 0.5f); 
        if (!hasResult)  //does not hit
        {
            animator.SetTrigger("miss");
            FindObjectOfType<GameHandler>().ShowResult("Miss");
            Debug.Log($"{gameObject.name} Miss triggered!");
            hasResult = true;
            StartCoroutine(ResetToIdle());
        }
    }
}

