using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Quiz quiz_screen;
    Result result_screen;

    void Awake() {
        quiz_screen = FindObjectOfType<Quiz>();
        result_screen = FindObjectOfType<Result>();
    }

    // Start is called before the first frame update
    void Start()
    {
        quiz_screen.gameObject.SetActive(true);
        result_screen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (quiz_screen.is_complete) {
            quiz_screen.gameObject.SetActive(false);
            result_screen.gameObject.SetActive(true);
            result_screen.ShowFinalResult();
        }
    }

    /* Same method as OnSelectAnswer() */
    public void OnSelectReplay() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
