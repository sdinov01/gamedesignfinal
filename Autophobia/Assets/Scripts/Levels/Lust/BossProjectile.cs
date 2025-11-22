using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 2f;             
    public float lifetime = 5f;         
    public float hitWindow = 0.2f;        
    public Vector2 direction = Vector2.right;

    [HideInInspector] public float travelDistance;

    private float spawnTime;
    private Vector3 startPos;
    private bool clickedOnce = false;
    float missDamage = 10f;

    void Start()
    {
        spawnTime = Time.time;
        startPos = transform.position;

        Destroy(gameObject, lifetime + hitWindow);
    }

    void Update()
    {
        float age = Time.time - spawnTime;
        float t = Mathf.Clamp01(age / lifetime);

        Vector3 dir3D = ((Vector3)direction).normalized;
        Vector3 targetPos = startPos + dir3D * travelDistance;

        transform.position = Vector3.Lerp(startPos, targetPos, t);
    }

    public void OnClick()
    {
        if (clickedOnce) return;
        clickedOnce = true;

        var handler = FindObjectOfType<LustLevelHandler>();
        if (handler == null)
        {
            Debug.LogWarning("BossProjectile: No LustLevelHandler found!");
            Destroy(gameObject);
            return;
        }

        float age = Time.time - spawnTime;
        float delta = age - lifetime;

        Debug.Log($"Projectile clicked. age={age:F3}, lifetime={lifetime}, delta={delta:F3}");

        if (Mathf.Abs(delta) <= hitWindow)
        {
            handler.ShowResult("Perfect");
        }
        else
        {
            handler.ShowResult("Miss");
            handler.UpdateHealth(missDamage);
        }

        Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (!Application.isPlaying) return;
        if (clickedOnce) return;         

        var handler = FindObjectOfType<LustLevelHandler>();
        if (handler == null) return;

        handler.ShowResult("Miss");
        handler.UpdateHealth(missDamage);
    }

    // 👉 NEW: how close this projectile is to its intended hit time
    public float GetTimeToHit()
    {
        float age = Time.time - spawnTime;
        return lifetime - age;   // smaller |value| means closer to the ideal hit moment
    }
}
