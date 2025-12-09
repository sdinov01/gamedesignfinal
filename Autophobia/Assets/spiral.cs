using UnityEngine;

public class spiral : MonoBehaviour
{
    public bool clockwise;
    public float rotoSpeed;
    private Transform RC;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clockwise   = false;
        RC          = transform.Find ("RotControl").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (clockwise)
        {
            RC.Rotate (0f, 0f, -rotoSpeed, Space.Self);
        }
        else
        {
            RC.Rotate (0f, 0f, rotoSpeed, Space.Self);
        }
    }

    public void ChangeDir()
    {
        clockwise = !clockwise;
    }
}
