using UnityEngine;

public class BossFiringPhases : MonoBehaviour
{
    public FiringPhase[] phases;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < phases.Length; i++)
        {
            phases[i].projectileSpeed = 2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
