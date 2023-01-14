using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 start_pos;
    [SerializeField] Vector3 movement_vec;
    [SerializeField] [Range(0,1)] float movement_factor;
    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        start_pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) {return;} // mathf.epsilon = a extreme small number
        float cycles = Time.time / period; // continually growing over time

        const float TAU = Mathf.PI * 2;
        float sin_wave = Mathf.Sin(TAU * cycles); // [-1,1]

        movement_factor = (sin_wave + 1f) / 2f; // [0,1]
        Vector3 offset = movement_vec * movement_factor;
        transform.position = start_pos + offset;
    }
}
