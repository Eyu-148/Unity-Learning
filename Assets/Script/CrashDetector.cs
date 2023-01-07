using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float reloadDelay = 1f;
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] AudioClip crashSFX;
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Ground") {
            // disable any operations after one crash by calling public method
            // from PlayerController class
            FindObjectOfType<PlayerController>().DisableController();
            crashEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(crashSFX);
            Invoke("ReloadScene", reloadDelay);
        }
    }

    void ReloadScene() {
        SceneManager.LoadScene(0);
    }
}
