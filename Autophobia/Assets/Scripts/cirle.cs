using UnityEngine;
using System.Collections;

public class cirle : MonoBehaviour
{
    public float growTime = 2f;
    private bool ready_click = true;
    private bool player_touch = false;
    private Vector3 initialScale;
    public Vector3 targetScale = new Vector3(2, 2, 2);
    private float lifeStart;

    void Start()
    {
        initialScale = transform.localScale;
        lifeStart = 0f;
    }


    void Update()
    {
        lifeStart += Time.deltaTime;

        float timer = Time.time;
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

        if (lifeStart > 3f)
        {
            // ready_click = false;
            // FindObjectOfType<GameHandler>().ShowResult("Miss!");
            // Debug.Log("Miss!");
            // CircleSpawner spawner = FindObjectOfType<CircleSpawner>();
            // if (spawner != null) {
            //     spawner.SpawnCircle();
            // }

            Destroy(gameObject);
        }

    }

    void OnClick()
    {
        float clickTime = Time.time - growTime;
        if (clickTime < 0) return; //not ready

        if (clickTime <= 0.5f)
        {
            //GameHandler.Instance.AddScore(1f);
            FindObjectOfType<GameHandler>().ShowResult("Perfect");
            Debug.Log("Perfect");
        }
        else if (clickTime <= 1f)
        {
            //GameHandler.Instance.AddScore(0.5f);
            FindObjectOfType<GameHandler>().ShowResult("Good");
            Debug.Log("Good");
        }
        else
        {
            FindObjectOfType<GameHandler>().ShowResult("Too Late");
            Debug.Log("Too Late");
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