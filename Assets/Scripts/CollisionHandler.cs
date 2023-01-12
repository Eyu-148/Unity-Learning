using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Header("Load Scene")]
    [SerializeField] float delay_load_scene = 1f;

    [Header("Sound Effects")]
    [SerializeField] AudioClip sound_crash;
    [SerializeField] AudioClip sound_finish;

    [Header("Particle Effects")]
    [SerializeField] ParticleSystem particle_finish;
    [SerializeField] ParticleSystem particle_crash;

    AudioSource audio_collision;

    bool isTransitioning = false;
    bool collisionDisabled;

    // Start is called before the first frame update
    void Start()
    {
        audio_collision = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // respond to debug keys
        RespondsToDebugKeys();
    }

    void RespondsToDebugKeys() {
        if (Input.GetKey(KeyCode.L)) {
            FinishSequence();
        }
        else if (Input.GetKey(KeyCode.C)) {
            Debug.Log("Collision Disabled: " + collisionDisabled);
            collisionDisabled = !collisionDisabled; // toggle collision
        }
    }

    void OnCollisionEnter(Collision other) {
        if (isTransitioning || collisionDisabled) {return;}

        switch (other.gameObject.tag) {
            default:
                CrashSequence();
                break;
            case "Friendly-Object":
                Debug.Log("a friendly object!");
                break;
            case "Finish":
                FinishSequence();
                break;
            case "Fuel":
                Debug.Log("a fuel point!");
                break;
        }
    }

    void CrashSequence() {
        isTransitioning = true;
        audio_collision.Stop();
        particle_crash.Play();
        audio_collision.PlayOneShot(sound_crash);
        GetComponent<Movement>().enabled = false;
        StartCoroutine(LoadingLevels(true)); // isCrashed = true
    }

    void FinishSequence() {
        isTransitioning = true;
        audio_collision.Stop();
        particle_finish.Play();
        audio_collision.PlayOneShot(sound_finish);                
        GetComponent<Movement>().enabled = false;
        StartCoroutine(LoadingLevels(false)); // isCrashed = false
    }

    IEnumerator LoadingLevels(bool isCrashed) {
        int current_scene_index = SceneManager.GetActiveScene().buildIndex;
        if (isCrashed) {
            yield return new WaitForSeconds(delay_load_scene); // wait for 1s
            SceneManager.LoadScene(current_scene_index); // reload the current scene
        }
        else {
            if (current_scene_index == SceneManager.sceneCountInBuildSettings - 1) {
                // this level is the last level
                yield return new WaitForSeconds(delay_load_scene);
                SceneManager.LoadScene(0); // reinitialize the scene
            } 
            else {
                yield return new WaitForSeconds(delay_load_scene);
                SceneManager.LoadScene(current_scene_index + 1);
            }
        }
    }
}
