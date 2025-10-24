using UnityEngine;

public class cirle : MonoBehaviour
{
    public float growTime = 2f;
    private float timer = 0f;
    private bool ready_click = true;
    private bool player_touch = false;
    private Vector3 initialScale;
    public Vector3 targetScale = new Vector3(2, 2, 2);

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer < growTime)
        {
            float t = timer / growTime; //calculate the percentage
            //between initialScale and targetScale, it grows with the growing percentage t
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
        }
        if (Input.GetKeyDown(KeyCode.Space) && ready_click && player_touch)
        {
            OnClick();
        }

        if (timer > growTime + 1f && ready_click)
        {
            ready_click = false;
            Debug.Log("Miss!");
            CircleSpawner spawner = FindObjectOfType<CircleSpawner>();
            if (spawner != null) {
                spawner.SpawnCircle();
            }

            Destroy(gameObject);
        }

    }

    void OnClick()
    {
        float clickTime = timer - growTime;
        if (clickTime < 0) return; //not ready

        if (clickTime <= 0.5f)
        {
            //GameHandler.Instance.AddScore(1f);
            Debug.Log("Perfect!");
        }
        else if (clickTime <= 1f)
        {
            //GameHandler.Instance.AddScore(0.5f);
            Debug.Log("Good!");
        }
        else
        {
            Debug.Log("Too late!");
        }

        ready_click = false;

        CircleSpawner spawner = FindObjectOfType<CircleSpawner>();
            if (spawner != null) {
                spawner.SpawnCircle();
            }

        Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            player_touch = true;
            Debug.Log("Player entered range!");
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
}