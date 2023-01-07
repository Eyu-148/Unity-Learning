using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    SurfaceEffector2D surfaceEffector2D;
    bool canMove = true; // flag to move, default to be true

    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float baseSpeed = 15f;
    [SerializeField] float boostSpeed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) { // all the operation should be called when the player can move
            RotatePlayer();
            RespondToBoost();
        }
    }

    // public method to be called in another class
    // set canMove value to false when a crash occurs
    public void DisableController() {
        canMove = false;
    }

    void RespondToBoost()
    {
        if (Input.GetKey(KeyCode.Space)) {
            surfaceEffector2D.speed = boostSpeed;
        } else {
            surfaceEffector2D.speed = baseSpeed;
        }
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.A)){
            rb2d.AddTorque(torqueAmount);
        }
        else if (Input.GetKey(KeyCode.D)){
            rb2d.AddTorque(-torqueAmount);
        }
    }
}
