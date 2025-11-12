using UnityEngine;

public class handMovement : MonoBehaviour
{
    /* The point that the minute hand should pivot around */
    [SerializeField] private GameObject pivotPoint;
    /* The pivot location of the minute hand */
    [SerializeField] private GameObject minuteHandPivot;
    /* The minute hand itself */
    [SerializeField] private GameObject minuteHand;
    /* The player */
    [SerializeField] private GameObject player;
    /* Keep public to figure out how much to rotate the object */
    public int addRotation;

    void Start()
    {
        /* Player should be positioned on the minute hand's center */
        //player.transform.position = new Vector3(minuteHand.transform.position.x, minuteHand.transform.position.y, player.transform.position.z);
        Debug.Log(player.transform.position.x + " " + player.transform.position.y);
        Debug.Log(minuteHand.transform.position.x + " " + minuteHand.transform.position.y);

            /* Minute hand should pivot around the pivot point */

            // Rotate object and then set pivot thing to be at the same position?
    }

    // Update is called once per frame
    void Update()
    {
    }
}
