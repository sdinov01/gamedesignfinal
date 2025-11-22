using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// One "phase" of the boss firing pattern
[System.Serializable]
public class FiringPhase
{
    public string name;               
    public float bpm = 120f;          
    public int beatsBetweenShots = 1; 
    public int[] lanePattern;         
    public int beatsInPhase = 16;     

    public float projectileSpeed = 2f;   
}

public class BossShooter : MonoBehaviour
{
    public FiringPhase[] phases;

    public GameObject bossProjectile;       
    public Transform[] firePoints;          

    public float[] laneTravelDistances = new float[] { 5f, 6f, 7f, 8f };

    private List<BossProjectile> laneOneProj   = new List<BossProjectile>();
    private List<BossProjectile> laneTwoProj   = new List<BossProjectile>();
    private List<BossProjectile> laneThreeProj = new List<BossProjectile>();
    private List<BossProjectile> laneFourProj  = new List<BossProjectile>();

    private int currentPhaseIndex = 0;
    private float secondsPerBeat = 0f;
    private int beatInPhase = 0;      

    private void Start()
    {
        if (phases == null || phases.Length == 0)
        {
            Debug.LogError("BossShooter: No phases defined!");
            enabled = false;
            return;
        }

        if (bossProjectile == null)
        {
            Debug.LogError("BossShooter: bossProjectile prefab not assigned!");
            enabled = false;
            return;
        }

        if (firePoints == null || firePoints.Length == 0)
        {
            Debug.LogError("BossShooter: firePoints not assigned!");
            enabled = false;
            return;
        }

        SetPhase(0);
        StartCoroutine(BeatRoutine());
    }

    private void SetPhase(int phaseIndex)
    {
        currentPhaseIndex = Mathf.Clamp(phaseIndex, 0, phases.Length - 1);
        FiringPhase phase = phases[currentPhaseIndex];

        if (phase.bpm <= 0f)
            phase.bpm = 120f;

        secondsPerBeat = 60f / phase.bpm;
        beatInPhase = 0;

        Debug.Log($"BossShooter: Switched to phase {currentPhaseIndex} ({phase.name}), BPM={phase.bpm}, projSpeed={phase.projectileSpeed}");
    }

    private void AdvancePhase()
    {
        int next = (currentPhaseIndex + 1) % phases.Length;
        SetPhase(next);
    }

    private IEnumerator BeatRoutine()
    {
        while (true)
        {
            FiringPhase phase = phases[currentPhaseIndex];

            if (phase.lanePattern == null || phase.lanePattern.Length == 0)
            {
                Debug.LogWarning($"BossShooter: Phase {currentPhaseIndex} ({phase.name}) has no lanePattern.");
                yield return new WaitForSeconds(secondsPerBeat);
                continue;
            }

            beatInPhase++;

            int interval = Mathf.Max(1, phase.beatsBetweenShots);

            if (beatInPhase % interval == 0)
            {
                FireOnBeat(phase);
            }

            int maxBeats = Mathf.Max(1, phase.beatsInPhase);
            if (beatInPhase >= maxBeats)
            {
                AdvancePhase();
            }

            yield return new WaitForSeconds(secondsPerBeat);
        }
    }

    private void FireOnBeat(FiringPhase phase)
    {
        int interval = Mathf.Max(1, phase.beatsBetweenShots);

        int shotsSoFar = (beatInPhase - 1) / interval;

        int patternIndex = shotsSoFar % phase.lanePattern.Length;
        int laneIndex = phase.lanePattern[patternIndex];

        if (laneIndex < 0)
            return;

        laneIndex = Mathf.Clamp(laneIndex, 0, firePoints.Length - 1);

        Transform fp = firePoints[laneIndex];
        if (fp != null)
        {
            SpawnProjectileFrom(fp, laneIndex, phase);
        }
    }

    private void SpawnProjectileFrom(Transform firePoint, int laneIndex, FiringPhase phase)
    {
        Vector3 spawnPosition = firePoint.position;
        spawnPosition.z = -1f;
        GameObject projObj = Instantiate(bossProjectile, spawnPosition, Quaternion.identity);

        BossProjectile proj = projObj.GetComponent<BossProjectile>();
        if (proj == null)
        {
            Debug.LogWarning("BossShooter: Spawned projectile does not have a BossProjectile component!");
            return;
        }

        Vector2 shootDir = (Vector2)firePoint.right;
        proj.direction = shootDir.normalized;

        proj.speed = phase.projectileSpeed;

        float distance = 5f;
        if (laneTravelDistances != null && laneIndex < laneTravelDistances.Length)
        {
            distance = laneTravelDistances[laneIndex];
        }

        proj.travelDistance = distance;

        if (proj.speed <= 0f)
        {
            proj.lifetime = 1f;  // safety fallback
        }
        else
        {
            proj.lifetime = distance / proj.speed;
        }
       
        switch (laneIndex)
        {
            case 0: laneOneProj.Add(proj);   break;
            case 1: laneTwoProj.Add(proj);   break;
            case 2: laneThreeProj.Add(proj); break;
            case 3: laneFourProj.Add(proj);  break;
            default: laneFourProj.Add(proj); break;
        }
    }

    private List<BossProjectile> GetLaneList(int currLane)
    {
        switch (currLane)
        {
            case 0: return laneOneProj;
            case 1: return laneTwoProj;
            case 2: return laneThreeProj;
            case 3: return laneFourProj;
            default: return null;
        }
    }

    private void CleanLane(List<BossProjectile> list)
    {
        if (list == null) return;

        while (list.Count > 0 && list[0] == null)
        {
            list.RemoveAt(0);
        }
    }

    public BossProjectile getCurrLaneProj(int currLane)
    {
        var list = GetLaneList(currLane);
        if (list == null) return null;

        CleanLane(list);
        if (list.Count == 0) return null;

        return list[0];
    }

    public void removeProj(int currLane)
    {
        var list = GetLaneList(currLane);
        if (list == null) return;

        CleanLane(list);
        if (list.Count == 0) return;

        list.RemoveAt(0);
    }
}
