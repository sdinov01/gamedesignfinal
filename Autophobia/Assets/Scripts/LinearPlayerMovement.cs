using UnityEngine;
using System.Collections.Generic;

public class LinearPlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject[] platformObjects;
    [SerializeField] GameObject player;
    private Platform[] platforms;
    private int currPosition;
    private float offset = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
