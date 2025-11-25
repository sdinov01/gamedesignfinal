using UnityEngine;
using System.Collections.Generic;


public class SentChildren : MonoBehaviour
{
    public Transform destination;
    public float moveSpeed = 5f;
    public float smoothness = 0.1f;

    public int ccount;
    
    private List<Transform> children = new List<Transform>();
    private bool isMoving = false;
    
    void Update()
    {
        ccount = transform.childCount;
        if (ccount != 0 )
        {
            for (int i = 0; i < ccount; i++)
            {
                children[i].position = Vector3.Lerp(children[i].position, destination.position, smoothness * Time.deltaTime * moveSpeed);
            }
        }
        

        // if (isMoving)
        // {
        //     MoveChildren();
        // }
    }
    
    public void StartMoving()
    {
        children.Clear();
        foreach (Transform child in transform)
        {
            children.Add(child);
        }
        isMoving = true;
    }
    
    public void StopMoving()
    {
        isMoving = false;
    }
    
    private void MoveChildren()
    {
        if (children.Count == 0 || destination == null) return;
        
        for (int i = children.Count - 1; i >= 0; i--)
        {
            if (children[i] == null)
            {
                children.RemoveAt(i);
                continue;
            }
            
            children[i].position = Vector3.Lerp(children[i].position, destination.position, smoothness * Time.deltaTime * moveSpeed);
        }
    }
}