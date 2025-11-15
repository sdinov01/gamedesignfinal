using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private bool canMove = false; 

    public void EnableMovement()
    {
        canMove = true;
    }

    void Update()
    {
        if (!canMove) return; 

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v) * speed * Time.deltaTime;
        transform.Translate(move, Space.World);
    }
}