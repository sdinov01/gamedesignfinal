using UnityEngine;

public class restrictMovement : MonoBehaviour
{
    [SerializeField] private healthBar health;
    private GameObject[] area;
    private int currArea;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* Track which area the player is in */

        /* If the area the player is in's color is RED, take damage */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            health.takeDamage(10f);
        }
        
    }

    
}
