using UnityEngine;

public class BallDmgHandler : MonoBehaviour
{
    private bool isAtMaxSize = false;
    private BallLifetime ballLifetime;
    private GameObject player;
    private Collider2D thiscollider;
    private Collider2D playercollider;
    public bool hascollided = false;
    private healthBar HB;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player          = GameObject.FindWithTag("Player");
        ballLifetime    = GetComponent<BallLifetime>();
        thiscollider    = transform.GetComponent<Collider2D>();
        playercollider  = player.GetComponent<Collider2D>();
        HB               = GameObject.FindWithTag("HealthBar").GetComponent<healthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        bool colliderin = thiscollider.IsTouching(playercollider);

        if (colliderin && ballLifetime.canHurt && !hascollided)
        {
            HB.takeDamage (10f);
            ballLifetime.EndLife();
            hascollided = true;
        }
    }
}
