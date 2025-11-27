using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossProjectile : MonoBehaviour
{
    public float speed = 2f;             
    public float lifetime = 5f;         
    public float hitWindow = 0.2f;        
    public Vector2 direction = Vector2.right;

    public float travelDistance;

    private float spawnTime;
    private Vector3 startPos;
    private bool clickedOnce = false;
    float hitDamage = 2f;

    [Header("Colliders and player finder")]
    public GameObject player;
    public GameObject healthbar;
    public healthBar hbscript;
    public Collider2D thiscollider;
    public Collider2D playercollider;
    public SpriteRenderer sprite;
    public bool RedOrPurple;
    public Sprite Red;
    public Sprite Purple;
    private bool CanScore = true;

    void Start()
    {
        spawnTime           = Time.time;
        startPos            = transform.position;
        player              = GameObject.FindWithTag("Player");
        healthbar           = GameObject.FindWithTag("HealthBar");
        hbscript            = healthbar.GetComponent<healthBar>();

        thiscollider        = transform.GetComponent<Collider2D>();
        playercollider      = player.GetComponent<Collider2D>();
        Destroy(gameObject, lifetime + hitWindow);

        sprite              = transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (sprite.sprite == Red)
        {
            RedOrPurple = true;
        } 
        else
        {
            RedOrPurple = false;
        }
        
    }

    void Update()
    {
        float age = Time.time - spawnTime;
        float t = Mathf.Clamp01(age / lifetime);

        Vector3 dir3D = ((Vector3)direction).normalized;
        Vector3 targetPos = startPos + dir3D * travelDistance;

        transform.position = Vector3.Lerp(startPos, targetPos, t);

        if (thiscollider.IsTouching(playercollider))
        {
            if (RedOrPurple)
            {
                hbscript.takeDamage ((float)10);
                Destroy(gameObject);   // Destroy projectile after hit
            }
            else
            {
                CanScore = true;
            }
        }
        else
        {
            CanScore = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && CanScore)
        {
            hbscript.healDamage ((float)5);
            Destroy(gameObject);
            CanScore = false;
        }

    }

    public void OnClick()
    {
        if (clickedOnce) return;
        clickedOnce = true;

        Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (!Application.isPlaying) return;
        if (clickedOnce) return;         

        var handler = FindObjectOfType<LustLevelHandler>();
        if (handler == null) return;

        // handler.ShowResult("Miss");
        // handler.UpdateHealth(hitDamage);
    }

    public float GetTimeToHit()
    {
        float age = Time.time - spawnTime;
        return lifetime - age;
    }
}
