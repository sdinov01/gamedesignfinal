using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

      //public Animator animator;
      public Rigidbody2D rb2D;
      private bool FaceRight = true; // determine which way player is facing.
      public static float runSpeed = 10f;
      public float startSpeed = 10f;
      public bool isAlive = true;
      //public AudioSource WalkSFX;
      private Vector3 hMove;
      private Vector3 vMove;

      void Start(){
           //animator = gameObject.GetComponentInChildren<Animator>();
           rb2D = transform.GetComponent<Rigidbody2D>();
      }

      void Update(){
        //NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
        hMove = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        vMove = new Vector3(0f, Input.GetAxis("Vertical"), 0f);

        if (isAlive == true){
            Vector3 moveDir = hMove + vMove;
            transform.position+= moveDir * runSpeed * Time.deltaTime;

            if (Input.GetAxis("Horizontal") != 0){
            //       animator.SetBool ("Walk", true);
            //       if (!WalkSFX.isPlaying){
            //             WalkSFX.Play();
            //      }
            } else {
            //      animator.SetBool ("Walk", false);
            //      WalkSFX.Stop();
            }

            // Turning: Reverse if input is moving the Player right and Player faces left
            if ((hMove.x <0 && !FaceRight) || (hMove.x >0 && FaceRight)){
                playerTurn();
            }
        }
      }

    void FixedUpdate(){
        //slow down on hills / stops sliding from velocity
        if (hMove.x == 0){
                rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x / 1.1f, rb2D.linearVelocity.y) ;
        }
    }

    private void playerTurn(){
            // NOTE: Switch player facing label
            FaceRight = !FaceRight;

            // NOTE: Multiply player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
    }
}
