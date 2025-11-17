using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerPlatMoveWD : MonoBehaviour
{
    [SerializeField]    GameObject[] platformObjects;
    private             Transform[] platforms;
    public int          platArrayLength;
    private             GameObject player;
    public int          platIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        platArrayLength     =   platformObjects.Length;
        player              =   GameObject.FindWithTag("Player");
        platforms           =   new Transform[platArrayLength];
        for (int i = 0; i < platArrayLength; i++) 
        {
            platforms[i]    =   platformObjects[i].transform;
        }
        platIndex = 0;
        // player.transform.SetParent(platforms[0]);  
    }

    void SetPlatParent (Transform A, int index)
    {
        A.SetParent (platforms[index]);
        A.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            platIndex = (platIndex + 1) % platArrayLength;
            SetPlatParent (player.transform, platIndex);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            platIndex = (platIndex + (platArrayLength - 1)) % platArrayLength;
            SetPlatParent (player.transform, platIndex);
        }
    }
}
