using UnityEngine;
using System.Collections;

public class KnifeController : MonoBehaviour
{
    public int sectorIndex;
    public float attackDistance = 1.2f;
    public float attackSpeed = 8f;
    public float returnSpeed = 8f;
    public bool canStart = false;

    private Vector3 basePos;
    private bool isAttacking = false;
    [SerializeField] private healthBar healthObject;
    private float lastHitTime = -999f;

    void Awake()
    {
        basePos = transform.position;
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
        Vector3 target = basePos - dir * attackDistance;


        // 刺出去
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
        healthObject.takeDamage(10f);
    }

    void ResetCollider()
    {
        Collider2D col = GetComponent<Collider2D>();
        col.enabled = false;
        col.enabled = true; 
    }
}