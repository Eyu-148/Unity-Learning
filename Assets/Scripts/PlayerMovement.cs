using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float turnSpeed = 20f;

    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    Animator m_Animator;
    Rigidbody rb_Player;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        rb_Player = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        /* Movement Vector */
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");
        
        m_Movement.Set(horizontalMove, 0f, verticalMove); // set the vector
        m_Movement.Normalize(); // normalize the vector


        /* Animator */
        bool hasHorizontalInput = !Mathf.Approximately(horizontalMove, 0f); // check if horizontal input exists
        bool hasVerticalInput = !Mathf.Approximately(verticalMove, 0f);
        
        bool isWalking = hasHorizontalInput || hasVerticalInput; // whether the player is moving
        m_Animator.SetBool("IsMoving", isWalking); // set a parameter (IsWalking) to the animator, & give it a value (isWalking)


        /* Character Facing Direction */
        Vector3 desiredFacing = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed*Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredFacing); // create a rotation looking in the given direction
    }


    /* Method to apply movement to the rigidbody of the character */
    void OnAnimatorMove() {
        rb_Player.MovePosition(rb_Player.position + m_Movement * m_Animator.deltaPosition.magnitude); // apply the position
        rb_Player.MoveRotation(m_Rotation); // apply the rotation
    }
}
