using UnityEngine;

public class KnifeController : MonoBehaviour
{
    public float fallSpeed = 3f;
    public float destroyY = -5f;
    public System.Action<KnifeController> OnHit; // 被命中回调（可选）

    bool active = true;

    void Update()
    {
        if (!active) return;
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        if (transform.position.y < destroyY) 
        {
            Destroy(gameObject);
        }
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     // 假设 player 有 tag "Player"
    //     if (other.CompareTag("Player"))
    //     {
    //         active = false;
    //         OnHit?.Invoke(this);
    //         // play hit animation
    //         Destroy(gameObject);
    //     }
    // }
}