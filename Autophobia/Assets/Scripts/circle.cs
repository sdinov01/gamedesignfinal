using UnityEngine;

public class circle : MonoBehaviour
{
    public float growTime = 2f;
    private float timer = 0f;
    private bool ready_click = true;
    private bool player_touch = false;
    public AudioSource musicSource;
    public float spawnTime;

    void Start(){
    }

    void Update(){
        float songTime = musicSource.time; // current time in music
        float delta = songTime - spawnTime; // 
        if (Input.GetKeyDown(KeyCode.Space) && ready_click && player_touch){
            OnClick(delta);
        }
        if (delta > 2.1f && ready_click){
            ready_click = false;
            FindObjectOfType<GameHandler>().ShowResult("Miss!"); //function in gamehandler.cs
            Debug.Log("Miss!");

            Destroy(gameObject);
        }

    }

    void OnClick(float delta) {
        if (delta < 0) return; //not ready

        if (delta <= 0.5f) {
            //GameHandler.Instance.AddScore(1f);
            FindObjectOfType<GameHandler>().ShowResult("Perfect");
            Debug.Log("Perfect");
        }
        else if (delta <= 1f) {
            //GameHandler.Instance.AddScore(0.5f);
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