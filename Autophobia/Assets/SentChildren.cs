using UnityEngine;
using System.Collections.Generic;

public class SentChildren : MonoBehaviour
{
    public Transform destination;
    public float moveDuration = 2f; // Time in seconds for movement
    public bool rotate;
    
    private Dictionary<Transform, ChildMovement> childMovements = new Dictionary<Transform, ChildMovement>();
    
    private class ChildMovement
    {
        public Vector3 startPosition;
        public float elapsedTime;
        public bool isMoving;
        
        public ChildMovement(Vector3 start)
        {
            startPosition = start;
            elapsedTime = 0f;
            isMoving = true;
        }
    }
    
    void Update()
    {
        if (destination == null) return;
        
        List<Transform> toRemove = new List<Transform>();
        
        foreach (Transform child in transform)
        {
            // Initialize movement data if not exists
            if (!childMovements.ContainsKey(child))
            {
                childMovements[child] = new ChildMovement(child.position);
            }
            
            ChildMovement movement = childMovements[child];
            
            if (movement.isMoving)
            {
                movement.elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(movement.elapsedTime / moveDuration);
                
                // Smooth interpolation (ease in-out)
                t = t * t * (3f - 2f * t);
                
                child.position = Vector3.Lerp(movement.startPosition, destination.position, t);
                
                // Check if reached destination
                if (t >= 1f)
                {
                    toRemove.Add(child);
                }
            }
            
            // Optional rotation
            // if (rotate && child.rotation.eulerAngles.z != 0)
            // {
            //     child.Rotate(0f, 0f, 0.35f, Space.World);
            // }
        }
        
        // Destroy children that reached destination
        foreach (Transform child in toRemove)
        {
            childMovements.Remove(child);
            Destroy(child.gameObject);
        }
    }
    
    public void StartMoving()
    {
        childMovements.Clear();
        foreach (Transform child in transform)
        {
            childMovements[child] = new ChildMovement(child.position);
        }
    }
    
    public void ResetChild(Transform child)
    {
        if (childMovements.ContainsKey(child))
        {
            childMovements[child] = new ChildMovement(child.position);
        }
    }
}