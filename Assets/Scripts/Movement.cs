using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb_player;
    [SerializeField] float defalut_thrust = 800f;
    [SerializeField] float defalut_rotate = 100f;

    AudioSource as_boost;

    // Start is called before the first frame update
    void Start()
    {
        rb_player = GetComponent<Rigidbody>();
        as_boost = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space)) { //bosting
            rb_player.AddRelativeForce(Vector3.up * defalut_thrust * Time.deltaTime); // vector3.up = (0,1,0)
            if (!as_boost.isPlaying) {
                as_boost.Play();
            }
        } else {
            if (as_boost.isPlaying) {
                as_boost.Pause();
            }
        }
    }

    void ProcessRotation() {
        rb_player.freezeRotation = true; // freezing rotation so we can manually rotate
        if (Input.GetKey(KeyCode.A)) { // rotate left
            transform.Rotate(Vector3.forward * defalut_rotate * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D)) { //rotate right
            transform.Rotate(-Vector3.forward * defalut_rotate * Time.deltaTime);
        }
        rb_player.freezeRotation = false; // unfreezing rotation so physics system can take over
    }
}
