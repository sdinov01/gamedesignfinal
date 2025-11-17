using System.Collections;
using UnityEngine;

public class BossShooter : MonoBehaviour
{
    public float bpm = 120f;
    public float startDelay = 0f;

    public GameObject bossProjectile;    
    public Transform[] firePoints;        
    public bool fireAllLanesEveryBeat = true;
    public int[] lanePattern;             

    private float secondsPerBeat;

    private void Start()
    {
        secondsPerBeat = 60f / bpm;
        StartCoroutine(BeatRoutine());
    }

    private IEnumerator BeatRoutine()
    {
        if (startDelay > 0f)
        {
            yield return new WaitForSeconds(startDelay);
        }

        int patternIndex = 0;

        while (true)
        {
            FireOnBeat(ref patternIndex);
            yield return new WaitForSeconds(secondsPerBeat);
        }
    }

    private void FireOnBeat(ref int patternIndex)
    {
        if (bossProjectile == null || firePoints == null || firePoints.Length == 0)
            return;

        if (fireAllLanesEveryBeat)
        {
            // Fire from all fire points
            foreach (Transform fp in firePoints)
            {
                if (fp != null)
                {
                    SpawnProjectileFrom(fp);
                }
            }
        }
        else
        {
            if (lanePattern == null || lanePattern.Length == 0)
                return;

            int lane = lanePattern[patternIndex % lanePattern.Length];
            lane = Mathf.Clamp(lane, 0, firePoints.Length - 1);

            Transform fp = firePoints[lane];
            if (fp != null)
            {
                SpawnProjectileFrom(fp);
            }

            patternIndex++;
        }
    }

    private void SpawnProjectileFrom(Transform firePoint)
    {
        GameObject projObj = Instantiate(bossProjectile, firePoint.position, Quaternion.identity);

        BossProjectile proj = projObj.GetComponent<BossProjectile>();
        if (proj == null)
        {
            Debug.LogWarning("Spawned projectile does not have a bossProjectile component!");
            return;
        }

        Vector2 shootDir = (Vector2)firePoint.right; 
        proj.direction = shootDir.normalized;
    }
}
