using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// One "phase" of the boss firing pattern
[System.Serializable]
public class FiringPhase
{
    public string name;               
    public int beatsBetweenShots = 1; 
    public int[] lanePattern;    
    public int beatsInPhase = 16;     
    public float projectileSpeed = 2f;   
    public bool  multi;
}

public class BossShooter : MonoBehaviour
{
    public ModularAudioHandler m;

    [Header("Spawn Points")]
    public Transform[] firePoints; 

    [Header("Phases")]
    public FiringPhase[] phases;

    [Header("Projectile")]
    public GameObject BossProjectile;               

    public float[] laneTravelDistances;

    private List<BossProjectile> laneOneProj   = new List<BossProjectile>();
    private List<BossProjectile> laneTwoProj   = new List<BossProjectile>();
    private List<BossProjectile> laneThreeProj = new List<BossProjectile>();
    private List<BossProjectile> laneFourProj  = new List<BossProjectile>();

    public  int[]   phaseOrder;
    public  int     POlen;
    public  int     POindex;

    private int currentPhaseIndex = 0;
    private float secondsPerBeat = 0f;
    private int beatInPhase = 0;      

    private void Start()
    {
        secondsPerBeat = (float)m.beatint;
        POlen = phaseOrder.Length;
        POindex = 0;

        // currentPhaseIndex = phaseOrder[POindex];

        if (!ValidateSetup())
        {
            enabled = false;
            return;
        }
        SetPhase(0);
        StartCoroutine(BeatRoutine());
    }



    private bool ValidateSetup()
    {
        if (phases == null || phases.Length == 0)
        {
            Debug.LogError("BossShooter: No phases defined!");
            return false;
        }

        if (BossProjectile == null)
        {
            Debug.LogError("BossShooter: BossProjectile prefab not assigned!");
            return false;
        }

        if (firePoints == null || firePoints.Length == 0)
        {
            Debug.LogError("BossShooter: FirePoints not assigned!");
            return false;
        }

        return true;
    }

    private void SetPhase(int phaseIndex)
    {
        currentPhaseIndex = Mathf.Clamp(phaseIndex, 0, phases.Length - 1);
        FiringPhase phase = phases[currentPhaseIndex];

        beatInPhase = 0;

        Debug.Log($"BossShooter: Switched to phase {currentPhaseIndex} ({phase.name}), Speed={phase.projectileSpeed}");
    }

   private void AdvancePhase()
    {
        POindex = (POindex + 1) % POlen;
        int next = phaseOrder[POindex];


        SetPhase(next);
    }

    private IEnumerator BeatRoutine()
    {
        while (true)
        {
            FiringPhase phase = phases[currentPhaseIndex];

            // if (phase.lanePattern.Length == 0)
            // {
            //     Debug.LogWarning($"BossShooter: Phase {currentPhaseIndex} ({phase.name}) has no lane pattern.");
            //     yield return new WaitForSeconds(secondsPerBeat);
            //     continue;
            // }

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

        // Negative lane index means skip this shot
        if (laneIndex < 0)
            return;

        laneIndex = Mathf.Clamp(laneIndex, 0, firePoints.Length - 1);

        Transform fp = firePoints[laneIndex];
        
        if (phase.multi == false)
        {
           SpawnProjectileFrom(fp, laneIndex, phase); 
        }
        else
        {
            if (phase.lanePattern.Length > 0)
            {
                for (int i = 0; i < phase.lanePattern.Length; i++)
                {
                    int lane = phase.lanePattern[i];
                    SpawnProjectileFrom (firePoints[lane], lane, phase );
                }
            }
        }
        
    }

    private void SpawnProjectileFrom(Transform firePoint, int laneIndex, FiringPhase phase)
    {
        // Get position spawn point
        Vector3 spawnPosition = firePoint.position;
        spawnPosition.z = -1f;
        
        // Instantiate at position spawn point
        GameObject projObj = Instantiate(BossProjectile, spawnPosition, Quaternion.identity);
        // Parents the spawned object with the fire point
        projObj.transform.SetParent(firePoints[laneIndex], true);

        BossProjectile proj = projObj.GetComponent<BossProjectile>();

        if (proj == null)
        {
            Debug.LogError("BossShooter: Spawned projectile does not have a BossProjectile component!");
            Destroy(projObj);
            return;
        }

        // Initialize projectile properties
        Vector2 shootDir = (Vector2)firePoint.right;
        proj.direction = shootDir.normalized;
        proj.speed = phase.projectileSpeed;

        // Set travel distance based on lane
        float distance = 0f;
        if (laneTravelDistances != null && laneIndex < laneTravelDistances.Length)
        {
            distance = laneTravelDistances[laneIndex];
        }

        proj.travelDistance = distance;
        proj.lifetime = distance / proj.speed;
        
        // Add to appropriate lane list
        AddProjectileToLane(laneIndex, proj);
    }

    private void AddProjectileToLane(int laneIndex, BossProjectile proj)
    {
        switch (laneIndex)
        {
            case 0: 
                laneOneProj.Add(proj);
                break;
            case 1: 
                laneTwoProj.Add(proj);
                break;
            case 2: 
                laneThreeProj.Add(proj);
                break;
            case 3: 
                laneFourProj.Add(proj);
                break;
            default: 
                Debug.LogWarning($"BossShooter: Invalid lane index {laneIndex}, defaulting to lane 3");
                laneFourProj.Add(proj);
                break;
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
            default: 
                Debug.LogWarning($"BossShooter: Invalid lane {currLane} in GetLaneList");
                return null;
        }
    }

    private void CleanLane(List<BossProjectile> list)
    {
        if (list == null) return;

        // Remove all null entries from the beginning
        while (list.Count > 0 && list[0] == null)
        {
            list.RemoveAt(0);
        }
    }

    public BossProjectile GetCurrLaneProj(int currLane)
    {
        var list = GetLaneList(currLane);
        if (list == null) return null;

        CleanLane(list);
        if (list.Count == 0) return null;

        return list[0];
    }

    public void RemoveProj(int currLane)
    {
        var list = GetLaneList(currLane);
        if (list == null) return;

        CleanLane(list);
        if (list.Count == 0) return;

        BossProjectile proj = list[0];
        list.RemoveAt(0);

        // Destroy the game object if it still exists
        if (proj != null)
        {
            Destroy(proj.gameObject);
        }
    }

    public void ClearAllLanes()
    {
        ClearLane(laneOneProj);
        ClearLane(laneTwoProj);
        ClearLane(laneThreeProj);
        ClearLane(laneFourProj);
    }

    private void ClearLane(List<BossProjectile> list)
    {
        if (list == null) return;

        foreach (var proj in list)
        {
            if (proj != null)
            {
                Destroy(proj.gameObject);
            }
        }
        list.Clear();
    }

    private void OnDestroy()
    {
        ClearAllLanes();
    }
}