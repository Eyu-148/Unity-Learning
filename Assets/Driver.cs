using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float steerSpeed = 0.30f;
    [SerializeField] float moveSpeed = 0.1f;
    [SerializeField] float slowSpeed = 0.05f;
    [SerializeField] float boostSpeed = 0.15f;
    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "SpeedUp") {
            Debug.Log("Speed upppp!");
            moveSpeed = boostSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("oooops, we crashed! ");
        moveSpeed = slowSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // press a/d to let object rotate by origin
        // lecture8: Time.deltaTime to let moving performance be the same on fast/slow computers
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed;// * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed;// * Time.deltaTime;
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, moveAmount, 0);
    }
}
