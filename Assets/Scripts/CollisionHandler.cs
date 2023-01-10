using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float load_scene_delay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag) {
            default:
                Debug.Log("uh-oh");
                //ReloadLevel();
                GetComponent<Movement>().enabled = false;
                StartCoroutine(LoadingLevels(true)); // isCrashed = true
                break;
            case "Friendly-Object":
                Debug.Log("a friendly object!");
                break;
            case "Finish":
                Debug.Log("Finish point!");
                //LoadNextLevel();
                GetComponent<Movement>().enabled = false;
                StartCoroutine(LoadingLevels(false)); // isCrashed = false
                break;
            case "Fuel":
                Debug.Log("a fuel point!");
                break;
        }
    }

    void ReloadLevel() {
        // get the current build scene index
        int current_scene_index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current_scene_index);
    }

    void LoadNextLevel() {
        int current_scene_index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current_scene_index + 1);
    }

    IEnumerator LoadingLevels(bool isCrashed) {
        int current_scene_index = SceneManager.GetActiveScene().buildIndex;
        if (isCrashed) {
            yield return new WaitForSeconds(load_scene_delay); // wait for 1s
            SceneManager.LoadScene(current_scene_index); // reload the current scene
        }
        else {
            if (current_scene_index == SceneManager.sceneCountInBuildSettings - 1) {
                // this level is the last level
                yield return new WaitForSeconds(load_scene_delay);
                SceneManager.LoadScene(0); // reinitialize the scene
            } 
            else {
                yield return new WaitForSeconds(load_scene_delay);
                SceneManager.LoadScene(current_scene_index + 1);
            }
        }
    }
}
