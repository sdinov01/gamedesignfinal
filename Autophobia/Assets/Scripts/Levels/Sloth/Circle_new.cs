using UnityEngine;

public class Circle : MonoBehaviour
{
    public Animator animator;  
    public AudioSource musicSource;
    public GameHandler GameHandler;
    public bool ready = false;        
    public bool playerInside = false;  
    public float hitWindow = 0.6f;     //time interval for hitting
    private bool judged = false;
    public healthBar health;
    private float idealHitTime = -1f;

    public void Trigger(float triggerTime)
    {
        idealHitTime = triggerTime + 1.09f;
        ready = true;  
        judged = false; 
        
        animator.ResetTrigger("hit");
        animator.ResetTrigger("miss");
        
        animator.Play("eye_animation", -1, 0f);
        // animator.SetBool("beat", true);
    }
    void Update()
    {
        //player click
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("you clicked");
            OnClick(musicSource.time);
        }
        //when player does not do anything
        if (ready && !judged)
        {
            if (Time.time > idealHitTime + hitWindow)
            {
                judged = true;
                ready = false;
                animator.SetTrigger("miss");
                GameHandler.Instance.ShowResult("Miss");
                health.takeDamage(-10);
            }
        }
    }

    public void OnClick(float clickTime)
    {
        if (idealHitTime <= 0) 
        {
            return;
        }
        judged = true;
        ready = false;
        Debug.Log("clickTime: " + clickTime);
        Debug.Log("ideal: " + idealHitTime);
        float delta = Mathf.Abs(clickTime - idealHitTime); // |delat|
        Debug.Log("delta" + delta);
        Debug.Log(playerInside);
        Debug.Log(delta <= hitWindow);
        if (delta <= hitWindow &&  playerInside)
        {
            Debug.Log("perfect");
            animator.SetTrigger("hit");
            GameHandler.Instance.ShowResult("Perfect");
            //animator.Play("", -1, 0f);
        }
        else
        {
            animator.SetTrigger("miss");
            GameHandler.Instance.ShowResult("Miss");
            health.takeDamage(-10);
        }
        idealHitTime = -1f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("playerInside");
        if (other.CompareTag("Player"))
            playerInside = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInside = false;
    }
}