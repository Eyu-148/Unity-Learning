using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float time_select = 30f;
    [SerializeField] float time_show = 5f;
    public bool if_load_next_question;
    public bool is_selceting = false;
    public float timer_image_fill_fraction;
    float time_val;

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer() {
        time_val = 0;
    }

    void UpdateTimer() {
        time_val -= Time.deltaTime;

        if (is_selceting) {
            if (time_val <= 0) {
                is_selceting = false;
                time_val = time_show;
            }
            else {
                timer_image_fill_fraction = time_val / time_select;
            }
        }
        else {
            if (time_val <= 0) {
                is_selceting = true;
                time_val = time_select;
                if_load_next_question = true; // only occurs when showing the correct answer
            }
            else {
                timer_image_fill_fraction = time_val / time_show;
            }
        }
    }
}
