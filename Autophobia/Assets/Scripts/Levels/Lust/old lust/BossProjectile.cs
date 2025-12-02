using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossProjectile : MonoBehaviour
{
    public float speed = 2f;             
    public float lifetime = 100f;         
    public float hitWindow = 0.2f;        

    public float travelDistance;

    private float spawnTime;
    private bool clickedOnce = false;
    float hitDamage = 2f;

    [Header("Colliders and player finder")]
    private GameObject player;
    public GameObject healthbar;
    public healthBar hbscript;
    public Collider2D thiscollider;
    public Collider2D playercollider;
    public SpriteRenderer sprite;
    // public bool RedOrBlue;
    // public Sprite Red;
    // public Sprite Blue;
    private bool CanScore = true;

    void Start()
    {
        spawnTime           = Time.time;
        player              = GameObject.FindWithTag("Player");
        healthbar           = GameObject.FindWithTag("HealthBar");
        hbscript            = healthbar.GetComponent<healthBar>();

        thiscollider        = transform.GetComponent<Collider2D>();
        playercollider      = player.GetComponent<Collider2D>();
        Destroy(gameObject, lifetime + hitWindow);

        sprite              = transform.GetChild(0).GetComponent<SpriteRenderer>();
        // if (sprite.sprite == Red)
        // {
        //     RedOrBlue = true;
        // } 
        // else
        // {
        //     RedOrBlue = false;
        // }
        
    }

    void Update()
    {
        float age = Time.time - spawnTime;
        float t = Mathf.Clamp01(age / lifetime);

        if (t >= 1)
        {
            Destroy(gameObject);
        }

        // Vector3 dir3D = ((Vector3)direction).normalized;
        // Vector3 targetPos = startPos + dir3D * travelDistance;

        // transform.position = Vector3.Lerp(startPos, targetPos, t);

        if (thiscollider.IsTouching(playercollider))
        {
            // if (RedOrBlue)
            
                hbscript.takeDamage ((float)10);
                Destroy(gameObject);   // Destroy projectile after hit
            
            // else
            // {
                CanScore = true;
            // }
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
