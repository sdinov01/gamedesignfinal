using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 2f;
    public float lifetime = 5f;
    public Vector2 direction = Vector2.right; 

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        Vector3 move = (Vector3)direction.normalized * speed * Time.deltaTime;
        transform.position += move;
    }
}

