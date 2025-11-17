using UnityEngine;
using System.Collections;

public class KnifeController : MonoBehaviour
{
    public float attackDistance = 0.5f;
    public float attackSpeed = 8f;
    public float returnSpeed = 8f;
    public bool canStart = false;

    private Vector3 basePos;
    private bool isAttacking = false;

    void Awake()
    {
        basePos = transform.position;
    }

    public void TriggerAttack()
    {
        if (!canStart || isAttacking) return;
        StartCoroutine(DoAttack());
    }

    IEnumerator DoAttack()
    {
        Debug.Log("attack.");
        isAttacking = true;
        
        Vector3 dir = transform.up; 
        Vector3 target = basePos + dir * attackDistance;
        Debug.Log("basePos: " + basePos + ", target: " + target);


        // 刺出去
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, attackSpeed * Time.deltaTime);
            Debug.Log("Moving to target: " + transform.position);
            yield return null;
        }

        // 回来
        while (Vector3.Distance(transform.position, basePos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, basePos, returnSpeed * Time.deltaTime);
            yield return null;
        }

        isAttacking = false;
    }
}