using UnityEngine;
using System.Collections;

public class LaneRoute : MonoBehaviour
{
    public Transform        destination;
    public float            speed = 4.28f;
    public GameObject       ballchild;
    public int              childcount;

    void Update()
    {
        childcount = transform.childCount;
        if (childcount == 0) return;

        ballchild = transform.GetChild (0).gameObject;
        ballchild.transform.position = Vector3.MoveTowards(
            ballchild.transform.position,
            destination.position,
            speed * Time.deltaTime
        );
    }
}
