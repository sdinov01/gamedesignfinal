using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class KnifeController : MonoBehaviour
{
    public int sectorIndex;
    public float attackDistance = 1.5f;
    public float attackSpeed = 8f;
    public float returnSpeed = 8f;
    public bool canStart = false;
    public GameObject self;

    private Vector3 basePos;
    private bool isAttacking = false;
    public healthBar health;
    private float lastHitTime = -999f;

    public Transform center; 
    public float angle;

    private float currentDamage = 10f;  
    public Light2D knifeLight;

    void Awake()
    {
        basePos = transform.position;
        self    = gameObject;

        Vector2 dir = transform.position - center.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360f;
    }

    public void TriggerAttack()
    {
        if (!canStart) return;
        StartCoroutine(DoAttack());
    }

    IEnumerator DoAttack()
    {
        isAttacking = true;
        
        Vector3 dir = -transform.up; 
        Vector3 target = basePos + dir * attackDistance;


        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, attackSpeed * Time.deltaTime);
            yield return null;
        }

        // return
        while (Vector3.Distance(transform.position, basePos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, basePos, returnSpeed * Time.deltaTime);
            yield return null;
        }
        ResetCollider();
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (Time.time - lastHitTime < 0.6f) return;
        lastHitTime = Time.time;
        Debug.Log("take damage"); 
        health.takeDamage(currentDamage);
    }

    void ResetCollider()
    {
        Collider2D col = GetComponent<Collider2D>();
        col.enabled = false;
        col.enabled = true; 
    }


//for beat attack
    public void TriggerBeatAttack()
    {
        if (!canStart) return;
        if (!isAttacking)
            StartCoroutine(DoBeatAttack());
    }

    IEnumerator DoBeatAttack()
    {
        isAttacking = true;
        currentDamage = 4f;

        Vector3 dir = -transform.up;
        Vector3 target = basePos + dir * attackDistance;

        float beatSpeed = attackSpeed * 0.6f;
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, beatSpeed * Time.deltaTime);
            yield return null;
        }

        while (Vector3.Distance(transform.position, basePos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, basePos, returnSpeed * Time.deltaTime);
            yield return null;
        }

        ResetCollider();
        currentDamage = 10f;
        isAttacking = false;
    }
}