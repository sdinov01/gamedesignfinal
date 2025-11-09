
using UnityEngine;

public class circle : MonoBehaviour
{
    public Animator animator;
    public float growTime = 1.1f;
    private float timer = 0f;
    private bool ready_click = false;
    private bool player_touch = false;
    private bool hasResult = false;  // avoid repeating hit/miss

    public AudioSource musicSource;
    public float spawnTime; 
    public float targetTime;

    public SpriteRenderer spriteRenderer;
    public Sprite[] eyeSprites;

    private bool hasBeatStarted = false;

    // public float waitTime;
    // public float checkTime;


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

    void Update()
    {
       
    }

    public void OnClick()
    {
        float songTime = musicSource.time;
        float delta = songTime - (spawnTime + growTime);
        Debug.Log($"{gameObject.name} clicked! songTime={musicSource.time}, growTime = {growTime}, spawnTime={spawnTime}, delta={musicSource.time - spawnTime}");
        hasResult = true;
        ready_click = false;
        Debug.Log("real_delta");
        if (Mathf.Abs(delta) <= 0.6f)
        {   
            animator.SetTrigger("hit");
            FindObjectOfType<GameHandler>().ShowResult("Perfect");
            Debug.Log("Perfect");
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
        yield return new WaitForSeconds(0.6f); // wait until the animation has finished
        animator.SetBool("beat", false);
        hasBeatStarted = false;
        hasResult = false;
        ready_click = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player_touch = true;
            Debug.Log("Player left range!");
                
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
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
            Debug.Log("Spawned circle at " + spawnTime);
            //StartCoroutine(WaitForMiss());
        }
    }

    // private System.Collections.IEnumerator WaitForMiss()
    // {
    //     yield return new WaitForSeconds(growTime + waitTime);
    //     if (!hasResult)  //does not hit
    //     {
    //         animator.SetTrigger("miss");
    //         FindObjectOfType<GameHandler>().ShowResult("Miss");
    //         Debug.Log($"{gameObject.name} Miss triggered!");
    //         hasResult = true;
    //         StartCoroutine(ResetToIdle());
    //     }
    // }
    public bool CanBeClicked()
    {
        Debug.Log("CanbeClicked!" + (ready_click && player_touch));
        return ready_click && player_touch;
    }
}

